using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inmobiliaria.Models;

public class ApiContrato
{
    [Key]
    public int Id_contrato { get; set; } 
[JsonIgnore]
    public Inquilinos? Inquilino { get; set; }
    [JsonIgnore]
    public ApiInmuebles? Inmueble { get; set; }
[JsonIgnore]
    [Required(ErrorMessage = "El inquilino es obligatorio.")]
    public int Id_inquilino { get; set; }  
[JsonIgnore]
    [Required(ErrorMessage = "El inmueble es obligatorio.")]
    [ForeignKey("Inmueble")]
    public int Id_inmueble { get; set; }  

    public int Monto { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    public DateTime Fecha_inicio { get; set; }

    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    public DateTime Fecha_fin { get; set; }

    public Boolean Vigencia { get; set; }
    
[JsonIgnore]
    public DateTime Fecha { get; set; } = DateTime.Now;
}
