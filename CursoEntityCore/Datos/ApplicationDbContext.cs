using CursoEntityCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones):base(opciones)
        {
        }

        //Escribir modelos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Etiqueta> Etiqueta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<DetalleUsuario> DetalleUsuario { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.ArticuloId, ae.EtiquetaId });

            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(ae => ae.Articulo)
                .WithMany(a => a.ArticuloEtiqueta)
                .HasForeignKey(ae => ae.ArticuloId);

            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(ae => ae.Etiqueta)
                .WithMany(a => a.ArticuloEtiqueta)
                .HasForeignKey(ae => ae.EtiquetaId);

            //Siembre de datos se hace en este método
            var categoria4 = new Categoria() { CategoriaId= 23, Nombre = "Categoria 4", FechaCreacion=new DateTime(2024, 12, 29), Activo = true};
            var categoria5 = new Categoria() { CategoriaId = 24, Nombre = "Categoria 5", FechaCreacion = new DateTime(2025, 1, 14), Activo = true };

            modelBuilder.Entity<Categoria>().HasData(new Categoria[] {categoria4, categoria5 });
            base.OnModelCreating(modelBuilder);
        }
    }
}
