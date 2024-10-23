using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models;

public class FotosInmueble
{
    [Key]
    public int Id_foto { get; set; }

    public string? FotoUrl { get; set; }

    [NotMapped]
    public IFormFile? FotoFile { get; set; }

    // Clave foránea correcta para 'Inmueble'
    [ForeignKey("Id_inmueble")]
    public int Id_inmueble { get; set; }

    // Relación correcta con 'Inmueble'
    
    public Inmuebles? Inmueble { get; set; }
}

