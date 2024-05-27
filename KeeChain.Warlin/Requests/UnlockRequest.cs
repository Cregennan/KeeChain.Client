namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class UnlockRequest : IWarlinRequestable<UnlockRequest, AckResponse>
    {
        public string IdentifyingToken => "UNLOCK";

        public readonly string MasterPassword;

        public UnlockRequest(string masterPassword) => MasterPassword = masterPassword;

        public IEnumerable<string>? ToTokens() => [MasterPassword];
    }
}