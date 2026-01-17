using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Routes;
using NaKolachServer.Domain.Points;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoutesController(CalculateRouteInRadius calculateRoute) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoute([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await calculateRoute.Execute(User.GetContext(), searchParams, cancellationToken);
        return Ok(data);
    }
}