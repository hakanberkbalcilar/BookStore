namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorModel
{
    public string Name { get; set; } = null!;
    public string FamilyName {get; set;} = null!;
    public DateTime Birthday { get; set; }
}