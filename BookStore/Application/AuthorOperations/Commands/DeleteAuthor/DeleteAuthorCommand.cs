using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly BookStoreDbContext _dbContext;

    public int Id { get; set; }

    public DeleteAuthorCommand(BookStoreDbContext dbContext) => _dbContext = dbContext;

    public void Handle()
    {
        var author = _dbContext.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == Id);
        if (author is null)
            throw new InvalidOperationException("Author is not exist!");

        if(author.Books.Any())
            throw new InvalidOperationException("Author has one or more books");

        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
    }
}