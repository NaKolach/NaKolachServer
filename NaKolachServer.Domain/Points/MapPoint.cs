namespace NaKolachServer.Domain.Points;

public record MapPoint(long Id, string? Name, string? Amenity, double Latitude, double Longitude);