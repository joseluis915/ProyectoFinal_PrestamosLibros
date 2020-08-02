using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal_PrestamosLibros.Migrations
{
    public partial class Migracion13 : Migration
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

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    EditorialId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.EditorialId);
                    table.ForeignKey(
                        name: "FK_Editoriales_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    Matricula = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Cedula = table.Column<long>(nullable: false),
                    Genero = table.Column<string>(nullable: true),
                    Telefono = table.Column<long>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    LibroId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    ISBN = table.Column<long>(nullable: false),
                    Existencia = table.Column<double>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.LibroId);
                    table.ForeignKey(
                        name: "FK_Libros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    DevolucionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones", x => x.DevolucionId);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    PrestamoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    LibrosTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.PrestamoId);
                    table.ForeignKey(
                        name: "FK_Prestamos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntradasLibros",
                columns: table => new
                {
                    EntradaLibroId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasLibros", x => x.EntradaLibroId);
                    table.ForeignKey(
                        name: "FK_EntradasLibros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntradasLibros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalidasLibros",
                columns: table => new
                {
                    SalidaLibroId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalidasLibros", x => x.SalidaLibroId);
                    table.ForeignKey(
                        name: "FK_SalidasLibros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalidasLibros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevolucionesDetalle",
                columns: table => new
                {
                    DevolucionDetalleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DevolucionId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false),
                    LibrosDevueltos = table.Column<double>(nullable: false),
                    Dias = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevolucionesDetalle", x => x.DevolucionDetalleId);
                    table.ForeignKey(
                        name: "FK_DevolucionesDetalle_Devoluciones_DevolucionId",
                        column: x => x.DevolucionId,
                        principalTable: "Devoluciones",
                        principalColumn: "DevolucionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevolucionesDetalle_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrestamosDetalle",
                columns: table => new
                {
                    PrestamoDetalleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrestamosId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false),
                    CantidadLibro = table.Column<double>(nullable: false),
                    PrestamoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestamosDetalle", x => x.PrestamoDetalleId);
                    table.ForeignKey(
                        name: "FK_PrestamosDetalle_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestamosDetalle_Prestamos_PrestamoId",
                        column: x => x.PrestamoId,
                        principalTable: "Prestamos",
                        principalColumn: "PrestamoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellidos", "Contrasena", "FechaCreacion", "NombreUsuario", "Nombres" },
                values: new object[] { 1, "Hernandez Rodriguez", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Jose Anderson" });

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_EstudianteId",
                table: "Devoluciones",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_UsuarioId",
                table: "Devoluciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DevolucionesDetalle_DevolucionId",
                table: "DevolucionesDetalle",
                column: "DevolucionId");

            migrationBuilder.CreateIndex(
                name: "IX_DevolucionesDetalle_LibroId",
                table: "DevolucionesDetalle",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_Editoriales_UsuarioId",
                table: "Editoriales",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasLibros_LibroId",
                table: "EntradasLibros",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasLibros_UsuarioId",
                table: "EntradasLibros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_UsuarioId",
                table: "Estudiantes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_UsuarioId",
                table: "Libros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_EstudianteId",
                table: "Prestamos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamosDetalle_LibroId",
                table: "PrestamosDetalle",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamosDetalle_PrestamoId",
                table: "PrestamosDetalle",
                column: "PrestamoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalidasLibros_LibroId",
                table: "SalidasLibros",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_SalidasLibros_UsuarioId",
                table: "SalidasLibros",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevolucionesDetalle");

            migrationBuilder.DropTable(
                name: "Editoriales");

            migrationBuilder.DropTable(
                name: "EntradasLibros");

            migrationBuilder.DropTable(
                name: "PrestamosDetalle");

            migrationBuilder.DropTable(
                name: "SalidasLibros");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
