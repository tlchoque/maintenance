using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class EjecucionActividadesRDAO:DataAccess
    {
        public EjecucionActividadesRDAO() { }

        public int ObtenerTotalNumRegistros_ActividadesR_Programadas_HastaFechaLimite(DateTime? FechaLimite)
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


                result = Convert.ToInt32(EjecuteEscalar("PA_Total_ActividadesR_Programadas", parametros));
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
        public List<ActividadR> ObtenerActividadesR_Programadas_HastaFechaLimite(int TamanioPagina, int NumeroPagina, DateTime? FechaLimite)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesRProgramadas";

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
                        throw new Exception("Error convirtiendo datos de Historial de  Actividad Rutinaria Programadas a Objecto", ex);
                    }
                    historialARs.Add(actividadRutinaria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Historial de  Actividad Rutinaria Programadas - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialARs;
        }

        public int ObtenerTotalNumRegistros_ActividadesRProgramadasPorLocalizacion_HastaFechaLimite(LocalizacionS localizacion, DateTime? FechaLimite)
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
                result = Convert.ToInt32(EjecuteEscalar("PA_TotalActividadesRutinariasProgramadasPorLocalizacionConFechaLimite", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de registros de  Actividades rutinarias Programadas hasta una fecha límite", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<ActividadR> Obtener_ActividadesProgramadasPorLocalizacion_HastaFechaLimite(int TamanioPagina, int NumeroPagina, LocalizacionS localizacion, DateTime? FechaLimite)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesRutinariasProgramadasPorLocalizacionConFechaLimite";

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
                        throw new Exception("Error convirtiendo datos de Historial de  Actividad Rutinaria Programadas a Objecto", ex);
                    }
                    historialARs.Add(actividadRutinaria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Historial de  Actividad Rutinaria Programadas - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialARs;
        }
       
        public int EjecutarActividadesRutinarias(ActividadR actividadR, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = actividadR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (actividadR.HIAR_FechaEjecutado == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = actividadR.HIAR_FechaEjecutado;
            param2.ParameterName = "HIAR_FechaEjecutado";
            parametros.Add(param2);
            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_EjecutarActividadRutinaria", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la actividad rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int AnularEjecutarActividadesRutinarias(ActividadR actividadR, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = actividadR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_AnularEjecutarActividadRutinaria", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al anular ejecutar la actividad rutinaria", ex);
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
