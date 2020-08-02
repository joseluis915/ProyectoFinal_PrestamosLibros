using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class EntradasLibros
    {
        [Key]
        public int EntradaLibroId { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        //———————————————————————————[ ForeingKey ]———————————————————————————
        [ForeignKey("UsuarioId")]
        public virtual Usuarios usuarios { get; set; } = new Usuarios();

        [ForeignKey("LibroId")]
        public Libros libros { get; set; }
    }
}
