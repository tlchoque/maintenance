using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;
namespace Mantenimiento.ReglasNegocio
{
    public class ControlEjecucionActividadesR
    {
        public ControlEjecucionActividadesR()
        {
        }
        public string ObtenerTituloTablaDeActividadesRProgramadas(PeriodoProgramacion periodo)
        {
            DateTime? FechaLimite = null;//fecha del dia en que termina el periodo
            
            string Titulo = "";
            switch (periodo.PPRO_Periodo)
            {
                case "Diario":
                    FechaLimite = DateTime.Now.Date;
                    Titulo = "Actividades programadas para hoy (" + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    break;
                case "Semanal":
                    int dia = (int)DateTime.Now.Date.DayOfWeek;//dia de la semana de la fecha actual
                    int nextDia = Convert.ToInt32(periodo.PPRO_DiaSemana);
                    if (nextDia - dia <= 0)
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDia - dia + 7);

                    }
                    else
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDia - dia);
                    }
                    string[] strDia = new string[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
                    Titulo = "Actividades programadas para hoy hasta el siguiente " + strDia[(int)FechaLimite.Value.DayOfWeek] + " (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    break;
                case "Mensual":

                    int diaMes = (int)DateTime.Now.Date.Day;//dia del mes actual
                    int nextDiaMes = Convert.ToInt32(periodo.PPRO_DiaMes);
                    if (nextDiaMes - diaMes <= 0)
                    {

                        FechaLimite = DateTime.Now.Date.AddMonths(1);//agregamos un mes

                        FechaLimite = FechaLimite.Value.AddDays(nextDiaMes - diaMes);//quitamos los dias de diferencia
                        Titulo = "Actividades programadas para hoy hasta " + FechaLimite.Value.Day + " del siguiente mes (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    }
                    else
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDiaMes - diaMes);//solo agregamos los dias
                        Titulo = "Actividades programadas para hoy hasta " + FechaLimite.Value.Day + " de este mes (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    }
                    break;
            }
            //System.Windows.Forms.MessageBox.Show(periodo.PPRO_Periodo.ToString() + FechaLimite.ToString());
            return Titulo;
        }
        public int CantidadActividadesRutinariasProgramadas(PeriodoProgramacion periodo)
        {
            EjecucionActividadesRDAO EjecDAO = new EjecucionActividadesRDAO();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);

            return EjecDAO.ObtenerTotalNumRegistros_ActividadesR_Programadas_HastaFechaLimite(FechaLimite);
        }
        public IEnumerable<ActividadR> ObtenerActividadesRutinariasProgramadas(PeriodoProgramacion periodo, int TamanioPagina, int NumeroPagina)
        {
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);
            EjecucionActividadesRDAO ejecDAO = new EjecucionActividadesRDAO();
            List<ActividadR> Actividades = ejecDAO.ObtenerActividadesR_Programadas_HastaFechaLimite(TamanioPagina, NumeroPagina, FechaLimite);


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
                //calculamos los dias de retrazo

                if (objActividad.HIAR_FechaProgramado == null)
                {
                    objActividad.HIAR_Retrazo = (DateTime.Now - objActividad.HIAR_SiguienteFecha.Value).Days;
                }
                else
                {
                    objActividad.HIAR_Retrazo = (DateTime.Now - objActividad.HIAR_FechaProgramado.Value).Days;
                }
                ActividadesRutinarias.Add(objActividad);
            }


            return ActividadesRutinarias;
        }

        public int CantidadActividadesRutinariasProgramadasPorLocalizacion(LocalizacionS localizacion, PeriodoProgramacion periodo)
        {
            EjecucionActividadesRDAO ejecDAO = new EjecucionActividadesRDAO();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);

            return ejecDAO.ObtenerTotalNumRegistros_ActividadesRProgramadasPorLocalizacion_HastaFechaLimite(localizacion, FechaLimite);
        }
        public IEnumerable<ActividadR> ObtenerActividadesRutinariasProgramadasPorLocalizacion(PeriodoProgramacion periodo, LocalizacionS localizacion, int TamanioPagina, int NumeroPagina)
        {
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);
            EjecucionActividadesRDAO ejecDAO = new EjecucionActividadesRDAO();
            List<ActividadR> Actividades = ejecDAO.Obtener_ActividadesProgramadasPorLocalizacion_HastaFechaLimite(TamanioPagina, NumeroPagina, localizacion, FechaLimite);


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
                //calculamos los dias de retrazo
                if (objActividad.HIAR_FechaProgramado == null)
                {
                    objActividad.HIAR_Retrazo = (DateTime.Now - objActividad.HIAR_SiguienteFecha.Value).Days;
                }
                else
                {
                    objActividad.HIAR_Retrazo = (DateTime.Now - objActividad.HIAR_FechaProgramado.Value).Days;
                }
                ActividadesRutinarias.Add(objActividad);
            }
            return ActividadesRutinarias;
        }

        public int EjecutarActividadesRutinarias(string actividadesR, int AUDI_UsuarioEdita)
        {
            //Formato de actividadesR = HIAR_Interno;ACRU_Interno;HIAR_FechaEjecutadoAnterior|HIAR_Interno;ACRU_Interno;HIAR_FechaEjecutadoAnterior
            int result = 0;
            if (actividadesR != "")
            {
                EjecucionActividadesRDAO ejecDAO = new EjecucionActividadesRDAO();
                HistorialAR historialAR = null;
                HistorialAR AuxHistorialAR = null;
                ActividadR actividadR = null;
                string[] IDAct = actividadesR.Split('|');
                ControlGrupo CG = new ControlGrupo();
                foreach (string IDs in IDAct)
                {
                    string[] ID = IDs.Split(';');
                    
                    actividadR = new ActividadR();
                    ActividadRDAO actDAO = new ActividadRDAO();
                    actividadR.HIAR_Interno = Convert.ToInt32(ID[0]);

                    actividadR.HIAR_FechaEjecutado = DateTime.ParseExact(ID[2], "dd/MM/yyyy", null);
                    //actividadR.HIAR_FechaEjecutado = Convert.ToDateTime(ID[2]);

                    //Ejecutamos la actividad rutinaria
                    int res = ejecDAO.EjecutarActividadesRutinarias(actividadR, AUDI_UsuarioEdita);
                    
                    if (res > 0)
                    {
                        //ahora iniciamos una nueva actividad rutinaria
                        HistorialARDAO histAR_DAO = new HistorialARDAO();
                        AuxHistorialAR = new HistorialAR();//para datos auxiliares
                        AuxHistorialAR.HIAR_Interno = actividadR.HIAR_Interno;
                        AuxHistorialAR = histAR_DAO.ObtenerHistorialActividadRutinaria(AuxHistorialAR);
                        actividadR.ACRU_Interno = Convert.ToInt32(ID[1]);

                        historialAR = new HistorialAR();
                        historialAR.HIAR_Interno = null;
                        historialAR.ACRU_Interno = actividadR.ACRU_Interno;
                        //ultima fecha de ejecucion
                        historialAR.HIAR_FechaEjecucionAnterior = actividadR.HIAR_FechaEjecutado;
                        
                        //1)obtenemos las frecuencias de la actividad rutinaria
                        actividadR = actDAO.ObtenerActividadPorId(actividadR);
                        if (actividadR != null)
                        {

                            switch (actividadR.ACRU_UnidadFrecuencia)//calculamos las siguientes fechas
                            {
                                case "M":
                                    historialAR.HIAR_SiguienteFecha = historialAR.HIAR_FechaEjecucionAnterior.Value.AddMonths(actividadR.ACRU_Frecuencia);
                                    break;
                                case "S":

                                    historialAR.HIAR_SiguienteFecha = historialAR.HIAR_FechaEjecucionAnterior.Value.AddDays(actividadR.ACRU_Frecuencia * 7);
                                    break;
                                case "D":
                                    historialAR.HIAR_SiguienteFecha = historialAR.HIAR_FechaEjecucionAnterior.Value.AddDays(actividadR.ACRU_Frecuencia);
                                    break;
                            }
                            historialAR.HIAR_Estado = "I";
                            if (AuxHistorialAR.EQUI_Interno != null)
                            {
                                historialAR.LOCA_Interno = null;
                                historialAR.EQUI_Interno = AuxHistorialAR.EQUI_Interno;
                            }
                            else
                            {
                                historialAR.EQUI_Interno = null;
                                historialAR.LOCA_Interno = AuxHistorialAR.LOCA_Interno;
                            }
                            res = histAR_DAO.InsertarHistorialAR(historialAR, AUDI_UsuarioEdita, null);
                            if (res > 0)
                            {
                                result++;
                            }
                            else
                            {
                                //corregimos la ejecucion de la actividad---anulamos la ejecucion de la actividad
                                ejecDAO.AnularEjecutarActividadesRutinarias(actividadR, AUDI_UsuarioEdita);
                            }
                        }
                        else
                        {
                            //corregimos la ejecucion de la actividad---anulamos la ejecucion de la actividad
                            ejecDAO.AnularEjecutarActividadesRutinarias(actividadR, AUDI_UsuarioEdita);
                        }
                    }

                }
                return result;
            }
            else
                return 0;
        }

        public DateTime? ObtenerFechaLimite(PeriodoProgramacion periodo)
        {
            DateTime? FechaLimite = null;//fecha del dia en que termina el periodo
            switch (periodo.PPRO_Periodo)
            {
                case "Diario":
                    FechaLimite = DateTime.Now.Date;
                    break;
                case "Semanal":
                    int dia = (int)DateTime.Now.Date.DayOfWeek;//dia de la semana de la fecha actual
                    int nextDia = Convert.ToInt32(periodo.PPRO_DiaSemana);
                    if (nextDia - dia <= 0)
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDia - dia + 7);
                    }
                    else
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDia - dia);
                    }
                    break;
                case "Mensual":

                    int diaMes = (int)DateTime.Now.Date.Day;//dia del mes actual
                    int nextDiaMes = Convert.ToInt32(periodo.PPRO_DiaMes);
                    if (nextDiaMes - diaMes <= 0)
                    {

                        FechaLimite = DateTime.Now.Date.AddMonths(1);//agregamos un mes

                        FechaLimite = FechaLimite.Value.AddDays(nextDiaMes - diaMes);//quitamos los dias de diferencia

                    }
                    else
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDiaMes - diaMes);//solo agregamos los dias
                    }
                    break;
            }
            //System.Windows.Forms.MessageBox.Show(periodo.PPRO_Periodo.ToString() + FechaLimite.ToString());
            return FechaLimite;
        }
    }
}
