namespace BookStore.Application.UserOperations.Commands.CreateToken;

public class CreateTokenModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}