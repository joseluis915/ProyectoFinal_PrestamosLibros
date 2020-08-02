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
    public class SalidasLibrosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(SalidasLibros salidasLibros)
        {
            if (!Existe(salidasLibros.SalidaLibroId))
                return Insertar(salidasLibros);
            else
                return Modificar(salidasLibros);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(SalidasLibros salidasLibros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.SalidasLibros.Add(salidasLibros);
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
        public static bool Modificar(SalidasLibros salidasLibros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(salidasLibros).State = EntityState.Modified;
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
                var salidasLibros = contexto.SalidasLibros.Find(id);
                if (salidasLibros != null)
                {
                    contexto.SalidasLibros.Remove(salidasLibros);
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
        public static SalidasLibros Buscar(int id)
        {
            Contexto contexto = new Contexto();
            SalidasLibros salidasLibros;

            try
            {
                salidasLibros = contexto.SalidasLibros.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return salidasLibros;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<SalidasLibros> GetList(Expression<Func<SalidasLibros, bool>> criterio)
        {
            List<SalidasLibros> lista = new List<SalidasLibros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.SalidasLibros.Where(criterio).ToList();
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
                encontrado = contexto.SalidasLibros.Any(s => s.SalidaLibroId == id);
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
        public static List<SalidasLibros> GetSalidasLibros()
        {
            List<SalidasLibros> lista = new List<SalidasLibros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.SalidasLibros.ToList();
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