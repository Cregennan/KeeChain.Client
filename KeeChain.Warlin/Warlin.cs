namespace KeeChain.Warlin
{
    using System.IO.Ports;
    using Exceptions;
    using Interfaces;
    using Utility;

    public class Warlin
    {
        private const int DefaultBaudRate = 115600;
        
        private readonly ISerialPort _connection;

        public Warlin(ISerialPort connection)
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
    }
}