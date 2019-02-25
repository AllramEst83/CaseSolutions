using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Gateway.API.ViewModels.Validation
{
    public class RemoveUserfromRoleViewModelValidator : AbstractValidator<RemoveUserfromRoleViewModel>
    {
        public RemoveUserfromRoleViewModelValidator()
        {
            RuleFor(vm => vm.UserId).NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(vm => vm.Role).NotEmpty().WithMessage("Role cannot be empty");
        }
    }
}
