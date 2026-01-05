using NaKolachServer.Presentation.Models;
using System.Globalization;
using System.Text.Json;

namespace NaKolachServer.Presentation.Services;

public class OverpassService : IOverpassService
{
	private readonly HttpClient _httpClient;

	public OverpassService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<OverpassElement>> GetMapElementsAsync(IEnumerable<string?> placeTypes, double lat,
	double lon, int radius)
	{
		if (placeTypes == null)
		{
			return new List<OverpassElement>();
		}

		var cleanTypes = placeTypes.
		Where(t => !string.IsNullOrWhiteSpace(t)).
		Select(t => t!.Trim().ToLower()).
		ToList();

		if (!cleanTypes.Any())
		{
			return new List<OverpassElement>();
		}

		string? filter = string.Join("|", cleanTypes);

		string latStr = lat.ToString(CultureInfo.InvariantCulture);
		string lonStr = lon.ToString(CultureInfo.InvariantCulture);

		string? query = $"""
            [out:json][timeout:25];
            (
              nwr["amenity"~"{filter}"](around:{radius},{latStr},{lonStr});
            );
            out center;
            """;

		var url = $"https://overpass-api.de/api/interpreter?data={Uri.EscapeDataString(query)}";

		try
		{
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();

			var jsonString = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var result = JsonSerializer.Deserialize<OverpassResponse>(jsonString, options);

			return result?.Elements ?? new List<OverpassElement>();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Błąd pobierania danych: {ex.Message}");

			return new List<OverpassElement>();
		}
	}
}