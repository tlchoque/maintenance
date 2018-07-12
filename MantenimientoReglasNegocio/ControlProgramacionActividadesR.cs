using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;
using System.Globalization;
namespace Mantenimiento.ReglasNegocio
{
    public class ControlProgramacionActividadesR
    {
        public int CantidadRegistrosParaProgramarActividades(PeriodoProgramacion periodo){
            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);
            
            return progDAO.ObtenerTotalNumRegistros_ActividadesR_Iniciadas_HastaFechaLimite(FechaLimite);
        }      
        public IEnumerable<ActividadR> ObtenerActividadesProgramablesIniciadas(PeriodoProgramacion periodo, int TamanioPagina, int NumeroPagina)
        {
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);

            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();           
            List<ActividadR> Actividades = progDAO.ObtenerActividadesR_Iniciadas_HastaFechaLimite(TamanioPagina, NumeroPagina, FechaLimite);

            foreach (ActividadR objActividad in Actividades)
            {
                //Obtenemos el nombre del equipo/inmueble
                if (objActividad.EQUI_Interno != null)
                {
                    Equipo equipo = new Equipo();
                    EquipoDAO equipoDAO = new EquipoDAO();
                    equipo.EQUI_Interno = objActividad.EQUI_Interno;
                    equipo = equipoDAO.ObtenerEquipoPorID(equipo);
                    //System.Windows.Forms.MessageBox.Show(equipo.EQUI_Descripcion);


                    if (equipo == null) continue;

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

        public int CantidadRegistrosParaProgramarActividadesPorLocalizacion(LocalizacionS localizacion,PeriodoProgramacion periodo)
        {
            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);

            return progDAO.ObtenerTotalNumRegistros_ActividadesRIniciadasPorLocalizacion_HastaFechaLimite(localizacion,FechaLimite);
        }
        public IEnumerable<ActividadR> ObtenerActividadesProgramablesIniciadasPorLocalizacion(PeriodoProgramacion periodo, LocalizacionS localizacion, int TamanioPagina, int NumeroPagina)
        { 
            //here
            List<ActividadR> ActividadesRutinarias = new List<ActividadR>();
            DateTime? FechaLimite = ObtenerFechaLimite(periodo);
            
            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
            List<ActividadR> Actividades = progDAO.Obtener_ActividadesIniciadasPorLocalizacion_HastaFechaLimite(TamanioPagina, NumeroPagina,localizacion, FechaLimite);

            
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

        public DateTime? ObtenerFechaLimite(PeriodoProgramacion periodo){
            DateTime? FechaLimite = null;//fecha del dia en que termina el periodo
            switch (periodo.PPRO_Periodo)
            {
                case "Diario":
                    FechaLimite = DateTime.Now.Date;
                    break;
                case "Semanal":
                    int dia = (int)DateTime.Now.Date.DayOfWeek;//dia de la semana de la fecha actual
                    int nextDia = Convert.ToInt32( periodo.PPRO_DiaSemana);
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

        public string ObtenerTituloTablaDeActividadesProgramables(PeriodoProgramacion periodo)
        {
            DateTime? FechaLimite = null;//fecha del dia en que termina el periodo
            //Equipos/Inmuebles por atender de hoy al Domingo (del 18/09/2013 al 22/09/2013)
            string Titulo = "";
            switch (periodo.PPRO_Periodo)
            {
                case "Diario":
                    FechaLimite = DateTime.Now.Date;
                    Titulo = "Equipos/Inmuebles por atender hoy ("+FechaLimite.Value.ToString("dd/MM/yyyy")+")";
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
                    string[] strDia = new string[] {"Domingo","Lunes","Martes","Miércoles","Jueves","Viernes","Sábado" };
                    Titulo = "Equipos/Inmuebles por atender de hoy al siguiente " + strDia[(int)FechaLimite.Value.DayOfWeek] + " (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    break;
                case "Mensual":

                    int diaMes = (int)DateTime.Now.Date.Day;//dia del mes actual
                    int nextDiaMes = Convert.ToInt32(periodo.PPRO_DiaMes);
                    if (nextDiaMes - diaMes <= 0)
                    {

                        FechaLimite = DateTime.Now.Date.AddMonths(1);//agregamos un mes

                        FechaLimite = FechaLimite.Value.AddDays(nextDiaMes - diaMes);//quitamos los dias de diferencia
                        Titulo = "Equipos/Inmuebles por atender de hoy al " + FechaLimite.Value.Day + " del siguiente mes (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    }
                    else
                    {
                        FechaLimite = DateTime.Now.Date.AddDays(nextDiaMes - diaMes);//solo agregamos los dias
                        Titulo = "Equipos/Inmuebles por atender de hoy al " + FechaLimite.Value.Day + " de este mes (del " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " al " + FechaLimite.Value.ToString("dd/MM/yyyy") + ")";
                    }
                    break;
            }
            //System.Windows.Forms.MessageBox.Show(periodo.PPRO_Periodo.ToString() + FechaLimite.ToString());
            return Titulo;
        }
        public PeriodoProgramacion ObtenerPeriodoDeProgramacionDeActividades()
        {
            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
            return progDAO.ObtenerPeriodoProgramacionActivo();
        }
        public int EditarPeriodoDeProgramacionDeActividades(PeriodoProgramacion periodo,int AUDI_UsuarioEdita)
        {
            ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
            return progDAO.EditarPeriodoDeProgramacionActividades(periodo,AUDI_UsuarioEdita);
        }

        public int EditarFechasProgramadasActividadesRutinarias(string fechasSiguientes, int AUDI_UsuarioEdita)
        {
            //formato de 'fechasSiguientes': 143;03/10/2013|139;03/10/2013|131;09/10/2013|127;09/10/2013
            int result = 0;
            if (fechasSiguientes != "")
            {
                ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
                HistorialAR historialAR = null;
                string[] IDs_Con_fechas = fechasSiguientes.Split('|');
                ControlGrupo CG = new ControlGrupo();
                foreach (string IDs_Fechas in IDs_Con_fechas)
                {
                    historialAR = new HistorialAR();
                    string[] fecha = IDs_Fechas.Split(';');
                    historialAR.HIAR_Interno = Convert.ToInt32(fecha[0]);

                    //historialAR.HIAR_FechaProgramado = Convert.ToDateTime(fecha[1]);

                    historialAR.HIAR_FechaProgramado = DateTime.ParseExact(fecha[1], "dd/MM/yyyy", null);
                    int res=progDAO.EditarFechasProgramadasActividadesRutinarias(historialAR, AUDI_UsuarioEdita);
                    if (res > 0)
                        result++;
                }
                return result;
            }
            else
                return 0;
        }

        public int ProgramarActividadesRutinarias(string actividadesR, int AUDI_UsuarioEdita)
        {
            //Formato de actividadesR = HIAR_Interno|HIAR_Interno|HIAR_Interno
            int result = 0;
            if (actividadesR != "")
            {
                ProgramacionActividadesDAO progDAO = new ProgramacionActividadesDAO();
                HistorialAR historialAR = null;
                string[] IDAct = actividadesR.Split('|');
                ControlGrupo CG = new ControlGrupo();
                foreach (string IDs in IDAct)
                {
                    historialAR = new HistorialAR();
                    ActividadRDAO actDAO = new ActividadRDAO();
                    historialAR.HIAR_Interno = Convert.ToInt32(IDs);
                    //para el numero de la semana
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;
                    int anio = DateTime.Now.Year;
                   // System.Windows.Forms.MessageBox.Show(anio.ToString());
                    int NumSemana = cal.GetWeekOfYear(DateTime.Now.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                    //System.Windows.Forms.MessageBox.Show(NumSemana.ToString());
                    
                    //Obtenemos el Id de la semana de programacion
                    ProgramaSemanal programa = new ProgramaSemanal();
                    programa.PERI_NumSemana = NumSemana;
                    programa.PERI_Anio = anio;

                    ProgramaDAO progSemDAO = new ProgramaDAO();
                    int IDprog = progSemDAO.ObtenerIDProgramaSemanal(programa);
                    
                    if (IDprog > 0)
                    {
                        historialAR.PERI_Interno = IDprog;
                    }
                    else
                    {
                        IDprog = progSemDAO.InsertarProgramaSemanal(programa);
                        if (IDprog > 0)
                            historialAR.PERI_Interno = IDprog;

                    }
                    
                    //programamos la actividad rutinaria
                    int res = progDAO.ProgramarActividadesRutinarias(historialAR, AUDI_UsuarioEdita);
                    
                    if (res > 0)
                        result++;
                }
                return result;
            }
            else
                return 0;
        }

        
    }
}
