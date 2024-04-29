namespace KeeChain.Warlin.Responses;

using Interfaces;

public class ACKResponse : IWarlinResponse<ACKResponse>
{
    public string IdentifyingToken => "ACK";

    public int RequiredPartsCount => 0;

    void IWarlinResponse<ACKResponse>.FromTokens(IEnumerable<string> tokens){}
    
}