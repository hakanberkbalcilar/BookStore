using System;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void WhenGivenIdIsInvalid_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange(Hazırlık)
        GetBookDetailQuery command = new GetBookDetailQuery(null!, null!);
        command.Id = id;

        //act(Çalıştırma)
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenIdIsValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        GetBookDetailQuery command = new GetBookDetailQuery(null!, null!);
        command.Id = 1;

        //act(Çalıştırma)
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}