using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;


public class Propietarios
{
    [Key]
    public int Id_Propietario { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo {1} caracteres.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Apellido debe tener como máximo {1} caracteres.")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "El campo Dni es obligatorio.")]
    [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe contener exactamente 7 u 8 dígitos.")]
    public string? Dni { get; set; }

    [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
    [StringLength(100, ErrorMessage = "El campo Dirección debe tener como máximo {1} caracteres.")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
    [StringLength(20, ErrorMessage = "El campo Teléfono debe tener como máximo {1} caracteres.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El campo Correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo Correo no tiene un formato de dirección de correo electrónico válido.")]
    public string? Correo { get; set; }

    public String? Contraseña {get;set;}
    
    public String? Avatar{get; set;}

    public int? Id_usuario { get; set; }

    public DateTime? Fecha { get; set; } = DateTime.Now;

    public Propietarios()
    {

    }

}