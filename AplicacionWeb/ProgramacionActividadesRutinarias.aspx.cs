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
    public partial class ProgramacionActividades : System.Web.UI.Page
    {
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;

            //int dia = (int)DateTime.Now.Date.DayOfWeek;//0=Domingo...Sabado=6
            //this.lblMsj.Text = dia.ToString();
            //ActividadesParaSerProgramadas("20","1","Mensual","02","15");
            //Localizaciones();
            //Periodo();
            //EditarPeriodo("Diario", "0", "30");
            //ObtenerTituloTabla(string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes);
            //ProgramarActividades("10");
        }
        [WebMethod]
        public static IEnumerable<ActividadR> ActividadesParaSerProgramadas(string TamanioPagina, string NumeroPagina, string PPRO_Periodo,string PPRO_DiaSemana,string PPRO_DiaMes, string LOCA_Interno)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno==""? null:(int?)Convert.ToInt32(LOCA_Interno);
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            if(localizacion.LOCA_Interno==null){
                return CtrlPA.ObtenerActividadesProgramablesIniciadas(periodo,Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina));
            }else{
                return CtrlPA.ObtenerActividadesProgramablesIniciadasPorLocalizacion(periodo,localizacion ,Convert.ToInt32(TamanioPagina), Convert.ToInt32(NumeroPagina));
            }
        }
        [WebMethod]
        public static int TotalRegistrosParaProgramar(string PPRO_Periodo, string PPRO_DiaSemana, string PPRO_DiaMes, string LOCA_Interno)
        {
            PeriodoProgramacion periodo = new PeriodoProgramacion();
            periodo.PPRO_Periodo = PPRO_Periodo == "" ? null : PPRO_Periodo;
            periodo.PPRO_DiaSemana = PPRO_DiaSemana == "" ? null : (int?)Convert.ToInt32(PPRO_DiaSemana);
            periodo.PPRO_DiaMes = PPRO_DiaMes == "" ? null : (int?)Convert.ToInt32(PPRO_DiaMes);
            LocalizacionS localizacion = new LocalizacionS();
            localizacion.LOCA_Interno = LOCA_Interno==""? null:(int?)Convert.ToInt32(LOCA_Interno);
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            if(localizacion.LOCA_Interno==null){
                return CtrlPA.CantidadRegistrosParaProgramarActividades(periodo);
            }else{
                return CtrlPA.CantidadRegistrosParaProgramarActividadesPorLocalizacion(localizacion,periodo);
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
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            return CtrlPA.ObtenerTituloTablaDeActividadesProgramables(periodo);
        }

        [WebMethod]
        public static int EditarProximasFechas(string FechasSiguientes)
        {
            //editamos la proximas fechas programadas tentativas
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            return CtrlPA.EditarFechasProgramadasActividadesRutinarias(FechasSiguientes,UsuarioLogeado);
        }
        [WebMethod]
        public static int ProgramarActividades(string actividadesR)
        {
            //actividadesR = HIAR_Interno;ACRU_Interno
            ControlProgramacionActividadesR CtrlPA = new ControlProgramacionActividadesR();
            return CtrlPA.ProgramarActividadesRutinarias(actividadesR, UsuarioLogeado);
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