using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
