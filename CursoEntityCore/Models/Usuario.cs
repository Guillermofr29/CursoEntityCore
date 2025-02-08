using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingresa un email válido")]

        public string Email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Direccion { get; set; }

        [Range(18,99, ErrorMessage = "La edad debe estar entre 18 y 99 años")]
        public int Edad { get; set; }

        [ForeignKey("DetalleUsuario")]
        public int? DetalleUsuarioId { get; set; }
        public DetalleUsuario DetalleUsuario { get; set; }
    }
}
