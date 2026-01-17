using System.ComponentModel.DataAnnotations;

namespace NaKolachServer.Presentation.Controllers.Dtos;

public record RegisterUserDto
(
    [Required]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "Login musi mieć od 5 do 10 znaków.")]
    string Login,

    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Hasło musi mieć od 8 do 20 znaków.")]
    // [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$", ErrorMessage = "Hasło musi mieć dużą literę, cyfrę i znak specjalny.")]
    string Password,

    [Required]
    [EmailAddress]
    string Email
);