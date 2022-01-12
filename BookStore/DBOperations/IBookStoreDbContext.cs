using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations;

public interface IBookStoreDbContext
{
    DbSet<User> Users {get;set;}
    DbSet<Book> Books {get;set;}
    DbSet<Genre> Genres {get;set;}
    DbSet<Author> Authors {get;set;}

    int SaveChanges();
}