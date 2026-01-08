using NaKolachServer.Presentation.Models;
using NaKolachServer.Infrastructure;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Presentation.Services;

public class LoginService : ILoginService
{
	private readonly PasswordService _passwordService;
	private readonly IUsersRepository _usersRepository;

	public LoginService(IUsersRepository usersRepository, PasswordService passwordService)
	{
		_passwordService = passwordService;
		_usersRepository = usersRepository;
	}

	public async Task<string?> LoginServiceAsync(LoginModel loginData)
	{
		var userData = await _usersRepository.GetUserByEmail(loginData.Email, CancellationToken.None);

		if (userData == null) return null;

		bool isPasswordOk = _passwordService.VerifyPassword(loginData.Password!, userData.Password!);

		if (!isPasswordOk) return null;

		return Convert.ToBase64String(userData.Id.ToByteArray());
	}
}