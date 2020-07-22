using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using ProyectoFinal_PrestamosLibros.DAL;

namespace ProyectoFinal_PrestamosLibros.BLL
{
    public class LoginBLL
    {
        public static bool Validar(string nombreusuario, string contrasena)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var validar = from usuario
                              in contexto.Usuarios
                              where usuario.NombreUsuario == nombreusuario && usuario.Contrasena == GetSHA256(contrasena)
                              select usuario;
                if (validar.Count() > 0)
                    paso = true;
                else
                    paso = false;
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
        public static string GetSHA256(string str)
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
