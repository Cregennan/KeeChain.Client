namespace KeeChain.Warlin.Requests
{
    using FluentValidation;
    using Interfaces;
    using Responses;
    using Utility;

    public class StoreEntryRequest : IWarlinRequestable<StoreEntryRequest, AckResponse>
    {
        public readonly string Name;

        public readonly string Secret;

        public readonly int Digits;
        
        public StoreEntryRequest(string name, string secret, int digits = 6)
        {
            Name = name;
            Secret = secret;
            Digits = digits;
        }

        public IEnumerable<string>? ToTokens() => [Name, Secret, Digits.ToString()];

        public string IdentifyingToken => "STORE_ENTRY";
    }

    internal class StoreEntryRequestValidator : AbstractValidator<StoreEntryRequest>
    {
        public StoreEntryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Название не должно быть пустым");

            RuleFor(x => x.Secret)
                .NotEmpty()
                .WithMessage("Не забудьте произнести секрет");

            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(20)
                .WithMessage("Длина названия должна быть от 2 до 20 символов");

            RuleFor(x => x.Secret)
                .MinimumLength(2)
                .MaximumLength(200)
                .WithMessage("Длина секрета должна быть от 2 до 50 символов");

            RuleFor(x => x.Secret)
                .Must(x => x.IsValidBase32String())
                .WithMessage("Секрет должен быть валидной строкой Base32");
        }
    }
}