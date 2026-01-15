using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class RefreshUserCredential(
    IJwtTokenProvider authCredentialProvider,
    IAuthRepository authRepository
)
{
    public async Task<(string, string)> Execute(UserContext userContext, string refreshToken, CancellationToken cancellationToken)
    {
        var token = await authRepository.GetRefreshToken(userContext.Id, refreshToken, cancellationToken)
            ?? throw new UnauthorizedException();

        if (token.IsRevoked || token.ExpiresAt <= DateTimeOffset.UtcNow)
            throw new RefreshTokenActionNotAllowedException("Provided token is revoked or expired.");

        await authRepository.RevokeRefreshTokenById(token.Id, cancellationToken);

        var newAccessToken = authCredentialProvider.NewAccessToken(userContext.Id.ToString(), userContext.Login);
        var newRefreshToken = authCredentialProvider.NewRefreshToken();

        await authRepository.InsertRefreshToken(
            new RefreshToken(
                Guid.NewGuid(),
                userContext.Id,
                newRefreshToken,
                false,
                DateTimeOffset.UtcNow.AddDays(7)
            ),
            cancellationToken
        );

        return (newAccessToken, newRefreshToken);
    }
}