using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
        RuleFor(command => command.Model.Birthday.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Name).MinimumLength(4);
        RuleFor(command => command.Model.FamilyName).MinimumLength(4);
    }
}