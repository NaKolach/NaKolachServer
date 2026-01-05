using NaKolachServer.Presentation.Models;

public interface IOverpassService
{
	Task<List<OverpassElement>> GetMapElementsAsync(IEnumerable<string?> placeTypes, double lat,
	double lon, int radius);
}