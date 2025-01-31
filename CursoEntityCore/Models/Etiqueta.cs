using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Etiqueta
    {
        [Key]
        public int EtiquetaId { get; set; }
        
        [Required(ErrorMessage = "El titulo es obligtorio")]
        [StringLength(80, ErrorMessage = "El titulo no puede superar los 80 caracteres")]
        public string Titulo { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; } = new List<ArticuloEtiqueta>();
    }
}
