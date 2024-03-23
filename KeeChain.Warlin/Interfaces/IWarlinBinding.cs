namespace KeeChain.Warlin.Interfaces;

public interface IWarlinBinding<TRequest, TResponse> : IWarlinRequest<TRequest>
    where TRequest: IWarlinRequest<TRequest>
    where TResponse: IWarlinResponse<TResponse>, new()
{
    
}