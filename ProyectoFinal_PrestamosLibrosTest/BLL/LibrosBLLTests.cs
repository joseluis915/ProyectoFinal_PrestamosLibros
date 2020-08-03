using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class LibrosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Libros libros = new Libros();

            libros.LibroId = 1;
            libros.UsuarioId = 1;
            libros.Titulo = "Titulo libro";
            libros.EditorialId = 2;
            libros.ISBN = 1234567890123;
            libros.Existencia = 50;
            libros.Fecha = DateTime.Now;
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(LibrosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(LibrosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(LibrosBLL.GetList(l => l.LibroId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(LibrosBLL.Existe(1));
        }

        [TestMethod()]
        public void GetLibrosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SumarSalidaLibrosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RestarSalidaLibrosTest()
        {
            Assert.Fail();
        }
    }
}