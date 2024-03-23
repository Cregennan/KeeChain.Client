namespace KeeChain.Warlin.Interfaces;

public interface IWarlinRequest<TRequest> : IWarlinPacket
    where TRequest: IWarlinRequest<TRequest>
{
    internal IEnumerable<string>? ToTokens();
}