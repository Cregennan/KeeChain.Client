namespace KeeChain.Warlin.Interfaces;

public interface IWarlinResponse<T> : IWarlinPacket
    where T: new()
{
    int RequiredPartsCount { get; }

    internal void FromTokens(IEnumerable<string> tokens);

}