namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class RemoveEntryRequest : IWarlinRequestable<RemoveEntryRequest, AckResponse>
    {
        public readonly int Index;
        public RemoveEntryRequest(int index)
        {
            Index = index;
        }

        public IEnumerable<string>? ToTokens() => [Index.ToString()];

        public string IdentifyingToken => "REMOVE_ENTRY";
    }
}