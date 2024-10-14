using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Inmobiliaria.Models;

public class Usuario
{
    [Key]
    public int Id_usuario { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string? Apellido { get; set; }

    public string? Avatar { get; set; } = "";

    [NotMapped]
    public IFormFile? AvatarFile { get; set; }

    [Required, EmailAddress(ErrorMessage = "El email es obligatorio")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public Roles? Rol { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public bool Borrado { get; set; }

    public enum Roles
    {
        Administrador = 1,
        Empleado = 2
    }
}
