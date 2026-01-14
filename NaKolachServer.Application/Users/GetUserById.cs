using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Users;

public class GetUserById(IUsersRepository usersRepository)
{
    public async Task<UserEntry?> Execute(Guid id, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserById(id, cancellationToken)
            ?? throw new UserNotFoundException($"User with id {id} not found.");

        return new UserEntry(user.Id, user.Login, user.Email);
    }
}