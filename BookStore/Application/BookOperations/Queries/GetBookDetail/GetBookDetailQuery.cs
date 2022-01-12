using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefault(x => x.Id == Id);
        
        if (book is null)
            throw new InvalidOperationException("Book is not exist!");
        
        var vm = _mapper.Map<BookDetailViewModel>(book);
        return vm;
    }
}