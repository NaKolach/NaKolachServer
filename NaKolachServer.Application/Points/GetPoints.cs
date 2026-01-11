using NaKolachServer.Application.Utils;
using NaKolachServer.Domain.Points;

namespace NaKolachServer.Application.Points;

public class GetPoints(IPointsRepository pointsRepository)
{
    public async Task<Point[]> Execute(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var startPoint = CRSConverter.CRS4326to3857(searchParams.Longitude, searchParams.Latitude);

        searchParams = searchParams with { Latitude = startPoint.X, Longitude = startPoint.Y };

        var points = await pointsRepository.GetPoints(searchParams, cancellationToken);

        for (var i = 0; i < points.Length; i++)
        {
            var pointConverted = CRSConverter.CRS3857to4326(points[i].Longitude, points[i].Latitude);
            points[i] = points[i] with { Latitude = pointConverted.Y, Longitude = pointConverted.X };
        }

        return points;
    }
}