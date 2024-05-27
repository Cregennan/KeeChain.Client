namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class SyncRequest : IWarlinRequestable<SyncRequest, SyncrResponse>
    {
        public IEnumerable<string>? ToTokens()
        {
            yield break;
        }

        public string IdentifyingToken => "SYNC";
    }
}