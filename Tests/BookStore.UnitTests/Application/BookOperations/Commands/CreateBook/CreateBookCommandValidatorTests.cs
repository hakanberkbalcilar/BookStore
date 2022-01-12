using System;
using BookStore.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Lord Of The Rings", 0, 0,1)]
    [InlineData("Lord Of The Rings", 0, 1,0)]
    [InlineData("", 0, 0, 1)]
    [InlineData("", 100, 1, 0)]
    [InlineData("", 0, 1, 0)]
    [InlineData("lor", 100, 1, 1)]
    [InlineData("lord", 100, 0, 0)]
    [InlineData("lord", 0, 1, 0)]
    [InlineData("Lord Of The Rings", 1, 1, 0)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorsId)
    {
        //arrange(Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null!, null!);
        command.Model = new CreateBookModel
        {
            Title = title,
            GenreId = genreId,
            AuthorId = authorsId,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };

        //act(Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsEqualNow_Validator_ShouldBeReturnError()
    {
        //arrange(Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null!, null!);
        command.Model = new CreateBookModel
        {
            Title = "Lord Of The Rings",
            GenreId = 1,
            AuthorId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date
        };

        //act(Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null!, null!);
        command.Model = new CreateBookModel
        {
            Title = "Lord Of The Rings",
            GenreId = 1,
            AuthorId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };

        //act(Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}