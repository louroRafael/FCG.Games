using FCG.Games.Domain.DTOs.Requests;
using FluentValidation;

namespace FCG.Games.Domain.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User Id is required.");

        RuleFor(x => x.GameIds)
            .NotNull().WithMessage("Games array cannot be null.")
            .NotEmpty().WithMessage("At least one game must be selected.")
            .Must(games => games != null && games.Length > 0).WithMessage("At least one game must be selected.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");
    }
}
