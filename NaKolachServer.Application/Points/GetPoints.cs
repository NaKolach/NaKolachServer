using NaKolachServer.Domain.Points;

using Proj4Net.Core;

namespace NaKolachServer.Application.Points;

public class GetPoints(IPointsRepository pointsRepository)
{
    public async Task<MapPoint[]> Execute(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        CoordinateReferenceSystemFactory crsFactory = new();
        CoordinateTransformFactory ctFactory = new();

        var sourceCRS = crsFactory.CreateFromName("EPSG:4326");
        var targetCRS = crsFactory.CreateFromName("EPSG:3857");

        var transform = ctFactory.CreateTransform(sourceCRS, targetCRS);

        var src = new ProjCoordinate(searchParams.Longitude, searchParams.Latitude);
        var dst = new ProjCoordinate();

        transform.Transform(src, dst);

        searchParams = searchParams with { Latitude = dst.X, Longitude = dst.Y };

        var points = await pointsRepository.GetPoints(searchParams, cancellationToken);

        transform = ctFactory.CreateTransform(targetCRS, sourceCRS);

        var returnPoints = new List<MapPoint>();
        foreach (var point in points)
        {
            src = new ProjCoordinate(point.Latitude, point.Longitude);
            dst = new ProjCoordinate();

            transform.Transform(src, dst);

            returnPoints.Add(point with { Latitude = dst.Y, Longitude = dst.X });
        }

        return [.. returnPoints];
    }
}