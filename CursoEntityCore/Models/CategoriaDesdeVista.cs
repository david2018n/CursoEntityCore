namespace CursoEntityCore.Models
{
    public class CategoriaDesdeVista
    {
        public int Categoria_Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}


/*
 * create view [dbo].[obtenerCategorias] as 
SELECT[c].[Categoria_Id], [c].[Activo], [c].[FechaCreacion], [c].[Nombre]
FROM[Categoria] AS[c]
GO

ALTER procedure [dbo].[SpObtenerUsuarioId] @idusuario int as
set nocount on;
select * from dbo.Usuario u where u.Id = @idusuario
*/