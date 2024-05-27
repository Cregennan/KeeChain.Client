namespace KeeChain.Warlin.Responses
{
    using Interfaces;

    public class AckResponse : IWarlinResponse<AckResponse>
    {
        public string IdentifyingToken => "ACK";
            
        public int RequiredPartsCount => 0;
        
        public void FromTokens(IEnumerable<string> tokens)
        {
        }
    }
}