namespace KeeChain.Warlin.Responses;

using Interfaces;

public class DiscoverResponse : IWarlinResponse<DiscoverResponse>
{
    public string IdentifyingToken => "HELLO";

    public int RequiredPartsCount => 0;

    void IWarlinResponse<DiscoverResponse>.FromTokens(IEnumerable<string> tokens){}
    
}