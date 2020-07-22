using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Libros
    {
        [Key]
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public String Titulo { get; set; }
        public String ISBN { get; set; }
        public int Existencia { get; set; }
        public DateTime Fecha { get; set; }

        public Libros()
        {
            LibroId = 0;
            //UsuarioId = 0;
            Titulo = String.Empty;
            ISBN = String.Empty;
            Existencia = 0;
            Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }
    }
}
