namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class DiscoverRequest : IWarlinRequestable<DiscoverRequest, AckResponse>
    {
        public IEnumerable<string>? ToTokens()
        {
            yield break;
        }

        public string IdentifyingToken => "DISCOVER";
    }
}