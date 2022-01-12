using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle(){
        var genreList = _context.Genres.Where(x=> x.IsActive).OrderBy(x => x.Title);
        var vm = _mapper.Map<List<GenresViewModel>>(genreList);
        return vm;
    }
}