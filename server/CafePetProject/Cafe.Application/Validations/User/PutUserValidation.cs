using Cafe.Application.DTOs.UserDTOs.Request;
using FluentValidation;

namespace Cafe.Application.Validations.User;

public class PutUserValidation : AbstractValidator<ChangeUserDataRequest>
{
    public PutUserValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name must not be empty.");
            
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name must not be empty.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number must not be empty.")
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage("Invalid phone number format.")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");
    }
}