using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Routes;
using NaKolachServer.Domain.Points;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoutesController(
    GetRouteById getRouteById,
    CalculateRouteInRadius calculateRoute,
    AssignRouteToUser assignRouteToUser,
    UnassignAssignRouteToUser unassignRouteToUser
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRouteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var data = await getRouteById.Execute(id, cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoute([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await calculateRoute.Execute(User.GetContext(), searchParams, cancellationToken);
        return Ok(data);
    }

    [HttpPost("{id}/saved")]
    public async Task<IActionResult> AssignRoute([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await assignRouteToUser.Execute(User.GetContext(), id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}/saved")]
    public async Task<IActionResult> UnassignRoute([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await unassignRouteToUser.Execute(User.GetContext(), id, cancellationToken);
        return NoContent();
    }
}