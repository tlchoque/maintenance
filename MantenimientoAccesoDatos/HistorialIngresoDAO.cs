using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class HistorialIngresoDAO:DataAccess
    {
        public HistorialIngresoDAO()
        {
        }
        public HistorialIngreso ObtenerUltimoIngresoUsuario(HistorialIngreso _HistorialIngreso)
        {

            HistorialIngreso historialIngreso = null;

            string StoredProcedure = "PA_ObtenerUltimoIngresoUsuario";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = _HistorialIngreso.USUA_Interno;
            param.ParameterName = "USUA_Interno";
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
                        historialIngreso = new HistorialIngreso();
                        historialIngreso.HIIN_Interno = (int)(dr["HIIN_Interno"]);
                        historialIngreso.USUA_Interno = (int)dr["USUA_Interno"];
                        historialIngreso.HIIN_FechaIngreso = (DateTime)(dr["HIIN_FechaIngreso"]);
                        historialIngreso.HIIN_IPacceso = dr["HIIN_IPacceso"] == System.DBNull.Value ? null : (string)(dr["HIIN_IPacceso"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de historialIngreso a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos del historialIngreso", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return historialIngreso;
        }

        public int InsertarHistorialIngreso(HistorialIngreso HistIng)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = HistIng.USUA_Interno;
            param.ParameterName = "USUA_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = HistIng.HIIN_IPacceso;
            param1.ParameterName = "HIIN_IPacceso";
            parametros.Add(param1);
            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_InsertarHistorialIngreso", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Historial Ingreso al sistema", ex);
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
