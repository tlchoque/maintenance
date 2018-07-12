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
    public partial class GesionUsuarios : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            if (!IsPostBack)
            {
                //listaUsuarios("1", "25");
            }
        }
        [WebMethod(EnableSession = true)]
        public static List<Usuario> listaUsuarios(string Pagina,string TamanioPagina)
        {
            ControlUsuario CtrlUsuario = new ControlUsuario();
            List<Usuario> Usuarios = CtrlUsuario.ObtenerCualquierPaginaUsuarios(int.Parse(TamanioPagina), int.Parse(Pagina));
            List<Usuario> usuarios = new List<Usuario>();

            foreach (Usuario usuario in Usuarios)
            {
                //usuario.USUA_ID = HttpUtility.UrlEncode(SeguridadWeb.Encriptar(usuario.USUA_Interno.ToString()));//para el URL
                usuario.USUA_ID = SeguridadWeb.Encriptar(usuario.USUA_Interno.ToString());
                usuario.USUA_ID=usuario.USUA_ID.Replace("+", "_");
                //usuario.USUA_ID = HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Server.UrlEncode(SeguridadWeb.Encriptar(usuario.USUA_Interno.ToString())));
                usuarios.Add(usuario);
            }
            return usuarios;
        }
        [WebMethod]
        public static int TotalUsuarios()
        {
            ControlUsuario CtrlUsuario = new ControlUsuario();
            
            return CtrlUsuario.ObtenerTotalUsuarios();
        }
        [WebMethod]
        public static List<Usuario> ListaFiltradaPorString(string TamanioPagina, string Pagina, string stringFiltro)
        {
            ControlUsuario CtrlUsuario = new ControlUsuario();
            List<Usuario> Usuarios = CtrlUsuario.ObtenerCualquierPaginaUsuariosFiltradoPorString(int.Parse(TamanioPagina), int.Parse(Pagina), stringFiltro);
            List<Usuario> usuarios = new List<Usuario>();

            foreach (Usuario usuario in Usuarios)
            {
                usuario.USUA_ID = SeguridadWeb.Encriptar(usuario.USUA_Interno.ToString());//para el URL
                usuario.USUA_ID = usuario.USUA_ID.Replace("+", "_");
                usuarios.Add(usuario);
            }
            return usuarios;
        }
        [WebMethod(EnableSession = true)]
        public static int ActivarVariosUsuarios(string IDs)
        {
            int result = 0;
            string[] IDUsuarios = IDs.Split('|');
            ControlUsuario CtrlUsuario = new ControlUsuario();
            foreach (string ID in IDUsuarios)
            {
                Usuario usuario = new Usuario();
                usuario.USUA_Interno = int.Parse(ID);
                if (CtrlUsuario.ActivarUsuario(usuario, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static int DesactivarVariosUsuarios(string IDs)
        {
            int result = 0;
            string[] IDUsuarios = IDs.Split('|');
            ControlUsuario CtrlUsuario = new ControlUsuario();
            foreach (string ID in IDUsuarios)
            {
                Usuario usuario = new Usuario();
                usuario.USUA_Interno = int.Parse(ID);
                if (CtrlUsuario.DesactivarUsuario(usuario, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static int EliminarVariosUsuarios(string IDs)
        {
            int result = 0;
            string[] IDUsuarios = IDs.Split('|');
            ControlUsuario CtrlUsuario = new ControlUsuario();
            foreach (string ID in IDUsuarios)
            {
                Usuario usuario = new Usuario();
                usuario.USUA_Interno = int.Parse(ID);
                if (CtrlUsuario.EliminarUsuario(usuario, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
    }
}