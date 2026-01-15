
using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Auth;

namespace NaKolachServer.Infrastructure.Database.Business;

public class EFAuthRepository(DatabaseContext databaseContext) : IAuthRepository
{
    public async Task<RefreshToken?> GetRefreshToken(Guid userId, string refreshToken, CancellationToken cancellationToken)
    {
        return await databaseContext.UserRefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Token == refreshToken, cancellationToken);
    }

    public async Task InsertRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await databaseContext.UserRefreshTokens.AddAsync(refreshToken, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RevokeRefreshTokenById(Guid id, CancellationToken cancellationToken)
    {
        await databaseContext.UserRefreshTokens
            .Where(rt => rt.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.IsRevoked, true), cancellationToken);
    }
}