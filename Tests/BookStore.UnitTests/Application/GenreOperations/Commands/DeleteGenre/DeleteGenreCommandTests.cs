using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.Id = 0;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not exist!");
    }

    [Fact]
    public void WhenGivenIdIsValid_Genre_ShouldBeDeleted()
    {
        //arrange(Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.Id = 1;

        //act(Çalıştırma)
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var genre = _context.Genres.FirstOrDefault(x=> x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        genre.Should().BeNull();
    }

}