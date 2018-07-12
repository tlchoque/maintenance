using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mantenimiento.ReglasNegocio;
using Mantenimiento.Entidades;
using System.Web.Services;//para [WebMethod]
using Mantenimiento.AplicacionWeb.clases;
using System.Text;
namespace Mantenimiento.AplicacionWeb
{
    public partial class Perfil : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            this.IDUsuario.Value = UsuarioLogeado.ToString();
            if (!IsPostBack)
            {
                //GuardarUsuario("2", "SOPORTE", "SOPORTE","soporte","Direccion", "", "true", "", "");
            }
        }
        [WebMethod]
        public static int GuardarUsuario(string USUA_Interno, string USUA_Nombre, string USUA_Apellido, string USUA_Usuario,
    string USUA_Direccion, string USUA_Correo, string USUA_Contrasenia, string USUA_ContraseniaNueva)
        {
            Usuario usuario = new Usuario();
            if (USUA_Interno == "") usuario.USUA_Interno = null;
            else usuario.USUA_Interno = int.Parse(USUA_Interno);

            usuario.USUA_Usuario = USUA_Usuario == "" ? null : USUA_Usuario;
            usuario.USUA_Nombre = USUA_Nombre == "" ? null : USUA_Nombre;
            usuario.USUA_Apellido = USUA_Apellido == "" ? null : USUA_Apellido;
            usuario.USUA_Direccion = USUA_Direccion == "" ? null : USUA_Direccion;
            usuario.USUA_Correo = USUA_Correo == "" ? null : USUA_Correo;
            usuario.USUA_Contrasenia = USUA_Contrasenia == "" ? null : USUA_Contrasenia;

            USUA_ContraseniaNueva = USUA_ContraseniaNueva == "" ? null : USUA_ContraseniaNueva;

            ControlUsuario CUser = new ControlUsuario();
            if (Seguridad.Autenticar(usuario))
            {
                if (USUA_ContraseniaNueva != null)
                    usuario.USUA_Contrasenia = USUA_ContraseniaNueva;//cambiamos la contraseña
                else
                    usuario.USUA_Contrasenia = null;//no cambiamos la contraseña

                return CUser.EditarPerfilUsuario(usuario);
            }
            else
            {
                return -1;//Contrasenia Actual Incorrecta
            }
            
            
        }

        [WebMethod]
        public static Usuario ObtenerUsuario(string USUA_Interno)
        {
            ControlUsuario CU = new ControlUsuario();
            Usuario usuario = new Usuario();
            usuario.USUA_Interno = int.Parse(USUA_Interno);
            return CU.ObtenerPerfil(usuario);
        }
    }
}