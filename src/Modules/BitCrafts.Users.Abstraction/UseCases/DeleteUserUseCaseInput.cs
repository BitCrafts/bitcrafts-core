using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.UseCases;

public class DeleteUserUseCaseInput
{
    public DeleteUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
}