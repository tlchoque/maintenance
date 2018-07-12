using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;

namespace Mantenimiento.ReglasNegocio
{
    public class ControlHistorialAR
    {
        public ControlHistorialAR() { }

        public int InsertarHistorialAR(HistorialAR HistorialAR, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            HistorialARDAO DataHistorialAR = new HistorialARDAO();
            return DataHistorialAR.InsertarHistorialAR(HistorialAR, AUDI_UsuarioCrea, AUDI_UsuarioEdita);
        }

        public int CantidadHistActividadesRutinariasEjecutadas(DateTime FechaInicio, DateTime FechaFin)
        {
            HistorialARDAO HistDAO = new HistorialARDAO();

            return HistDAO.ObtenerTotalNumRegistros_HistorialActividadesR_EntreFechas(FechaInicio, FechaFin);
        }
        public IEnumerable<ActividadR> ObtenerHistActividadesRutinariasEjecutadas(int TamanioPagina, int NumeroPagina, DateTime FechaInicio, DateTime FechaFin)
        {
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            HistorialARDAO HistDAO = new HistorialARDAO();
            List<ActividadR> Actividades = HistDAO.ObtenerActividadesR_Ejecutas_EntreFechas(TamanioPagina, NumeroPagina, FechaInicio, FechaFin);


            foreach (ActividadR objActividad in Actividades)
            {

                //Obtenemos el nombre del equipo/inmueble
                if (objActividad.EQUI_Interno != null)
                {
                    Equipo equipo = new Equipo();
                    EquipoDAO equipoDAO = new EquipoDAO();
                    equipo.EQUI_Interno = objActividad.EQUI_Interno;
                    equipo = equipoDAO.ObtenerEquipoPorID(equipo);
                    objActividad.EQUI_Descripcion = equipo.EQUI_Descripcion;
                    //obtenemos la localizacion actual del equipo
                    LocalizacionEquipo localEquipo = equipoDAO.ObtenerUltimaLocalizacionEquipo(equipo);
                    if (localEquipo != null)//si no pongo esto no funciona
                    {
                        objActividad.EQUI_LocalizacionExtendida = localEquipo.LOCA_NombreExtendido;
                    }
                    else
                    {
                        objActividad.EQUI_LocalizacionExtendida = null;
                    }
                }
                if (objActividad.LOCA_Interno != null)
                {
                    LocalizacionS localizacions = new LocalizacionS();
                    LocalizacionSDAO locasDAO = new LocalizacionSDAO();
                    localizacions.LOCA_Interno = objActividad.LOCA_Interno;
                    localizacions = locasDAO.ObtenerLocalizacion(localizacions);
                    objActividad.LOCA_NombreExtendido = localizacions.LOCA_NombreExtendido;
                }
                //calculamos los dias de retrazo que hubo
                if(!(objActividad.HIAR_FechaEjecutado ==null || objActividad.HIAR_FechaProgramado==null)){
                    objActividad.HIAR_Retrazo = (objActividad.HIAR_FechaEjecutado.Value - objActividad.HIAR_FechaProgramado.Value).Days;
                }
                
                ActividadesRutinarias.Add(objActividad);
            }


            return ActividadesRutinarias;
        }

        public int CantidadHistActividadesRutinariasProgramadasPorLocalizacion(LocalizacionS localizacion, DateTime FechaInicio, DateTime FechaFin)
        {
            HistorialARDAO HistDAO = new HistorialARDAO();

            return HistDAO.ObtenerTotalNumRegistros_ActividadesREjecutadasPorLocalizacion_EntreFechas(localizacion, FechaInicio, FechaFin);
        }
        public IEnumerable<ActividadR> ObtenerHistActividadesRutinariasEjecutadasPorLocalizacion(LocalizacionS localizacion, int TamanioPagina, int NumeroPagina, DateTime FechaInicio, DateTime FechaFin)
        {
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            HistorialARDAO HistDAO = new HistorialARDAO();
            List<ActividadR> Actividades = HistDAO.Obtener_ActividadesEjecutadasPorLocalizacion_EntreFechas(TamanioPagina, NumeroPagina, localizacion, FechaInicio, FechaFin);


            foreach (ActividadR objActividad in Actividades)
            {

                //Obtenemos el nombre del equipo/inmueble
                if (objActividad.EQUI_Interno != null)
                {
                    Equipo equipo = new Equipo();
                    EquipoDAO equipoDAO = new EquipoDAO();
                    equipo.EQUI_Interno = objActividad.EQUI_Interno;
                    equipo = equipoDAO.ObtenerEquipoPorID(equipo);
                    objActividad.EQUI_Descripcion = equipo.EQUI_Descripcion;
                    //obtenemos la localizacion actual del equipo
                    LocalizacionEquipo localEquipo = equipoDAO.ObtenerUltimaLocalizacionEquipo(equipo);
                    if (localEquipo != null)//si no pongo esto no funciona
                    {
                        objActividad.EQUI_LocalizacionExtendida = localEquipo.LOCA_NombreExtendido;
                    }
                    else
                    {
                        objActividad.EQUI_LocalizacionExtendida = null;
                    }
                }
                if (objActividad.LOCA_Interno != null)
                {
                    LocalizacionS localizacions = new LocalizacionS();
                    LocalizacionSDAO locasDAO = new LocalizacionSDAO();
                    localizacions.LOCA_Interno = objActividad.LOCA_Interno;
                    localizacions = locasDAO.ObtenerLocalizacion(localizacions);
                    objActividad.LOCA_NombreExtendido = localizacions.LOCA_NombreExtendido;
                }
                //calculamos los dias de retrazo que hubo
                objActividad.HIAR_Retrazo = (objActividad.HIAR_FechaEjecutado.Value - objActividad.HIAR_FechaProgramado.Value).Days;
                ActividadesRutinarias.Add(objActividad);
            }
            return ActividadesRutinarias;
        }

        
    }
}
