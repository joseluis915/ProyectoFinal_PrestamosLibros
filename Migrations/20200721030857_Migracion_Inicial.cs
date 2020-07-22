using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal_PrestamosLibros.Migrations
{
    public partial class Migracion_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    Contrasena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellidos", "Contrasena", "FechaCreacion", "NombreUsuario", "Nombres" },
                values: new object[] { 1, "Hernandez Rodriguez", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Jose Anderson" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
