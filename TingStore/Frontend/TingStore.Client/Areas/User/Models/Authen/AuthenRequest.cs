using System.ComponentModel.DataAnnotations;

namespace TingStore.Client.Areas.User.Models.Authen
{
    public class AuthenRequest
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email format. Please enter a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
    }
}
