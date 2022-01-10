using FluentValidation;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
    }
}