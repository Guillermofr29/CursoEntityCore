using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; } // Clave primaria

        public string Nombre { get; set; } // Nombre de la categoría

        public DateTime FechaCreacion { get; set; } // Fecha de creación

        public bool Activo { get; set; } = true; // Valor por defecto

        // Relación 1:N (Una categoría tiene muchos artículos)
        public List<Articulo> Articulo { get; set; } = new List<Articulo>();
    }
}
