using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Etiqueta
    {
        public int EtiquetaId { get; set; } // Clave primaria

        public string Titulo { get; set; } // Título de la etiqueta

        public DateTime Fecha { get; set; } // Fecha de creación

        // Relación muchos a muchos con Articulo a través de ArticuloEtiqueta
        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; } = new List<ArticuloEtiqueta>();
    }
}
