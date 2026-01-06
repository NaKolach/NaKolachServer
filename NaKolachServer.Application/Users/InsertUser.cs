using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Users;

public class InsertUser(IUsersRepository usersRepository)
{
    public async Task<Guid> Execute(string name, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), name);
        Console.WriteLine(user);
        await usersRepository.InsertUser(user, cancellationToken);
        return user.Id;
    }
}