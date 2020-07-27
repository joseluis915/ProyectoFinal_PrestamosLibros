using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.RightsManagement;
using System.Linq.Expressions;
using System.Security.Cryptography;
using ProyectoFinal_PrestamosLibros.Entidades;
using ProyectoFinal_PrestamosLibros.DAL;

namespace ProyectoFinal_PrestamosLibros.BLL
{
    public class UsuariosBLL
    {
        //———————————————————————————————————————————————————[ EXISTE - EN LA BD]———————————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Usuarios.Any(e => e.UsuarioId == id);
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
        //———————————————————————————————————————————————————[ GUARDAR - REGISTRO ]———————————————————————————————————————————————————
        public static bool Guardar(Usuarios usuario)
        {
            if (!Existe(usuario.UsuarioId))
                return Insertar(usuario);
            else
                return Modificar(usuario);
        }
        //———————————————————————————————————————————————————[ INSERTAR (GUARDAR) - EN LA BD ]———————————————————————————————————————————————————
        private static bool Insertar(Usuarios usuario)
        {
            Contexto contexto = new Contexto();
            bool guardado = false;

            try
            {
                usuario.Contrasena = GetSHA256(usuario.Contrasena);
                if (contexto.Usuarios.Add(usuario) != null)
                    guardado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return guardado;
        }
        //———————————————————————————————————————————————————[ MODIFICAR - EN LA BD ]———————————————————————————————————————————————————
        private static bool Modificar(Usuarios usuario)
        {
            Contexto contexto = new Contexto();
            bool modificado = false;
            usuario.Contrasena = GetSHA256(usuario.Contrasena);

            try
            {
                contexto.Entry(usuario).State = EntityState.Modified;
                modificado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return modificado;
        }
        //———————————————————————————————————————————————————[ ELIMINAR - REGISTRO ]———————————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool eliminado = false;

            try
            {
                var usuario = contexto.Usuarios.Find(id);

                if (usuario != null)
                {
                    contexto.Usuarios.Remove(usuario);
                    eliminado = (contexto.SaveChanges() > 0);
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

            return eliminado;
        }
        //———————————————————————————————————————————————————[ BUSCAR - REGISTRO ]———————————————————————————————————————————————————
        public static Usuarios Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Usuarios usuario = new Usuarios();

            try
            {
                usuario = contexto.Usuarios.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }
        //———————————————————————————————————————————————————[ LISTAR ]———————————————————————————————————————————————————
        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool>> usuario)
        {
            Contexto contexto = new Contexto();
            List<Usuarios> Lista = new List<Usuarios>();

            try
            {
                Lista = contexto.Usuarios.Where(usuario).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Lista;
        }
        //———————————————————————————————————————————————————[ AUTENTICAR ]———————————————————————————————————————————————————
        public static bool Autenticar(string nombreusuario, string contrasena)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                paso = contexto.Usuarios.Any(u => u.NombreUsuario.Equals(nombreusuario) && u.Contrasena.Equals(GetSHA256(contrasena)));
                
                //var autenticar = from usuario in contexto.Usuarios where usuario.NombreUsuario == nombreusuario && usuario.Contrasena == GetSHA256(contrasena) select usuario;
                //if (autenticar.Count() > 0)
                //    paso = true;
                //else
                //    paso = false;
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
        //———————————————————————————————————————————————————[ ENCRIPTAR CLAVE ]———————————————————————————————————————————————————
        private static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();
        }
    }
}