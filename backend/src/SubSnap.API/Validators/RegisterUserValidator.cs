using FluentValidation;
using SubSnap.Core.DTOs.Application.Commands.Users;

namespace SubSnap.API.Validators;

//Validator vive solo nell’API, non tocca Domain, Repository o DB!
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email must be valid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters");
    }

}