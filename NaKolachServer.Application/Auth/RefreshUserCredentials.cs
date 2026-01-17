using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class RefreshUserCredential(
    IUsersRepository usersRepository,
    IJwtTokenProvider authCredentialProvider,
    IAuthRepository authRepository
)
{
    public async Task<(string, string)> Execute(Guid userId, string refreshToken, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserById(userId, cancellationToken)
            ?? throw new UserNotFoundException($"User with Id {userId} not found");

        var token = await authRepository.GetRefreshToken(userId, refreshToken, cancellationToken)
            ?? throw new RefreshTokenActionNotAllowedException("Refresh token is invalid.");

        if (token.IsRevoked || token.ExpiresAt <= DateTimeOffset.UtcNow)
            throw new RefreshTokenActionNotAllowedException("Provided token is revoked or expired.");

        await authRepository.RevokeRefreshTokenById(token.Id, cancellationToken);

        var newAccessToken = authCredentialProvider.NewAccessToken(userId.ToString(), user.Login);
        var newRefreshToken = authCredentialProvider.NewRefreshToken();

        await authRepository.InsertRefreshToken(
            new RefreshToken(
                Guid.NewGuid(),
                userId,
                newRefreshToken,
                false,
                DateTimeOffset.UtcNow.AddDays(7)
            ),
            cancellationToken
        );

        return (newAccessToken, newRefreshToken);
    }
}