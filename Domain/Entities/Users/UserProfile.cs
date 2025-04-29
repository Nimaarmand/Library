using Domain.Entities.Commons;

namespace Domain.Entity.Users
{
    public class UserProfile : BaseEntityNotId
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
