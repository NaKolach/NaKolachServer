namespace NaKolachServer.Domain.Users;

public class User
{
	public Guid Id { get; init; }
	public string? Login { get; init; }
	public string? Email { get; set; }
	public string? Password { get; set; }
}