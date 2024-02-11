
namespace CursoEntityCore.Datos
{
    using CursoEntityCore.Models;
    using EntityFrameworkCoreDBFirs.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {
            
        }

        //Modelos de las modelos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<DetalleUsuario> DetalleUsuario { get; set; }

        public DbSet<Etiqueta> Etiqueta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.Etiqueta_Id, ae.Articulo_Id});

            //Siembra de Datos se hace en este metodo
            var categoria30 = new Categoria() { Categoria_Id = 30, Nombre="Categoria 5", FechaCreacion = new DateTime(2024, 02, 7), Activo= true};
            var categoria31 = new Categoria() { Categoria_Id = 31, Nombre = "Categoria 6", FechaCreacion = new DateTime(2024, 02, 6), Activo = true };
            modelBuilder.Entity<Categoria>().HasData(new Models.Categoria[] { categoria30 });

            base.OnModelCreating(modelBuilder);

        }



        //Cuando crear migraciones
        //1: cuando creo una clase
        //2: cuando creo un nuevo campo
        //3: Cuando modifique un campo de clase

    }
}
