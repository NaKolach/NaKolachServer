using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Users;

namespace NaKolachServer.Infrastructure.Database.Business;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
