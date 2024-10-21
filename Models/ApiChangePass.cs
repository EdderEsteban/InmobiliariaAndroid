using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class ApiChangePass
    {
        [Required]
        public string OldPassword { get; set; }
       [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string NewPassword { get; set; }
    }
}