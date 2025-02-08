using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Articulo
    {
        public int ArticuloId { get; set; } // Clave primaria

        public string TituloArticulo { get; set; } // Título del artículo

        public string Descripcion { get; set; } // Descripción

        public double Calificacion { get; set; } // Calificación del artículo

        public DateTime Fecha { get; set; } // Fecha de publicación

        // Relación con Categoria (1:N)
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        // Relación muchos a muchos con Etiqueta a través de ArticuloEtiqueta
        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; } = new List<ArticuloEtiqueta>();
    }
}
