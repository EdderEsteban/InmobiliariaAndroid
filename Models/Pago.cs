using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;

public class Pago
{
    [Key]
    public int Id_Pago { get; set; }

    public int Id_Contrato { get; set; } 

    public Inquilinos? Inquilino { get; set; }
    public int Id_Inquilino { get; set; }
    public Inmuebles? Inmueble { get; set; }

    public int Id_Inmueble { get; set; }

    public DateTime Fecha_Pago { get; set; } = DateTime.Now;
    
    [Required]
    public decimal Monto { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Periodo { get; set; }

    public int Id_Usuario { get; set; }

   
}