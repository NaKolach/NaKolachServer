namespace NaKolachServer.Domain.Users;

public class UserExistsException(string message) : Exception(message);