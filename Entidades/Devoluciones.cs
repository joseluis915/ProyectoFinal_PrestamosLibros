using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Devoluciones
    {
        [Key]
        public int DevolucionId { get; set; }
        public int UsuarioId { get; set; }
        //———————————————————————————[ForeingKey UsuarioId]———————————————————————————
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        //————————————————————————————————————————————————————————————————————————————
        public int EstudianteId { get; set; }
        //———————————————————————————[ForeingKey EstudianteId]———————————————————————————
        [ForeignKey("EstudianteId")]
        public Estudiantes estudiantes { get; set; } = new Estudiantes();
        //————————————————————————————————————————————————————————————————————————————
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
