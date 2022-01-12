using System;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Ro")]
    [InlineData("Rom")]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string title)
    {
        //arrange(Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(null!, null!);
        command.Model = new CreateGenreModel { Title = title };

        //act(Çalıştırma)
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(null!, null!);
        command.Model = new CreateGenreModel { Title = "Romance" };

        //act(Çalıştırma)
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}