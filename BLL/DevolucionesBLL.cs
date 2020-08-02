using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_PrestamosLibros.DAL;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.BLL
{
    public class DevolucionesBLL
    {
        //—————————————————————————————————————————————————————[ GUARDAR ]—————————————————————————————————————————————————————
        public static bool Guardar(Devoluciones devoluciones)
        {
            bool paso;

            if (!Existe(devoluciones.DevolucionId))
                paso = Insertar(devoluciones);
            else
                paso = Modificar(devoluciones);

            return paso;
        }
        //—————————————————————————————————————————————————————[ INSERTAR ]—————————————————————————————————————————————————————
        public static bool Insertar(Devoluciones devoluciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                foreach (var item in devoluciones.Detalle)
                {
                    item.libros.Existencia += item.LibrosDevueltos;
                    contexto.Entry(item.libros).State = EntityState.Modified;
                }

                contexto.Devoluciones.Add(devoluciones);
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
        //—————————————————————————————————————————————————————[ MODIFICAR ]—————————————————————————————————————————————————————
        public static bool Modificar(Devoluciones devoluciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM DevolucionesDetalle WHERE DevolucionId={devoluciones.DevolucionId}");

                foreach (var item in devoluciones.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(devoluciones).State = EntityState.Modified;
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
        //—————————————————————————————————————————————————————[ ELIMINAR ]—————————————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var devolucion = DevolucionesBLL.Buscar(id);
                if (devolucion != null)
                {   
                    contexto.Devoluciones.Remove(devolucion);
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
        //—————————————————————————————————————————————————————[ GETLIST ]—————————————————————————————————————————————————————
        public static List<Devoluciones> GetList(Expression<Func<Devoluciones, bool>> criterio)
        {
            List<Devoluciones> lista = new List<Devoluciones>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Devoluciones.Where(criterio).ToList();
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
        //—————————————————————————————————————————————————————[ EXISTE ]—————————————————————————————————————————————————————
        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Devoluciones.Any(p => p.DevolucionId == id);
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
        //—————————————————————————————————————————————————————[ BUSCAR ]————————————————————————————————————————————————————
        public static Devoluciones Buscar(int id)
        {
            Devoluciones devoluciones = new Devoluciones();
            Contexto contexto = new Contexto();

            try
            {
                devoluciones = contexto.Devoluciones
                    .Where(d => d.DevolucionId == id)
                    .Include(d => d.Detalle).ThenInclude(dl => dl.libros)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return devoluciones;
        }
    }
}