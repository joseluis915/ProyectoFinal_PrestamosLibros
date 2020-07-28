using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Editoriales
    {
        [Key]
        public int EditorialId { get; set; }
        public int UsuarioId { get; set; }
        public String Descripcion { get; set; }

        //———————————————————————————[ ForeingKey ]———————————————————————————
        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();
    }
}