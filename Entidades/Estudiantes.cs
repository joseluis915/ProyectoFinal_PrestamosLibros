using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Estudiantes
    {
        [Key]
        public int EstudianteId { get; set; }
        public int UsuarioId { get; set; }
        //———————————————————————————[ForeingKey UsuarioId]———————————————————————————
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        //————————————————————————————————————————————————————————————————————————————
        public String Matricula { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public String Genero { get; set; }
        public String Telefono { get; set; }
        public String Direccion { get; set; }
        public String Correo { get; set; }
        public String Cedula { get; set; }
    }
}
