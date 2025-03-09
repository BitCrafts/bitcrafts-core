using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BitCrafts.Users.Abstraction.Entities;

public sealed class UsersDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public UsersDbContext(DbContextOptions<UsersDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite(_configuration.GetConnectionString("InternalDb"));
    }
}