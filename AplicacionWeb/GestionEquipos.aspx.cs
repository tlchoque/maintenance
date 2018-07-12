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
    public partial class GestionEquipos : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            //listaEquipos("1", "1", "","", "25");
            //ListaFiltradaPorString("25", "1", "xxx");
        }
        [WebMethod]
        public static List<Equipo> listaEquipos(string Pagina,string OrdenarPor,string Orden,string TamanioPagina)
        {
            ControlEquipo CtrlEquipo = new ControlEquipo();

            List<Equipo> Equipos = CtrlEquipo.ObtenerCualquierPaginaEquipos(int.Parse(TamanioPagina), int.Parse(Pagina));
            List<Equipo> equipos = new List<Equipo>();

            foreach (Equipo equipo in Equipos)
            {
                equipo.LOCA_ID = SeguridadWeb.Encriptar(equipo.EQUI_Interno.ToString());//para el URL
                equipo.LOCA_ID = equipo.LOCA_ID.Replace("+", "_");
                equipos.Add(equipo);
            }
            return equipos;
            
        }
        [WebMethod]
        public static int TotalEquipos()
        {
            ControlEquipo CtrlEquipo = new ControlEquipo();
            return CtrlEquipo.ObtenerTotalRegistros();
        }
        [WebMethod]
        public static List<TipoEquipo> TiposEquipo()
        {
            ControlTipoEquipo CTiposEquipo = new ControlTipoEquipo();
            return CTiposEquipo.ObtenerTiposEquipo();
        }
        [WebMethod]
        public static int EliminarMultiplesRegistrosEquipo(string IDs)
        {
            int result = 0;
            string[] IDEquipos = IDs.Split('|');
            ControlEquipo CtrlEquipo = new ControlEquipo();
            foreach (string ID in IDEquipos)
            {
                Equipo equipo = new Equipo(int.Parse(ID));

                if (CtrlEquipo.EliminarEquipo(equipo, UsuarioLogeado) > 0)
                    result++;
            }
            return result;
        }
        [WebMethod]
        public static List<Equipo> ListaFiltradaPorString(string TamanioPagina, string Pagina, string StringFiltro)
        {
            ControlEquipo CtrlEquipo = new ControlEquipo();

            List<Equipo> Equipos= CtrlEquipo.ObtenerCualquierPaginaEquiposFiltradoPorString(int.Parse(TamanioPagina), int.Parse(Pagina), StringFiltro);
            List<Equipo> equipos = new List<Equipo>();

            foreach (Equipo equipo in Equipos)
            {
                equipo.LOCA_ID = SeguridadWeb.Encriptar(equipo.EQUI_Interno.ToString());//para el URL
                equipo.LOCA_ID = equipo.LOCA_ID.Replace("+", "_");
                equipos.Add(equipo);
            }
            return equipos;
        }
    }
}