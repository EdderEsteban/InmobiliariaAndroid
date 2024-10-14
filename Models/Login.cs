using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;

public class Login
{
    [Required, EmailAddress(ErrorMessage = "Email requerido")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email invalido")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password, ErrorMessage = "Contraseña requerida")]
    public string Password { get; set; }
}