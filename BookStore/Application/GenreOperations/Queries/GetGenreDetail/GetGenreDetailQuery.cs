using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public int Id { get; set; }

    public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
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