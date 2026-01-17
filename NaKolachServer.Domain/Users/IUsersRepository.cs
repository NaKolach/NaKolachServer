namespace NaKolachServer.Domain.Users;

public interface IUsersRepository
{
    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);
    public Task<User?> GetUserByLogin(string login, CancellationToken cancellationToken);
    Task<bool> CheckIfUserExistsByLoginAndEmail(string login, string email, CancellationToken cancellationToken);
    public Task InsertUser(User user, CancellationToken cancellationToken);
}