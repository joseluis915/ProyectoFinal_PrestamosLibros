using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String NombreUsuario { get; set; }
        public String Contrasena { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            NombreUsuario = string.Empty;
            Contrasena = string.Empty;
        }
    }
}