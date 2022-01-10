using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Entities;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;

namespace BookStore.Common;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        //Author
        CreateMap<CreateAuthorModel, Author>();
        CreateMap<Author, AuthorDetailViewModel>();
        CreateMap<Author, AuthorsViewModel>();

        //Book
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Title)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.FamilyName));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Title)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.FamilyName));

        //Genre
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<Genre, GenresViewModel>();
    }

}