using CursoEntityCore.Models;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreDBFirs.Models
{
    public class DetalleUsuario
    {
        [Key]
        public int DetalleUsuario_ID { get; set; }

        [Required]
        public string Cedula { get; set; }

        public string Deporte { get; set; }

        public string Mascota { get; set; }

        public Usuario Usuario { get; set; }

    }
}
