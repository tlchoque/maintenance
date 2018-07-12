using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;

namespace Mantenimiento.ReglasNegocio
{
    public class ControlPlan
    {
        public ControlPlan() { }

        public int ObtenerTotalPlanes()
        {
            PlanTrabajoDAO DPlan = new PlanTrabajoDAO();
            return DPlan.ObtenerTotalPlanes();
        }

        public List<PlanTrabajo> ObtenerPlanesP(int TamanioPagina, int NumeroPagina)
        {
            PlanTrabajoDAO DPlan = new PlanTrabajoDAO();
            return DPlan.ObtenerPlanesP(TamanioPagina, NumeroPagina);
        }

        public List<PlanTrabajo> ObtenerPlanesFiltradoPorNombre(int TamanioPagina, int NumeroPagina, string str)
        {
            PlanTrabajoDAO DPlan = new PlanTrabajoDAO();
            return DPlan.ObtenerPlanesFiltradoPorNombre(TamanioPagina, NumeroPagina, str);
            
        }

        public int InsertarPlan(PlanTrabajo PlanTrabajo, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            PlanTrabajoDAO DataPlanTrabajo = new PlanTrabajoDAO();
            int PLAN_Interno = DataPlanTrabajo.InsertarPlanTrabajo(PlanTrabajo, AUDI_UsuarioCrea, AUDI_UsuarioEdita);
            PartePlanDAO PartePlanDAO = new PartePlanDAO();
            PartePlan PartePlan = new PartePlan();
            int ResultadoParte = 0;
            if (AUDI_UsuarioCrea == null)
            {
                PartePlan.PART_Interno = this.ObtenerParteOrigenPLan(PLAN_Interno);
                PartePlan.PART_Nombre = PlanTrabajo.PLAN_Descripcion;
                PartePlan.PART_Origen = null;
                PartePlan.PLAN_Interno = PLAN_Interno;
                ResultadoParte = PartePlanDAO.InsertarPartePlan(PartePlan, null, AUDI_UsuarioEdita);
            }
            else 
            {
                PartePlan.PART_Interno = null;
                PartePlan.PART_Nombre = PlanTrabajo.PLAN_Descripcion;
                PartePlan.PART_Origen = null;
                PartePlan.PLAN_Interno = PLAN_Interno;
                ResultadoParte = PartePlanDAO.InsertarPartePlan(PartePlan, AUDI_UsuarioCrea, null);
            }
            if (ResultadoParte > 0)
                return PLAN_Interno;
            else
                return 0;
        }

        public List<PlanTrabajo> ObtenerPlanes()
        {
            PlanTrabajoDAO DataPlanTrabajo = new PlanTrabajoDAO();
            return DataPlanTrabajo.ObtenerPlanesTrabajo();
        }

        public PlanTrabajo ObtenerPlanPorId(PlanTrabajo PlanTrabajo)
        {
            PlanTrabajoDAO DataPlanTrabajo = new PlanTrabajoDAO();
            return DataPlanTrabajo.ObtenerPlanTrabajoPorId(PlanTrabajo);
        }

        public int EliminarPlan(PlanTrabajo PlanTrabajo, int AUDI_UsuarioEdita)
        {
            PlanTrabajoDAO DataPlanTrabajo = new PlanTrabajoDAO();
            return DataPlanTrabajo.EliminarPlanTrabajo(PlanTrabajo, AUDI_UsuarioEdita);
        }

        public List<PartePlan> ObtenerParteActividades(int PLAN_Interno)
        {
            PartePlanDAO DataPartePlan = new PartePlanDAO();
            ActividadRDAO DataActividad = new ActividadRDAO();
            List<PartePlan> Partes = DataPartePlan.ObtenerPartesPorPlan(PLAN_Interno);
            List<PartePlan> ParteActividades = new List<PartePlan>();

            foreach (PartePlan objParte in Partes)
            {
                PartePlan PartePlan = null;
                PartePlan = objParte;
                List<ActividadR> Actividades= null;
                Actividades = DataActividad.ObtenerActividadesParte(Convert.ToInt32(PartePlan.PART_Interno));
                if (Actividades.Count != 0)
                {
                    PartePlan.PART_Actividades = Actividades;
                }
                else
                {
                    PartePlan.PART_Actividades = null;
                }
                ParteActividades.Add(PartePlan);
            }
            return ParteActividades;
        }

        public int CopiarPlan(PlanTrabajo PlanTrabajo, string PLAN_Descripcion, int AUDI_UsuarioCrea)
        {
            //ControlActividadR ControlActividad = new ControlActividadR();
            ActividadRDAO DataActividad = new ActividadRDAO();
            PlanTrabajo Plan = this.ObtenerPlanPorId(PlanTrabajo);
            PlanTrabajo PlanCopia = new PlanTrabajo();
            PlanCopia.PLAN_Interno = null;
            PlanCopia.PLAN_Descripcion = PLAN_Descripcion;
            PlanCopia.PLAN_Regimen = Plan.PLAN_Regimen;
            PlanCopia.PLAN_UnidadLecturas = Plan.PLAN_UnidadLecturas;
            int PLAN_InternoCopia = this.InsertarPlan(PlanCopia,AUDI_UsuarioCrea,null);
            int? PART_Interno = this.ObtenerParteOrigenPLan(Convert.ToInt32(PlanTrabajo.PLAN_Interno));
            int? PART_InternoCopia = this.ObtenerParteOrigenPLan(PLAN_InternoCopia);

            //List<ActividadR> Actividades = ControlActividad.ObtenerActividadesPorParte(Convert.ToInt32(PART_Interno));
            List<ActividadR> Actividades = DataActividad.ObtenerActividadesParte(Convert.ToInt32(PART_Interno));
            foreach (ActividadR Actividad in Actividades)
            {
                ActividadR ActividadCopia = new ActividadR();
                ActividadCopia.ACRU_Interno = null;
                ActividadCopia.ACRU_Descripcion = Actividad.ACRU_Descripcion;
                ActividadCopia.ACRU_Tipo = Actividad.ACRU_Tipo;
                ActividadCopia.ACRU_ConCorte = Actividad.ACRU_ConCorte;
                ActividadCopia.ACRU_ConMedicion = Actividad.ACRU_ConMedicion;
                ActividadCopia.ACRU_UnidadMedicion = Actividad.ACRU_UnidadMedicion;
                ActividadCopia.ACRU_Frecuencia = Actividad.ACRU_Frecuencia;
                ActividadCopia.ACRU_UnidadFrecuencia = Actividad.ACRU_UnidadFrecuencia;
                ActividadCopia.PART_Interno = Convert.ToInt32(PART_InternoCopia);
                ActividadCopia.NOMB_Interno = Actividad.NOMB_Interno;
                //int res = ControlActividad.InsertarActividadR(ActividadCopia, AUDI_UsuarioCrea, null);
                int res = DataActividad.InsertarActividad(ActividadCopia, AUDI_UsuarioCrea, null);
            }
            
            CopiarPartesActividades(Convert.ToInt32(PART_Interno), Convert.ToInt32(PART_InternoCopia), PLAN_InternoCopia, AUDI_UsuarioCrea);
            return 1;
        }

        protected void CopiarPartesActividades(int PART_Origen, int PART_OrigenCopia, int PLAN_InternoCopia, int AUDI_UsuarioCrea)
        {
            PartePlanDAO DataParte = new PartePlanDAO();
            //ControlActividadR ControlActividad = new ControlActividadR();
            ActividadRDAO DataActividad = new ActividadRDAO();
            List<PartePlan> Partes = this.ObterPartesPorOrigen(PART_Origen);            
            foreach (PartePlan Parte in Partes)
            {
                PartePlan ParteCopia = new PartePlan();
                ParteCopia.PART_Interno = null;
                ParteCopia.PART_Nombre = Parte.PART_Nombre;
                ParteCopia.PART_Origen = PART_OrigenCopia;
                ParteCopia.PLAN_Interno = PLAN_InternoCopia;
                int PART_InternoCopia=DataParte.InsertarPartePlan(ParteCopia,AUDI_UsuarioCrea,null);
                //List<ActividadR> Actividades = ControlActividad.ObtenerActividadesPorParte(Convert.ToInt32(Parte.PART_Interno));
                List<ActividadR> Actividades = DataActividad.ObtenerActividadesParte(Convert.ToInt32(Parte.PART_Interno));
                foreach (ActividadR Actividad in Actividades)
                {
                    ActividadR ActividadCopia = new ActividadR();
                    ActividadCopia.ACRU_Interno = null;
                    ActividadCopia.ACRU_Descripcion = Actividad.ACRU_Descripcion;
                    ActividadCopia.ACRU_Tipo = Actividad.ACRU_Tipo;
                    ActividadCopia.ACRU_ConCorte = Actividad.ACRU_ConCorte;
                    ActividadCopia.ACRU_ConMedicion = Actividad.ACRU_ConMedicion;
                    ActividadCopia.ACRU_UnidadMedicion = Actividad.ACRU_UnidadMedicion;
                    ActividadCopia.ACRU_Frecuencia = Actividad.ACRU_Frecuencia;
                    ActividadCopia.ACRU_UnidadFrecuencia = Actividad.ACRU_UnidadFrecuencia;
                    ActividadCopia.PART_Interno = PART_InternoCopia;
                    ActividadCopia.NOMB_Interno = Actividad.NOMB_Interno;
                    //int res = ControlActividad.InsertarActividadR(ActividadCopia, AUDI_UsuarioCrea, null);
                    int res = DataActividad.InsertarActividad(ActividadCopia, AUDI_UsuarioCrea, null);
                }
                CopiarPartesActividades(Convert.ToInt32(Parte.PART_Interno), PART_InternoCopia, PLAN_InternoCopia, AUDI_UsuarioCrea);
            }
        }

        public int InsertarMantenimientoInicial(PlanTrabajo PlanTrabajo, string items,Boolean opc, int? AUDI_UsuarioCrea)
        {
            LocalizacionSDAO DataLocalizacion = new LocalizacionSDAO();
            EquipoDAO DataEquipo = new EquipoDAO();
            ActividadRDAO DataActividad = new ActividadRDAO();
            HistorialARDAO DataHistorialAR = new HistorialARDAO();
            string[] IDItems = items.Split('|');
            List<ActividadR> Actividades = DataActividad.ObtenerActividadesPlan(PlanTrabajo);
            int res = 0;
            foreach (string ID in IDItems)
            {
                //if (opc)
                //{
                //    Equipo Equipo = new Equipo(int.Parse(ID));
                //    Equipo.EQUI_EstadoMantenimiento = "I";
                //    Equipo.PLAN_Interno = PlanTrabajo.PLAN_Interno;
                //    res = DataEquipo.InsertarEquipo(Equipo, null, AUDI_UsuarioCrea);
                //}
                //else 
                //{
                //    LocalizacionS Localizacion = new LocalizacionS(int.Parse(ID));
                //    Localizacion.LOCA_EstadoMantenimiento = "I";
                //    Localizacion.PLAN_Interno = PlanTrabajo.PLAN_Interno;
                //    res = DataLocalizacion.InsertarLocalizacion(Localizacion, null, AUDI_UsuarioCrea);
                //}

                foreach (ActividadR Actividad in Actividades)
                {
                    HistorialAR HistorialAR = new HistorialAR();
                    HistorialAR.ACRU_Interno = Actividad.ACRU_Interno;
                    HistorialAR.HIAR_FechaEjecucionAnterior = DateTime.Now;
                    switch (Actividad.ACRU_UnidadFrecuencia)
                    {
                        case "M":
                            HistorialAR.HIAR_SiguienteFecha = DateTime.Now.AddMonths(Actividad.ACRU_Frecuencia);
                            break;
                        case "S":
                            HistorialAR.HIAR_SiguienteFecha = DateTime.Now.AddDays(Actividad.ACRU_Frecuencia * 7);
                            break;
                        case "D":
                            HistorialAR.HIAR_SiguienteFecha = DateTime.Now.AddDays(Actividad.ACRU_Frecuencia);
                            break;
                    }
                    HistorialAR.HIAR_Estado = "I";
                    if (opc)
                        HistorialAR.EQUI_Interno = int.Parse(ID);
                    else
                        HistorialAR.LOCA_Interno = int.Parse(ID);
                    res = DataHistorialAR.InsertarHistorialAR(HistorialAR, AUDI_UsuarioCrea,null);
                }
            }
            return res;
        }

        #region PartesPLan
        public int? ObtenerParteOrigenPLan(int PLAN_Interno)
        {            
            PartePlanDAO PartePlanDAO = new PartePlanDAO();
            int? ParteOrigen = (from fila in PartePlanDAO.ObtenerPartesPorPlan(PLAN_Interno)
                                where fila.PLAN_Interno == PLAN_Interno && fila.PART_Origen == null
                                select fila.PART_Interno).FirstOrDefault();
            return ParteOrigen;            
        }

        public PartePlan ObtenerPartePorId(PartePlan Parte)
        {
            PartePlanDAO PartePlanDAO = new PartePlanDAO();
            return PartePlanDAO.ObtenerPartesPorId(Parte);
        }


        public List<PartePlan> ObterPartesPorOrigen(int PART_Interno)
        {
            PartePlanDAO DataParte = new PartePlanDAO();
            return DataParte.ObtenerPartesPorOrigen(PART_Interno);
        }

        public int InsertaParte(PartePlan Parte, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            PartePlanDAO DataParte = new PartePlanDAO();
            return DataParte.InsertarPartePlan(Parte, AUDI_UsuarioCrea, AUDI_UsuarioEdita);
        }

        public int EliminarNodosPorPadre(PartePlan PartePlan, int AUDI_UsuarioEdita)
        {
            PartePlanDAO DataParte = new PartePlanDAO();
            return DataParte.EliminarPartes(PartePlan, AUDI_UsuarioEdita);
        }
        #endregion


    }
}
