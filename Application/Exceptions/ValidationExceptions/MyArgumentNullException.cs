global using Domain.Enumorations;

namespace Application.Exceptions.ValidationExceptions
{
    public class MyArgumentNullException : ArgumentNullException
    {
        public ErrorType RuleId { get; set; }
        public MyArgumentNullException(ErrorType rule)
        {
            RuleId = rule;
        }
    }
}