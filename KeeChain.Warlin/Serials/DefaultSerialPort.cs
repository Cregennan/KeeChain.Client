namespace KeeChain.Warlin.Serials
{
    using System.IO.Ports;
    using System.Text;
    using Exceptions;
    using Interfaces;

    public class DefaultSerialPort : ISerialPort
    {
        private readonly SerialPort _port = new();
        public void Begin(string name, int baudRate)
        {
            _port.BaudRate = baudRate;
            _port.PortName = name;
            try
            {
                _port.Open();
            }
            catch (UnauthorizedAccessException)
            {
                throw new PortAccessDeniedException();
            }
            catch (IOException)
            {
                throw new PortAccessDeniedException();
            }
            
            _port.Parity = Parity.None;
            _port.StopBits = StopBits.One;
            _port.DataBits = 8;
            _port.Encoding = Encoding.UTF8;
            _port.RtsEnable = true;
            _port.DtrEnable = true;
            _port.BreakState = false;
        }

        public string ReadLine() => _port.ReadLine();

        public void Write(string text) => _port.Write(text);

        public void WriteLine(string text) => _port.WriteLine(text);
        public void Stop()
        {
            _port.Close();
        }

        public void Dispose()
        {
            _port.Dispose();
        }

        public string[] PortNames => SerialPort.GetPortNames();
    }
}