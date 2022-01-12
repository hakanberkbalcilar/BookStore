using System;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange(Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
        command.Id = id;

        //act(Çalıştırma)
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenIdIsValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
        command.Id = 1;

        //act(Çalıştırma)
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}