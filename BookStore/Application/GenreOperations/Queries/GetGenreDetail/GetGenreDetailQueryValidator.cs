using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
    }
}