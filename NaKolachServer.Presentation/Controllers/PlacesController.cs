using Microsoft.AspNetCore.Mvc;
using NaKolachServer.Presentation.Services;
using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
	private readonly IOverpassService _overpassService;

	public PlacesController(IOverpassService overpassService)
	{
		_overpassService = overpassService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPlaces([FromQuery] List<string?> types, [FromQuery] double lat,
	[FromQuery] double lon, [FromQuery] int radius)
	{
		var data = await _overpassService.GetMapElementsAsync(types, lat, lon, radius);
		return Ok(data);
	}
}