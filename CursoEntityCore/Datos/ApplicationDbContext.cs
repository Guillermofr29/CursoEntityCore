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

            //Siembra de datos se hace en este método
            //var categoria4 = new Categoria() { CategoriaId= 23, Nombre = "Categoria 4", FechaCreacion=new DateTime(2024, 12, 29), Activo = true};
            //var categoria5 = new Categoria() { CategoriaId = 24, Nombre = "Categoria 5", FechaCreacion = new DateTime(2025, 1, 14), Activo = true };

            //modelBuilder.Entity<Categoria>().HasData(new Categoria[] {categoria4, categoria5 });
            //base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Categoria
            modelBuilder.Entity<Categoria>()
                .ToTable("Categorias") // Nombre de la tabla en BD
                .HasKey(c => c.CategoriaId); // Clave primaria

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Categoria>()
                .Property(c => c.FechaCreacion)
                .HasColumnType("date"); // Especificamos que se almacene solo la fecha

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Activo)
                .HasDefaultValue(true); // Valor por defecto

            // Relación 1:N entre Categoria y Articulo
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Articulo) // Una categoría tiene muchos artículos
                .WithOne(a => a.Categoria) // Un artículo pertenece a una categoría
                .HasForeignKey(a => a.CategoriaId) // Clave foránea
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina una categoría, también sus artículos

            // Configuración de la entidad Articulo
            modelBuilder.Entity<Articulo>()
                .ToTable("Articulos")
                .HasKey(a => a.ArticuloId);

            modelBuilder.Entity<Articulo>()
                .Property(a => a.TituloArticulo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Articulo>()
                .Property(a => a.Calificacion)
                .HasColumnType("decimal(3,1)") // Guardar con 1 decimal (ej. 4.5)
                .IsRequired();

            modelBuilder.Entity<Articulo>()
                .Property(a => a.Fecha)
                .HasColumnType("date");

            // Relación N:M entre Articulo y Etiqueta con tabla intermedia ArticuloEtiqueta
            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasKey(ae => new { ae.ArticuloId, ae.EtiquetaId }); // Clave compuesta

            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(ae => ae.Articulo)
                .WithMany(a => a.ArticuloEtiqueta)
                .HasForeignKey(ae => ae.ArticuloId);

            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(ae => ae.Etiqueta)
                .WithMany(e => e.ArticuloEtiqueta)
                .HasForeignKey(ae => ae.EtiquetaId);
        }
    }
}
