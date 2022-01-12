using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateAuthorModel Model { get; set; } = null!;

    public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper) {
         _dbContext = dbContext;
         _mapper = mapper;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Name == Model.Name);
        if (author is not null)
            throw new InvalidOperationException("Author is already exist!");

        author = _mapper.Map<Author>(Model);

        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
    }
}