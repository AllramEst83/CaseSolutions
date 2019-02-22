using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.ViewModels.Validation
{
    public class AddUserToRoleViewModelValidator : AbstractValidator<AddUserToRoleViewModel>
    {
        public AddUserToRoleViewModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email cannot be empty");
            //RuleFor(vm => vm.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(vm => vm.Role).NotEmpty().WithMessage("Role cannot be empty");
        }
    }
}
