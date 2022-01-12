using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == Id);
        
        if (author is null)
            throw new InvalidOperationException("Author is not found!");
        
        var vm = _mapper.Map<AuthorDetailViewModel>(author);
        return vm;
    }
}