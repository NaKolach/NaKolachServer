using System.ComponentModel.DataAnnotations;

namespace NaKolachServer.Presentation.Models;

public record LoginModel
{
	[Required(ErrorMessage = "Login jest wymagany.")]
	public string? Login { get; set; }

	[Required(ErrorMessage = "Hasło jest wymagane.")]
	public string? Password { get; set; }
}