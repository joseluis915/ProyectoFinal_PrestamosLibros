using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public int EstudianteId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public Prestamos()
        {
            PrestamoId = 0;
            //UsuarioId = 0;
            EstudianteId = 0;
            Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            FechaVencimiento = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }
    }
}
