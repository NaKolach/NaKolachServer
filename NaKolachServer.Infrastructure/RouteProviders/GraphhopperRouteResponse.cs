namespace NaKolachServer.Infrastructure.RouteProviders;

public record GraphhopperRouteResponse(
    GraphhopperPath[] Paths
);

public record GraphhopperPath(
    double Distance,
    long Time,
    GraphhopperPoint Points
);

public record GraphhopperPoint(
    double[][] Coordinates
);