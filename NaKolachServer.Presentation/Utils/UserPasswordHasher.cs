using Microsoft.AspNetCore.Identity;

using NaKolachServer.Domain.Users;

namespace NaKolachServer.Presentation.Utils;

public class UserPasswordHasher : IUserPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password, string storedHash)
    {
        var result = _hasher.VerifyHashedPassword(user, storedHash, password);
        return result == PasswordVerificationResult.Success;
    }
}