using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using CursoEntityCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<Usuario> listaUsuarios = _context.Usuario.ToList();

            return View(listaUsuarios);
        }

        [HttpGet]
        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var usuario = _context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Borrar(int? Id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.UsuarioId == Id);
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detalle(int? Id)
        {
            if (Id == null) 
            {
                return View();
            }

            var usuario = _context.Usuario.Include(d => d.DetalleUsuario).FirstOrDefault(u=>u.UsuarioId==Id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult AgregarDetalle(Usuario usuario)
        {
            if (usuario.DetalleUsuario.DetalleUsuarioId == 0)
            {
                //Creamos los detalles para ese usuario
                _context.DetalleUsuario.Add(usuario.DetalleUsuario);
                _context.SaveChanges();

                //Despues de crear el detalle del usuario obtenemos el usuario de la BD y le actualizamos el campo DetalleUsuarioId
                var usuarioBd = _context.Usuario.FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);
                usuarioBd.DetalleUsuarioId = usuario.DetalleUsuario.DetalleUsuarioId;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));

        }



    }
}
