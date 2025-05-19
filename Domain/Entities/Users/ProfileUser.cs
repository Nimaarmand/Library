using Domain.Entities.Commons;
using Domain.Entities.Reservations;

namespace Domain.Entities.Users

{
    public class ProfileUser : BaseEntityNotId
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public long DeliveriesId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public string BirthDate { get; set; }
        public ICollection<Deliverys> Deliveries { get; set; }
    }
}
