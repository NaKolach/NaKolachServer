using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Points;
using NaKolachServer.Domain.Points;
using NaKolachServer.Presentation.Controllers.Dtos;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController(GetPoints getPoints) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPlaces([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await getPoints.Execute(searchParams, cancellationToken);
        return Ok(data);
    }
}