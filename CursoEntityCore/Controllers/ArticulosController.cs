using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using CursoEntityCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Controllers
{
    public class ArticulosController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ArticulosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {


            //Opción 1 sin datos relacionados (solo trae el Id de la categoría)
            //List<Articulo> listaArticulos = _context.Articulo.ToList();

            //foreach (var articulo in listaArticulos)
            //{
            //    //opcion 2: carga manual se generan muchas consulats SQL no es eficiente si necesitamos cargar mas propiedades
            //    //articulo.Categoria = _context.Categoria.FirstOrDefault(c => c.CategoriaId == articulo.CategoriaId);

            //    //opcion 3: carga explicita (Explicit loading)
            //    _context.Entry(articulo).Reference(c => c.Categoria).Load();
            //}

            //Opoción 4: Carga diligente (eager loading)

            List<Articulo> listaArticulos = _context.Articulo.Include(c => c.Categoria).ToList();

            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.CategoriaId.ToString()
            });

            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Articulo.Add(articulo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            //Para que al retornar la vista por algún error, también retorne la lista de categorías
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.CategoriaId.ToString()
            });

            return View(articuloCategorias);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            //Para que al retornar la vista por algún error, también retorne la lista de categorías
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.CategoriaId.ToString()
            });

            articuloCategorias.Articulo = _context.Articulo.FirstOrDefault(c => c.ArticuloId == id);

            if (articuloCategorias == null)
            {
                return NotFound();
            }

            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ArticuloCategoriaVM articuloVM)
        {
            if (articuloVM.Articulo.ArticuloId == 0)
            {
                return View(articuloVM.Articulo);
            }
            else
            {
                _context.Articulo.Update(articuloVM.Articulo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public IActionResult Borrar(int? Id)
        {
            var articulo = _context.Articulo.FirstOrDefault(c => c.ArticuloId == Id);
            _context.Articulo.Remove(articulo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AdministrarEtiquetas(int id)
        {
            ArticuloEtiquetaVM articuloEtiquetas = new ArticuloEtiquetaVM
            {
                ListaArticuloEtiquetas = _context.ArticuloEtiqueta.Include(e => e.Etiqueta).Include(a => a.Articulo)
                .Where(a => a.ArticuloId == id),

                ArticuloEtiqueta = new ArticuloEtiqueta()
                {
                    ArticuloId = id
                },
                Articulo = _context.Articulo.FirstOrDefault(a => a.ArticuloId == id)
            };

            List<int> listaTemporalEtiquetasArticulo = articuloEtiquetas.ListaArticuloEtiquetas.Select(e => e.EtiquetaId).ToList();

            //Obtener todas las etiquetas cuyos id no estén en la listaTemporalEtiquetasArticulo
            //Crear un NOT IN usando LINQ
            var listaTemporal = _context.Etiqueta.Where(e => !listaTemporalEtiquetasArticulo.Contains(e.EtiquetaId)).ToList();

            //Crear lista de etiquetas para el dropdown
            articuloEtiquetas.ListaEtiquetas = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.Titulo,
                Value = i.EtiquetaId.ToString()
            });

            return View(articuloEtiquetas);
        }

        [HttpPost]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if (articuloEtiquetas.ArticuloEtiqueta.ArticuloId !=0 && articuloEtiquetas.ArticuloEtiqueta.EtiquetaId !=0)
            {
                _context.ArticuloEtiqueta.Add(articuloEtiquetas.ArticuloEtiqueta);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = articuloEtiquetas.ArticuloEtiqueta.ArticuloId });
        }

        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            int idArticulo = articuloEtiquetas.Articulo.ArticuloId;
            ArticuloEtiqueta articuloEtiqueta = _context.ArticuloEtiqueta.FirstOrDefault(
                u=>u.EtiquetaId == idEtiqueta && u.ArticuloId == idArticulo
                );

            _context.ArticuloEtiqueta.Remove(articuloEtiqueta);
            _context.SaveChanges();
            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = idArticulo });
        }
    }
}