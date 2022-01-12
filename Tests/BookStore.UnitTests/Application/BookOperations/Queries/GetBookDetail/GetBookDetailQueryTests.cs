using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
        command.Id = 0;

        //assert(Doğrulama)
        FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book is not exist!");
    }

    [Fact]
    public void WhenGivenIdIsValid_Book_ShouldBeReturn()
    {
        //arrange(Hazırlık)
        GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
        command.Id = 2;

        //act(Çalıştırma)
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(command);
        FluentActions.Invoking(()=> command.Handle()).Invoke();

        //assert(Doğrulama)
        var book = _context.Books.FirstOrDefault(x=> x.Id == command.Id);
        result.Errors.Count.Should().Be(0);
        book.Should().NotBeNull();
    }

}