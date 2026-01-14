namespace NaKolachServer.Domain.Auth;

public interface IAuthCredentialProvider
{
    public string NewCredential(string sub, string name);
}