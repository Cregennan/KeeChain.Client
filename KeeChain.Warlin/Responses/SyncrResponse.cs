namespace KeeChain.Warlin.Responses
{
    using Exceptions;
    using Interfaces;

    public class SyncrResponse : IWarlinResponse<SyncrResponse>
    {
        public string IdentifyingToken => "SYNCR";

        public int RequiredPartsCount => 1;
        
        public int VaultEntriesCount { get; private set; }
        
        public void FromTokens(IEnumerable<string> tokens)
        {
            if (!int.TryParse(tokens.First(), out var number) || number < 0)
            {
                throw new WarlinException("Не удалось распознать SYNCR ответ");
            }
            
            VaultEntriesCount = number;
        }
    }
}