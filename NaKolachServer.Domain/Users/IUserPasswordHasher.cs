namespace NaKolachServer.Domain.Users;

public interface IUserPasswordHasher
{
    public string HashPassword(User user, string password);
    public bool VerifyPassword(User user, string password, string storedHash);
}