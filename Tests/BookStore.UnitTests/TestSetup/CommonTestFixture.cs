using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; } = null!;
    public IMapper Mapper { get; set; } = null!;

    public CommonTestFixture(){
        var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();
        
        Context.AddAuthors();
        Context.AddGenres();
        Context.AddBooks();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }
}