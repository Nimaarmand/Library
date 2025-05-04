
namespace Application.Exceptions.BusinessExceptions
{
    public class BusinessException : Exception
    {
        public ErrorType RuleId { get; set; }
        public BusinessException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }

    }
}


