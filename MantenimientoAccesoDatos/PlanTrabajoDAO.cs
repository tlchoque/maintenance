using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;
using Mantenimiento.Entidades;


namespace Mantenimiento.DAO
{
    public class PlanTrabajoDAO:DataAccess
    {
        public PlanTrabajoDAO() { }

        public int InsertarPlanTrabajo(PlanTrabajo PlanTrabajo, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Direction = ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (PlanTrabajo.PLAN_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = PlanTrabajo.PLAN_Interno;
            param.ParameterName = "PLAN_Interno";
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = PlanTrabajo.PLAN_Descripcion.ToUpper();;
            param0.ParameterName = "PLAN_Descripcion";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = PlanTrabajo.PLAN_Regimen;
            param1.ParameterName = "PLAN_Regimen";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (PlanTrabajo.PLAN_UnidadLecturas == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = PlanTrabajo.PLAN_UnidadLecturas;
            param2.ParameterName = "PLAN_UnidadLecturas";
            parametros.Add(param2);

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

            int resultado = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                resultado = EjecuteNonQueryOutID("PA_InsertarPlanTrabajo", parametros, "PLAN_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Plan de Trabajo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return resultado;
        }

        public List<PlanTrabajo> ObtenerPlanesTrabajo()
        {
            List<PlanTrabajo> PlanesTrabajo = new List<PlanTrabajo>();
            string StoredProcedure = "PA_ObtenerPlanesTrabajo";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                while (dr.Read())
                {
                    PlanTrabajo PlanTrabajo = null;
                    try
                    {
                        PlanTrabajo = new PlanTrabajo();
                        PlanTrabajo.PLAN_Interno = (int)dr["PLAN_Interno"];
                        PlanTrabajo.PLAN_Descripcion = (string)dr["PLAN_Descripcion"];
                        PlanTrabajo.PLAN_Regimen = (string)dr["PLAN_Regimen"];
                        PlanTrabajo.PLAN_UnidadLecturas = dr["PLAN_UnidadLecturas"] == System.DBNull.Value ? null : (string)(dr["PLAN_UnidadLecturas"]);
                    }
                    catch (Exception ex) 
                    {
                        throw new Exception("Error convirtiendo datos de Plan de Trabajo a Objecto", ex);
                    }
                    PlanesTrabajo.Add(PlanTrabajo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Planes", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return PlanesTrabajo;
        }

        public PlanTrabajo ObtenerPlanTrabajoPorId(PlanTrabajo objPlanTrabajo)
        {
            PlanTrabajo PlanTrabajo = null;
            string StoredProcedure = "PA_ObtenerPlanTrabajoPorId";
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = objPlanTrabajo.PLAN_Interno;
            param.ParameterName = "PLAN_Interno";
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
                        PlanTrabajo = new PlanTrabajo();
                        PlanTrabajo.PLAN_Interno = (int)dr["PLAN_Interno"];
                        PlanTrabajo.PLAN_Descripcion = (string)dr["PLAN_Descripcion"];
                        PlanTrabajo.PLAN_Regimen = (string)dr["PLAN_Regimen"];
                        PlanTrabajo.PLAN_UnidadLecturas = dr["PLAN_UnidadLecturas"] == System.DBNull.Value ? null : (string)(dr["PLAN_UnidadLecturas"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Plan a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Plan", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return PlanTrabajo;
        }

        public int EliminarPlanTrabajo(PlanTrabajo PlanTrabajo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = PlanTrabajo.PLAN_Interno;
            param.ParameterName = "PLAN_Interno";
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
                result = EjecuteNonQuery("PA_EliminarPlanTrabajo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar plan de trabajo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public List<PlanTrabajo> ObtenerPlanesP(int TamanioPagina, int NumeroPagina)
        {
            //System.Windows.Forms.MessageBox.Show("hola");
            List<PlanTrabajo> planes = new List<PlanTrabajo>();
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = TamanioPagina;
            param.ParameterName = "TamanioPagina";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = NumeroPagina;
            param1.ParameterName = "NumeroPagina";
            parametros.Add(param1);
            string StoredProcedure = "PA_ObtenerPlanesP";
            //System.Windows.Forms.MessageBox.Show(TamanioPagina.ToString()+' '+NumeroPagina.ToString());
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    PlanTrabajo plan = null;
                    try
                    {
                        plan = new PlanTrabajo();
                        plan.PLAN_Interno = (int)dr["PLAN_Interno"];
                        plan.PLAN_Descripcion = (string)(dr["PLAN_Descripcion"]);
                        plan.PLAN_Regimen = (string)(dr["PLAN_Regimen"]);
                        plan.PLAN_UnidadLecturas = dr["PLAN_UnidadLecturas"] == System.DBNull.Value ? null : (string)(dr["PLAN_UnidadLecturas"]); 
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de plan a Objecto", ex);
                    }
                    planes.Add(plan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de planes", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return planes;
        }

        public int ObtenerTotalPlanes()
        {

            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = Convert.ToInt32(EjecuteEscalar("PA_ObtenerTotalPlanes", null));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de los planes", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public List<PlanTrabajo> ObtenerPlanesFiltradoPorNombre(int TamanioPagina, int NumeroPagina, string str)
        {
            List<PlanTrabajo> planes = new List<PlanTrabajo>();
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
            param2.Value = str;
            param2.ParameterName = "String";
            parametros.Add(param2);
            string StoredProcedure = "PA_ObtenerPlanesFiltradoPorNombre";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    PlanTrabajo plan = null;
                    try
                    {
                        plan = new PlanTrabajo();
                        plan.PLAN_Interno = (int)dr["PLAN_Interno"];
                        plan.PLAN_Descripcion = (string)(dr["PLAN_Descripcion"]);
                        plan.PLAN_Regimen = (string)(dr["PLAN_Regimen"]);
                        plan.PLAN_UnidadLecturas = dr["PLAN_UnidadLecturas"] == System.DBNull.Value ? null : (string)(dr["PLAN_UnidadLecturas"]); 
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de grupo a Objecto", ex);
                    }
                    planes.Add(plan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de planes", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return planes;
        }
    }
}
