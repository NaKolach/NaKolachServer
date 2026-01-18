
using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Routes;

namespace NaKolachServer.Infrastructure.Database.Business;

public class EFRoutesRepository(DatabaseContext databaseContext) : IRoutesRepository
{
    public async Task<Route?> GetRouteById(Guid id, CancellationToken cancellationToken)
    {
        return await databaseContext.Routes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Route[]> GetRoutesByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await databaseContext.RouteUsers
            .Where(ru => ru.UserId == userId)
            .Join(databaseContext.Routes,
                ru => ru.RouteId,
                r => r.Id,
                (ru, r) => r)
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

    public async Task RemoveUserRoute(RouteUser routeUser, CancellationToken cancellationToken)
    {
        databaseContext.RouteUsers.Remove(routeUser);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
