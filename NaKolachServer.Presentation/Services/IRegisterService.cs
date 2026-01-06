using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public interface IRegisterService
{
	Task RegisterServiceAsync(RegistrationModel registrationData);
}