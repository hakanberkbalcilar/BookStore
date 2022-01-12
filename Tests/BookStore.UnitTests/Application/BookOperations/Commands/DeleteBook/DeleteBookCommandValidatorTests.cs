using System;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange(Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(null!);
        command.Id = id;

        //act(Çalıştırma)
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenIdIsValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(null!);
        command.Id = 1;

        //act(Çalıştırma)
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}