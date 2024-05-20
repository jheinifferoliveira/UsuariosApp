using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PERFIL_ID",
                table: "USUARIO",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PERFIL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIL", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PERFIL_ID",
                table: "USUARIO",
                column: "PERFIL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PERFIL_NOME",
                table: "PERFIL",
                column: "NOME",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERFIL_PERFIL_ID",
                table: "USUARIO",
                column: "PERFIL_ID",
                principalTable: "PERFIL",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERFIL_PERFIL_ID",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "PERFIL");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_PERFIL_ID",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "PERFIL_ID",
                table: "USUARIO");
        }
    }
}
