using NaKolachServer.Models;
using System.Text.Json;

class OverpassService : IOverpassService
{
	private readonly HttpClient _httpClient;

	public OverpassService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<OverpassElement>> GetMapElementsAsync()
	{
		string query = """
            [out:json][timeout:25];
            (
              node["amenity"~"pub|bar|cafe"](54.34,18.63,54.36,18.66);
            );
            out body;
            >;
            out skel qt;
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
			Console.WriteLine($"❌ Błąd pobierania danych: {ex.Message}");

			return new List<OverpassElement>();
		}
	}
}