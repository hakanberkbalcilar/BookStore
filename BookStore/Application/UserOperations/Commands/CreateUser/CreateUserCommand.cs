using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateUserModel Model { get; set; } = null!;

    public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper) {
         _dbContext = dbContext;
         _mapper = mapper;
    }

    public void Handle()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("User is already exist!");

        user = _mapper.Map<User>(Model);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }
}