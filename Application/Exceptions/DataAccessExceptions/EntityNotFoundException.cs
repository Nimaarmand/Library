

namespace Application.Exceptions.DataAccessExceptions
{
    public class EntityNotFoundException : Exception
    {
        public ErrorType RuleId { get; set; }
        public EntityNotFoundException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }

}
