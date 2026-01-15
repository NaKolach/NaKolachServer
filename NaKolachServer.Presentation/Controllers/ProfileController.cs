using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Users;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ProfileController(GetUserById getUserById) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userContext = User.GetContext();
        var user = await getUserById.Execute(userContext.Id, cancellationToken);
        return Ok(user);
    }
}