using BLL.DTOs.Order;
using FluentValidation;

namespace PL.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);
        }
    }
}
