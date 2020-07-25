using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using ProyectoFinal_PrestamosLibros.DAL;
using ProyectoFinal_PrestamosLibros.Entidades;

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
        public static List<Libros> GetPrestamos()
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
    }
}