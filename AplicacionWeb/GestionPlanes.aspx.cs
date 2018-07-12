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

using System.Text;
using System.Data.Common;//para DbParameter
using System.Data;//para DbType

namespace Mantenimiento.AplicacionWeb
{
    public partial class GestionPlanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<PlanTrabajo> listaPlanes()
        {
            ControlPlan Plan = new ControlPlan();
            return Plan.ObtenerPlanes();
        }

        [WebMethod]
        public static int GuardarPlan(string PLAN_Interno, string PLAN_Descripcion, string PLAN_Regimen, string PLAN_UnidadLecturas)
        {
            PlanTrabajo PlanTrabajo = new PlanTrabajo();
            if (PLAN_Interno == "") PlanTrabajo.PLAN_Interno = null;
            else PlanTrabajo.PLAN_Interno = int.Parse(PLAN_Interno);
            PlanTrabajo.PLAN_Descripcion = PLAN_Descripcion;
            PlanTrabajo.PLAN_Regimen = PLAN_Regimen;
            if (PLAN_UnidadLecturas == "") PlanTrabajo.PLAN_UnidadLecturas = null;
            else PlanTrabajo.PLAN_UnidadLecturas = PLAN_UnidadLecturas;
            ControlPlan ctrlPlan = new ControlPlan();
            if (PLAN_Interno == "")
            {
                return ctrlPlan.InsertarPlan(PlanTrabajo, 1, null);
            }
            else if (PLAN_Interno != "")
            {
                return ctrlPlan.InsertarPlan(PlanTrabajo, null, 1);
            }
            else
                return 0;
        }

        [WebMethod]
        public static int EliminarPlanes(string IDs)
        {
            int resultado = 0;
            string[] IDEquipos = IDs.Split('|');
            ControlPlan CtrlPlan = new ControlPlan();
            foreach (string ID in IDEquipos)
            {
                PlanTrabajo PlanTrabajo = new PlanTrabajo(int.Parse(ID));
                if (CtrlPlan.EliminarPlan(PlanTrabajo, 1) > 0)
                    resultado++;
            }
            return resultado;
        }

        [WebMethod]
        public static int CopiarPlan(string PLAN_Interno, string PLAN_Descripcion)
        {
            PlanTrabajo Plan = new PlanTrabajo(int.Parse(PLAN_Interno));
            ControlPlan ControlPlan = new ControlPlan();
            return ControlPlan.CopiarPlan(Plan, PLAN_Descripcion, 1);
        }

        [WebMethod]
        public static int TotalPlanes()
        {
            ControlPlan CP = new ControlPlan();
            return CP.ObtenerTotalPlanes();
        }

        [WebMethod]
        public static List<PlanTrabajo> listaPlanesP(string TamanioPagina, String Pagina)
        {
            ControlPlan CP = new ControlPlan();
            List<PlanTrabajo> Planes = CP.ObtenerPlanesP(int.Parse(TamanioPagina), int.Parse(Pagina));
            List<PlanTrabajo> planes = new List<PlanTrabajo>();

            foreach (PlanTrabajo plan in Planes)
            {
                plan.PLAN_ID= SeguridadWeb.Encriptar(plan.PLAN_Interno.ToString());
                plan.PLAN_ID = plan.PLAN_ID.Replace("+", "_");
                planes.Add(plan);
            }
            return planes;
        }

        [WebMethod]
        public static List<PlanTrabajo> listaPlanesFiltradoPorString(string TamanioPagina, String Pagina, string stringFiltro)
        {
            ControlPlan CP = new ControlPlan();

            List<PlanTrabajo> Planes = CP.ObtenerPlanesFiltradoPorNombre(int.Parse(TamanioPagina), int.Parse(Pagina), stringFiltro);
            List<PlanTrabajo> planes = new List<PlanTrabajo>();

            foreach (PlanTrabajo plan in Planes)
            {
                plan.PLAN_ID = SeguridadWeb.Encriptar(plan.PLAN_Interno.ToString());
                plan.PLAN_ID = plan.PLAN_ID.Replace("+", "_");
                planes.Add(plan);
            }
            return planes;
        }
    }
}