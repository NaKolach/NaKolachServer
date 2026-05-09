using NaKolachServer.Domain.Roads;

namespace NaKolachServer.Domain.Routes;

public record CustomRouteSearchParams(
    double Latitude,
    double Longitude,
    long[] Points,
    RoadCategory RoadCategory
);