using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mantenimiento.ReglasNegocio;
using Mantenimiento.Entidades;
using System.Web.Services;//para [WebMethod]
using Mantenimiento.AplicacionWeb.clases;//para mis clases
namespace Mantenimiento.AplicacionWeb
{
    public partial class HistorialEjecucionActividadesR : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            //para la fecha fin y la fecha fin del la consulta --predterminado
            DateTime? Fechai = null; DateTime? Fechaf = null;
            Fechaf = DateTime.Now.Date;
            Fechai = Fechaf.Value.AddDays(1-Fechaf.Value.Day);
            this.FechaFin.Value = Fechaf.Value.ToString("dd/MM/yyyy");
            this.FechaInicio.Value = Fechai.Value.ToString("dd/MM/yyyy");
            this.TituloTabla.InnerHtml = "Actividades Rutinarias Ejecutadas en este mes (" + this.FechaInicio.Value + " - " + this.FechaFin.Value + ")";
        }
        [WebMethod]
        public static IEnumerable<ActividadR> HistorialactividadesREjecutadas(string TamanioPagina, string NumeroPagina, string LOCA_Interno, string DateInicio, string DateFin)
        {

            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno == "" ? null : (int?)Convert.ToInt32(LOCA_Interno);
            ControlHistorialAR CtrlHAR = new ControlHistorialAR();
            if (localizacion.LOCA_Interno == null)
            {
                //return CtrlHAR.ObtenerHistActividadesRutinariasEjecutadas(Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina), Convert.ToDateTime(DateInicio), Convert.ToDateTime(DateFin));
                return CtrlHAR.ObtenerHistActividadesRutinariasEjecutadas(Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina), DateTime.ParseExact(DateInicio, "dd/MM/yyyy", null), DateTime.ParseExact(DateFin, "dd/MM/yyyy", null));
            }
            else
            {
                return CtrlHAR.ObtenerHistActividadesRutinariasEjecutadasPorLocalizacion(localizacion, Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina), DateTime.ParseExact(DateInicio, "dd/MM/yyyy", null), DateTime.ParseExact(DateFin, "dd/MM/yyyy", null));
            }
        }
        [WebMethod]
        public static int TotalRegistrosHistActREjecutadas(string DateInicio, string DateFin,string LOCA_Interno)
        {

            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno == "" ? null : (int?)Convert.ToInt32(LOCA_Interno);
            ControlHistorialAR CtrlHAR = new ControlHistorialAR();
            if (localizacion.LOCA_Interno == null)
            {
                //return CtrlHAR.CantidadHistActividadesRutinariasEjecutadas(Convert.ToDateTime(DateInicio), Convert.ToDateTime(DateFin));
                return CtrlHAR.CantidadHistActividadesRutinariasEjecutadas(DateTime.ParseExact(DateInicio, "dd/MM/yyyy", null), DateTime.ParseExact(DateFin, "dd/MM/yyyy", null));
            }
            else
            {
                //return CtrlHAR.CantidadHistActividadesRutinariasProgramadasPorLocalizacion(localizacion, Convert.ToDateTime(DateInicio), Convert.ToDateTime(DateFin));
                return CtrlHAR.CantidadHistActividadesRutinariasProgramadasPorLocalizacion(localizacion, DateTime.ParseExact(DateInicio, "dd/MM/yyyy", null), DateTime.ParseExact(DateFin, "dd/MM/yyyy", null) );
            }
        }
        [WebMethod]
        public static List<LocalizacionS> Localizaciones()
        {
            ControlLocalizacion CLoca = new ControlLocalizacion();
            return CLoca.ObtenerLocalizaciones();
        }
    }

}