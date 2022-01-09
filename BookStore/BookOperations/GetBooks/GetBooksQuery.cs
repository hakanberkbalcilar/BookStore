using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext) => _dbContext = dbContext;

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).Select(x =>
            new BooksViewModel
            {
                Title = x.Title,
                Genre = ((GenreEnum)x.GenreId).ToString(),
                PublishDate = x.PublishDate.Date.ToString("dd/MM/yyy"),
                PageCount = x.PageCount
            }
        ).ToList();

        return bookList;
    }
}

public class BooksViewModel
{
    public string? Title { get; set; }
    public int PageCount { get; set; }
    public string? PublishDate { get; set; }
    public string? Genre { get; set; }
}