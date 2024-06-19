using FluentValidation;
using Kiron.Application.Models;

namespace Kiron.Application.Validation;

internal class UserCredentialsValidator : AbstractValidator<UserCredentials>
{
    public UserCredentialsValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty")
            .EmailAddress().WithMessage("Username must be a valid email address")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters");

        RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be empty")
                     .MinimumLength(8).WithMessage("Password length must be at least 8.")
                     .MaximumLength(16).WithMessage("Password length must not exceed 16.")
                     .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                     .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                     .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
                     .Matches(@"[\W_]+").WithMessage("Password must contain at least one special character.");



    }


}

