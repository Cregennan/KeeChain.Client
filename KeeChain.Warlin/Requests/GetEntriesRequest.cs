namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class GetEntriesRequest : IWarlinRequestable<GetEntriesRequest, EntriesResponse>
    {
        public IEnumerable<string>? ToTokens() => [];

        public string IdentifyingToken => "GET_ENTRIES";
    }
}