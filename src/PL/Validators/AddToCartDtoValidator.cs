using BLL.DTOs.Cart;
using FluentValidation;

namespace PL.Validators
{
    public class AddToCartDtoValidator : AbstractValidator<AddToCartDto>
    {
        public AddToCartDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
