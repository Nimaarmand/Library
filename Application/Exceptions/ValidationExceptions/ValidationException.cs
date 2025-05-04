using FluentValidation.Results;

namespace Application.Exceptions.ValidationExceptions
{
    public class ValidationExceptionApp : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationExceptionApp(ValidationResult validationResult)
        {
            foreach (var err in validationResult.Errors)
            {
                Errors.Add(err.ErrorMessage);
            }
        }
    }
}
