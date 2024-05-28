namespace KeeChain.Warlin.Requests
{
    using FluentValidation;
    using Interfaces;
    using Responses;

    public class UnlockRequest : IWarlinRequestable<UnlockRequest, AckResponse>
    {
        public string IdentifyingToken => "UNLOCK";

        public readonly string MasterPassword;

        public UnlockRequest(string masterPassword) => MasterPassword = masterPassword;

        public IEnumerable<string>? ToTokens() => [MasterPassword];
    }

    internal class UnlockRequestValidator : AbstractValidator<UnlockRequest>
    {
        public UnlockRequestValidator()
        {
            RuleFor(x => x.MasterPassword)
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым");
        }
    }
}