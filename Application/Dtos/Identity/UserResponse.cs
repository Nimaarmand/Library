namespace Application.Dtos.Identity
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public IList<string> Roles { get; set; }
    }
}
