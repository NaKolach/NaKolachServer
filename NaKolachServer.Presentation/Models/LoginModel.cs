using System.ComponentModel.DataAnnotations;

namespace NaKolachServer.Presentation.Models;

public record LoginModel
{
	[Required(ErrorMessage = "Email jest wymagany.")]
	public string? Email { get; set; }

	[Required(ErrorMessage = "Hasło jest wymagane.")]
	public string? Password { get; set; }
}