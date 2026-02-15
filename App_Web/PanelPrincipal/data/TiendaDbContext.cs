using Microsoft.EntityFrameworkCore;
using PanelPrincipal.Models;

namespace PanelPrincipal.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Venta>().ToTable("ventas");
            modelBuilder.Entity<VentaDetalle>().ToTable("venta_detalle");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.Precio_Unitario)
                .HasPrecision(10, 2);
        }
    }
}