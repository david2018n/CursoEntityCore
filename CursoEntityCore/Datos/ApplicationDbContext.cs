
namespace CursoEntityCore.Datos
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {
            
        }

        //Modelos de las entidades
    }
}
