using AuthorizationServer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Infrastructure.Data
{
    public class RefreshTokensDbContext : DbContext
    {
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public RefreshTokensDbContext(DbContextOptions<RefreshTokensDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
