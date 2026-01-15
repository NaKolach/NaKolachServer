using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class VerifyUserCredentials(
    IJwtTokenProvider authCredentialProvider,
    IUserPasswordHasher userPasswordHasher,
    IAuthRepository authRepository,
    IUsersRepository usersRepository
)
{
    public async Task<(string, string)> Execute(string login, string password, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByLogin(login, cancellationToken)
            ?? throw new UnauthorizedException();

        var isPasswordValid = userPasswordHasher.VerifyPassword(user, password, user.PasswordHash);
        if (!isPasswordValid) throw new UnauthorizedException();

        var accessToken = authCredentialProvider.NewAccessToken(user.Id.ToString(), login);
        var refreshToken = authCredentialProvider.NewRefreshToken();

        await authRepository.InsertRefreshToken(
            new RefreshToken(
                Guid.NewGuid(),
                user.Id,
                refreshToken,
                false,
                DateTimeOffset.UtcNow.AddDays(7)
            ),
            cancellationToken
        );

        return (accessToken, refreshToken);
    }
}