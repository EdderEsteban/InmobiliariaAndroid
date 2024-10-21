using Inmobiliaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ApiInmuebles> Inmueble { get; set; }
        public DbSet<Propietarios> Propietario { get; set; }
        public DbSet<Inquilinos> Inquilino { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<InmuebleTipo> Tipo { get; set; }
        public DbSet<Pago> Pago { get; set; }


    }
}