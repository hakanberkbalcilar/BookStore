using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    public string Title {get; set;} = null!;

    public int GenreId {get; set;}
    public int AuthorId {get; set;}

    public int PageCount {get; set;}

    public DateTime PublishDate {get; set;}

    public Genre Genre {get; set;} = null!;
    public Author Author {get; set;} = null!;

}
