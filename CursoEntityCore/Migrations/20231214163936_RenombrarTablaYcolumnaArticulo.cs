using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class RenombrarTablaYcolumnaArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulo",
                table: "Articulo");

            migrationBuilder.RenameTable(
                name: "Articulo",
                newName: "Tbl_Articulo");

            migrationBuilder.RenameColumn(
                name: "TipoArticulo",
                table: "Tbl_Articulo",
                newName: "Titulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_Articulo",
                table: "Tbl_Articulo",
                column: "ArticuloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_Articulo",
                table: "Tbl_Articulo");

            migrationBuilder.RenameTable(
                name: "Tbl_Articulo",
                newName: "Articulo");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Articulo",
                newName: "TipoArticulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulo",
                table: "Articulo",
                column: "ArticuloId");
        }
    }
}
