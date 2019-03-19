using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels.Aerende.Validation
{
    public class GetAllPatientJournalslWithCapViewModelValidator : AbstractValidator<GetAllPatientJournalslWithCapViewModel>
    {
        public GetAllPatientJournalslWithCapViewModelValidator()
        {
            RuleFor(vm => vm.Cap).NotEmpty().WithMessage("cap cannot be empty");
        }
    }
}
