using API.Helpers.DTO;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers.Validators
{
    public class EditEmployerDTOValidator : AbstractValidator<EditEmployerDTO>
    {
        private List<string> PossibleEmployeeCountValues = new List<string> { "1-10", "10-50", "50-100", "100-200", "200-500", "500-1000", "1000+"};
        public EditEmployerDTOValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.EmployeeCount).Must(count => this.PossibleEmployeeCountValues.Contains(count)).When(x => !x.EmployeeCount.IsNullOrEmpty());
            RuleFor(x => x.Address).MaximumLength(120);
        }
    }
}
