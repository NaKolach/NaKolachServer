using NaKolachServer.Domain.Roads;

namespace NaKolachServer.Domain.Points;

public record PointsSearchParams(
    string[] Categories,
    RoadCategory RoadCategory,
    double Latitude,
    double Longitude,
    int Radius
);