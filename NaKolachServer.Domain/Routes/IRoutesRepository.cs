namespace NaKolachServer.Domain.Routes;

public interface IRoutesRepository
{
    public Task<Route?> GetRouteById(Guid id, CancellationToken cancellationToken);
    public Task<RouteData[]> GetRoutesByUserId(Guid userId, CancellationToken cancellationToken);
    public Task InsertRoute(Route route, CancellationToken cancellationToken);
    public Task InsertUserRoute(RouteUser routeUser, CancellationToken cancellationToken);
    public Task UpdateUserRouteName(Guid id, Guid userId, string name, CancellationToken cancellationToken);
    public Task RemoveUserRoute(Guid id, Guid userId, CancellationToken cancellationToken);
}