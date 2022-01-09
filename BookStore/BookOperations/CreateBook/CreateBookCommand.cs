using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookModel Model { get; set; } = null!;

    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper) {
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