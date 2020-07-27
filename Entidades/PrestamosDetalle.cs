using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class PrestamosDetalle
    {
        [Key]
        public int PrestamoDetalleId { get; set; }
        public int PrestamosId { get; set; }
        public int EstudianteId { get; set; }
        public int LibroId { get; set; }
        public double CantidadLibro { get; set; }

        //———————————————————————————[ ForeingKey ]———————————————————————————
        [ForeignKey("LibroId")]
        public Libros libros { get; set; } = new Libros();
    }
}
