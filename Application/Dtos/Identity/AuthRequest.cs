namespace Application.Dtos.Identity
{
    public class AuthRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }   
    }
}
