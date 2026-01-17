using System.Transactions;

using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class RegisterUser(IUserPasswordHasher passwordHasher, IUsersRepository usersRepository)
{
    public async Task Execute(string login, string email, string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login,
            Email = email
        };

        var userExists = await usersRepository.CheckIfUserExistsByLoginAndEmail(login, email, cancellationToken);
        if (userExists) throw new UserActionNotAllowedException("User already exists.");

        user.PasswordHash = passwordHasher.HashPassword(user, password);

        await usersRepository.InsertUser(user, cancellationToken);
    }
}