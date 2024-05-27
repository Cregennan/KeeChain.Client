namespace KeeChain.Warlin.Exceptions
{
    public class WarlinValidationException : WarlinException
    {
        public override string Message => ConvertErrorCode(_message);

        private readonly string _message;

        public WarlinValidationException(string message) : base(message)
        {
            _message = message;
        }
        
        private static string ConvertErrorCode(string errorCode)
        {
            return errorCode switch
            {
                _ => errorCode
            };
        }
    }
}