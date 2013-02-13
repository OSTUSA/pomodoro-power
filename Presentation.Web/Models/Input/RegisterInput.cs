using System.ComponentModel.DataAnnotations;
using Core.Domain.Services;

namespace Presentation.Web.Models.Input
{
    public class RegisterInput
    {
        [Required, UniqueEmail]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}