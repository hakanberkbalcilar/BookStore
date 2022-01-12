using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public int Id { get; set; }

    public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == Id);
        if (genre is null)
            throw new InvalidOperationException("Genre is not found!");
        
        var vm = _mapper.Map<GenreDetailViewModel>(genre);
        return vm;
    }

}