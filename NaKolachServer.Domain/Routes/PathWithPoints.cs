using NaKolachServer.Domain.Points;

namespace NaKolachServer.Domain.Routes;

public record PathWithPoints
(
    double Distance,
    long Time,
    double[][] Paths,
    Point[] Points
);