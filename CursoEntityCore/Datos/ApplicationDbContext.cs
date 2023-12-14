
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
            base.OnModelCreating(modelBuilder);
        }



        //Cuando crear migraciones
        //1: cuando creo una clase
        //2: cuando creo un nuevo campo
        //3: Cuando modifique un campo de clase

    }
}
