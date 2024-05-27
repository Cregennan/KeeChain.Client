namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class GenerateRequest : IWarlinRequestable<GenerateRequest, OtpResponse>
    {
        public readonly int Index;

        public string IdentifyingToken => "GENERATE";

        public GenerateRequest(int index) => Index = index;
        
        public IEnumerable<string>? ToTokens() => [Index.ToString(), DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()];
    }
}