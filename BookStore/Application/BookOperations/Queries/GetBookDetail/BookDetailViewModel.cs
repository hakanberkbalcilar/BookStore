namespace BookStore.Application.BookOperations.Queries.GetBookDetail;

public class BookDetailViewModel
{
    public string Title { get; set; } = null!;
    public int PageCount { get; set; }
    public string PublishDate { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string Author { get; set; } = null!;
}