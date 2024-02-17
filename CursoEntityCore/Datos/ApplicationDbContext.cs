
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
        public DbSet<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }

        //Vista
        public DbSet<CategoriaDesdeVista> CategoriaDesdeVista { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.Etiqueta_Id, ae.Articulo_Id});

            //Siembra de Datos se hace en este metodo
            //var categoria30 = new Categoria() { Categoria_Id = 30, Nombre="Categoria 5", FechaCreacion = new DateTime(2024, 02, 7), Activo= true};
            //var categoria31 = new Categoria() { Categoria_Id = 31, Nombre = "Categoria 6", FechaCreacion = new DateTime(2024, 02, 6), Activo = true };
            //modelBuilder.Entity<Categoria>().HasData(new Models.Categoria[] { categoria30 });

            //Fluent Api para categoria
            modelBuilder.Entity<Categoria>().HasKey(c => c.Categoria_Id);
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).IsRequired();
            modelBuilder.Entity<Categoria>().Property(c => c.FechaCreacion).HasColumnType("date");

            //Fluent Api para Articulo
            modelBuilder.Entity<Articulo>().HasKey(c => c.Articulo_Id);
            modelBuilder.Entity<Articulo>().Property(c => c.TituloArticulo).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Articulo>().Property(c => c.Descripcion).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Articulo>().Property(c => c.Fecha).HasColumnType("date");

            //Fuent Api nombre de tabla y columna
            modelBuilder.Entity<Articulo>().ToTable("Tbl_Articulo");
            modelBuilder.Entity<Articulo>().Property(c => c.TituloArticulo).HasColumnName("Titulo");




            //Fluent Api para Usuario
            modelBuilder.Entity<Usuario>().HasKey(c => c.Id);
            modelBuilder.Entity<Usuario>().Ignore(u => u.Edad);


            //Fluent Api para DetalleUsuario
            modelBuilder.Entity<DetalleUsuario>().HasKey(c => c.DetalleUsuario_ID);
            modelBuilder.Entity<DetalleUsuario>().Property(c => c.Cedula).IsRequired();


            //Fluent Api para Etiqueta
            modelBuilder.Entity<Etiqueta>().HasKey(c => c.Etiqueta_Id);
            modelBuilder.Entity<Etiqueta>().Property(c => c.Fecha).HasColumnType("date");

            //FueltApi 1:1 usuario y DetalleUsuario
            modelBuilder.Entity<Usuario>()
                .HasOne(d => d.DetalleUsuario)
                .WithOne(b => b.Usuario).HasForeignKey<Usuario>("DetalleUsuario_ID");

            //FluentApi  1:* Articulo y Categoria
            modelBuilder.Entity<Articulo>()
               .HasOne(d => d.Categoria)
               .WithMany(b => b.Articulo).HasForeignKey(c => c.Categoria_Id);

            //FluentApi *:* Articulo y Etiqueta

            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.Etiqueta_Id, ae.Articulo_Id });
            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(d => d.Articulo)
               .WithMany(b => b.ArticuloEtiqueta).HasForeignKey(c => c.Articulo_Id);

            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(d => d.Etiqueta)
               .WithMany(b => b.ArticuloEtiqueta).HasForeignKey(c => c.Etiqueta_Id);


            //carga desde vista sin llave primaria
            modelBuilder.Entity<CategoriaDesdeVista>()




            base.OnModelCreating(modelBuilder);

        }



        //Cuando crear migraciones
        //1: cuando creo una clase
        //2: cuando creo un nuevo campo
        //3: Cuando modifique un campo de clase

    }
}
