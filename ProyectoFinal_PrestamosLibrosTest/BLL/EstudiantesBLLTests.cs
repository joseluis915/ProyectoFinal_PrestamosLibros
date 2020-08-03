using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_PrestamosLibros.BLL.Tests
{
    [TestClass()]
    public class EstudiantesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Estudiantes estudiantes = new Estudiantes();

            estudiantes.EstudianteId = 1;
            estudiantes.UsuarioId = 1;
            estudiantes.Matricula = 20209999;
            estudiantes.Nombres = "Juan";
            estudiantes.Apellidos = "Perez";
            estudiantes.FechaNacimiento = DateTime.Now;
            estudiantes.Cedula = 40200000001;
            estudiantes.Genero = "Masculino";
            estudiantes.Telefono = 8090000000;
            estudiantes.Direccion = "Direccion X";
            estudiantes.Correo = "correo@outlook.com";
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(EstudiantesBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(EstudiantesBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(EstudiantesBLL.GetList(e => e.EstudianteId == 1) != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(EstudiantesBLL.Existe(1));
        }

        [TestMethod()]
        public void GetEstudiantesTest()
        {
            Assert.Fail();
        }
    }
}