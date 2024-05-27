namespace KeeChain.Warlin.Responses
{
    using Exceptions;
    using Interfaces;

    public class OtpResponse : IWarlinResponse<OtpResponse>
    {
        public string IdentifyingToken => "OTP";

        public int RequiredPartsCount => 1;

        public string Code { get; internal set; }

        public void FromTokens(IEnumerable<string> tokens) =>
            Code = tokens.FirstOrDefault() ?? throw new WarlinException("Не удалось получить код");
    }
}