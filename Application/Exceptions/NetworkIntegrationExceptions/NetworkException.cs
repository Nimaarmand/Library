

namespace Application.Exceptions.NetworkIntegrationExceptions
{
    public class NetworkException : Exception
    {
        public ErrorType RuleId { get; set; }
        public NetworkException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }
}