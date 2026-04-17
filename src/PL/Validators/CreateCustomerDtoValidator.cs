using BLL.DTOs.Customer;
using FluentValidation;

namespace PL.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PasswordHash)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
