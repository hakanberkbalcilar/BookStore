using System;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange(Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(null!);
        command.Id = id;

        //act(Çalıştırma)
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenIdIsValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(null!);
        command.Id = 1;

        //act(Çalıştırma)
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}