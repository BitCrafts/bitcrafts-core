using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.UseCases;

public class UpdateUserUseCaseInput
{
    public UpdateUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
}