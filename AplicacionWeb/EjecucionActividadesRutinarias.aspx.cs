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
    public partial class EjecucionActividadesRutinarias : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            //EjecutarActividades("139;67;06/10/2013|66;138;03/10/2013");
        }
        [WebMethod]
        public static IEnumerable<ActividadR> ActividadesParaSerEjecutadas(string TamanioPagina, string NumeroPagina, string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes, string LOCA_Interno)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno == "" ? null : (int?)Convert.ToInt32(LOCA_Interno);
            ControlEjecucionActividadesR CtrlEA = new ControlEjecucionActividadesR();
            if (localizacion.LOCA_Interno == null)
            {
                return CtrlEA.ObtenerActividadesRutinariasProgramadas(periodo, Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina));
            }
            else
            {
                return CtrlEA.ObtenerActividadesRutinariasProgramadasPorLocalizacion(periodo, localizacion, Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina));
            }
        }
        [WebMethod]
        public static int TotalRegistrosParaEjecutar(string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes, string LOCA_Interno)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno == "" ? null : (int?)Convert.ToInt32(LOCA_Interno);
            ControlEjecucionActividadesR CtrlEA = new ControlEjecucionActividadesR();
            if (localizacion.LOCA_Interno == null)
            {
                return CtrlEA.CantidadActividadesRutinariasProgramadas(periodo);
            }
            else
            {
                return CtrlEA.CantidadActividadesRutinariasProgramadasPorLocalizacion(localizacion, periodo);
            }
        }
        [WebMethod]
        public static int EditarPeriodo(string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            return CtrlPA.EditarPeriodoDeProgramacionDeActividades(periodo, UsuarioLogeado);
        }
        [WebMethod]
        public static PeriodoProgramacion Periodo()
        {
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            return CtrlPA.ObtenerPeriodoDeProgramacionDeActividades();
        }
        [WebMethod]
        public static string ObtenerTituloTabla(string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            ControlEjecucionActividadesR CtrlEA = new ControlEjecucionActividadesR();
            return CtrlEA.ObtenerTituloTablaDeActividadesRProgramadas(periodo);
        }

        //[WebMethod]
        //public static int EditarProximasFechas(string FechasSiguientes)
        //{
        //    ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
        //    return CtrlPA.EditarFechasProgramadasActividadesRutinarias(FechasSiguientes, UsuarioLogeado);
        //}
        [WebMethod]
        public static int EjecutarActividades(string actividadesR)
        {
            //actividadesR = HIAR_Interno;ACRU_Interno;FechadeEjecucion|...
            ControlEjecucionActividadesR CtrlEA = new ControlEjecucionActividadesR();
            return CtrlEA.EjecutarActividadesRutinarias(actividadesR, UsuarioLogeado);
        }
        //para filtrar por localizacion
        [WebMethod]
        public static List<LocalizacionS> Localizaciones()
        {
            ControlLocalizacion CLoca = new ControlLocalizacion();
            return CLoca.ObtenerLocalizaciones();
        }
    }
}