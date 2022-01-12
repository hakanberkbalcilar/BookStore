using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIdIsNotExist_InvalidOperationExeception_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.Id = 10;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book is not exist!");
    }

    [Fact]
    public void WhenGivenIdIsValid_Book_ShouldBeDeleted()
    {
        //arrange(Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.Id = 1;

        //act(Çalıştırma)
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var book = _context.Books.FirstOrDefault(x=> x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        book.Should().BeNull();
    }

}