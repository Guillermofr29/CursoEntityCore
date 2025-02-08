using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Controllers
{
    public class CategoriasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Consulta Inicial con todos los datos
            List<Categoria> listaCategorias = _context.Categoria.ToList();

            //Consulta filtrando la fecha
            //DateTime fechaComparacion = new DateTime(2025, 1, 26);
            //List<Categoria> listaCategorias = _context.Categoria.Where(f=>f.FechaCreacion >= fechaComparacion).OrderBy(f=>f.FechaCreacion).ToList();
            //return View(listaCategorias);

            //Seleccionar Columnas específicas
            //var categorias = _context.Categoria.Where(n=>n.Nombre == "Test 5").Select(n=>n).ToList();
           
            //Agrupando registros
            //var listaCategorias = _context.Categoria
            //    .GroupBy(c => new { c.Activo })
            //    .Select(c => new { c.Key, Count = c.Count() }).ToList(); 
            
            //Paginando registros
            //List<Categoria> listaCategorias = _context.Categoria.Skip(3).Take(2).ToList();
            
            //Consulats convencionales
            //List<Categoria> listaCategorias = _context.Categoria.FromSqlRaw("select * from categoria where nombre like 'categoria%'").ToList();

            //Interpolacion de string
            //var id = 31;
            //var categoria = _context.Categoria.FromSqlRaw($"select * from categoria where CategoriaId = {id}").ToList();

            return View(listaCategorias);   
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CrearMultipleOpcion2()
        {
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 2; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_context.Categoria.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _context.Categoria.AddRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CrearMultipleOpcion5()
        {
            for (int i = 0; i < 5; i++)
            {
                _context.Categoria.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult VistaCrearMultipleOpcionFormulario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearMultipleOpcionFormulario()
        {
            string categoriasForm = Request.Form["Nombre"];
            var listaCategoria = from val in categoriasForm.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) select (val);

            List<Categoria> categorias = new List<Categoria>();

            foreach (var c in listaCategoria)
            {
                categorias.Add(new Categoria
                {
                    Nombre = c
                });
            }
            _context.Categoria.AddRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var categoria = _context.Categoria.FirstOrDefault(c => c.CategoriaId == id);
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [HttpGet]
        public IActionResult Borrar(int? Id)
        {
            var categoria = _context.Categoria.FirstOrDefault(c=>c.CategoriaId == Id);
            _context.Categoria.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple2()
        {
            IEnumerable<Categoria> categorias = _context.Categoria.OrderByDescending(c => c.CategoriaId).Take(2);
            _context.Categoria.RemoveRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple5()
        {
            IEnumerable<Categoria> categorias = _context.Categoria.OrderByDescending(c => c.CategoriaId).Take(5);
            _context.Categoria.RemoveRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
