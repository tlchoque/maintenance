using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.Entidades;
using Mantenimiento.DAO;
using System.Security.Cryptography;
namespace Mantenimiento.ReglasNegocio
{
    public class Seguridad
    {
        public static string EncriptarContrasenia(Usuario usuario)
        {
            //Clave que se utilizará para encriptar el usuario y la contraseña
            string clave = "4ca7f8f19b6027f103eb4f1c63cd9dea:88c8b2e21ea6ff5f32975ca869f2102b/RAJC2013";
            //Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            string cadena = string.Concat(usuario.USUA_Usuario, usuario.USUA_Contrasenia);
            //Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(cadena + clave);
            //Se calcula la matriz de bytes del arreglo anterior y se encripta.
            byte[] hash = sha512.ComputeHash(inputBytes);
            //Convertimos el arreglo de bytes a cadena.
            return Convert.ToBase64String(hash);
            //para usuario:admin y pasword:123456 genera el siguiente pasword encryptado
            //7EjTeWh5ntH4nsUDDrnKTfdlBSbTwR2UeBy2Qp0/y4UH1yoasJeJO0FfSkvO2v3konOXrsmh0860a5Q5Pv3tyw==
            //para usuario:soporte y pasword:123456 genera el siguiente pasword encryptado
            //R3ch2k+yZJLQET7V+R8sHjQmNIWQA4EapK9tZ3VbzQ+ZVWgHTjxrpCl7pJzCLUwxDlT/AvMewoV33UcsoWyrww==
        }
        public static Boolean Autenticar(Usuario usuario)
        {
            //esta funcion la usamos cuando el usuario quiere modificar su contraseña
            //o un accion de riesgo y necesitamos que se vuelva a autenticar
            UsuarioDAO DataUser = new UsuarioDAO();
            usuario.USUA_Contrasenia = Seguridad.EncriptarContrasenia(usuario);
            int IDUser = DataUser.Autenticar(usuario);
            if (IDUser > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public static string GetCrypt(string text)
        //{
        //    string hash = "";
        //    SHA512 alg = SHA512.Create();
        //    SHA512 sha512 = new SHA512CryptoServiceProvider();
        //    byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
            
        //    hash = Encoding.UTF8.GetString(result);
        //    return hash;
        //}
    }

}
