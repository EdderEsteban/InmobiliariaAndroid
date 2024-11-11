using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inmobiliaria.Models;

public class Pago
{
    [Key]
    public int Id_Pago { get; set; }

    public int Id_Contrato { get; set; }

    [JsonIgnore]
    public ApiContrato? Contrato { get; set; }

    [JsonIgnore]
    public Inquilinos? Inquilino { get; set; }

    [JsonIgnore]
    public int Id_Inquilino { get; set; }

    public DateTime Fecha_Pago { get; set; } = DateTime.Now;

    [Required]
    public decimal Monto { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Periodo { get; set; }

    [JsonIgnore]
    public int Id_Usuario { get; set; }
}
