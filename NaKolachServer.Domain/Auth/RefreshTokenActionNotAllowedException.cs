namespace NaKolachServer.Domain.Auth;

public class RefreshTokenActionNotAllowedException(string message) : Exception(message);