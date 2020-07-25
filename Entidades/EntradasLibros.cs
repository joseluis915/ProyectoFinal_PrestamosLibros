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
        //———————————————————————————[ForeingKey UsuarioId]———————————————————————————
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        //————————————————————————————————————————————————————————————————————————————
        public int LibroId { get; set; }
        //———————————————————————————[ForeingKey LibroId]———————————————————————————
        [ForeignKey("LibroId")]
        public Libros eLibro { get; set; } = new Libros();
        //————————————————————————————————————————————————————————————————————————————
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Cantidad { get; set; }
    }
}
