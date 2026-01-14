namespace NaKolachServer.Domain.Users;

public record User
{
    public required Guid Id { get; init; }
    public required string Login { get; init; }
    public required string Email { get; init; }
    public string? PasswordHash { get; set; }
}