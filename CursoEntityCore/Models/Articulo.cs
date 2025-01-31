using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Articulo
    {
        [Key]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        [StringLength(50, ErrorMessage = "El titulo no puede superar los 50 caracteres")]
        public string TituloArticulo { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La calificacion es obligatoria")]
        public double Calificacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set;} = new List<ArticuloEtiqueta>();
    }
}
