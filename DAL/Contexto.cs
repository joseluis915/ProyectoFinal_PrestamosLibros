using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging().UseSqlite(@"Data Source= Data\ProyectoFinal_PrestamosLibros.DB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().HasData(new Usuarios
            {
                UsuarioId = 1,
                Nombres = "Jose Anderson",
                Apellidos = "Hernandez Rodriguez",
                NombreUsuario = "admin",
                Contrasena = "12345"
            });
        }
    }
}