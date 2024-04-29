namespace KeeChain.Warlin.Requests;

using Interfaces;
using Responses;

public class EEPROMHeaderRequest : IWarlinRequestable<EEPROMHeaderRequest, ServiceResponse>
{
    public IEnumerable<string> ToTokens() => Enumerable.Empty<string>();

    public string IdentifyingToken => "SERVICE_TRY_READ_EEPROM";
}