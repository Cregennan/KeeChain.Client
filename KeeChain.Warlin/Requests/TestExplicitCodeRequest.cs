namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class TestExplicitCodeRequest : IWarlinRequestable<TestExplicitCodeRequest, OtpResponse>
    {
        public readonly string Secret;

        public string IdentifyingToken => "TEST_EXPLICIT_CODE";
        
        public IEnumerable<string>? ToTokens() => [Secret, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()];

        public TestExplicitCodeRequest(string secret) => Secret = secret;
    }
}