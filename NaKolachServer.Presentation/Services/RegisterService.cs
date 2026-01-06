using NaKolachServer.Presentation.Models;
using Microsoft.AspNetCore.Identity;

namespace NaKolachServer.Presentation.Services;

public class RegisterService : IRegisterService
{
	private readonly PasswordService _passwordService;
	private readonly FileService _fileService;

	public RegisterService(PasswordService passwordService, FileService fileService)
	{
		_passwordService = passwordService;
		_fileService = fileService;
	}

	public async Task RegisterServiceAsync(RegistrationModel registrationData)
	{
		string? hashedPassword = _passwordService.HashPassword(registrationData.Password);

		var userToSafe = new RegistrationModel
		{
			Login = registrationData.Login,
			Password = hashedPassword,
			Email = registrationData.Email
		};

		await _fileService.SaveToFileAsync(userToSafe);
	}
}