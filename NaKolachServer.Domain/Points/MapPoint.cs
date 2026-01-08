namespace NaKolachServer.Domain.Points;

public record MapPoint(long Id, string? Name, string? Category, double Latitude, double Longitude);