using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public class LoginService : ILoginService
{
	private readonly PasswordService _passwordService;
	private readonly FileService _fileService;

	public LoginService(FileService fileService, PasswordService passwordService)
	{
		_passwordService = passwordService;
		_fileService = fileService;
	}

	public async Task<string?> LoginServiceAsync(LoginModel loginData)
	{
		var userData = await _fileService.ReadFromFileAsync();

		if (userData!.Email == loginData.Email && _passwordService.VerifyPassword(loginData.Password!, userData!.Password))
		{
			return "Tu beda klucze JWT";
		}
		return null;
	}
}