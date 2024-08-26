using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Categories.AddChild;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(r => r.City)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        RuleFor(r => r.PhoneNumber)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
    }
}
