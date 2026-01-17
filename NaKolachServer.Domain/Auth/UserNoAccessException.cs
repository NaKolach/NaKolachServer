namespace NaKolachServer.Domain.Auth;

public class UserNoAccessException(string message) : Exception(message);