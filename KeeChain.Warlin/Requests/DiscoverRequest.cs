namespace KeeChain.Warlin.Requests;

using Interfaces;
using Responses;

public class DiscoverRequest : IWarlinRequestable<DiscoverRequest, ACKResponse>
{
    IEnumerable<string> IWarlinRequest<DiscoverRequest>.ToTokens() => Enumerable.Empty<string>();

    public string IdentifyingToken => "DISCOVER";
}