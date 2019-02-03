using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.ViewModels.Validation
{
    public class DeleteUserViewModelValidator : AbstractValidator<DeleteUserViewModel>
    {

        public DeleteUserViewModelValidator()
        {
            RuleFor(vm => vm.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(vm => vm.Id).NotNull().WithMessage("User does not exists");
        }
    }
}
