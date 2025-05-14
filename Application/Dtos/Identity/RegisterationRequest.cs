using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class RegisterationRequest
    {
       
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        public string RePassword { get; set; }
        //public string roleName { get; set; }

        //public string DisposableCode { get; set; }
    }

}
