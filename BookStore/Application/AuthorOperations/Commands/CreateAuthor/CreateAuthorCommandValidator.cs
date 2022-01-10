using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Birthday.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Name).MinimumLength(4);
        RuleFor(command => command.Model.FamilyName).MinimumLength(4);
    }
}