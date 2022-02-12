using AutoMapper;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BookStore.Application.UserOperations.Commands.CreateToken;
using BookStore.TokenOperations.Models;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Application.UserOperations.Commands.RefreshToken;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);
        command.Model = newUser;
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
        command.Model = login;

        return Ok(command.Handle());
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] String token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;

        return Ok(command.Handle());
    }
}