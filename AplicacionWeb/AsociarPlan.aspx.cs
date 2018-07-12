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

namespace Mantenimiento.AplicacionWeb
{
    public partial class AsociarPlan : System.Web.UI.Page
    {
        static string opc = null;
        static string items = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarPlanes();
                if (Request.QueryString["opc"] == "equ")
                {
                    this.opcURL.Value = "equi";
                    opc = "equi";
                }
                else
                {
                    this.opcURL.Value = "loc";
                    opc = "loc";
                }
                this.IDs.Value = Request.QueryString["id"];
                items = Request.QueryString["id"]; ;
            }
        }

        protected void LlenarPlanes()
        {
            ControlPlan ControlPlan = new ControlPlan();
            List<PlanTrabajo> Planes = ControlPlan.ObtenerPlanes();
            int i = 1;
            foreach (PlanTrabajo Plan in Planes)
            {
                this.combobox.Items.Insert(i, Plan.PLAN_Descripcion);
                this.combobox.Items[i].Value = Plan.PLAN_Interno.ToString();
                i++;
            }
        }

        [WebMethod]
        public static List<PartePlan> ObtenerParteActividades(string PLAN_Interno)
        {
            ControlPlan Plan = new ControlPlan();
            return Plan.ObtenerParteActividades(int.Parse(PLAN_Interno));
        }

        [WebMethod]
        public static List<Equipo> ObtenerEquipos()
        {
            ControlEquipo ControlEquipo = new ControlEquipo();
            List<Equipo> equipos = new List<Equipo>();
            string[] IDEquipos = items.Split('|');
            foreach (string ID in IDEquipos)
            {
                Equipo equipo = new Equipo(int.Parse(ID));
                equipo=ControlEquipo.ObtenerEquipoPorID(equipo);
                equipos.Add(equipo);
            }
            return equipos;   
        }

        [WebMethod]
        public static int InsertarMantenimientoInicial(string PLAN_Interno)
        {
            ControlPlan ControlPlan = new ControlPlan();
            PlanTrabajo Plan = new PlanTrabajo(int.Parse(PLAN_Interno));
            var opcItem=false;
            if (opc == "equi")
                opcItem = true;
            return ControlPlan.InsertarMantenimientoInicial(Plan, items, opcItem, 1);
        }
    }
}