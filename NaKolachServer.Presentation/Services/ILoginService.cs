using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public interface ILoginService
{
	Task<string?> LoginService(LoginModel loginData);
}