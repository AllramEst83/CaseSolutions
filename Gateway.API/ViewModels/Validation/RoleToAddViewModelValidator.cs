using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels.Validation
{
    public class RoleToAddViewModelValidator : AbstractValidator<RoleToAddViewModel>
    {
        public RoleToAddViewModelValidator()
        {
            RuleFor(vm => vm.RoleToAdd).NotEmpty().WithMessage("Role cannot be empty");
        }
    }
}
