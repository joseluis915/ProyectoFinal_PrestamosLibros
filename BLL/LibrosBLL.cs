using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using ProyectoFinal_PrestamosLibros.DAL;
using ProyectoFinal_PrestamosLibros.Entidades;
using System.Windows;
using System.Windows.Documents;

namespace ProyectoFinal_PrestamosLibros.BLL
{
    public class LibrosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Libros libros)
        {
            if (!Existe(libros.LibroId))
                return Insertar(libros);
            else
                return Modificar(libros);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(Libros libros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Libros.Add(libros);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ MODIFICAR ]——————————————————————————————————————————————
        public static bool Modificar(Libros libros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(libros).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ ELIMINAR ]——————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var libros = contexto.Libros.Find(id);
                if (libros != null)
                {
                    contexto.Libros.Remove(libros);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ BUSCAR ]——————————————————————————————————————————————
        public static Libros Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Libros libros;

            try
            {
                libros = contexto.Libros.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return libros;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Libros> GetList(Expression<Func<Libros, bool>> criterio)
        {
            List<Libros> lista = new List<Libros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Libros.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
        //——————————————————————————————————————————————[ EXISTE ]——————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Libros.Any(l => l.LibroId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Libros> GetLibros()
        {
            List<Libros> lista = new List<Libros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Libros.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
        //——————————————————————————————————————————————[ GetList ]——————————————————————————————————————————————
        public static List<Libros> GetList()
        {
            List<Libros> libros = new List<Libros>();
            Contexto contexto = new Contexto();

            try
            {
                libros = contexto.Libros.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return libros;
        }
        //——————————————————————————————————————————————[ Sumar - SalidaLibros ]——————————————————————————————————————————————
        public static void SumarSalidaLibros(int id, double cantidad)
        {
            Libros libros = Buscar(id);

            libros.Existencia += cantidad;

            Modificar(libros);
        }
        //——————————————————————————————————————————————[ Restar - SalidaLibros ]——————————————————————————————————————————————
        public static void RestarSalidaLibros(int id, double cantidad)
        {
            Libros libros = Buscar(id);

            libros.Existencia -= cantidad;

            if (libros.Existencia >= 0)
            {
                Modificar(libros);
            }
            else
            {
                // TODO: No dejar que guarde al mostrar este mensaje. 
                MessageBox.Show("No puedes dar salida a esta catidad de libros, porque es menor que 0.\n\nVerifique la existencia actual del libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}