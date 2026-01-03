namespace NaKolachServer.Models;

public record OverpassResponse(List<OverpassElement> Elements);

public record OverpassElement
{
	public long Id { get; init; }
	public string Type { get; init; }
	public double Lat { get; init; }
	public double Lon { get; init; }
	public Dictionary<string, string>? Tags;
}