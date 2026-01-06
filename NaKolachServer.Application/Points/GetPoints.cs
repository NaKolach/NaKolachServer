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

        var src = new ProjCoordinate(searchParams.Longtidude, searchParams.Latidude);
        var dst = new ProjCoordinate();

        transform.Transform(src, dst);

        searchParams = searchParams with { Latidude = dst.X, Longtidude = dst.Y };

        return await pointsRepository.GetPoints(searchParams, cancellationToken);
    }
}