using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using NaKolachServer.Application.Auth;
using NaKolachServer.Presentation.Controllers.Dtos;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(RegisterUser insertUser, VerifyUserCredentials verifyUserCredentials) : ControllerBase
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
        var token = await verifyUserCredentials.Execute(dto.Login, dto.Password, cancellationToken);
        return Ok(token);
    }
}