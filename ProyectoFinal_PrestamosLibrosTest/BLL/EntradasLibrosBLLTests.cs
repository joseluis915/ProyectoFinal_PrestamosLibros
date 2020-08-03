using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class EntradasLibrosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            EntradasLibros entradasLibros = new EntradasLibros();

            entradasLibros.EntradaLibroId = 1;
            entradasLibros.UsuarioId = 1;
            entradasLibros.LibroId = 1;
            entradasLibros.Cantidad = 20;
            entradasLibros.Fecha = DateTime.Now;
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(EntradasLibrosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(EntradasLibrosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(EntradasLibrosBLL.GetList(el => el.EntradaLibroId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(EntradasLibrosBLL.Existe(1));
        }

        [TestMethod()]
        public void GetEntradasLibrosTest()
        {
            Assert.Fail();
        }
    }
}