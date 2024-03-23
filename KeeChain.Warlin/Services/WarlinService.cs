namespace KeeChain.Warlin.Services
{
    using System.Text;
    using Exceptions;
    using KeeChain.Warlin.Interfaces;
    using KeeChain.Warlin.Utility;

    public class WarlinService
    {
        private const int DefaultBaudRate = 115600;
        
        private readonly ISerialPort _connection;

        public WarlinService(ISerialPort connection)
        {
            _connection = connection;
        }

        public bool TryConnectToBoard()
        {
            var portName = _connection.TryFindBoard(DefaultBaudRate);
            if (portName is null)
            {
                return false;
            }
            _connection.Begin(portName, DefaultBaudRate);
            return true;
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IWarlinBinding<TRequest, TResponse>
            where TResponse : IWarlinResponse<TResponse>, new()
        {
            await SendToDeviceAsync(request).ConfigureAwait(false);

            return await GetFromDeviceAsync<TResponse>().ConfigureAwait(false);
        }

        private async Task SendToDeviceAsync<TRequest>(TRequest request)
            where TRequest : IWarlinRequest<TRequest>
        {
            var sb = new StringBuilder();

            sb.Append(Definitions.WarlinMagicHeader);
            sb.Append(Definitions.WarlinDefaultDivider);
            sb.Append(request.IdentifyingToken);
            
            var tokens = request.ToTokens();

            if (tokens != null)
            {
                foreach (var token in tokens)
                {
                    sb.Append(Definitions.WarlinDefaultDivider);
                    sb.Append(token);
                }
            }
            
            //Отправляем на устройство
            await _connection.WriteLineAsync(sb).ConfigureAwait(false);
            sb.Clear();
        }

        private async Task<TResponse> GetFromDeviceAsync<TResponse>()
            where TResponse : IWarlinResponse<TResponse>, new()
        {
            while (true)
            {
                // Получаем ответ с устройства
                var deviceResponse = await _connection.ReadLineAsync().ConfigureAwait(false);
                
                var parts = deviceResponse.Split(Definitions.WarlinDefaultDivider);
                if (parts[0] != Definitions.WarlinMagicHeader)
                {
                    continue;
                }
                
                var response = new TResponse();
                
                // Идентифицируем тип ответа
                if (parts[1] != response.IdentifyingToken)
                {
                    throw new InvalidRequestResponseTypeReturnedException();
                }
            
                if (parts.Length != response.RequiredPartsCount + 2)
                {
                    throw new NotEnoughTokensInResponseException();
                }

                // Заполняем ответ
                response.FromTokens(parts.Skip(2));

                return response;
            }
        }
    }
}