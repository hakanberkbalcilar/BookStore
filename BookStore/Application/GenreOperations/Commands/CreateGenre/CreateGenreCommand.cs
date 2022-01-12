using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateGenreModel Model { get; set; } = null!;

    public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper) {
         _dbContext = dbContext;
         _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Title == Model.Title);
        if (genre is not null)
            throw new InvalidOperationException("Genre is already exist!");

        genre = _mapper.Map<Genre>(Model);

        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();
    }
}