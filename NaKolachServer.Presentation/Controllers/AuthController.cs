using Microsoft.AspNetCore.Mvc;
using NaKolachServer.Presentation.Models;
using NaKolachServer.Presentation.Services;
using NaKolachServer.Presentation.Controllers;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
	private readonly IRegisterService _registerService;
	private readonly ILoginService _loginService;

	public AuthController(IRegisterService registerService, ILoginService loginService)
	{
		_registerService = registerService;
		_loginService = loginService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegistrationModel model)
	{
		try
		{
			await _registerService.RegisterServiceAsync(model);
			return Ok(new { message = "Sukces!" });
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Błąd: {ex.Message}");
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		string? result = await _loginService.LoginServiceAsync(model);

		if (result == null)
		{
			return Unauthorized(new { message = "Błędny email lub hasło!" });
		}

		return Ok(new
		{
			token = result,
			message = "Zalogowano pomyślnie!"
		});
	}
}