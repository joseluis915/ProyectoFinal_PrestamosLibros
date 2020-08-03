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
        public int EstudianteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("DevolucionId")]
        public virtual List<DevolucionesDetalle> Detalle { get; set; } = new List<DevolucionesDetalle>();

        [ForeignKey("UsuarioId")]
        public Usuarios usuarios { get; set; }

        [ForeignKey("EstudianteId")]
        public Estudiantes estudiantes { get; set; }
    }
}