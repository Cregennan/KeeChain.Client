namespace KeeChain.Warlin.Responses
{
    using Interfaces;

    public class ServiceResponse : IWarlinResponse<ServiceResponse>
    {
        public string IdentifyingToken => "SERVICE";
        
        public int RequiredPartsCount => 0;

        public string[] Messages { get; internal set; } = [];
        
        public void FromTokens(IEnumerable<string>? tokens) => Messages = tokens?.ToArray() ?? [];
    }
}