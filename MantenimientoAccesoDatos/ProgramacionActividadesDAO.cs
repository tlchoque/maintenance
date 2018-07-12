using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class ProgramacionActividadesDAO:DataAccess
    {
        public ProgramacionActividadesDAO() { }
        #region Para la configuracion del periodo de programacion
        public PeriodoProgramacion ObtenerPeriodoProgramacionActivo()
        {
            PeriodoProgramacion periodo = null;

            string StoredProcedure = "PA_ObtenerPeriodoProgramacion";

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                if (dr.Read())
                {
                    try
                    {
                        periodo = new PeriodoProgramacion();
                        //PPRO_Interno,PPRO_Periodo,PPRO_DiaSemana,PPRO_DiaMes
                        periodo.PPRO_Interno = (int)(dr["PPRO_Interno"]);
                        periodo.PPRO_Periodo = dr["PPRO_Periodo"] == System.DBNull.Value ? null : (string)(dr["PPRO_Periodo"]);
                        periodo.PPRO_DiaSemana = dr["PPRO_DiaSemana"] == System.DBNull.Value ? null : (int?)(dr["PPRO_DiaSemana"]);
                        periodo.PPRO_DiaMes = dr["PPRO_DiaMes"] == System.DBNull.Value ? null : (int?)(dr["PPRO_DiaMes"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de periodo de programacion a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el registro del periodo de programación de actividades", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return periodo;
        }
        public int EditarPeriodoDeProgramacionActividades(PeriodoProgramacion periodo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            //DbParameter param = dpf.CreateParameter();
            //param.Value = periodo.PPRO_Interno;
            //param.ParameterName = "PPRO_Interno";
            //parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            if (periodo.PPRO_Periodo == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = periodo.PPRO_Periodo;
            param1.ParameterName = "PPRO_Periodo";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (periodo.PPRO_DiaSemana == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = periodo.PPRO_DiaSemana;
            param2.ParameterName = "PPRO_DiaSemana";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            if (periodo.PPRO_DiaMes == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = periodo.PPRO_DiaMes;
            param3.ParameterName = "PPRO_DiaMes";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            param4.Value = AUDI_UsuarioEdita;
            param4.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param4);
            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_EditarPeriodoProgramacion", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el periodo de programación de actividades", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        #endregion

        public int ObtenerTotalNumRegistros_ActividadesR_Iniciadas_HastaFechaLimite(DateTime? FechaLimite)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = DbType.DateTime;
            if (FechaLimite == null) param.Value = DBNull.Value; else param.Value = FechaLimite;
            param.ParameterName = "FechaLimite";
            parametros.Add(param);

            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = Convert.ToInt32(EjecuteEscalar("PA_Total_HistorialAR_Iniciadas_HastaFechaLimite", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de registros de Historial de Actividades rutinarias hasta una fecha límite", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<ActividadR> ObtenerActividadesR_Iniciadas_HastaFechaLimite(int TamanioPagina, int NumeroPagina, DateTime? FechaLimite)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesIniciadasParaProgramar";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = TamanioPagina;
            param.ParameterName = "TamanioPagina";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = NumeroPagina;
            param1.ParameterName = "NumeroPagina";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.DbType = DbType.DateTime;
            if (FechaLimite == null) param2.Value = DBNull.Value; else param2.Value = FechaLimite;           
            param2.ParameterName = "FechaLimite";
            parametros.Add(param2);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {

                    ActividadR actividadRutinaria = null;

                    try
                    {
                        //NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia
                        actividadRutinaria = new ActividadR();
                        actividadRutinaria.NOMB_Interno = (int)dr["NOMB_Interno"];
                        actividadRutinaria.NOMB_Descripcion = (string)dr["NOMB_Descripcion"];
                        actividadRutinaria.ACRU_Frecuencia = (int)dr["ACRU_Frecuencia"];
                        actividadRutinaria.ACRU_UnidadFrecuencia = (string)dr["ACRU_UnidadFrecuencia"];

                        actividadRutinaria.HIAR_Interno = (int)dr["HIAR_Interno"];
                        
                        actividadRutinaria.HIAR_FechaEjecucionAnterior = dr["HIAR_FechaEjecucionAnterior"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaEjecucionAnterior"];
                        actividadRutinaria.HIAR_FechaEjecutado = dr["HIAR_FechaEjecutado"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaEjecutado"];
                        actividadRutinaria.HIAR_SiguienteFecha = dr["HIAR_SiguienteFecha"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_SiguienteFecha"];
                        actividadRutinaria.HIAR_FechaProgramado = dr["HIAR_FechaProgramado"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaProgramado"];
                        //actividadRutinaria.HIAR_Ejecutor = dr["HIAR_Ejecutor"] == System.DBNull.Value ? null : (string)(dr["HIAR_Ejecutor"]);
                        //actividadRutinaria.HIAR_Observacion = dr["HIAR_Observacion"] == System.DBNull.Value ? null : (string)(dr["HIAR_Observacion"]);
                        //actividadRutinaria.HIAR_Estado = (string)(dr["HIAR_Estado"]);
                        actividadRutinaria.ACRU_Interno = dr["ACRU_Interno"] == System.DBNull.Value ? null : (int?)(dr["ACRU_Interno"]);
                        actividadRutinaria.LOCA_Interno = dr["LOCA_Interno"] == System.DBNull.Value ? null : (int?)(dr["LOCA_Interno"]);
                        actividadRutinaria.EQUI_Interno = dr["EQUI_Interno"] == System.DBNull.Value ? null : (int?)(dr["EQUI_Interno"]);
                        //actividadRutinaria.PERI_Interno = dr["PERI_Interno"] == System.DBNull.Value ? null : (int?)(dr["PERI_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Historial de  Actividad Rutinaria Iniciadas a Objecto", ex);
                    }
                    historialARs.Add(actividadRutinaria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Historial de  Actividad Rutinaria Iniciadas - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialARs;
        }

        public int ObtenerTotalNumRegistros_ActividadesRIniciadasPorLocalizacion_HastaFechaLimite(LocalizacionS localizacion, DateTime? FechaLimite)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = DbType.DateTime;
            if (FechaLimite == null) param.Value = DBNull.Value; else param.Value = FechaLimite;
            param.ParameterName = "FechaLimite";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = DbType.Int32;
            if (localizacion.LOCA_Interno == null) param1.Value = DBNull.Value; else param1.Value = localizacion.LOCA_Interno;
            param1.ParameterName = "LOCA_Interno";
            parametros.Add(param1);
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = Convert.ToInt32(EjecuteEscalar("PA_TotalActividadesRutinariasIniciadasPorLocalizacionConFechaLimite", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de registros de  Actividades rutinarias Iniciadas hasta una fecha límite", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<ActividadR> Obtener_ActividadesIniciadasPorLocalizacion_HastaFechaLimite(int TamanioPagina, int NumeroPagina,LocalizacionS localizacion, DateTime? FechaLimite)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesRutinariasIniciadasPorLocalizacionConFechaLimite";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = TamanioPagina;
            param.ParameterName = "TamanioPagina";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = NumeroPagina;
            param1.ParameterName = "NumeroPagina";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.DbType = DbType.DateTime;
            if (FechaLimite == null) param2.Value = DBNull.Value; else param2.Value = FechaLimite;
            param2.ParameterName = "FechaLimite";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.DbType = DbType.Int32;
            if (localizacion.LOCA_Interno == null) param3.Value = DBNull.Value; else param3.Value = localizacion.LOCA_Interno;
            param3.ParameterName = "LOCA_Interno";
            parametros.Add(param3);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {

                    ActividadR actividadRutinaria = null;

                    try
                    {
    //                    NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,
    //HIAR_SiguienteFecha,ACRU_Interno,EQUI_Interno
                        actividadRutinaria = new ActividadR();
                        actividadRutinaria.NOMB_Interno = (int)dr["NOMB_Interno"];
                        actividadRutinaria.NOMB_Descripcion = (string)dr["NOMB_Descripcion"];
                        actividadRutinaria.ACRU_Frecuencia = (int)dr["ACRU_Frecuencia"];
                        actividadRutinaria.ACRU_UnidadFrecuencia = (string)dr["ACRU_UnidadFrecuencia"];

                        actividadRutinaria.HIAR_Interno = (int)dr["HIAR_Interno"];
                        actividadRutinaria.HIAR_FechaEjecucionAnterior = dr["HIAR_FechaEjecucionAnterior"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaEjecucionAnterior"];
                        actividadRutinaria.HIAR_FechaEjecutado = dr["HIAR_FechaEjecutado"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaEjecutado"];
                        actividadRutinaria.HIAR_SiguienteFecha = dr["HIAR_SiguienteFecha"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_SiguienteFecha"];
                        actividadRutinaria.HIAR_FechaProgramado = dr["HIAR_FechaProgramado"] == System.DBNull.Value ? null : (DateTime?)dr["HIAR_FechaProgramado"];
                        //actividadRutinaria.HIAR_Ejecutor = dr["HIAR_Ejecutor"] == System.DBNull.Value ? null : (string)(dr["HIAR_Ejecutor"]);
                        //actividadRutinaria.HIAR_Observacion = dr["HIAR_Observacion"] == System.DBNull.Value ? null : (string)(dr["HIAR_Observacion"]);
                        //actividadRutinaria.HIAR_Estado = (string)(dr["HIAR_Estado"]);
                        actividadRutinaria.ACRU_Interno = dr["ACRU_Interno"] == System.DBNull.Value ? null : (int?)(dr["ACRU_Interno"]);
                        actividadRutinaria.LOCA_Interno = dr["LOCA_Interno"] == System.DBNull.Value ? null : (int?)(dr["LOCA_Interno"]);
                        actividadRutinaria.EQUI_Interno = dr["EQUI_Interno"] == System.DBNull.Value ? null : (int?)(dr["EQUI_Interno"]);
                        //actividadRutinaria.PERI_Interno = dr["PERI_Interno"] == System.DBNull.Value ? null : (int?)(dr["PERI_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Historial de  Actividad Rutinaria Iniciadas a Objecto", ex);
                    }
                    historialARs.Add(actividadRutinaria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Historial de  Actividad Rutinaria Iniciadas - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialARs;
        }

        public int EditarFechasProgramadasActividadesRutinarias(HistorialAR historialAR, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = historialAR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = historialAR.HIAR_FechaProgramado;
            param1.ParameterName = "HIAR_FechaProgramado";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.Value = AUDI_UsuarioEdita;
            param2.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param2);
            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_EditarFechaProgramadaActividadRutinaria", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar las fechas siguientes de la actividad rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int ProgramarActividadesRutinarias(HistorialAR historialAR, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = historialAR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);
           
            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (historialAR.PERI_Interno == null)
                param2.Value = DBNull.Value;
            else
                param2.Value = historialAR.PERI_Interno;
            param2.ParameterName = "PERI_Interno";
            parametros.Add(param2);

            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_ProgramarActividadRutinaria", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al programar la actividad rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
    }
}
