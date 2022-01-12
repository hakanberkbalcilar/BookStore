using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestSetup;

public static class Authors
{

    public static void AddAuthors(this BookStoreDbContext context)
    {
        context.Authors.AddRange(
            new Author { Name = "Frank", FamilyName = "Herbert", Birthday = new DateTime(2001, 06, 12) },
            new Author { Name = "Charlotte Perkins", FamilyName = "Gilman", Birthday = new DateTime(2010, 05, 23) },
            new Author { Name = "Eric", FamilyName = "Ries", Birthday = new DateTime(2001, 12, 21) });
    }
}