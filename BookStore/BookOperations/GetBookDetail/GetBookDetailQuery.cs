using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int id { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);
        
        if (book is null)
            throw new InvalidOperationException("Book is not found!");
        
        var vm = _mapper.Map<BookDetailViewModel>(book);
        return vm;
    }
}