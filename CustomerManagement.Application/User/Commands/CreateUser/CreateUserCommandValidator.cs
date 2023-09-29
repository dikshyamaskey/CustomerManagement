using FluentValidation;

namespace CustomerManagement.Application.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).Length(1,50).NotEmpty()
            .WithMessage("Please do not leave this FirstName field empty.");
        RuleFor(x => x.LastName).MaximumLength(50).NotEmpty()
            .WithMessage("Please do not leave this LastName field empty.");
        RuleFor(x => x.EmailAddress).MaximumLength(50).NotEmpty()
            .WithMessage("Please do not leave this EmailAddress field empty.");
        RuleFor(x => x.Address).MaximumLength(50).NotEmpty()
            .WithMessage("Please do not leave this Address field empty.");
        RuleFor(x => x.UserName).MaximumLength(50).NotEmpty()
            .WithMessage("Please do not leave this UserName field empty.");
        RuleFor(x => x.Password).MaximumLength(50).NotEmpty()
            .WithMessage("Please do not leave this Password field empty.");
        
    }
}