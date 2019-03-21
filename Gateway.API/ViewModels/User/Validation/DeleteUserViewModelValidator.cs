using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Gateway.API.ViewModels.Validation
{
    public class DeleteUserViewModelValidator : AbstractValidator<DeleteUserViewModel>
    {
        public DeleteUserViewModelValidator()
        {
            RuleFor(vm => vm.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}
