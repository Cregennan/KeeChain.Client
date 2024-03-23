namespace KeeChain.Warlin.Utility
{
    using Exceptions;
    using Interfaces;

    public static class SerialPortUtility
    {
        public static string? TryFindBoard(this ISerialPort _connection, int baudRate)
        {
            var portNames = _connection.PortNames;
            if (portNames.Length == 0)
            {
                return null;
            }

            var portFound = false;
            
            foreach (var portName in portNames)
            {
                try
                {
                    _connection.Begin(portName, baudRate);
                    _connection.WriteLine("WARLIN<PART>DISCOVER");
                    try
                    {
                        var line = _connection.ReadLine();
                        if (line == "WARLIN<PART>HELLO")
                        {
                            _connection.Stop();
                            return portName;
                        }
                    }
                    catch (TimeoutException e)
                    {
                        _connection.Stop();
                    }
                }
                catch (PortAccessDeniedException)
                {
                    
                }
            }
            
            _connection.Stop();
            return null;
        }
    }
}