namespace NaKolachServer.Domain.Users;

public class UserActionNotAllowedException(string message) : Exception(message);