namespace KeeChain.Warlin.Requests
{
    using FluentValidation;
    using Interfaces;
    using Responses;
    using Utility;

    public class TestExplicitCodeRequest : IWarlinRequestable<TestExplicitCodeRequest, OtpResponse>
    {
        public readonly string Secret;

        public string IdentifyingToken => "TEST_EXPLICIT_CODE";
        
        public IEnumerable<string>? ToTokens() => [Secret, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()];

        public TestExplicitCodeRequest(string secret) => Secret = secret;
    }

    internal class TestExplicitCodeRequestValidator : AbstractValidator<TestExplicitCodeRequest>
    {
        public TestExplicitCodeRequestValidator()
        {
            RuleFor(x => x.Secret).NotEmpty().Must(x => x.IsValidBase32String()).WithMessage("Переданный секрет не является Base32 строкой");
        }
    }
}