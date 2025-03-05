using BitCrafts.Users.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BitCrafts.Users.Entities;

public class UsersDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("InternalDb"));
            //optionsBuilder.UseSqlite(ServerVersion.AutoDetect(_configuration.GetConnectionString("MainDb")));
        }
    }
}