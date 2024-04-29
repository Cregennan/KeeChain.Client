namespace KeeChain.Warlin.Requests;

using Interfaces;
using Responses;

public class SyncRequest : IWarlinRequestable<SyncRequest, SyncResponse>
{
    public IEnumerable<string>? ToTokens()
    {
        yield return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    }

    public string IdentifyingToken => "SYNC";
}