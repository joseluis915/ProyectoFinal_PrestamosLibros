using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class UsuariosBLLTests
    {
        [TestMethod()]
        public void ExisteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GuardarTest()
        {
            Usuarios usuarios = new Usuarios();

            usuarios.UsuarioId = 1;
            usuarios.Nombres = "Juan";
            usuarios.Apellidos = "Perez";
            usuarios.FechaCreacion = DateTime.Now;
            usuarios.NombreUsuario = "juanperez1";
            usuarios.Contrasena = "juanperez1";
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(UsuariosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(UsuariosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(UsuariosBLL.GetList(u => u.UsuarioId == 1) != null);
        }

        [TestMethod()]
        public void AutenticarTest()
        {
            Usuarios usuarios = new Usuarios();
            usuarios.NombreUsuario = "juanperez1";
        }

        [TestMethod()]
        public void GetUsuariosTest()
        {
            Usuarios usuarios = new Usuarios();
            usuarios.NombreUsuario = "juanperez1";
            usuarios.Contrasena = "juanperez1";
        }
    }
}