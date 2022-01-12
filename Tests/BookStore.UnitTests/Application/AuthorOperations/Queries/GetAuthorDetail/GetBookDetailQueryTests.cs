using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
        command.Id = 0;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not exist!");
    }

    [Fact]
    public void WhenGivenIdIsValid_Author_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
        command.Id = 2;

        //act(Çalıştırma)
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var book = _context.Books.FirstOrDefault(x=> x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        book.Should().NotBeNull();
    }

}