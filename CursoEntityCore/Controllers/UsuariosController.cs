using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CursoEntityCore.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        [HttpGet]
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
            if (ModelState.IsValid) {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id== null)
            {
                return View();
            }
            var usuario = _context.Usuario.FirstOrDefault( c => c.Id == id);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
           if(ModelState.IsValid)
            {
                _context.Usuario.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
           return View(usuario);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if ( id== null)
            {
                return View();
            }

            var usuario = _context.Usuario.Include( u => u.DetalleUsuario).FirstOrDefault(u => u.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult AgregarDetalle(Usuario usuario)
        {
            if (usuario.DetalleUsuario.DetalleUsuario_ID == 0)
            {
                //Crear detalle de usuario
                _context.DetalleUsuario.Add(usuario.DetalleUsuario);
                _context.SaveChanges();

                //Despues de crear Detalle, obtengo IdDetalle para actualizar usuario
                var usuarioDB = _context.Usuario.FirstOrDefault(u => u.Id == usuario.Id);
                if (usuarioDB != null)
                {
                    usuarioDB.DetalleUsuario_ID = usuario.DetalleUsuario.DetalleUsuario_ID;
                    //_context.Usuario.Update(usuarioDB);
                    _context.SaveChanges();
                }
            return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }
    }
}
