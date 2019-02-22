
using Auth.API.ViewModels.Validation;
using FluentValidation;

namespace Auth.API.ViewModels
{
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
