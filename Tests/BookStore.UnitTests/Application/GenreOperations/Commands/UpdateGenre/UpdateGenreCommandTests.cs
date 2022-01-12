using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.Id = 100;
        command.Model = new UpdateGenreModel { Title = "SampleGenre" };
        //act(Çalıştırma)
        //assert(Doğrulama)
        FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is not exist!");
    }

    [Fact]
    public void WhenGivenInputsAreValid_Genre_ShouldBeUpdated()
    {
        //arrange(Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        var model = new UpdateGenreModel { Title = "WhenGivenInputsAreValid_Genre_ShouldBeUpdated"};
        command.Id = 3;
        command.Model = model;

        //act(Çalıştırma)
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert(Doğrulama)
        var genre = _context.Genres.FirstOrDefault(x => x.Title == model.Title);
        result.Errors.Count.Should().Be(0);
        genre.Should().NotBeNull();
    }

}