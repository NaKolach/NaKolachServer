using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Points;
using NaKolachServer.Domain.Points;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointsController(GetPoints getPoints) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPoints([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await getPoints.Execute(searchParams, cancellationToken);
        return Ok(data);
    }
}