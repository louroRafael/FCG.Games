using FCG.Games.Domain.DTOs.Requests;
using FluentValidation;

namespace FCG.Games.Domain.Validators;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(2).WithMessage("Title must have at least 2 characters.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(10).WithMessage("Description must have at least 10 characters.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Genre)
            .IsInEnum().WithMessage("Invalid game genre.");

        RuleFor(x => x.Platform)
            .IsInEnum().WithMessage("Invalid game platform.");

        RuleFor(x => x.Developer)
            .NotEmpty().WithMessage("Developer name is required.")
            .MinimumLength(2).WithMessage("Developer name must have at least 2 characters.")
            .MaximumLength(50).WithMessage("Developer name cannot exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be zero or positive.")
            .LessThanOrEqualTo(9999.99m).WithMessage("Price cannot exceed 9999.99.");

        
    }
}
