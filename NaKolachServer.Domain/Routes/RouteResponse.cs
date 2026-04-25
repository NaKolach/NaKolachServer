using NaKolachServer.Domain.Points;

namespace NaKolachServer.Domain.Routes;

public record RouteResponse
(
    Guid Id,
    Guid AuthorId,
    double Distance,
    long Time,
    string[] Categories,
    double[][] Paths,
    Point[] Points,
    DateTimeOffset CreatedAt
);