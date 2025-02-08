using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CursoEntityCore.ViewModel
{
    public class ArticuloCategoriaVM
    {
        public Articulo Articulo { get; set; } 
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
