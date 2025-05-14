using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string ?LastName { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
