using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations;

public class BookStoreDbContext : DbContext{

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options){}

    public DbSet<Author> Authors{get; set;} = null!;
    public DbSet<Book> Books{get; set;} = null!;
    public DbSet<Genre> Genres{get; set;} = null!;

}