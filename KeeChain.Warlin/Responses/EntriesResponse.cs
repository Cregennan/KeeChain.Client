namespace KeeChain.Warlin.Responses
{
    using Interfaces;

    public class EntriesResponse : IWarlinResponse<EntriesResponse>
    {
        public string IdentifyingToken => "ENTRIES";

        public int RequiredPartsCount => 0;

        public IReadOnlyCollection<string> Entries { get; internal set; } = [];

        public void FromTokens(IEnumerable<string>? tokens) => Entries = tokens?.ToArray() ?? [];
    }
}