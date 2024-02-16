using EntityFrameworkCoreDBFirs.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        
        public string Email { get; set; }

        [Display(Name = "Dirección Usuario")]
        public string Direccion { get; set; }

        public int Edad { get; set; }

        public int? DetalleUsuario_ID { get; set; }

        public DetalleUsuario DetalleUsuario { get; set; }
    }
}
