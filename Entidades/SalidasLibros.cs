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
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        //———————————————————————————[ ForeingKey ]———————————————————————————
        [ForeignKey("SalidaLibroId")]
        public virtual List<SalidasLibros> SL { get; set; } = new List<SalidasLibros>();

        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();

        [ForeignKey("LibroId")]
        public virtual Libros sLibro { get; set; } = new Libros();
    }
}