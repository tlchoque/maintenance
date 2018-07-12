using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mantenimiento.ReglasNegocio;
using Mantenimiento.Entidades;
using System.Web.Services;//para [WebMethod]
using System.Web.Script.Serialization;//para serializar
using Mantenimiento.AplicacionWeb.clases;//para mis clases
namespace Mantenimiento.AplicacionWeb
{
    public partial class GestionGrupos : System.Web.UI.Page
    {
        private static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            //listaGrupos("20", "1");

        }
        [WebMethod]
        public static List<Grupo> listaGrupos(string TamanioPagina, String Pagina)
        {
            ControlGrupo CG = new ControlGrupo();
            List<Grupo> Grupos = CG.ObtenerGrupos(int.Parse(TamanioPagina), int.Parse(Pagina));
            List<Grupo> grupos = new List<Grupo>();

            foreach (Grupo grupo in Grupos)
            {         
                grupo.GRUP_ID = SeguridadWeb.Encriptar(grupo.GRUP_Interno.ToString());
                grupo.GRUP_ID = grupo.GRUP_ID.Replace("+", "_");
                grupos.Add(grupo);
            }
            return grupos;
        }
        [WebMethod]
        public static List<Grupo> listaGruposFiltradoPorString(string TamanioPagina, String Pagina, string stringFiltro)
        {
            ControlGrupo CG = new ControlGrupo();

            List<Grupo> Grupos = CG.ObtenerGruposFiltradoPorNombre(int.Parse(TamanioPagina), int.Parse(Pagina), stringFiltro);
            List<Grupo> grupos = new List<Grupo>();

            foreach (Grupo grupo in Grupos)
            {
                grupo.GRUP_ID = SeguridadWeb.Encriptar(grupo.GRUP_Interno.ToString());
                grupo.GRUP_ID = grupo.GRUP_ID.Replace("+", "_");
                grupos.Add(grupo);
            }
            return grupos;
        }
        [WebMethod]
        public static int ActivarVariosGrupos(string IDs)
        {
            int result = 0;
            string[] IDGrupos = IDs.Split('|');
            ControlGrupo CG = new ControlGrupo();
            foreach (string ID in IDGrupos)
            {
                Grupo grupo = new Grupo();
                grupo.GRUP_Interno = int.Parse(ID);
                if (CG.ActivarGrupo(grupo, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static int DesactivarVariosGrupos(string IDs)
        {
            int result = 0;
            string[] IDGrupos = IDs.Split('|');
            ControlGrupo CG = new ControlGrupo();
            foreach (string ID in IDGrupos)
            {
                Grupo grupo = new Grupo();
                grupo.GRUP_Interno = int.Parse(ID);
                if (CG.DesactivarGrupo(grupo, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static int EliminarVariosGrupos(string IDs)
        {
            int result = 0;
            string[] IDGrupos = IDs.Split('|');
            ControlGrupo CG = new ControlGrupo();
            foreach (string ID in IDGrupos)
            {
                Grupo grupo = new Grupo();
                grupo.GRUP_Interno = int.Parse(ID);
                if (CG.EliminarGrupo(grupo, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static int TotalGrupos()
        {
            ControlGrupo CG = new ControlGrupo();
            return CG.ObtenerTotalGrupos();
        }
    }
}