using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Infrastructure.Database.OSM;

public class OSMDatabaseContext(DbContextOptions<OSMDatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");

        builder.Entity<PlanetOsmPoint>()
            .HasKey(m => m.OsmId);

        base.OnModelCreating(builder);
    }
    public DbSet<PlanetOsmPoint> PlanetOsmPoint { get; set; }
}
