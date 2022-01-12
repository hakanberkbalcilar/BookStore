using System;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Jules", "Verne", 0)]
    [InlineData("Jules", "Ver", 0)]
    [InlineData("Jul", "Ver", 0)]
    [InlineData("Jul", "Ver", 1)]
    [InlineData("Jul", "Verne", 1)]
    [InlineData("Jules", "Ver", 1)]
    public void WhenGivenInputsAreInvalid_Validator_ShouldBeReturnErrors(string name, string familyname, int id)
    {
        //arrange(Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(null!);
        command.Id = id;
        command.Model = new UpdateAuthorModel { Name = name, FamilyName = familyname };

        //act(Çalıştırma)
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenInputsAreValid_Validator_ShouldNotBeReturnError()
    {
        //arrange(Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(null!);
        command.Id = 1;
        command.Model = new UpdateAuthorModel { Name = "Jules", FamilyName = "Verne", Birthday = DateTime.Now.AddYears(-1) };

        //act(Çalıştırma)
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert(Doğrulama)
        result.Errors.Count.Should().Be(0);
    }


}