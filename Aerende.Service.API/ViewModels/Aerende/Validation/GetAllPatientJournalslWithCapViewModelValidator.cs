using Aerende.Service.API.ViewModels.Aerende;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerende.Service.API.ViewModels.Aerende.Validation
{
    public class GetAllPatientJournalslWithCapViewModelValidator : AbstractValidator<GetAllPatientJournalslWithCapViewModel>
    {
        public GetAllPatientJournalslWithCapViewModelValidator()
        {
            RuleFor(vm => vm.Cap).GreaterThanOrEqualTo(0).WithMessage("Cap cannot be empty");
        }
    }
}
