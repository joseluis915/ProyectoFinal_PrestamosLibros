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
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Cantidad { get; set; }

        //———————————————————————————[ ForeingKey ]———————————————————————————
        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();

        [ForeignKey("LibroId")]
        public virtual Libros eLibro { get; set; }
    }
}
