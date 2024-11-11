using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inmobiliaria.Models;

public class FotosInmueble
{
    [Key]
    public int Id_foto { get; set; }

    public string? FotoUrl { get; set; }

    [JsonIgnore]
    [NotMapped]
    public IFormFile? FotoFile { get; set; }

    // Definir la clave foránea correctamente
    public int Id_inmueble { get; set; }

    // Relacionar correctamente con el inmueble
    [ForeignKey("Id_inmueble")]
    [JsonIgnore]
    public ApiInmuebles? Inmueble { get; set; }
}


