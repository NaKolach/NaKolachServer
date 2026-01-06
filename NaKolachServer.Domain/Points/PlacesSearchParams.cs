namespace NaKolachServer.Domain.Points;

public record PointsSearchParams(
    AmenityType[] Types,
    double Latidude,
    double Longtidude,
    int Radius
);