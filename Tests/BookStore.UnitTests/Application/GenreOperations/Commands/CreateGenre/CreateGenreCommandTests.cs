using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreTitleIsAlreadyExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
        command.Model = new CreateGenreModel{Title = "Romance"};
        //act(Çalıştırma)
        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre is already exist!");
    }

    [Fact]
    public void WhenGivenInputsAreValid_Genre_ShouldBeCreated()
    {
        //arrange(Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
        var model = new CreateGenreModel { Title = "WhenGivenInputsAreValid_Genre_ShouldBeCreated" };
        command.Model = model;

        //act(Çalıştırma)
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var genre = _context.Genres.FirstOrDefault(x=> x.Title == model.Title);
        result.Errors.Count.Should().Be(0);
        genre.Should().NotBeNull();
    }

}