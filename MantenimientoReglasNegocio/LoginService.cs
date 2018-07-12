using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable
namespace Mantenimiento.ReglasNegocio
{
    public static class LoginService
    {
        public static int Autenticar(Usuario usuario, string IPCliente){
            //Esta funcion la usamos para el ingreso al sistema
            UsuarioDAO DataUser = new UsuarioDAO();
            //encriptamos la contraseña antes de autenticar
            usuario.USUA_Contrasenia = Seguridad.EncriptarContrasenia(usuario);

            int IDUser = DataUser.Autenticar(usuario);
            if (IDUser > 0)
            {
                HistorialIngresoDAO histDAO = new HistorialIngresoDAO();
                //guardamos el historial de ingreso
                HistorialIngreso objHistorial = new HistorialIngreso();
                objHistorial.USUA_Interno = IDUser;
                objHistorial.HIIN_IPacceso = IPCliente;
                histDAO.InsertarHistorialIngreso(objHistorial);
                return IDUser;
            }
            else
            {
                return 0;
            }
        }
        
    }
}
