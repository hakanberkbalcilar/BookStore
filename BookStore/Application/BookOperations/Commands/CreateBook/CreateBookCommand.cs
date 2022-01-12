using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookModel Model { get; set; } = null!;

    public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper) {
         _dbContext = dbContext;
         _mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Title == Model.Title);
        if (book is not null)
            throw new InvalidOperationException("Book is already exist!");

        book = _mapper.Map<Book>(Model);

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}