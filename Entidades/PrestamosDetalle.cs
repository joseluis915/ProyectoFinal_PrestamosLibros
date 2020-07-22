using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class PrestamosDetalle
    {
        [Key]
        public int PrestamosDetaleId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public int PrestamosId { get; set; }
        public int LibroId { get; set; }
        public int CantidadLibro { get; set; }

        public PrestamosDetalle()
        {
            PrestamosDetaleId = 0;
            //UsuarioId = 0;
            PrestamosId =0;
            LibroId = 0;
            CantidadLibro = 1;
        }
    }
}
