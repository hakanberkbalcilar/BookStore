using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    public int id { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext) => _dbContext = dbContext;

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);
        if(book is null)
            throw new InvalidOperationException("Book is not found!");
        var vm = new BookDetailViewModel
        {
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
            PageCount = book.PageCount
        };
        return vm;
    }
}