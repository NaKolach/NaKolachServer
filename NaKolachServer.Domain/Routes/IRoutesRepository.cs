namespace NaKolachServer.Domain.Routes;

public interface IRoutesRepository
{
    public Task<Route?> GetRouteById(Guid id, CancellationToken cancellationToken);
    public Task<Route[]> GetRoutesByUserId(Guid userId, CancellationToken cancellationToken);
    public Task InsertRoute(Route route, CancellationToken cancellationToken);
    public Task InsertUserRoute(RouteUser routeUser, CancellationToken cancellationToken);
    public Task RemoveUserRoute(RouteUser routeUser, CancellationToken cancellationToken);
}