using System;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Lord Of The Rings", 1, 0)]
    [InlineData("Lor",  0, 0)]
    [InlineData("Lor",  1, 1)]
    [InlineData("Lord Of The Rings",  0, 1)]
    [InlineData("Lord Of The Rings",  0, 0)]
    [InlineData("Lor",  0, 1)]
    [InlineData("Lor",  1, 0)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string title, int id, int genreId)
    {
        //arrange(Hazırlık)
        UpdateBookCommand command = new UpdateBookCommand(null!);
        command.Id = id;
        command.Model = new UpdateBookModel { Title = title, GenreId = genreId };

        //act(Çalıştırma)
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        UpdateBookCommand command = new UpdateBookCommand(null!);
        command.Id = 1;
        command.Model = new UpdateBookModel { Title = "Lord Of The Rings", GenreId = 1 };

        //act(Çalıştırma)
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}