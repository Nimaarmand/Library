namespace Application.Dtos.Commons.ErrorException
{
    public class ErrorMasterDto
    {
        public int StatusCode { get; set; }
        public ICollection<ErrorDetailsDto> ErrorDetails { get; set; } = new List<ErrorDetailsDto>();
    }
}
