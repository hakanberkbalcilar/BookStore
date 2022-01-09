using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();

        var vm = _mapper.Map<List<BooksViewModel>>(bookList);

        return vm;
    }
}