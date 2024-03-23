namespace KeeChain.Warlin.Utility
{
    using System.Text;
    using Exceptions;
    using Interfaces;

    public static class SerialPortUtility
    {
        internal static string? TryFindBoard(this ISerialPort connection, int baudRate)
        {
            var portNames = connection.PortNames;
            if (portNames.Length == 0)
            {
                return null;
            }

            var portFound = false;
            
            foreach (var portName in portNames)
            {
                try
                {
                    connection.Begin(portName, baudRate);
                    connection.WriteLine("WARLIN<PART>DISCOVER");
                    try
                    {
                        var line = connection.ReadLine();
                        if (line == "WARLIN<PART>HELLO")
                        {
                            connection.Stop();
                            return portName;
                        }
                    }
                    catch (TimeoutException e)
                    {
                        connection.Stop();
                    }
                }
                catch (PortAccessDeniedException)
                {
                    
                }

                connection.Stop();
            }
            
            connection.Stop();
            return null;
        }

        public static Task<string> ReadLineAsync(this ISerialPort serialPort)
        {
            return Task.FromResult(serialPort.ReadLine());
        }

        internal static Task WriteAsync(this ISerialPort serialPort, string text)
        {
            serialPort.Write(text);
            return Task.CompletedTask;
        }

        internal static Task WriteLineAsync(this ISerialPort serialPort, string text)
        {
            serialPort.WriteLine(text);
            return Task.CompletedTask;
        }

        internal static void Write(this ISerialPort port, StringBuilder builder) => port.Write(builder.ToString());
        internal static void WriteLine(this ISerialPort port, StringBuilder builder) => port.WriteLine(builder.ToString());
        internal static Task WriteLineAsync(this ISerialPort port, StringBuilder builder) => port.WriteLineAsync(builder.ToString());
        internal static Task WriteAsync(this ISerialPort port, StringBuilder builder) => port.WriteAsync(builder.ToString());
    }
}