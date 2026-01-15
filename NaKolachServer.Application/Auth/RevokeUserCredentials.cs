using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Auth;

public class RevokeUserCredentials(IAuthRepository authRepository)
{
    public async Task Execute(UserContext userContext, string refreshToken, CancellationToken cancellationToken)
    {
        var token = await authRepository.GetRefreshToken(userContext.Id, refreshToken, cancellationToken)
            ?? throw new Exception("Valid token is missing");

        await authRepository.RevokeRefreshTokenById(token.Id, cancellationToken);
    }
}