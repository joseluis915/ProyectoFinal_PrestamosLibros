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
    public class EntradasLibrosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(EntradasLibros entradasLibros)
        {
            if (!Existe(entradasLibros.EntradaLibroId))
                return Insertar(entradasLibros);
            else
                return Modificar(entradasLibros);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(EntradasLibros entradasLibros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.EntradasLibros.Add(entradasLibros);
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
        public static bool Modificar(EntradasLibros entradasLibros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(entradasLibros).State = EntityState.Modified;
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
                var entradasLibros = contexto.EntradasLibros.Find(id);
                if (entradasLibros != null)
                {
                    contexto.EntradasLibros.Remove(entradasLibros);
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
        public static EntradasLibros Buscar(int id)
        {
            Contexto contexto = new Contexto();
            EntradasLibros entradasLibros;

            try
            {
                entradasLibros = contexto.EntradasLibros.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entradasLibros;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<EntradasLibros> GetList(Expression<Func<EntradasLibros, bool>> criterio)
        {
            List<EntradasLibros> lista = new List<EntradasLibros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradasLibros.Where(criterio).ToList();
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
                encontrado = contexto.EntradasLibros.Any(e => e.EntradaLibroId == id);
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
        public static List<EntradasLibros> GetEntradasLibros()
        {
            List<EntradasLibros> lista = new List<EntradasLibros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradasLibros.ToList();
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
    }
}