using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Users;

namespace NaKolachServer.Infrastructure.Database.Business;

public class EFUsersRepository(DatabaseContext databaseContext) : IUsersRepository
{

    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return await databaseContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task InsertUser(User user, CancellationToken cancellationToken)
    {
        await databaseContext.Users.AddAsync(user, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}