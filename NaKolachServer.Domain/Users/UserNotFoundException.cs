namespace NaKolachServer.Domain.Users;

public class UserNotFoundException(string message) : Exception(message);