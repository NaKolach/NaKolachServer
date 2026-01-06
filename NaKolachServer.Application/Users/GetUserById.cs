using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Users;

public class GetUserById(IUsersRepository usersRepository)
{
    public async Task<User?> Execute(Guid id, CancellationToken cancellationToken)
    {
        return await usersRepository.GetUserById(id, cancellationToken);
    }
}