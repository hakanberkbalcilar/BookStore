using FluentValidation;

namespace BookStore.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Model.Email).MinimumLength(6);
        RuleFor(command => command.Model.Password).MinimumLength(6);
        RuleFor(command => command.Model.Name).MinimumLength(4);
        RuleFor(command => command.Model.Surname).MinimumLength(4);
    }
}