namespace KeeChain.Warlin.Interfaces
{
    public interface ISerialPort : IDisposable
    {
        public string[] PortNames { get; }
        
        public void Begin(string name, int baudRate);

        public string ReadLine();

        public void Write(string text);

        public void WriteLine(string text);

        public void Stop();
    }
}