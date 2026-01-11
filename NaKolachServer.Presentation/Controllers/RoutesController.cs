using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Points;
using NaKolachServer.Application.Routes;
using NaKolachServer.Domain.Points;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutesController(CalculateRoute calculateRoute) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoute([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await calculateRoute.Execute(searchParams, cancellationToken);
        return Ok(data);
    }
}