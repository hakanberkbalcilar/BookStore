using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.Id = 100;
        command.Model = new UpdateAuthorModel { Name = "SampleAuthor" };
        //act(Çalıştırma)
        //assert(Doğrulama)
        FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not exist!");
    }

    [Fact]
    public void WhenGivenAuthorNameIsAlreadyExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.Id = 2;
        command.Model = new UpdateAuthorModel { Name = "Charlotte Perkins", FamilyName = "Gilman" };
        //act(Çalıştırma)
        //assert(Doğrulama)
        FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("An author with the same name is already exists!");
    }

    [Fact]
    public void WhenGivenInputsAreValid_Author_ShouldBeUpdated()
    {
        //arrange(Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        var model = new UpdateAuthorModel
        {
            Name = "WhenGivenInputsAreValid_Book_ShouldBeUpdated",
            FamilyName = "WhenGivenInputsAreValid_Book_ShouldBeUpdated",
            Birthday = DateTime.Now.AddYears(-1)
        };
        command.Id = 1;
        command.Model = model;

        //act(Çalıştırma)
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert(Doğrulama)
        var author = _context.Authors.FirstOrDefault(x => x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        author.Should().NotBeNull();
    }

}