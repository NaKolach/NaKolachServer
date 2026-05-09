using NaKolachServer.Domain.Roads;

namespace NaKolachServer.Domain.Routes;

public interface IRouteProvider
{
    public Task<Path> CalculateRoute(Coordinates[] coordinates, RoadCategory roadCategory, CancellationToken cancellationToken);
}