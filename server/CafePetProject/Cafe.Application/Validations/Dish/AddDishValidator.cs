using Cafe.Application.DTO.DishDTOs.Request;
using Cafe.Application.DTOs.DishDTOs.Request;
using FluentValidation;

namespace Cafe.Application.Validations;

public class AddDishValidator : AbstractValidator<AddDishRequest>
{
    public AddDishValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title must not be empty.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description must not be empty.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl must not be empty.")
            .Must(BeAValidUrl).WithMessage("Image URL must be a valid URL.");
    }

    private bool BeAValidUrl(string url)
    {
        return string.IsNullOrWhiteSpace(url) || Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}