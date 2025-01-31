using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class ArticuloEtiqueta
    {
        [Key]
        public int ArticuloEtiquetaId { get; set; }

        [ForeignKey("Articulo")]
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        [ForeignKey("Etiqueta")]
        public int EtiquetaId { get; set; }
        public Etiqueta Etiqueta { get; set; }

    }
}
