using System;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void WhenGivenIdIsInvalid_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange(Hazırlık)
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null!, null!);
        command.Id = id;

        //act(Çalıştırma)
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenIdIsValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null!, null!);
        command.Id = 1;

        //act(Çalıştırma)
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}