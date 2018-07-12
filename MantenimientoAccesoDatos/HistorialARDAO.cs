using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType


namespace Mantenimiento.DAO
{
    public class HistorialARDAO:DataAccess
    {
        public HistorialARDAO() { }

        public int InsertarHistorialAR(HistorialAR HistorialAR, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            //System.Windows.Forms.MessageBox.Show("reach this");
            //System.Windows.Forms.MessageBox.Show(HistorialAR.HIAR_Estado + ' ' + HistorialAR.HIAR_FechaEjecutado + ' ' + HistorialAR.HIAR_SiguienteFecha + ' ' + HistorialAR.LOCA_Interno + ' ' + HistorialAR.ACRU_Interno);
            //System.Windows.Forms.MessageBox.Show(HistorialAR.ACRU_Interno.ToString());
            List<DbParameter> parametros = new List<DbParameter>();
	
            DbParameter param = dpf.CreateParameter();
            param.Direction = System.Data.ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (HistorialAR.HIAR_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = HistorialAR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = HistorialAR.HIAR_FechaProgramado;
            if (HistorialAR.HIAR_FechaProgramado == null)
                param0.Value = System.DBNull.Value;
            else
                param0.Value = HistorialAR.HIAR_FechaProgramado;
            param0.ParameterName = "HIAR_FechaProgramado";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if (HistorialAR.HIAR_Ejecutor == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = HistorialAR.HIAR_Ejecutor;
            param1.ParameterName = "HIAR_Ejecutor";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (HistorialAR.HIAR_FechaEjecucionAnterior == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = HistorialAR.HIAR_FechaEjecucionAnterior;
            param2.ParameterName = "HIAR_FechaEjecucionAnterior";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            if (HistorialAR.HIAR_SiguienteFecha == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = HistorialAR.HIAR_SiguienteFecha;
            param3.ParameterName = "HIAR_SiguienteFecha";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (HistorialAR.HIAR_Observacion == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = HistorialAR.HIAR_Observacion;
            param4.ParameterName = "HIAR_Observacion";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            if (HistorialAR.HIAR_Estado == null)
                param5.Value = System.DBNull.Value;
            else
                param5.Value = HistorialAR.HIAR_Estado;
            param5.ParameterName = "HIAR_Estado";
            parametros.Add(param5);

            DbParameter param7 = dpf.CreateParameter();
            if (HistorialAR.ACRU_Interno == null)
                param7.Value = System.DBNull.Value;
            else
                param7.Value = HistorialAR.ACRU_Interno;
            param7.ParameterName = "ACRU_Interno";
            parametros.Add(param7);

            DbParameter param8 = dpf.CreateParameter();
            if (HistorialAR.LOCA_Interno == null)
                param8.Value = System.DBNull.Value;
            else
                param8.Value = HistorialAR.LOCA_Interno;
            param8.ParameterName = "LOCA_Interno";
            parametros.Add(param8);

            DbParameter param9 = dpf.CreateParameter();
            if (HistorialAR.EQUI_Interno == null)
                param9.Value = System.DBNull.Value;
            else
                param9.Value = HistorialAR.EQUI_Interno;
            param9.ParameterName = "EQUI_Interno";
            parametros.Add(param9);

            DbParameter param10 = dpf.CreateParameter();
            if (HistorialAR.PERI_Interno == null)
                param10.Value = System.DBNull.Value;
            else
                param10.Value = HistorialAR.PERI_Interno;
            param10.ParameterName = "PERI_Interno";
            parametros.Add(param10);

            DbParameter param11 = dpf.CreateParameter();
            if (AUDI_UsuarioCrea == null)
                param11.Value = System.DBNull.Value;
            else
                param11.Value = AUDI_UsuarioCrea;
            param11.ParameterName = "AUDI_UsuarioCrea";
            parametros.Add(param11);

            DbParameter param6 = dpf.CreateParameter();
            if (AUDI_UsuarioEdita == null)
                param6.Value = System.DBNull.Value;
            else
                param6.Value = AUDI_UsuarioEdita;
            param6.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param6);

            int resultado = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                resultado = EjecuteNonQueryOutID("PA_InsertarHistorialActRutinaria", parametros, "HIAR_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar Actividad a Historial", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return resultado;
        }

        public HistorialAR ObtenerHistorialActividadRutinaria(HistorialAR ObjHistorialAR)
        {
            HistorialAR historialAR = null;
            string StoredProcedure = "PA_ObtenerHistorialActividadRutinariaPorID";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.Value = ObjHistorialAR.HIAR_Interno;
            param.ParameterName = "HIAR_Interno";
            parametros.Add(param);
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                if (dr.Read())
                {
                    try
                    {   
                        
                        historialAR = new HistorialAR();
                        historialAR.HIAR_Interno = (int)dr["HIAR_Interno"];
                        historialAR.HIAR_FechaProgramado = dr["HIAR_FechaProgramado"] == System.DBNull.Value ? null : (DateTime?)(dr["HIAR_FechaProgramado"]);
                        historialAR.HIAR_Ejecutor = dr["HIAR_Ejecutor"] == System.DBNull.Value ? null : (string)(dr["HIAR_Ejecutor"]);
                        historialAR.HIAR_FechaEjecutado = dr["HIAR_FechaEjecutado"] == System.DBNull.Value ? null : (DateTime?)(dr["HIAR_FechaEjecutado"]);
                        historialAR.HIAR_SiguienteFecha = dr["HIAR_SiguienteFecha"] == System.DBNull.Value ? null : (DateTime?)(dr["HIAR_SiguienteFecha"]);
                        historialAR.HIAR_Observacion = dr["HIAR_Observacion"] == System.DBNull.Value ? null : (string)(dr["HIAR_Observacion"]);
                        historialAR.HIAR_Estado = dr["HIAR_Estado"] == System.DBNull.Value ? null : (string)(dr["HIAR_Estado"]);
                        historialAR.ACRU_Interno = dr["ACRU_Interno"] == System.DBNull.Value ? null : (int?)(dr["ACRU_Interno"]);
                        historialAR.LOCA_Interno = dr["LOCA_Interno"] == System.DBNull.Value ? null : (int?)(dr["LOCA_Interno"]);
                        historialAR.EQUI_Interno = dr["EQUI_Interno"] == System.DBNull.Value ? null : (int?)(dr["EQUI_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Historial Actividad Rutinaria a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Historial Actividad Rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return historialAR;
        }

        //para el historial de actividades rutinarias ejecutadas

        public int ObtenerTotalNumRegistros_HistorialActividadesR_EntreFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = DbType.DateTime;
            param.Value = FechaInicio;
            param.ParameterName = "FechaInicio";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = DbType.DateTime;
            param1.Value = FechaFin;
            param1.ParameterName = "FechaFin";
            parametros.Add(param1);


            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = Convert.ToInt32(EjecuteEscalar("PA_Total_ActividadesR_EjecutadasEntreFechas", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de registros de Historial de Actividades rutinarias ejecutas", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<ActividadR> ObtenerActividadesR_Ejecutas_EntreFechas(int TamanioPagina, int NumeroPagina, DateTime FechaInicio, DateTime FechaFin)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesREjecutadas";

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
            param2.Value = FechaInicio;
            param2.ParameterName = "FechaInicio";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.DbType = DbType.DateTime;
            param3.Value = FechaFin;
            param3.ParameterName = "FechaFin";
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

        public int ObtenerTotalNumRegistros_ActividadesREjecutadasPorLocalizacion_EntreFechas(LocalizacionS localizacion, DateTime FechaInicio, DateTime FechaFin)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = DbType.DateTime;
            param.Value = FechaInicio;
            param.ParameterName = "FechaInicio";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = DbType.DateTime;
            param1.Value = FechaFin;
            param1.ParameterName = "FechaFin";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.DbType = DbType.Int32;
            param2.Value = localizacion.LOCA_Interno;
            param2.ParameterName = "LOCA_Interno";
            parametros.Add(param2);

            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = Convert.ToInt32(EjecuteEscalar("PA_TotalActividadesR_EjecutadasPorLocalizacion_EntreFechas", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de registros de  Actividades rutinarias ejecutas - Entre Fechas", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<ActividadR> Obtener_ActividadesEjecutadasPorLocalizacion_EntreFechas(int TamanioPagina, int NumeroPagina, LocalizacionS localizacion, DateTime FechaInicio, DateTime FechaFin)
        {

            List<ActividadR> historialARs = new List<ActividadR>();

            string StoredProcedure = "PA_ObtenerActividadesRutinariasEjecutadasPorLocalizacion";

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
            param2.Value = FechaInicio;
            param2.ParameterName = "FechaInicio";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.DbType = DbType.DateTime;
            param3.Value = FechaFin;
            param3.ParameterName = "FechaFin";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            param4.DbType = DbType.Int32;
            param4.Value = localizacion.LOCA_Interno;
            param4.ParameterName = "LOCA_Interno";
            parametros.Add(param4);

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
                        throw new Exception("Error convirtiendo datos de Historial de  Actividad Rutinaria Ejecutas a Objecto", ex);
                    }
                    historialARs.Add(actividadRutinaria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Historial de  Actividad Rutinaria Ejecutas - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialARs;
        }
    }
}
