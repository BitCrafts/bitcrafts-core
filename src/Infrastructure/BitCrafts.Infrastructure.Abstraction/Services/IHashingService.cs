namespace BitCrafts.Infrastructure.Abstraction.Services;

public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, string salt);
    string GenerateSalt();

}