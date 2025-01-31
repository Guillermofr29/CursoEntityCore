using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class DetalleUsuario
    {
        [Key]
        public int DetalleUsuarioId { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "La Cédula no puede superar los 50 caracteres")]
        public string Cedula { get; set; }

        [Display(Name = "Ingreasa el deporte que practica el usuario")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Deporte { get; set; }

        [Display(Name = "Ingresa el tipo de mascota del usuario")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Mascota { get; set; }

        public Usuario Usuario { get; set; }
    }
}
