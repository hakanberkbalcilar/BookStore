using System;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Eri", "Clapman")]
    [InlineData("Eric", "Cla")]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string name, string familyName)
    {
        //arrange(Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
        command.Model = new CreateAuthorModel
        {
            Name = name,
            FamilyName = familyName,
            Birthday = DateTime.Now.Date.AddYears(-1)
        };

        //act(Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsEqualNow_Validator_ShouldBeReturnError()
    {
        //arrange(Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
        command.Model = new CreateAuthorModel
        {
            Name = "Eric",
            FamilyName = "Clapman",
            Birthday = DateTime.Now.Date
        };

        //act(Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
        command.Model = new CreateAuthorModel
        {
            Name = "Eric",
            FamilyName = "Clapman",
            Birthday = DateTime.Now.Date.AddYears(-1)
        };

        //act(Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}