namespace NaKolachServer.Domain.Points;

public record PointsSearchParams(
    string[] Categories,
    double Latitude,
    double Longitude,
    int Radius
);