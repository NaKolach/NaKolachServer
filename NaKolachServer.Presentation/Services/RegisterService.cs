using NaKolachServer.Presentation.Models;
using NaKolachServer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Presentation.Services;

public class RegisterService : IRegisterService
{
	private readonly PasswordService _passwordService;
	private readonly IUsersRepository _userRepository;

	public RegisterService(PasswordService passwordService, IUsersRepository usersRepository)
	{
		_passwordService = passwordService;
		_userRepository = usersRepository;
	}

	public async Task RegisterServiceAsync(RegistrationModel registrationData)
	{
		string? hashedPassword = _passwordService.HashPassword(registrationData.Password!);

		var userToSave = new User
		{
			Id = Guid.NewGuid(),
			Login = registrationData.Login,
			Email = registrationData.Email,
			Password = hashedPassword
		};

		await _userRepository.InsertUser(userToSave, CancellationToken.None);
	}
}