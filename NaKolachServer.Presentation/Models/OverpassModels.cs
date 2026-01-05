using System.Text.Json.Serialization;

namespace NaKolachServer.Presentation.Models;

public record OverpassResponse(List<OverpassElement> Elements);

public class OverpassElement
{
	public long Id { get; set; }
	public string? Type { get; set; }

	public Dictionary<string, string>? Tags { get; set; }

	public string? Name => GetTagValue("name") ?? "Bez nazwy";

	public string? OpeningHours => GetTagValue("opening_hours");

	public double Lat { get; set; }
	public double Lon { get; set; }

	public CenterInfo? Center { get; set; }
	public double Latitude => Lat != 0 ? Lat : (Center?.Lat ?? 0);
	public double Longitude => Lon != 0 ? Lon : (Center?.Lon ?? 0);
	private string? GetTagValue(string key)
	{
		if (Tags != null && Tags.TryGetValue(key, out var value))
		{
			return value;
		}
		return null;
	}
}

public class CenterInfo
{
	public double Lat { get; set; }
	public double Lon { get; set; }
}