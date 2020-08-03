using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class PrestamosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Prestamos prestamos = new Prestamos();

            prestamos.PrestamoId = 1;
            prestamos.UsuarioId = 1;
            prestamos.EstudianteId = 1;
            prestamos.Fecha = DateTime.Now;
            prestamos.FechaVencimiento = DateTime.Now;
            prestamos.Detalle.Add(new PrestamosDetalle
            {
                PrestamoDetalleId = 1,
                PrestamosId = 1,
                EstudianteId = 1,
                LibroId = 1,
                CantidadLibro = 1,
            });
            prestamos.LibrosTotal = 5;
        }

        [TestMethod()]
        public void InsertarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(PrestamosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(PrestamosBLL.GetList(p => p.PrestamoId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(PrestamosBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(PrestamosBLL.Buscar(1) != null);
        }
    }
}