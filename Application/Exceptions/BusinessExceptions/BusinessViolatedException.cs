using System.Runtime.Serialization;

namespace Application.Exceptions.BusinessExceptions
{
    public class BusinessViolatedException : ApplicationException
    {
        public BusinessRule BusinessRule { get; protected set; }
        public BusinessViolatedException()
        {

        }
        public BusinessViolatedException(string message, BusinessRule businessRule) : base(message)
        {
            BusinessRule = businessRule;
        }

        public BusinessViolatedException(string message) : base(message)
        {
        }

        public BusinessViolatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessViolatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public ErrorType RuleId { get; set; }
        public BusinessViolatedException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }
}