using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly IBookStoreDbContext _dbContext;

    public int Id { get; set; }

    public DeleteGenreCommand(IBookStoreDbContext dbContext) => _dbContext = dbContext;

    public void Handle()
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == Id);
        if (genre is null)
            throw new InvalidOperationException("Genre is not exist!");

        _dbContext.Genres.Remove(genre);
        _dbContext.SaveChanges();
    }
}