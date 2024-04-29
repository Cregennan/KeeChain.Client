namespace KeeChain.Warlin.Responses;

using Interfaces;

public class SyncResponse : IWarlinResponse<SyncResponse>
{
    public string IdentifyingToken => "SYNCR";
    public int RequiredPartsCount => 1;
    
    /// <summary>
    /// Количество паролей, хранящихся на токене
    /// </summary>
    public int StoredSecretsCount { get; private set; }
    
    public void FromTokens(IEnumerable<string> tokens)
    {
        StoredSecretsCount = int.Parse(tokens.First());
    }
}