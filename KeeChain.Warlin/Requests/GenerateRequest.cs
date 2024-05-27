namespace KeeChain.Warlin.Requests
{
    using FluentValidation;
    using Interfaces;
    using Responses;

    public class GenerateRequest : IWarlinRequestable<GenerateRequest, OtpResponse>
    {
        public readonly int Index;

        public string IdentifyingToken => "GENERATE";

        public GenerateRequest(int index) => Index = index;
        
        public IEnumerable<string>? ToTokens() => [Index.ToString(), DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()];
    }

    internal class GenerateRequestValidator : AbstractValidator<GenerateRequest>
    {
        public GenerateRequestValidator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0).WithMessage("Индекс секрета не должен быть меньше 0");
        }
    }
}