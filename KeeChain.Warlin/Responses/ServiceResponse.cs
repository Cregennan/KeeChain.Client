namespace KeeChain.Warlin.Responses;

using System.Collections.ObjectModel;
using Interfaces;

public class ServiceResponse : IWarlinResponse<ServiceResponse>
{
    public string IdentifyingToken => "SERVICE";
    public int RequiredPartsCount => 0;
    public void FromTokens(IEnumerable<string> tokens)
    {
        Tokens = new ReadOnlyCollection<string>(tokens.ToList());
    }
    
    public IReadOnlyCollection<string> Tokens { get; private set; }
}