using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CircleOfFunk.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Your Name is required")]
        [DisplayName("Name")]
        [StringLength(50, ErrorMessage = "50 characters at most please")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your Email is required")]
        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "The Email address is not valid")]
        public string Email { get; set; }
    }
}