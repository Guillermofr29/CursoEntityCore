using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class ArticuloEtiqueta
    {
        public int ArticuloId { get; set; } // Clave foránea
        public Articulo Articulo { get; set; }

        public int EtiquetaId { get; set; } // Clave foránea
        public Etiqueta Etiqueta { get; set; }
    }
}
