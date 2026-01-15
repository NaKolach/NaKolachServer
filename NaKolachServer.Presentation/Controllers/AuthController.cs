using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Auth;
using NaKolachServer.Presentation.Controllers.Dtos;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(
    RegisterUser insertUser,
    VerifyUserCredentials verifyUserCredentials,
    RefreshUserCredential refreshUserCredential
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        await insertUser.Execute(dto.Login, dto.Email, dto.Password, cancellationToken);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto, CancellationToken cancellationToken)
    {
        var tokenPair = await verifyUserCredentials.Execute(dto.Login, dto.Password, cancellationToken);
        return Ok(new { AccessToken = tokenPair.Item1, RefreshToken = tokenPair.Item2 });
    }

    // [HttpPost("refresh")]
    // public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenDto dto, CancellationToken cancellationToken)
    // {
    //     var tokenPair = await refreshUserCredential.Execute(dto.RefreshToken, cancellationToken);
    //     return Ok(new { AccessToken = tokenPair.Item1, RefreshToken = tokenPair.Item2 });
    // }
}