using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ChangePasswordRequest
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string NewPassword { get; set; }

        public string DisposableCode { get; set; }
    }

}
