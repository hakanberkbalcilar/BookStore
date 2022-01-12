using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.Id = 0;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not exist!");
    }

    [Fact]
    public void WhenAuthorHasBook_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.Id = 2;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author has one or more books");
    }

    [Fact]
    public void WhenGivenIdIsValid_Author_ShouldBeDeleted()
    {
        //arrange(Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.Id = 3;

        //act(Çalıştırma)
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var author = _context.Authors.FirstOrDefault(x=> x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        author.Should().BeNull();
    }

}