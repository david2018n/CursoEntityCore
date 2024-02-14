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
    }
}
