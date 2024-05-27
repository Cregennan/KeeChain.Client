namespace KeeChain.Warlin.Requests
{
    using FluentValidation;
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

    internal class RemoveEntryRequestValidator : AbstractValidator<RemoveEntryRequest>
    {
        public RemoveEntryRequestValidator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0).WithMessage("Номер секрета не должен быть меньше нуля");
        }
    }
}