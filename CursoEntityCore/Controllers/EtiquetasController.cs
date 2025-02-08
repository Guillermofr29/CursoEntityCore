using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoEntityCore.Controllers
{
    public class EtiquetasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public EtiquetasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //Consulta Inicial con todos los datos
            List<Etiqueta> listaEtiquetas = _context.Etiqueta.ToList();

            return View(listaEtiquetas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                _context.Etiqueta.Add(etiqueta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var etiqueta = _context.Etiqueta.FirstOrDefault(e => e.EtiquetaId == id);
            return View(etiqueta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                _context.Etiqueta.Update(etiqueta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(etiqueta);
        }

        [HttpGet]
        public IActionResult Borrar(int? Id)
        {
            var etiqueta = _context.Etiqueta.FirstOrDefault(e => e.EtiquetaId == Id);
            _context.Etiqueta.Remove(etiqueta);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
