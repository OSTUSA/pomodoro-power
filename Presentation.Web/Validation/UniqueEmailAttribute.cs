using System.ComponentModel.DataAnnotations;
using Core.Domain.Model.Users;
using Ninject;

namespace Presentation.Web.Validation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        [Inject]
        public IUserRepository Repo { get; set; }

        protected string Message { get; set; }

        public UniqueEmailAttribute(string message = "")
        {
            Message = "This email address is already in use.";
            if (!string.IsNullOrEmpty(message))
                Message = message;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return GetValidationResult((string)value);
        }

        protected ValidationResult GetValidationResult(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            var users = Repo.GetByEmail(email);
            if (users != null)
                return new ValidationResult(Message);

            return null;
        }
    }
}