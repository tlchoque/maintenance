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
    public partial class DatosUsuario : System.Web.UI.Page
    {
        static string opc = null;
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            string IDUsuario=Request.QueryString["id"];
            string opcion = Request.QueryString["opc"];
            if (IDUsuario != null)
            {
                //reemplazamos si el parametro id existe y esta bien escrito
                IDUsuario=Request.QueryString["id"].Replace("_", "+");
                IDUsuario = SeguridadWeb.Desencriptar(IDUsuario);
            }
            //LbMsg.Text = IDUsuario;
            //if (IDUsuario == null)
            //{
            //    LbMsg.Text = "IDUsuario es null";
            //}
            //else if (IDUsuario == "")
            //{
            //    LbMsg.Text = "IDUsuario es vacio";
            //}
            //else
            //{
            //    LbMsg.Text = "IDUsuario no es nulo ni vacio";
            //}
            //
            if (opcion == "editar" && IDUsuario != "" && IDUsuario != null)
            {
                this.opcURL.Value = "editar";
                opc = "editar";
                this.IDUsuario.Value = IDUsuario;
                LlenarSelectGrupo();
            }
            else if (opcion == "nuevo" && IDUsuario == null)
            {
                this.opcURL.Value = "nuevo";
                opc = "nuevo";
                this.IDUsuario.Value = "";
                LlenarSelectGrupo();
            }
            else
            {
                this.msj_box_server.Visible = true;
                this.ContenidoPagina.Visible = false;
            }
            if (!IsPostBack)
            {
                //GuardarUsuario("2", "SOPORTE", "SOPORTE","soporte","Direccion", "", "true", "", "");
            }
        }
        protected void LlenarSelectGrupo()
        {
            ControlGrupo CGrupo = new ControlGrupo();
            List<Grupo> ListaGrupos = new List<Grupo>();
            ListaGrupos = CGrupo.ObtenerGrupos();
            int i = 1;
            foreach (Grupo grup in ListaGrupos)
            {
                this.Grupo.Items.Insert(i, grup.GRUP_Nombre);
                this.Grupo.Items[i].Value = grup.GRUP_Interno.ToString();
                i++;
            }
        }
        [WebMethod]
        public static int GuardarUsuario(string USUA_Interno,string USUA_Nombre,string USUA_Apellido,string USUA_Usuario,
	string USUA_Direccion,string USUA_Correo,string USUA_Activo,string USUA_Contrasenia,string GRUP_Interno)
        {

            Usuario usuario = new Usuario();
            if (USUA_Interno == "") usuario.USUA_Interno = null;
            else usuario.USUA_Interno = int.Parse(USUA_Interno);

            usuario.USUA_Usuario = USUA_Usuario == "" ? null : USUA_Usuario;
            usuario.USUA_Nombre = USUA_Nombre == "" ? null : USUA_Nombre;
            usuario.USUA_Apellido = USUA_Apellido == "" ? null : USUA_Apellido;
            usuario.USUA_Direccion = USUA_Direccion == "" ? null : USUA_Direccion;
            usuario.USUA_Correo = USUA_Correo == "" ? null : USUA_Correo;

            if (USUA_Activo == "true") usuario.USUA_Activo = true;
            else usuario.USUA_Activo = false;

            usuario.USUA_Contrasenia = USUA_Contrasenia == "" ? null : USUA_Contrasenia;

            if (GRUP_Interno == "") usuario.GRUP_Interno = null;
            else usuario.GRUP_Interno = int.Parse(GRUP_Interno);

            ControlUsuario CUser = new ControlUsuario();
            if (opc == "nuevo" || opc == "editar")
            {
                return CUser.InsertarUsuario(usuario, UsuarioLogeado);

            }
            else
                return 0;

        }

        [WebMethod]
        public static Usuario ObtenerUsuario(string USUA_Interno)
        {
            
            ControlUsuario CU = new ControlUsuario();
            Usuario usuario = new Usuario();
            usuario.USUA_Interno = int.Parse(USUA_Interno);
            return CU.ObtenerDatosUsuario(usuario);
        }
    }
}