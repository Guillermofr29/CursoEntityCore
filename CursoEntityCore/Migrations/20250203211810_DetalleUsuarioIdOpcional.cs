using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class DetalleUsuarioIdOpcional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_DetalleUsuarioId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "DetalleUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DetalleUsuarioId",
                table: "Usuario",
                column: "DetalleUsuarioId",
                unique: true,
                filter: "[DetalleUsuarioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuarioId",
                table: "Usuario",
                column: "DetalleUsuarioId",
                principalTable: "DetalleUsuario",
                principalColumn: "DetalleUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_DetalleUsuarioId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "DetalleUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DetalleUsuarioId",
                table: "Usuario",
                column: "DetalleUsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DetalleUsuario_DetalleUsuarioId",
                table: "Usuario",
                column: "DetalleUsuarioId",
                principalTable: "DetalleUsuario",
                principalColumn: "DetalleUsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
