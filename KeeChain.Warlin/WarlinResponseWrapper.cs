namespace KeeChain.Warlin;

using Exceptions;
using Interfaces;

/// <summary>
/// Обертка над WarlinResponse для предоставления информации об ошибке
/// </summary>
/// <typeparam name="TResponse">Тип результата запроса Warlin</typeparam>
public sealed class WarlinResponseWrapper<TResponse> where TResponse: IWarlinResponse<TResponse>, new()
{
    private string? _error;
    
    
    /// <summary>
    /// Результат запроса
    /// </summary>
    public TResponse? Response { get; private set; }
    
    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Error
    {
        get => _error ?? "Неизветная оишбка";
        private init => _error = value;
    }

    /// <summary>
    /// Наличие ошибки в результате выполнения запроса
    /// </summary>
    public bool IsError { get; private set; }

    internal WarlinResponseWrapper(TResponse response)
    {
        Response = response;
    }

    internal WarlinResponseWrapper(string errorMessage)
    {
        IsError = true;
        Error = errorMessage;
    }

    public void ThrowIfError()
    {
        if (this.IsError)
        {
            throw new WarlinException(this.Error);
        }
    }
}