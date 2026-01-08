namespace NaKolachServer.Domain.Points;

public record PointsSearchParams(
    AmenityType[] Types,
    double Latitude,
    double Longitude,
    int Radius
);