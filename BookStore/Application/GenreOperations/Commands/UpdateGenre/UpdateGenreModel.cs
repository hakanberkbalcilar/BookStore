namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreModel
{
    public string Title { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}