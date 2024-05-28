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
            if (syncr.IsError)
            {
                throw new SalavatException(syncr.Error ?? "Неизвестная ошибка");
            }

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
            if (result.IsError)
            {
                throw new SalavatException(result.Error);
            }

            _unlocked = true;
        }
        
        
    }
}