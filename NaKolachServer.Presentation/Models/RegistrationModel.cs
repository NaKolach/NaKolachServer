using System.ComponentModel.DataAnnotations;

namespace NaKolachServer.Presentation.Models;

public record RegistrationModel
{
	[Required]
	[StringLength(10, MinimumLength = 5, ErrorMessage = "Login musi mieć od 5 do 10 znaków.")]
	[RegularExpression(@"^[A-Z][^\s]*$", ErrorMessage = "Login musi zaczynać się wielką literą i bez spacji.")]
	public string? Login { get; init; }

	[Required]
	[StringLength(20, MinimumLength = 8, ErrorMessage = "Hasło musi mieć od 8 do 20 znaków.")]
	[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$", ErrorMessage = "Hasło musi mieć dużą literę, cyfrę i znak specjalny.")]
	public string? Password { get; init; }

	[Required]
	[EmailAddress]
	public string? Email { get; init; }
}