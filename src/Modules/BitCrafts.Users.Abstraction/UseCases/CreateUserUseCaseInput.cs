using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.UseCases;

public class CreateUserUseCaseInput
{
    public string Password { get; set; }
    public User User { get; set; }
}