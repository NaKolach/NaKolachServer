using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Users;
namespace NaKolachServer.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(GetUserById getUserById) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await getUserById.Execute(id, cancellationToken));
    }
}
