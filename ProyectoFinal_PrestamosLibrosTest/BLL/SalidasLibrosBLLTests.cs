using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class SalidasLibrosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            SalidasLibros salidasLibros = new SalidasLibros();

            salidasLibros.SalidaLibroId = 1;
            salidasLibros.UsuarioId = 1;
            salidasLibros.LibroId = 1;
            salidasLibros.Cantidad = 15;
            salidasLibros.Fecha = DateTime.Now;
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(SalidasLibrosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(SalidasLibrosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(SalidasLibrosBLL.GetList(sl => sl.SalidaLibroId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(SalidasLibrosBLL.Existe(1));
        }

        [TestMethod()]
        public void GetSalidasLibrosTest()
        {
            Assert.Fail();
        }
    }
}