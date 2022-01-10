using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
                return;
            
            context.Authors.AddRange(new Author
            {
                Name = "Frank",
                FamilyName = "Herbert",
                Birthday = new DateTime(2001, 06, 12)
            },
            new Author
            {
                Name = "Charlotte Perkins",
                FamilyName = "Gilman",
                Birthday = new DateTime(2010, 05, 23)
            },
            new Author
            {
                Name = "Eric",
                FamilyName = "Ries",
                Birthday = new DateTime(2001, 12, 21)
            });

            context.Genres.AddRange(
                new Genre
                {
                    Title = "Personal Growth"
                },
                new Genre
                {
                    Title = "Science Fiction"
                },
                new Genre
                {
                    Title = "Romance"
                }
            );

            context.Books.AddRange(new Book
            {
                Title = "Lean Startup",
                GenreId = 1,
                AuthorId = 3,
                PageCount = 200,
                PublishDate = new DateTime(2001, 06, 12)
            },
            new Book
            {
                Title = "Herland",
                GenreId = 2,
                AuthorId = 2,
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                Title = "Dune",
                GenreId = 2,
                AuthorId = 1,
                PageCount = 540,
                PublishDate = new DateTime(2001, 12, 21)
            });

            context.SaveChanges();
        }
    }
}