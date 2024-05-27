namespace KeeChain.Warlin.Requests
{
    using Interfaces;
    using Responses;

    public class ServiceTryReadEEPROMRequest : IWarlinRequestable<ServiceTryReadEEPROMRequest, ServiceResponse>
    {
        public IEnumerable<string>? ToTokens() => [];

        public string IdentifyingToken => "SERVICE_TRY_READ_EEPROM";
    }
}