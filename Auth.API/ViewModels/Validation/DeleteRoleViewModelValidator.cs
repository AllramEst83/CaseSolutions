using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Auth.API.ViewModels.Validation
{
    public class DeleteRoleViewModelValidator : AbstractValidator<DeleteRoleViewModel>
    {
        public DeleteRoleViewModelValidator()
        {
            RuleFor(vm => vm.RoleId).NotEmpty().WithMessage("RoleId cannot be empty");
            RuleFor(vm => vm.RoleName).NotEmpty().WithMessage("RoleName cannot be empty");
        }
    }
}
