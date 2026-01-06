namespace NaKolachServer.Domain.Users;

public interface IUsersRepository
{
    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);

    public Task InsertUser(User user, CancellationToken cancellationToken);
}