using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Auth;
using NaKolachServer.Presentation.Controllers.Dtos;
using NaKolachServer.Presentation.Utils;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(
    RegisterUser insertUser,
    VerifyUserCredentials verifyUserCredentials,
    RefreshUserCredential refreshUserCredential,
    RevokeUserCredentials revokeUserCredentials
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        await insertUser.Execute(dto.Login, dto.Email, dto.Password, cancellationToken);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto, CancellationToken cancellationToken)
    {
        var tokenPair = await verifyUserCredentials.Execute(dto.Login, dto.Password, cancellationToken);
        var now = DateTime.UtcNow;

        Response.Cookies.Append("accessToken", tokenPair.Item1,
        new CookieOptions
        {
            HttpOnly = true,
            Secure = false, //to change when https
            SameSite = SameSiteMode.Strict,
            Expires = now.AddMinutes(5)
        });

        Response.Cookies.Append("refreshToken", tokenPair.Item2,
        new CookieOptions
        {
            HttpOnly = true,
            Secure = false, //to change when https
            SameSite = SameSiteMode.Strict,
            Expires = now.AddDays(7)
        });

        return NoContent();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userContext = User.GetContext();
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return BadRequest("Refresh Token is missing.");
        }

        await revokeUserCredentials.Execute(userContext, refreshToken, cancellationToken);

        Response.Cookies.Delete("accessToken");
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessToken(CancellationToken cancellationToken)
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return BadRequest("Refresh Token is missing.");
        }

        var tokenPair = await refreshUserCredential.Execute(User.GetContext(), refreshToken, cancellationToken);
        return Ok(new { AccessToken = tokenPair.Item1, RefreshToken = tokenPair.Item2 });
    }
}