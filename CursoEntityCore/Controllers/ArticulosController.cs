using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using CursoEntityCore.ViewModels;
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
            //Primera forma Sin datos relacionados (solo trae id categoria)
            //var listaArticulos = _context.Articulo.ToList();}


            //var listaArticulos = _context.Articulo.ToList();

            //foreach (var articulo in listaArticulos)
            //{
            //    //Caso 2, carga manual, se genera muchas consultas sql
            //    //articulo.Categoria = _context.Categoria.FirstOrDefault(c => c.Categoria_Id == articulo.Categoria_Id);

            //    //Caso 3, carga explicita
            //    _context.Entry(articulo).Reference(c => c.Categoria).Load();
            //}

            //Metodo 4: Eager loading: Carga Diligente

            var listaArticulos = _context.Articulo.Include(c => c.Categoria).ToList();

            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });
            return View(articuloCategorias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Articulo articulo)
        {
            if(ModelState.IsValid)
            {
                _context.Articulo.Add(articulo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            //Para que al retornar la vista por error, retorne lista categorias
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });
            return View(articuloCategorias);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if( id == null)
            {
                return View();
            }

            //Para que al retornar la vista por error, retorne lista categorias
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _context.Categoria.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });
            articuloCategorias.Articulo = _context.Articulo.FirstOrDefault(c => c.Articulo_Id == id);

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

            if( articuloVM.Articulo.Articulo_Id == 0)
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
        public IActionResult Borrar(int? id)
        {
            var articulo = _context.Articulo.FirstOrDefault(c => c.Articulo_Id == id);
            _context.Articulo.Remove(articulo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AdministrarEtiquetas(int id)
        {
            ArticuloEtiquetaVM articuloEtiquetas = new ArticuloEtiquetaVM
            {
                ListaArticuloEtiquetas = _context.ArticuloEtiqueta.Include(ae => ae.Etiqueta).Include(ae => ae.Articulo)
                .Where(a => a.Articulo_Id == id),
                ArticuloEtiqueta = new ArticuloEtiqueta()
                {
                    Articulo_Id = id
                },
                Articulo = _context.Articulo.FirstOrDefault(a => a.Articulo_Id == id)
            };
            List<int> listaTemporalEtiquetaArticulos = articuloEtiquetas.ListaArticuloEtiquetas.Select(e => e.Etiqueta_Id).ToList();
            //Obtener todas las etiquetas cuyos Id no estan en la lista temporal
            //crear un not in de sql
            var listaTemporal = _context.Etiqueta.Where(e => !listaTemporalEtiquetaArticulos.Contains(e.Etiqueta_Id)).ToList();
            //Crear lista de etiquetas para dropDown
            articuloEtiquetas.ListaEtiquetas = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.Titulo,
                Value = i.Etiqueta_Id.ToString()
            });
            return View(articuloEtiquetas);
        }
        [HttpPost]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if ( articuloEtiquetas.ArticuloEtiqueta.Articulo_Id != 0  && articuloEtiquetas.ArticuloEtiqueta.Etiqueta_Id!= 0)
            {
                _context.ArticuloEtiqueta.Add(articuloEtiquetas.ArticuloEtiqueta);
                _context.SaveChanges();
                
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new {@id = articuloEtiquetas.ArticuloEtiqueta.Articulo_Id});
        }

        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            int idArticulo = articuloEtiquetas.Articulo.Articulo_Id;

            ArticuloEtiqueta articuloEtiqueta = _context.ArticuloEtiqueta.FirstOrDefault(
                u => u.Etiqueta_Id == idEtiqueta && u.Articulo_Id == idArticulo); 

                _context.ArticuloEtiqueta.Remove(articuloEtiqueta);
                _context.SaveChanges();

            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = idArticulo });
        }
    }
}
