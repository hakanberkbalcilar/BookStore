using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestSetup;

public static class Genres
{

    public static void AddGenres(this BookStoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre { Title = "Personal Growth" },
            new Genre { Title = "Science Fiction" },
            new Genre { Title = "Romance" });
    }
}