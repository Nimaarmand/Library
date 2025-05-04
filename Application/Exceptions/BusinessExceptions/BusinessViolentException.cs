namespace Application.Exceptions.BusinessExceptions
{
    public class BusinessManualyMessageException : Exception
    {
        public BusinessManualyMessageException()
        {
        }

        public BusinessManualyMessageException(string message)
            : base(message)
        {
        }

        public BusinessManualyMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}