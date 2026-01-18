using NaKolachServer.Domain.Points;

namespace NaKolachServer.Domain.Routes;

public record CustomRouteSearchParams(
    double Latitude,
    double Longitude,
    long[] Points
);