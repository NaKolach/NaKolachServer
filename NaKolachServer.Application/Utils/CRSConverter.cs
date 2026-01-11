using Proj4Net.Core;

namespace NaKolachServer.Application.Utils;

public static class CRSConverter
{
    private static readonly CoordinateReferenceSystemFactory _crsFactory = new();
    private static readonly CoordinateTransformFactory _ctFactory = new();

    private static readonly CoordinateReferenceSystem _crs4326 = _crsFactory.CreateFromName("EPSG:4326");
    private static readonly CoordinateReferenceSystem _crs3857 = _crsFactory.CreateFromName("EPSG:3857");

    public static ProjCoordinate CRS4326to3857(double longitude, double latitude)
    {
        var sourcePoint = new ProjCoordinate(longitude, latitude);
        var resultPoint = new ProjCoordinate();

        var transform = _ctFactory.CreateTransform(_crs4326, _crs3857);
        transform.Transform(sourcePoint, resultPoint);

        return resultPoint;
    }

    public static ProjCoordinate CRS3857to4326(double longitude, double latitude)
    {
        var sourcePoint = new ProjCoordinate(latitude, longitude);
        var resultPoint = new ProjCoordinate();

        var transform = _ctFactory.CreateTransform(_crs3857, _crs4326);
        transform.Transform(sourcePoint, resultPoint);

        return resultPoint;
    }
}