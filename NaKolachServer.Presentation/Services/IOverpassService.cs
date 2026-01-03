using NaKolachServer.Models;

public interface IOverpassService
{
	Task<List<OverpassElement>> GetMapElementsAsync();
}