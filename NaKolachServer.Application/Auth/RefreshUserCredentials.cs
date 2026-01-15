using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class RefreshUserCredential(
    IJwtTokenProvider authCredentialProvider,
    IAuthRepository authRepository,
    IUsersRepository usersRepository
)
{
    public async Task Execute(Guid userId, string refreshToken, CancellationToken cancellationToken)
    {
        var token = await authRepository.GetRefreshToken(userId, refreshToken, cancellationToken)
            ?? throw new UnauthorizedException();

        if (token.IsRevoked || token.ExpiresAt <= DateTimeOffset.UtcNow)
            throw new RefreshTokenActionNotAllowedException("Provided token is revoked or expired.");


    }
}