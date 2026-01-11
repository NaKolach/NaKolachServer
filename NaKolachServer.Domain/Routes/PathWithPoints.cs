namespace NaKolachServer.Domain.Routes;

public record PathWithPoints
(
    double Distance,
    long Time,
    double[][] Paths,
    Points.Point[] Points
);