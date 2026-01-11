namespace NaKolachServer.Domain.Points;

public record Point(
    long Id,
    string? Name,
    string? Category,
    double Latitude,
    double Longitude
);