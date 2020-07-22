using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PrestamosLibros.Entidades
{
    public class Editoriales
    {
        [Key]
        public int EditorialId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public String Descripcion { get; set; }

        public Editoriales()
        {
            EditorialId = 0;
            //UsuarioId = 0;
            Descripcion = String.Empty;
        }
    }
}
