using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Routes;
using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Presentation.Controllers.Dtos;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoutesController(
    GetRoutesByUserId getRoutesByUserId,
    GetRouteById getRouteById,
    CalculateRouteInRadius calculateRouteInRadius,
    CalculateCustomRoute calculateCustomRoute,
    AssignRouteToUser assignRouteToUser,
    UpdateRouteName updateRouteName,
    UnassignAssignRouteToUser unassignRouteToUser
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserRoutes(CancellationToken cancellationToken)
    {
        var data = await getRoutesByUserId.Execute(User.GetContext().Id, cancellationToken);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRouteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var data = await getRouteById.Execute(id, cancellationToken);
        return Ok(new { Paths = data });
    }

    [HttpGet("auto")]
    public async Task<IActionResult> GetRoute([FromQuery] PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await calculateRouteInRadius.Execute(User.GetContext(), searchParams, cancellationToken);
        return Ok(new { Paths = data });
    }

    [HttpGet("custom")]
    public async Task<IActionResult> GetCustomRoute([FromQuery] CustomRouteSearchParams searchParams, CancellationToken cancellationToken)
    {
        var data = await calculateCustomRoute.Execute(User.GetContext(), searchParams, cancellationToken);
        return Ok(new { Paths = data });
    }

    [HttpPost("{id}/saved")]
    public async Task<IActionResult> AssignRoute([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await assignRouteToUser.Execute(User.GetContext(), id, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/saved")]
    public async Task<IActionResult> UpdateRouteName([FromBody] RouteNameUpdateDto dto, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await updateRouteName.Execute(User.GetContext(), id, dto.Name, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}/saved")]
    public async Task<IActionResult> UnassignRoute([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await unassignRouteToUser.Execute(User.GetContext(), id, cancellationToken);
        return NoContent();
    }
}