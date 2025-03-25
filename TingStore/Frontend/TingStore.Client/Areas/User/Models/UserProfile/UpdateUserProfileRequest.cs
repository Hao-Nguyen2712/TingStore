

namespace TingStore.Client.Areas.User.Models.UserProfile
{
    public class UpdateUserProfileRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
