using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CursoEntityCore.Controllers
{
    public class EtiquetasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public EtiquetasController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        public IActionResult Index()
        {
            List<Etiqueta> listaEtiqueta = _context.Etiqueta.ToList();

            return View(listaEtiqueta);
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
            if (ModelState.IsValid) {
            _context.Etiqueta.Add(etiqueta);
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
            var etiqueta = _context.Etiqueta.FirstOrDefault(e => e.Etiqueta_Id == id);
            return View(etiqueta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Etiqueta etiqueta)
        {
           if(ModelState.IsValid)
            {
                _context.Etiqueta.Update(etiqueta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
           return View(etiqueta);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var etiqueta = _context.Etiqueta.FirstOrDefault(e => e.Etiqueta_Id == id);
            _context.Etiqueta.Remove(etiqueta);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
