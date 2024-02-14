using EntityFrameworkCoreDBFirs.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //[RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage ="Por Favor Ingrese un Email correcto")]
        [EmailAddress(ErrorMessage = "Por Favor Ingrese un Email correcto")]
        public string Email { get; set; }

        [Display(Name = "Dirección Usuario")]
        public string Direccion { get; set; }

        [NotMapped]
        public int Edad { get; set; }

        [ForeignKey("DetalleUsuario")]
        public int? DetalleUsuario_ID { get; set; }

        public DetalleUsuario DetalleUsuario { get; set; }
    }
}
