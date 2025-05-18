using Application.Dtos.Identity.UserProfile;

namespace Library.Models.ViewModels
{
    public class CreateDeliveryViewModel
    {
        public long BookId { get; set; }  // شناسه کتاب
        public List<UserProfileDto> Users { get; set; }  // لیست کاربران
    }

}
