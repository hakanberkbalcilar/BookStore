using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookTitleIsAlreadyExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        command.Model = new CreateAuthorModel { Name = "Frank" };
        //act(Çalıştırma)
        //assert(Doğrulama)
        FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is already exist!");
    }

    [Fact]
    public void WhenGivenInputsAreValid_Author_ShouldBeCreated()
    {
        //arrange(Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        var model = new CreateAuthorModel
        {
            Name = "WhenGivenInputsAreValid_Book_ShouldBeCreated",
            FamilyName = "WhenGivenInputsAreValid_Book_ShouldBeCreated",
            Birthday = DateTime.Now.Date.AddYears(-1)
        };
        command.Model = model;

        //act(Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert(Doğrulama)
        var author = _context.Authors.FirstOrDefault(x => x.Name == model.Name);
        result.Errors.Count.Should().Be(0);
        author.Should().NotBeNull();
    }

}