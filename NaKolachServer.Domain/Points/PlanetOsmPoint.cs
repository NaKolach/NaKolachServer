using NetTopologySuite.Geometries;

namespace NaKolachServer.Domain.Points;

public record PlanetOsmPoint(
    long OsmId,
    string? Name,
    string? Amenity,
    string? Shop,
    string? Tourism,
    string? Leisure,
    Geometry Way
);