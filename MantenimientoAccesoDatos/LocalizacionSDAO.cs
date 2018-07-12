using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;
using Mantenimiento.Entidades;

namespace Mantenimiento.DAO
{
    public class LocalizacionSDAO:DataAccess
    {
        public LocalizacionSDAO() { }

        public int InsertarLocalizacion( LocalizacionS Localizacion, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Direction = ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (Localizacion.LOCA_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = Localizacion.LOCA_Interno;
            param.ParameterName = "LOCA_Interno";
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = Localizacion.LOCA_Nombre.ToUpper();
            param0.ParameterName = "LOCA_Nombre";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if (Localizacion.LOCA_Origen == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = Localizacion.LOCA_Origen;
            param1.ParameterName = "LOCA_Origen";
            parametros.Add(param1);

            DbParameter param3 = dpf.CreateParameter();
            if (AUDI_UsuarioCrea == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = AUDI_UsuarioCrea;
            param3.ParameterName = "AUDI_UsuarioCrea";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (AUDI_UsuarioEdita == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = AUDI_UsuarioEdita;
            param4.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param4);

            //System.Windows.Forms.MessageBox.Show(PartePlan.PART_Interno+ ' ' + PartePlan.PART_Nombre + ' ' + PartePlan.PART_Origen + ' ' + PartePlan.PLAN_Interno.ToString() + ' ' + AUDI_UsuarioEdita);
            int resultado = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                resultado = EjecuteNonQueryOutID("PA_InsertarLocalizacionS", parametros, "LOCA_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar o editar Parte de Plan de Trabajo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return resultado;
        }

        public List<LocalizacionS> ObtenerLocalizacionesPorOrigen(int LOCA_Origen)
        {
            List<LocalizacionS> Localizaciones = new List<LocalizacionS>();
            string StoredProcedure = "PA_ObtenerLocalizacionesPorOrigen";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = LOCA_Origen;
            param.ParameterName = "LOCA_Origen";
            parametros.Add(param);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    LocalizacionS Localizacion = null;
                    try
                    {
                        Localizacion = new LocalizacionS();
                        Localizacion.LOCA_Interno = (int)dr["LOCA_Interno"];
                        Localizacion.LOCA_Nombre = (string)dr["LOCA_Nombre"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Localizaciones por Origen", ex);
                    }
                    Localizaciones.Add(Localizacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Localizaciones por Origen", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Localizaciones;
        }


        public List<LocalizacionS> ObtenerLocalizaciones()
        {
            List<LocalizacionS> Localizaciones = new List<LocalizacionS>();
            string StoredProcedure = "PA_ObtenerLocalizacionesS";
            
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                while (dr.Read())
                {
                    LocalizacionS Localizacion = null;
                    try
                    {
                        Localizacion = new LocalizacionS();
                        Localizacion.LOCA_Interno = (int)dr["LOCA_Interno"];
                        Localizacion.LOCA_Nombre = (string)dr["LOCA_Nombre"];
                        Localizacion.LOCA_Origen = (int)dr["LOCA_Origen"];
                        Localizacion.LOCA_NombreExtendido = dr["LOCA_NombreExtendido"]==DBNull.Value? null:(string)(dr["LOCA_NombreExtendido"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Localizaciones por Origen", ex);
                    }
                    Localizaciones.Add(Localizacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Localizaciones", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Localizaciones;
        }

        public int EliminarLocalizaciones(LocalizacionS Localizacion, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Localizacion.LOCA_Interno;
            param.ParameterName = "LOCA_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            int result = 0;
            try
            {
                Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_EliminarLocalizaciones", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Localizaciones de Plan", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public List<LocalizacionS> ObtenerLocalizacionesLike(string LOCA_Nombre)
        {
            List<LocalizacionS> Localizaciones = new List<LocalizacionS>();
            string StoredProcedure = "PA_ObtenerLocalizacionesLike";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = LOCA_Nombre;
            param.ParameterName = "LOCA_Nombre";
            parametros.Add(param);
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    LocalizacionS Localizacion = null;
                    try
                    {
                        Localizacion = new LocalizacionS();
                        Localizacion.LOCA_Interno= (int)dr["LOCA_Interno"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Búsqueda de Localizaciones ", ex);
                    }
                    Localizaciones.Add(Localizacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Búsqueda de Localizacion", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Localizaciones;
        }
        //----agregado
        public LocalizacionS ObtenerLocalizacion(LocalizacionS loca)
        {
            LocalizacionS Localizacion = null;
            string StoredProcedure = "PA_ObtenerLocalizacionPorID";
            List<LocalizacionS> Localizaciones = new List<LocalizacionS>();

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = loca.LOCA_Interno;
            param.ParameterName = "LOCA_Interno";
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
                        Localizacion = new LocalizacionS();
                        Localizacion.LOCA_Interno = (int)dr["LOCA_Interno"];
                        Localizacion.LOCA_Nombre = (string)dr["LOCA_Nombre"];
                        Localizacion.LOCA_Origen = (int)dr["LOCA_Origen"];
                        Localizacion.LOCA_NombreExtendido = (string)dr["LOCA_NombreExtendido"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Localización", ex);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de Localización", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Localizacion;
        }
    }
}
