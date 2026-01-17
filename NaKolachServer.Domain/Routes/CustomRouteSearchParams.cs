namespace NaKolachServer.Domain.Routes;

public record CustomRouteSearchParams(
    string[] Categories,
    double Latitude,
    double Longitude,
    int Radius
);