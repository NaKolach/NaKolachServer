using System.Net.Http.Headers;

using NaKolachServer.Domain.Routes;

using Newtonsoft.Json;

namespace NaKolachServer.Infrastructure.RouteProviders;

public class GraphhopperRouteProvider(HttpClient httpClient) : IRouteProvider
{
    public async Task<Domain.Routes.Path> CalculateRoute(Coordinates[] coordinates, CancellationToken cancellationToken)
    {
        var body = new
        {
            profile = "bike",
            points = coordinates.Select(c => new[] { c.X, c.Y }),
            points_encoded = false,
            snap_preventions = new[] { "motorway", "ferry", "tunnel" },
            details = new[] { "road_class", "surface" }
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("http://graphhopper:8989/route"),
            Content = new StringContent(JsonConvert.SerializeObject(body))
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        using var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var route = JsonConvert.DeserializeObject<GraphhopperRouteResponse>(responseContent)
            ?? throw new Exception("Cannot deserialize graphhopper route response.");

        return MapToPath(route.Paths.First()); // todo possible error if no paths found
    }

    private static Domain.Routes.Path MapToPath(GraphhopperPath path)
    {
        return new Domain.Routes.Path(
            path.Distance,
            path.Time,
            [.. path.Points.Coordinates.Select(cords => new[] { cords[1], cords[0] })]
        );
    }
}