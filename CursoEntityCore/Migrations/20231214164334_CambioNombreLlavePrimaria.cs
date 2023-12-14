using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class CambioNombreLlavePrimaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticuloId",
                table: "Tbl_Articulo",
                newName: "Articulo_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Articulo_Id",
                table: "Tbl_Articulo",
                newName: "ArticuloId");
        }
    }
}
