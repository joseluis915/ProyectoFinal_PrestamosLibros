using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class DevolucionesDetalle
    {
        [Key]
        public int DevolucionDetalleId { get; set; }
        public int DevolucionId { get; set; }
        public int LibroId { get; set; }
        public double LibrosDevueltos { get; set; }
        public int Dias { get; set; }

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("LibroId")]
        public Libros libros { get; set; } = new Libros();
    }
}