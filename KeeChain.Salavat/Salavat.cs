namespace KeeChain.Salavat
{
    using Exceptions;
    using Warlin.Requests;
    using Warlin.Responses;
    using Warlin.Serials;
    using Warlin.Services;

    public class Salavat
    {
        private readonly WarlinService _warlin;
        
        private bool _initialized;
        
        private bool _unlocked;

        private bool _connectionEstablished;

        private List<string> _names = [];
        
        public int EntriesCount { get; private set; }
        
        public Salavat()
        {
            _warlin = new WarlinService(new DefaultSerialPort(), Console.WriteLine);
        }

        public async Task Initialize()
        {
            if (!_connectionEstablished)
            {
                _connectionEstablished = _warlin.TryConnectToBoard();
            }

            if (!_connectionEstablished)
            {
                throw new SalavatException("Не удалось подключиться к плате");
            }

            var syncr = await _warlin.SendRequestAsync<SyncRequest, SyncrResponse>(new());
            syncr.ThrowIfError();

            EntriesCount = syncr.Response?.VaultEntriesCount ?? 0;
            _initialized = true;
        }

        public async Task Unlock(string masterPassword)
        {
            if (!_initialized)
            {
                throw new SalavatException("Устройство не инициализировано");
            }
            
            var result = await _warlin.SendRequestAsync<UnlockRequest, AckResponse>(new(masterPassword));
            result.ThrowIfError();

            _unlocked = true;
        }

        public async Task<OTPDescription[]> GetCodes()
        {
            var entriesResponse = await _warlin.SendRequestAsync<GetEntriesRequest, EntriesResponse>(new());
            entriesResponse.ThrowIfError();
            var entries = entriesResponse.Response?.Entries.ToArray() ?? [];
            var descriptions = new List<OTPDescription>();
            
            for (var index = 0; index < entries.Length; index++)
            {
                var code = await _warlin.SendRequestAsync<GenerateRequest, OtpResponse>(new GenerateRequest(index));
                code.ThrowIfError();
                
                descriptions.Add(new OTPDescription(entries[index], code.Response?.Code ?? "Ошибка"));
            }

            return descriptions.ToArray();
        }

        public async Task AddEntry(string name, string secret)
        {
            var ackResponse = await _warlin.SendRequestAsync<StoreEntryRequest, AckResponse>(new(name, secret));
            ackResponse.ThrowIfError();
        }

        public async Task RemoveEntry(int index)
        {
            var removeResponse = await _warlin.SendRequestAsync<RemoveEntryRequest, AckResponse>(new(index));
            removeResponse.ThrowIfError();
        }
        
    }
}