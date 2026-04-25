
using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Routes;

namespace NaKolachServer.Infrastructure.Database.Business;

public class EFRoutesRepository(DatabaseContext databaseContext) : IRoutesRepository
{
    public async Task<Route?> GetRouteById(Guid id, CancellationToken cancellationToken)
    {
        return await databaseContext.Routes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<RouteData[]> GetRoutesByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await databaseContext.RouteUsers
            .Where(ru => ru.UserId == userId)
            .Join(databaseContext.Routes,
                ru => ru.RouteId,
                r => r.Id,
                (ru, r) => new RouteData(r.Id, ru.Name, r.Distance, r.Time, r.Categories))
            .ToArrayAsync(cancellationToken);
    }

    public async Task InsertRoute(Route route, CancellationToken cancellationToken)
    {
        await databaseContext.Routes.AddAsync(route, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task InsertUserRoute(RouteUser routeUser, CancellationToken cancellationToken)
    {
        await databaseContext.RouteUsers.AddAsync(routeUser, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserRouteName(Guid id, Guid userId, string name, CancellationToken cancellationToken)
    {
        await databaseContext.RouteUsers.Where(rt => rt.RouteId == id && rt.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.Name, name), cancellationToken);
    }

    public async Task RemoveUserRoute(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        await databaseContext.RouteUsers.Where(r => r.RouteId == id && r.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
