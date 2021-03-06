using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{

    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList();

        var vm = _mapper.Map<List<BooksViewModel>>(bookList);

        return vm;
    }
}