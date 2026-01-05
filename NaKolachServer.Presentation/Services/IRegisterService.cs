using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public interface IRegisterService
{
	Task RegisterService(RegistrationModel registrationData);
}