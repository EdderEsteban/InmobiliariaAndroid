using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;


public class CambioPass
{
    [Key]
    public int Id_usuario { get; set; }

    
    public string? Password { get; set; }

    
    public string? ConfirmPassword { get; set; }

    public string? Email { get; set; }

    

    

}