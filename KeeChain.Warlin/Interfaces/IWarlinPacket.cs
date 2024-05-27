namespace KeeChain.Warlin.Interfaces;

public interface IWarlinPacket
{
    /// <summary>
    /// Строка-идентификатор сообщения
    /// </summary>
    public string IdentifyingToken { get; }
}