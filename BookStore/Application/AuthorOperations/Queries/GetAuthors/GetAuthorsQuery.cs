using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{

    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authorList = _dbContext.Authors.OrderBy(x => x.Id).ToList();

        var vm = _mapper.Map<List<AuthorsViewModel>>(authorList);

        return vm;
    }
}