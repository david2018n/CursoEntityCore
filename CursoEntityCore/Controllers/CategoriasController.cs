using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CursoEntityCore.Controllers
{
    public class CategoriasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        public IActionResult Index()
        {
            //Consulta con todos los datos
            //List<Categoria> listaCategoria = _context.Categoria.ToList();

            //Consulta con filtro de fecha
            //DateTime fechacomparacion = new DateTime(2024, 1, 5);
            //List<Categoria> listaCategoria = _context.Categoria.Where(f => f.FechaCreacion >= fechacomparacion).OrderByDescending(f => f.FechaCreacion).ToList();
            //return View(listaCategoria);

            //Seleccionar Columnas especificas.
            //var categorias = _context.Categoria.Where(n => n.Nombre == "Test 5").Select(n => n).ToList();
            //Agrupar

            //var listaCategoria = _context.Categoria
            //    .GroupBy(c => new {c.Activo})
            //    .Select(c => new {c.Key, Count = c.Count()})
            //    .ToList();

            //Paginando
            //List < Categoria > listaCategoria = _context.Categoria.Skip(3).Take(2).ToList();

            //SQL Crudo:
            //List<Categoria> listaCategoria = _context.Categoria.FromSqlRaw("select * from categoria where nombre like 'categoria%' and activo=1").ToList();

            //Interpolacion de Strings
            var id = 23;
            //var listaCategoria = _context.Categoria.FromSqlRaw($"select * from categoria where Categoria_id={id}").ToList();
            List<Categoria> listaCategoria = _context.Categoria.ToList();

            return View(listaCategoria);
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
            if (ModelState.IsValid) {
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
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 5; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_context.Categoria.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _context.Categoria.AddRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult VistaCrearMultipleFormulario() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CrearMultipleOpcionFormulario() 
        {
            string categoriasForm = Request.Form["Nombre"];
            var listaCategorias = from val in categoriasForm.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) select val;
            List<Categoria> categorias = new List<Categoria>();
            foreach(var categoria in listaCategorias) 
            {
                categorias.Add(new Categoria
                {
                    Nombre = categoria
                });
                
            }
            _context.Categoria.AddRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id== null)
            {
                return View();
            }
            var categoria = _context.Categoria.FirstOrDefault( c => c.Categoria_Id == id);
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
           if(ModelState.IsValid)
            {
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
           return View(categoria);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.Categoria_Id == id);
            _context.Categoria.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple2()
        {
            IEnumerable<Categoria> categorias = _context.Categoria.OrderByDescending(c => c.Categoria_Id).Take(2);
            _context.Categoria.RemoveRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple5()
        {
            IEnumerable<Categoria> categorias = _context.Categoria.OrderByDescending(c => c.Categoria_Id).Take(5);
            _context.Categoria.RemoveRange(categorias);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public void EjecucionDiferida()
        {
            //Se ejecuta Ejecucion Diferida en los siguientes casos:
            //1: Cuando se hace ina iteracion sobre ellos:
            var categorias = _context.Categoria;
            foreach(var categoria in categorias)
            {
                var nombreCat = "";
                nombreCat = categoria.Nombre;
            }
            //2: Cuando se llama a cualquera de los metodos: ToDictionary, ToList, ToArray
            var categoria2 = _context.Categoria.ToList();

            foreach(var categoria in categoria2)
            {
                var nombreCat = "";
                nombreCat = categoria.Nombre;
            }

            //3: Cuando se llama cualquier metodo que retorna un solo objeto:
            //first, Single, Count, Max, ...
            var categoria3 = _context.Categoria;
            var totalCategorias = categoria3.Count();
            var totalCategorias2 = _context.Categoria.Count();
            var pr = "";
        }

        [HttpGet]
        public void TestIEnumerable()
        {
            //1: Codigo con IEnumerable
            IEnumerable<Categoria> listaCategorias = _context.Categoria;
            var categoriasActivas = listaCategorias.Where(a => a.Activo == true).ToList();
            //2: Consulta resultante:
            //SELECT [c].[Categoria_Id], [c].[Activo], [c].[FechaCreacion], [c].[Nombre]
            //FROM[Categoria] AS[c]
            //El filtro se genera en memoria en el cliente


        }

        [HttpGet]
        public void TestIQueryable()
        {
            //1: Codigo con IQueryable
            //IQueruable hereda de IEnumerable
            //Todo lo que se puede hacer con IEnumerable se puede hacer con IQueryable
            IQueryable<Categoria> listaCategorias = _context.Categoria;
            var categoriasActivas = listaCategorias.Where(a => a.Activo == true).ToList();

            //Se genera:
            //SELECT[c].[Categoria_Id], [c].[Activo], [c].[FechaCreacion], [c].[Nombre]
            //FROM[Categoria] AS[c]
            //WHERE[c].[Activo] = CAST(1 AS bit)

        }

        [HttpGet]
        public void TestUpdate()
        {
            var datosusuario = _context.Usuario.Include(a => a.DetalleUsuario).FirstOrDefault( d => d.Id == 2);
            datosusuario.DetalleUsuario.Deporte = "Codear";
            _context.Update(datosusuario);
            _context.SaveChanges();
            

        }

        [HttpGet]
        public void TestAttach()
        {
            var datousuario = _context.Usuario.Include(u => u.DetalleUsuario).FirstOrDefault(a => a.Id == 2);
            datousuario.DetalleUsuario.Deporte = "beber";
            _context.Attach(datousuario);
            _context.SaveChanges();

        }

        //Metodo para llamar Vista SQL
        public void ObtenerCategoriasVistaSql()
        {
            var categoriasvista = _context.CategoriaDesdeVista.ToList();

            var categoriasvista2 = _context.CategoriaDesdeVista.FirstOrDefault();

            var vista3 = _context.CategoriaDesdeVista.Where(a => a.Activo == true).ToList();
        }

        //Consulta de sql
        public void ConsultaFromSql()
        {

            //Consulta directa, no es segura
            var usuario = _context.Usuario.FromSqlRaw("select * from dbo.usuario").ToList();

            //Consulta con params para evitar Sql injection
            var idusuario = 1;
            var usuario2 = _context.Usuario.FromSqlInterpolated($"select * from dbo.usuario where id = {idusuario}").ToList();

            var usuarioPorProcedimiento = _context.Usuario.FromSqlInterpolated($"EXEC dbo.SpObtenerUsuarioId {idusuario}").ToList();

        }

    }
}
