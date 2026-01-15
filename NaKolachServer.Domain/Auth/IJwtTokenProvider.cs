namespace NaKolachServer.Domain.Auth;

public interface IJwtTokenProvider
{
    public string NewAccessToken(string sub, string name);
    public string NewRefreshToken();
}