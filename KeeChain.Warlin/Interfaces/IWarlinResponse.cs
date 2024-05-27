namespace KeeChain.Warlin.Interfaces;

public interface IWarlinResponse<T> : IWarlinPacket
    where T: new()
{
    /// <summary>
    /// Количество частей сообщения, минимально необходимых для распознавания пакета
    /// </summary>
    int RequiredPartsCount { get; }

    /// <summary>
    /// Десериализует ответ из частей сообщения
    /// </summary>
    internal void FromTokens(IEnumerable<string> tokens);

}