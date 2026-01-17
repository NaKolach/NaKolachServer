using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Infrastructure.Database.Business;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Route>()
            .Property(r => r.Path)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Route>()
            .Property(r => r.Points)
            .HasColumnType("jsonb");

        modelBuilder.Entity<RouteUser>()
            .HasKey(ru => new { ru.RouteId, ru.UserId });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> UserRefreshTokens { get; set; }

    public DbSet<Route> Routes { get; set; }
    public DbSet<RouteUser> RouteUsers { get; set; }
}
