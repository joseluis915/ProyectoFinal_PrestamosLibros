using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class DevolucionesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Devoluciones devoluciones = new Devoluciones();

            devoluciones.DevolucionId = 1;
            devoluciones.EstudianteId = 1;
            devoluciones.Fecha = DateTime.Now;
            devoluciones.Detalle.Add(new DevolucionesDetalle
            {
                LibroId = 1,
                LibrosDevueltos = 1,
                Dias = 20,
            });
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(DevolucionesBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(DevolucionesBLL.GetList(d => d.DevolucionId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(DevolucionesBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(DevolucionesBLL.Buscar(1) != null);
        }
    }
}