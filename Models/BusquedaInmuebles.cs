using System.Collections.Generic;

namespace Inmobiliaria.Models
{
    public class BusquedaInmuebles
    { 
        
        public string? Direccion { get; set; }

        
        public List<Inmuebles> Resultados { get; set; }

        public BusquedaInmuebles()
        {
            Resultados = new List<Inmuebles>(); 
        }
    }
}
