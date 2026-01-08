using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public interface ILoginService
{
	Task<LoginResponseDto?> LoginServiceAsync(LoginModel loginData);
}