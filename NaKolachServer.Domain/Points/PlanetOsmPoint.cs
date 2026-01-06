using NetTopologySuite.Geometries;

namespace NaKolachServer.Domain.Points;

public record PlanetOsmPoint(long OsmId, string? Name, string? Amenity, Geometry Way);