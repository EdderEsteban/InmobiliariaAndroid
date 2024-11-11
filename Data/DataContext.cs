using Inmobiliaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<ApiInmuebles> Inmueble { get; set; }
        public DbSet<Propietarios> Propietario { get; set; }
        public DbSet<Inquilinos> Inquilino { get; set; }
        public DbSet<ApiContrato> Contrato { get; set; }
        public DbSet<InmuebleTipo> Tipo_inmueble { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<FotosInmueble> Fotos_inmueble { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ApiContrato>()
                .HasOne(c => c.Inquilino)
                .WithMany()
                .HasForeignKey(c => c.Id_inquilino);
            
            modelBuilder
                .Entity<Pago>()
                .HasOne(p => p.Inquilino)
                .WithMany()
                .HasForeignKey(p => p.Id_Inquilino);

            modelBuilder
                .Entity<Pago>()
                .HasOne(p => p.Contrato)
                .WithMany()
                .HasForeignKey(p => p.Id_Contrato);
        }
    }
}
