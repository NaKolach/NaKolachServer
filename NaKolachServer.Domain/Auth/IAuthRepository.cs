namespace NaKolachServer.Domain.Auth;

public interface IAuthRepository
{
    public Task<RefreshToken?> GetRefreshToken(Guid userId, string refreshToken, CancellationToken cancellationToken);
    public Task InsertRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken);
    public Task RevokeRefreshTokenById(Guid id, CancellationToken cancellationToken);
}