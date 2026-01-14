using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Users;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfileController(GetUserById getUserById) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var user = await getUserById.Execute(User.GetUserId(), cancellationToken);
        return Ok(user);
    }
}