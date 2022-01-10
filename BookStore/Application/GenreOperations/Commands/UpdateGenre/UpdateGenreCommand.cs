using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly BookStoreDbContext _dbContext;

    public int Id { get; set; }
    public UpdateGenreModel Model { get; set; } = null!;

    public UpdateGenreCommand(BookStoreDbContext dbContext) => _dbContext = dbContext;

    public void Handle()
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == Id);
        if (genre is null)
            throw new InvalidOperationException("Genre is not exist!");
        if(_dbContext.Genres.Any(x=> x.Title.ToLower() == Model.Title.ToLower() && x.Id != Id))
            throw new InvalidOperationException("A genre with the same name already exists!");

        genre.Title = Model.Title != default ? Model.Title : genre.Title;
        genre.IsActive = Model.IsActive;

        _dbContext.SaveChanges();
    }
}