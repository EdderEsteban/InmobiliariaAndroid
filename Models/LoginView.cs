using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class LoginView
    {
        [Required(ErrorMessage = "Email requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Password { get; set; }
    }
}
