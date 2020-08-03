using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class EditorialesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Editoriales editoriales = new Editoriales();

            editoriales.EditorialId = 1;
            editoriales.UsuarioId = 1;
            editoriales.Descripcion = "Editorial Test";
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(EditorialesBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(EditorialesBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(EditorialesBLL.GetList(e => e.EditorialId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(EditorialesBLL.Existe(1));
        }

        [TestMethod()]
        public void GetEditorialesTest()
        {
            Assert.Fail();
        }
    }
}