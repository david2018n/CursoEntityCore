using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class LLaveForaneaUsuarioDetalleUsuarioNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuario_ID",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_DetalleUsuario_ID",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "DetalleUsuario_ID",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DetalleUsuario_ID",
                table: "Usuario",
                column: "DetalleUsuario_ID",
                unique: true,
                filter: "[DetalleUsuario_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuario_ID",
                table: "Usuario",
                column: "DetalleUsuario_ID",
                principalTable: "DetalleUsuario",
                principalColumn: "DetalleUsuario_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuario_ID",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_DetalleUsuario_ID",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "DetalleUsuario_ID",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DetalleUsuario_ID",
                table: "Usuario",
                column: "DetalleUsuario_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuario_ID",
                table: "Usuario",
                column: "DetalleUsuario_ID",
                principalTable: "DetalleUsuario",
                principalColumn: "DetalleUsuario_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
