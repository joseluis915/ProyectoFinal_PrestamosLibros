using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class SalidasLibros
    {
        [Key]
        public int SalidaLibroId { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("UsuarioId")]
        public Usuarios usuarios { get; set; }

        [ForeignKey("LibroId")]
        public Libros libros { get; set; }
    }
}