using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Models.Input
{
    public class LoginInput
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}