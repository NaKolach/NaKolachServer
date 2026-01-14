using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class VerifyUserCredentials(IAuthCredentialProvider authCredentialProvider, IUserPasswordHasher userPasswordHasher, IUsersRepository usersRepository)
{
    public async Task<string> Execute(string login, string password, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByLogin(login, cancellationToken)
            ?? throw new UnauthorizedException();

        var isPasswordValid = userPasswordHasher.VerifyPassword(user, password, user.PasswordHash);
        if (!isPasswordValid) throw new UnauthorizedException();

        return authCredentialProvider.NewCredential(user.Id.ToString(), login);
    }
}