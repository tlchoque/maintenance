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
    public partial class GrupoUsuario : System.Web.UI.Page
    {
        static string opc = null;
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;           
            this.ContenidoPagina.Visible = true;
            
            string IDGrupo = Request.QueryString["id"];
            string opcion = Request.QueryString["opc"];
            if (IDGrupo != null)
            {
                //reemplazamos si el parametro id existe y esta bien escrito
                IDGrupo = Request.QueryString["id"].Replace("_", "+");
                IDGrupo = SeguridadWeb.Desencriptar(IDGrupo);
            }
            
            if (opcion == "editar" && IDGrupo != "" && IDGrupo != null)
            {
                this.opcURL.Value = "editar";
                opc = "editar";
                this.IDGrupo.Value = IDGrupo;
                
            }
            else if (opcion == "nuevo" && IDGrupo == null)
            {
                this.opcURL.Value = "nuevo";
                opc = "nuevo";
                this.IDGrupo.Value = "";
                
            }
            else
            {
                this.msj_box_server.Visible = true;
                this.ContenidoPagina.Visible = false;
                
            }
            if (!IsPostBack)
            {
                ObtenerTareasDeGrupo("1");
            }
        }
        [WebMethod]
        public static Grupo ObtenerGrupo(string GRUP_Interno)
        {
            ControlGrupo CG = new ControlGrupo();
            Grupo grupo = new Grupo();
            grupo.GRUP_Interno = Convert.ToInt32(GRUP_Interno);
            return CG.ObtenerGrupo(grupo);
        }
        [WebMethod]
        public static int GuardarGrupo(string GRUP_Interno,string GRUP_Nombre ,string GRUP_Descripcion,string GRUP_Activo,string GRUP_Tareas)
        {

            Grupo grupo = new Grupo();
            if (GRUP_Interno == "") grupo.GRUP_Interno = null;
            else grupo.GRUP_Interno = int.Parse(GRUP_Interno);

            grupo.GRUP_Nombre = GRUP_Nombre == "" ? null : GRUP_Nombre;
            grupo.GRUP_Descripcion = GRUP_Descripcion == "" ? null : GRUP_Descripcion;

            if (GRUP_Activo == "true") grupo.GRUP_Activo = true;
            else grupo.GRUP_Activo = false;

            grupo.GRUP_Tareas = GRUP_Tareas == "" ? null : GRUP_Tareas;


            ControlGrupo CGrup = new ControlGrupo();
            if (opc == "nuevo" || opc == "editar")
            {
                return CGrup.InsertarGrupo(grupo, UsuarioLogeado);

            }
            else
                return 0;

        }

        [WebMethod]
        public static List<Tarea> TareasPorModulo()
        {
            ControlGrupo CG = new ControlGrupo();
            return CG.ObtenerTareasPorModulo();
        }
        [WebMethod]
        public static List<TareaGrupo> ObtenerTareasDeGrupo(string GRUP_Interno)
        {
            ControlGrupo CG = new ControlGrupo();
            Grupo grupo = new Grupo();
            grupo.GRUP_Interno = Convert.ToInt32(GRUP_Interno);
            return CG.ObtenerTareasDeGrupo(grupo);
        }
    }
}