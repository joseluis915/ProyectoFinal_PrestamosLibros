using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Libros
    {
        [Key]
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        //———————————————————————————[ForeingKey UsuarioId]———————————————————————————
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        //————————————————————————————————————————————————————————————————————————————
        public String Titulo { get; set; }
        public String ISBN { get; set; }
        public int Existencia { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        //public Libros()
        //{
        //    LibroId = 0;
        //    //UsuarioId = 0;
        //    Titulo = String.Empty;
        //    ISBN = String.Empty;
        //    Existencia = 0;
        //    Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //}
    }
}
