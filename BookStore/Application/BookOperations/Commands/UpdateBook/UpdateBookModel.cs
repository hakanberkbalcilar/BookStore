namespace BookStore.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookModel
{
    public string Title { get; set; } = null!;
    public int GenreId { get; set; }
}