namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

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
}