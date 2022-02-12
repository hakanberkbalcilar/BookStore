using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public String RefreshToken { get; set; } = null!;

    public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is null)
            throw new InvalidOperationException("Refresh Token is not found!");

        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

        _dbContext.SaveChanges();
        return token;
    }
}