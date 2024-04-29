namespace KeeChain.Warlin.Interfaces;

public interface IWarlinRequestable<TRequest, TResponse> : IWarlinRequest<TRequest>
    where TRequest: IWarlinRequest<TRequest>
    where TResponse: IWarlinResponse<TResponse>, new()
{
    
}