using System.Linq;
using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        return Ok(query.Handle());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailViewModel book;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.id = id;
            book = query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        try
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = id;
            command.Model = updatedBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = id;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

}