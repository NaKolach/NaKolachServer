namespace NaKolachServer.Domain.Routes;

public interface IRouteProvider
{
    public Task<Path> CalculateRoute(Coordinates[] coordinates, CancellationToken cancellationToken);
}