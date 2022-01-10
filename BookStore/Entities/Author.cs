using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities;

public class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    public string Name {get; set;} = null!;
    public string FamilyName {get; set;} = null!;

    public DateTime Birthday {get; set;}

    public Collection<Book> Books {get; set;} = null!;

}
