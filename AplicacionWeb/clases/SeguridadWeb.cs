using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

using System.Security.Cryptography;
//using Microsoft.Win32;
namespace Mantenimiento.AplicacionWeb.clases
{
    public static class SeguridadWeb
    {
        //para Key & IV son para la encriptacion de los parametros URL
        private static string Key = "LJ7SY5PsijORoUaakqpjYK7j/h7IOygO";//clave simetrica para el descifrado y cifrado/32
        private static string IV = "yCyhZLlh9fTDigjC";//vector de inicializacion/16
        public static string Encriptar(string texto)
        {
            byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(texto);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            crypto.Dispose();
            
            return Convert.ToBase64String(encrypted);

            //ToBase64String:
            //Los dígitos de base 64 en orden ascendente desde cero son los caracteres de la "A" a la "Z" mayúsculas, 
            //los caracteres de la "a" a la "z" minúsculas, los números del "0" al "9" y los símbolos "+" y "/". Se utiliza 
            //el carácter sin valor "=" para el relleno final. 
        }
        public static string Desencriptar(string texto)
        {
            //la funcion retornará null si hubo manipulacion en la variable 'textoEncriptado'
            byte[] encryptedBytes = null;
            string result = null;
            try
            {
                encryptedBytes = Convert.FromBase64String(texto);

                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = ASCIIEncoding.ASCII.GetBytes(Key);
                aes.IV = ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] Secreto = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                crypto.Dispose();
                result = ASCIIEncoding.ASCII.GetString(Secreto);
            }
            catch (Exception)
            {
                //capturamos el error y no lo mostramos
            }
            return result;
        }
        public static void RegistrarRedireccionarCuandoFinaliceLaSesion(this Page page)
        {
            /// Login Page, We can retrieve for configuration file (Web.Config)
            string loginPage = "~Default.aspx";
            /// Session Timeout (Default 20 minutos)
            int sessionTimeout = HttpContext.Current.Session.Timeout;
            /// Timeout for Redirect to Login Page (10 milliseconds before)
            int redirectTimeout = (sessionTimeout * 60000) - 10;
            /// JavaScript Code
            StringBuilder javascript = new StringBuilder();
            javascript.Append("var redirectTimeout;");
            javascript.Append("clearTimeout(redirectTimeout);");
            javascript.Append(String.Format("setTimeout(\"window.location.href='{0}'\",{1});", loginPage, redirectTimeout));
            /// Register JavaScript Code on WebPage
            page.ClientScript.RegisterStartupScript(page.GetType(), "RegistrarRedireccionarCuandoFinaliceLaSesion", javascript.ToString(), true);
        }

        
    }
}