using NaKolachServer.Domain.Points;

namespace NaKolachServer.Domain.Routes;

public record RouteFullDetails
(
    PathWithPoints[] Paths
);