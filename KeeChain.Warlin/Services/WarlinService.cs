namespace KeeChain.Warlin.Services
{
    using System.Text;
    using Exceptions;
    using KeeChain.Warlin.Interfaces;
    using KeeChain.Warlin.Utility;

    public class WarlinService
    {
        private const int DefaultBaudRate = 115600;

        private const int DefaultTimeout = 10 * 60;
        
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

        /// <summary>
        /// Отправка запроса на устройство
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <typeparam name="TRequest">Тип запроса, реализующий <see cref="IWarlinRequest{TRequest}"/></typeparam>
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
            var writeTask = _connection.WriteLineAsync(sb);
            var timeoutTask = Task.Delay(DefaultTimeout);
            if (timeoutTask == await Task.WhenAny(writeTask, timeoutTask))
            {
                throw new TimeoutException("Устройство не отвечает");
            }
            
            sb.Clear();
        }

        /// <summary>
        /// Получение ответа с устройства
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа</typeparam>
        /// <returns>Ответ с устройства, если его удалось десериализовать</returns>
        /// <exception cref="InvalidRequestResponseTypeReturnedException">Выбрасывается в случае, если с устройства пришел ответ не того типа, который ожидается</exception>
        /// <exception cref="NotEnoughTokensInResponseException">Выбрасывается в случае, если количество элементов в ответе не соответствует ожидаемому для данного типа ответа</exception>
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