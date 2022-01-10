namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorModel
{
    public string Name { get; set; } = null!;
    public string FamilyName {get; set;} = null!;
    public DateTime Birthday { get; set; }
}