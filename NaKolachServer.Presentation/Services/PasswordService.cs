using Microsoft.AspNetCore.Identity;

namespace NaKolachServer.Presentation.Services;

public class User { }

public class PasswordService
{
	private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

	public string? HashPassword(string? password)
	{
		return _hasher.HashPassword(null!, password);
	}

	public bool VerifyPassword(string? password, string? storedHash)
	{
		var result = _hasher.VerifyHashedPassword(null!, storedHash, password);

		return result == PasswordVerificationResult.Success;
	}
}