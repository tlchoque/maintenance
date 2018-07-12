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
    public partial class Plan : System.Web.UI.Page
    {
        static string opc = null;
        static int Interno;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarNombreActividades();
                if (Request.QueryString["opc"] == "editar")
                {
                    this.opcURL.Value = "editar";
                    opc = "editar";
                    this.PLAN_Interno.Value = Request.QueryString["id"];
                    Interno = int.Parse(Request.QueryString["id"]);
                    this.PLAN_Descripcion.Value = this.ObtenerNombrePlan();
                    this.PART_Interno.Value = this.ObtenerNodoPadre().ToString();
                }
                //else
                //{
                //    this.opcURL.Value = "nuevo";
                //    opc = "nuevo";
                //}
            }
        }

        protected void LlenarNombreActividades()
        {
            ControlNombre Nombres = new ControlNombre();
            List<NombreActividad> ListaNombres = new List<NombreActividad>();
            ListaNombres = Nombres.ObtenerNombreActividades();
            int i = 1;
            foreach (NombreActividad nombre in ListaNombres)
            {
                this.Actividad.Items.Insert(i, nombre.NOMB_Descripcion);
                this.Actividad.Items[i].Value = nombre.NOMB_Interno.ToString();
                i++;
            }
        }

        [WebMethod]
        public static List<Nodo> ObtenerNodoPrincipal(string id)
        {
            List<Nodo> ArrayNodo = new List<Nodo>();
            ControlPlan Plan = new ControlPlan();
            int? Origen = Plan.ObtenerParteOrigenPLan(int.Parse(id));
            PartePlan ParteOrigen = new PartePlan();
            ParteOrigen.PART_Interno = Origen;
            ParteOrigen = Plan.ObtenerPartePorId(ParteOrigen);

            Nodo NodoP = new Nodo();
            NodoP.data = ParteOrigen.PART_Nombre;
            NodoP.state = "false";
            NodoP.attr = new AtributoNodo { id = Origen.ToString(), selected = false };
            NodoP.children = ObtenerNodos(NodoP);
            ArrayNodo.Add(NodoP);
            return ArrayNodo;
        }

        public static List<Nodo> ObtenerNodos(Nodo Nodo)
        {
            List<Nodo> ArrayNodo = new List<Nodo>();
            ControlPlan Plan = new ControlPlan();
            List<PartePlan> Partes = Plan.ObterPartesPorOrigen(int.Parse(Nodo.attr.id));
            if (Partes.Count() != 0)
            {
                foreach (PartePlan Parte in Partes)
                {
                    Nodo _Nodo = new Nodo();
                    _Nodo.data = Parte.PART_Nombre;
                    _Nodo.state = "false";
                    _Nodo.attr = new AtributoNodo { id = Parte.PART_Interno.ToString(), selected = true };
                    _Nodo.children = ObtenerNodos(_Nodo);
                    ArrayNodo.Add(_Nodo);
                }
                return ArrayNodo;
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static int InsertarNodo(int? idnodo, string name, int idnodopadre, Boolean op)
        {
            PartePlan Parte = new PartePlan();
            ControlPlan Plan = new ControlPlan();
            Parte.PART_Nombre = name;
            Parte.PLAN_Interno = Interno;
            if (op)
            {
                Parte.PART_Interno = null;
                Parte.PART_Origen = idnodopadre;
                return Plan.InsertaParte(Parte,1,null);
            }
            else
            {
                Parte.PART_Interno = idnodo;
                Parte.PART_Origen = idnodopadre;
                return Plan.InsertaParte(Parte, null, 1);
            }
        }

        [WebMethod]
        public static int EliminarNodosPorPadre(int id)
        {
            ControlPlan Plan = new ControlPlan();
            PartePlan PartePadre = new PartePlan(id);
            return Plan.EliminarNodosPorPadre(PartePadre,1);
        }

        protected int? ObtenerNodoPadre()
        {
            ControlPlan Plan = new ControlPlan();
            return Plan.ObtenerParteOrigenPLan(Interno);
        }

        [WebMethod]
        public static List<ActividadR> ObtenerActividadesPorParte(int PART_Interno)
        {
            ControlActividadR ActividadR = new ControlActividadR();
            return ActividadR.ObtenerActividadesPorParte(PART_Interno);
        }

        [WebMethod]
        public static int GuardarActvidad(string ACRU_Interno, string ACRU_Tipo, Boolean ACRU_ConCorte, Boolean ACRU_ConMedicion,
            string ACRU_UnidadMedicion,string ACRU_Frecuencia,string ACRU_UnidadFrecuencia, string PART_Interno, string NOMB_Interno)
        {
            ActividadR actividad = new ActividadR();
            if (ACRU_Interno == "") actividad.ACRU_Interno = null;
            else actividad.ACRU_Interno = int.Parse(ACRU_Interno);
            actividad.ACRU_Tipo = ACRU_Tipo;
            actividad.ACRU_ConCorte = ACRU_ConCorte;
            actividad.ACRU_ConMedicion = ACRU_ConMedicion;
            actividad.ACRU_UnidadMedicion = ACRU_UnidadMedicion;
            actividad.ACRU_Frecuencia = int.Parse(ACRU_Frecuencia);
            actividad.ACRU_UnidadFrecuencia = ACRU_UnidadFrecuencia;
            actividad.PART_Interno = int.Parse(PART_Interno);
            actividad.NOMB_Interno = int.Parse(NOMB_Interno);
            ControlActividadR ctrlActividad = new ControlActividadR();
            if (ACRU_Interno == "")
            {
                return ctrlActividad.InsertarActividadR(actividad, 1, null);
            }
            else if (ACRU_Interno != "")
            {
                return ctrlActividad.InsertarActividadR(actividad, null, 1);
            }
            else
                return 0;
        }

        [WebMethod]
        public static int EliminarActividad(int id)
        {
            ActividadR actividad = new ActividadR(id);
            ControlActividadR ctrlActividad = new ControlActividadR();
            return ctrlActividad.EdliminarActividadR(actividad, 1);
        }

        [WebMethod]
        public static List<PartePlan> ObtenerParteActividades()
        {
            ControlPlan Plan = new ControlPlan();
            return Plan.ObtenerParteActividades(Interno);
        }

        protected string ObtenerNombrePlan()
        {
            ControlPlan Plan = new ControlPlan();
            PlanTrabajo ObjPlan = new PlanTrabajo(Interno);
            ObjPlan = Plan.ObtenerPlanPorId(ObjPlan);
            return ObjPlan.PLAN_Descripcion;
        }

        [WebMethod]
        public static ActividadR ObtenerActividadPorId(string ACRU_Interno)
        {
            ActividadR Actividad = new ActividadR(int.Parse(ACRU_Interno));
            ControlActividadR ctrlActividad = new ControlActividadR();
            return ctrlActividad.ObtenerActividadPorId(Actividad);
        }
    }
}