using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;

    public int Id { get; set; }
    public UpdateAuthorModel Model { get; set; } = null!;

    public UpdateAuthorCommand(IBookStoreDbContext dbContext) => _dbContext = dbContext;

    public void Handle()
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == Id);
        if (author is null)
            throw new InvalidOperationException("Author is not exist!");
        if(_dbContext.Authors.Any(x=> (x.Name+x.FamilyName).ToLower() == (Model.Name+Model.FamilyName).ToLower() && x.Id != Id))
            throw new InvalidOperationException("An author with the same name already exists!");

        author.Name = Model.Name != default ? Model.Name : author.Name;
        author.FamilyName = Model.FamilyName != default ? Model.FamilyName : author.FamilyName;
        author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;

        _dbContext.SaveChanges();
    }
}