namespace NaKolachServer.Domain.Routes;

public record RouteData(
    Guid Id,
    string Name,
    double Distance,
    long Time,
    string[] Categories
);