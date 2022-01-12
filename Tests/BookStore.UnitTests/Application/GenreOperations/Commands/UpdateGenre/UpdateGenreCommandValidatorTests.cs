using System;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Romance",  0)]
    [InlineData("Rom",  1)]
    [InlineData("Rom",  0)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string title, int id)
    {
        //arrange(Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(null!);
        command.Id = id;
        command.Model = new UpdateGenreModel { Title = title};

        //act(Çalıştırma)
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(null!);
        command.Id = 1;
        command.Model = new UpdateGenreModel { Title = "Romance"};

        //act(Çalıştırma)
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}