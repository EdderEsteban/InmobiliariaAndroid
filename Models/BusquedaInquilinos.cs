using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;


public class BusquedaInquilinos
{
    [Key]
    public int Id_Inquilino{ get; set; }

    [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo {1} caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(50, ErrorMessage = "El campo Apellido debe tener como máximo {1} caracteres.")]
    public string? Apellido { get; set; }

    [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe contener exactamente 7 u 8 dígitos.")]
    public string? Dni { get; set; }

    public List<Inquilinos> Resultados { get; set; }

    public BusquedaInquilinos()
        {
            Resultados = new List<Inquilinos>();
        }

}