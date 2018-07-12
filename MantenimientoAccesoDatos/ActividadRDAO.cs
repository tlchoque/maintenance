using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;
using Mantenimiento.Entidades;

namespace Mantenimiento.DAO
{
    public class ActividadRDAO:DataAccess
    {
        public ActividadRDAO() { }

        public int InsertarActividad(ActividadR ActividadR, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Direction = ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (ActividadR.ACRU_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = ActividadR.ACRU_Interno;
            param.ParameterName = "ACRU_Interno";
            parametros.Add(param);
            
            DbParameter param0 = dpf.CreateParameter();
            if (ActividadR.ACRU_Descripcion == null)
                param0.Value = System.DBNull.Value;
            else
                param0.Value = ActividadR.ACRU_Descripcion;
            param0.ParameterName = "ACRU_Descripcion";
            parametros.Add(param0);


            DbParameter param1 = dpf.CreateParameter();
            param1.Value = ActividadR.ACRU_Tipo;
            param1.ParameterName = "ACRU_Tipo";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.Value = ActividadR.ACRU_ConCorte;
            param2.ParameterName = "ACRU_ConCorte";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.Value = ActividadR.ACRU_ConMedicion;
            param3.ParameterName = "ACRU_ConMedicion";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (ActividadR.ACRU_UnidadMedicion == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = ActividadR.ACRU_UnidadMedicion;
            param4.ParameterName = "ACRU_UnidadMedicion";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            param5.Value = ActividadR.ACRU_Frecuencia;
            param5.ParameterName = "ACRU_Frecuencia";
            parametros.Add(param5);

            DbParameter param6 = dpf.CreateParameter();
            param6.Value = ActividadR.ACRU_UnidadFrecuencia;
            param6.ParameterName = "ACRU_UnidadFrecuencia";
            parametros.Add(param6);

            DbParameter param7 = dpf.CreateParameter();
            param7.Value = ActividadR.PART_Interno;
            param7.ParameterName = "PART_Interno";
            parametros.Add(param7);

            DbParameter param8 = dpf.CreateParameter();
            param8.Value = ActividadR.NOMB_Interno;
            param8.ParameterName = "NOMB_Interno";
            parametros.Add(param8);

            DbParameter param9 = dpf.CreateParameter();
            if (AUDI_UsuarioCrea == null)
                param9.Value = System.DBNull.Value;
            else
                param9.Value = AUDI_UsuarioCrea;
            param9.ParameterName = "AUDI_UsuarioCrea";
            parametros.Add(param9);

            DbParameter param10 = dpf.CreateParameter();
            if (AUDI_UsuarioEdita == null)
                param10.Value = System.DBNull.Value;
            else
                param10.Value = AUDI_UsuarioEdita;
            param10.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param10);

            int resultado = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                resultado = EjecuteNonQueryOutID("PA_InsertarActividadR", parametros, "ACRU_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Actividad Rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return resultado;
        }

        public List<ActividadR> ObtenerActividadesParte(int PART_Interno)
        {
            List<ActividadR> Actividades = new List<ActividadR>();
            string StoredProcedure = "PA_ObtenerActividadesParte";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = PART_Interno;
            param.ParameterName = "PART_Interno";
            parametros.Add(param);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    ActividadR Actividad = null;
                    try
                    {
                        Actividad = new ActividadR();
                        Actividad.ACRU_Interno = (int)dr["ACRU_Interno"];
                        Actividad.ACRU_Descripcion = dr["ACRU_Descripcion"] == System.DBNull.Value ? null : (string)(dr["ACRU_Descripcion"]);
                        Actividad.ACRU_Tipo = (string)dr["ACRU_Tipo"];
                        Actividad.ACRU_ConCorte = (Boolean)dr["ACRU_ConCorte"];
                        Actividad.ACRU_ConMedicion = (Boolean)dr["ACRU_ConMedicion"];
                        Actividad.ACRU_UnidadMedicion = dr["ACRU_UnidadMedicion"] == System.DBNull.Value ? null : (string)(dr["ACRU_UnidadMedicion"]);
                        Actividad.ACRU_Frecuencia = (int)dr["ACRU_Frecuencia"];
                        Actividad.ACRU_UnidadFrecuencia = (string)dr["ACRU_UnidadFrecuencia"];
                        Actividad.NOMB_Descripcion = (string)dr["NOMB_Descripcion"];
                        Actividad.NOMB_Interno = (int)dr["NOMB_Interno"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Actividades de Parte", ex);
                    }
                    Actividades.Add(Actividad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Actividades de Parte", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Actividades;
        }

        public int EliminarActividad(ActividadR Actividad, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Actividad.ACRU_Interno;
            param.ParameterName = "ACRU_Interno";
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
                result = EjecuteNonQuery("PA_EliminarActividadR", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Actividad Rutinaria", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public ActividadR ObtenerActividadPorId(ActividadR ObjActividad)
        {
            ActividadR Actividad = null;
            string StoredProcedure = "PA_ObtenerActividadPorId";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = ObjActividad.ACRU_Interno;
            param.ParameterName = "ACRU_Interno";
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
                        Actividad = new ActividadR();
                        Actividad.ACRU_Interno = (int)dr["ACRU_Interno"];
                        Actividad.ACRU_Descripcion = dr["ACRU_Descripcion"] == System.DBNull.Value ? null : (string)(dr["ACRU_Descripcion"]);
                        Actividad.ACRU_Tipo = (string)(dr["ACRU_Tipo"]);
                        Actividad.ACRU_ConCorte = (Boolean)(dr["ACRU_ConCorte"]);
                        Actividad.ACRU_ConMedicion = (Boolean)(dr["ACRU_ConMedicion"]);
                        Actividad.ACRU_UnidadMedicion = dr["ACRU_UnidadMedicion"] == System.DBNull.Value ? null : (string)(dr["ACRU_UnidadMedicion"]);
                        Actividad.ACRU_Frecuencia = (int)(dr["ACRU_Frecuencia"]);
                        Actividad.ACRU_UnidadFrecuencia = dr["ACRU_UnidadFrecuencia"]==System.DBNull.Value ? null : (string)(dr["ACRU_UnidadFrecuencia"]);
                        Actividad.NOMB_Descripcion = (string)(dr["NOMB_Descripcion"]);
                        Actividad.NOMB_Interno = (int)(dr["NOMB_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Actividad a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Actividad", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return Actividad;
        }

        public List<ActividadR> ObtenerActividadesPlan(PlanTrabajo PlanTrabajo)
        {
            List<ActividadR> Actividades = new List<ActividadR>();
            string StoredProcedure = "PA_ObtenerActividadesPlan";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = PlanTrabajo.PLAN_Interno;
            param.ParameterName = "PLAN_Interno";
            parametros.Add(param);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    ActividadR Actividad = null;
                    try
                    {
                        Actividad = new ActividadR();
                        Actividad.ACRU_Interno = (int)dr["ACRU_Interno"];
                        Actividad.ACRU_Frecuencia = (int)dr["ACRU_Frecuencia"];
                        Actividad.ACRU_UnidadFrecuencia = (string)dr["ACRU_UnidadFrecuencia"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Actividades de Plan", ex);
                    }
                    Actividades.Add(Actividad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Actividades de Plan", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Actividades;
        }

        //Para el nombre de la actividad
        public NombreActividad ObtenerNombreActividadRutinaria(NombreActividad nomActividad)
        {
            NombreActividad nombreActividad = null;
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = nomActividad.NOMB_Interno;
            param.ParameterName = "NOMB_Interno";
            parametros.Add(param);
            string StoredProcedure = "PA_ObtenerNombreActividadRutinaria";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                if (dr.Read())
                {
                    
                    try
                    {
                        nombreActividad = new NombreActividad();
                        nombreActividad.NOMB_Interno = (int)dr["NOMB_Interno"];
                        nombreActividad.NOMB_Descripcion = (string)(dr["NOMB_Descripcion"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo dato de Nombre Actividad Rutinaria a Objecto", ex);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el nombre de la actividad", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return nombreActividad;
        }
    }
}
