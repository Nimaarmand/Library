using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ForgotsPasswordRequest
    {
        [Required]
        [RegularExpression(@"^09[0|1|2|3][0-9]{8}$", ErrorMessage = "Invalid Phone Number!")]
        public string PhoneNumber { get; set; }
        public string DisposableCode { get; set; }
        public string NewPassword { get; set; }
    }

}
