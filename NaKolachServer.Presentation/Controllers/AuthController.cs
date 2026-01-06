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

	public AuthController(IRegisterService registerService)
	{
		_registerService = registerService;
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
}