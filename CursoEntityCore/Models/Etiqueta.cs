using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Etiqueta
    {
        [Key]
        public int Etiqueta_Id { get; set; }

        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        //Para muchos a muchos

        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }

    }
}
