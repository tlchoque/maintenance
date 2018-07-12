using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;
using Mantenimiento.Entidades;

namespace Mantenimiento.DAO
{
    public class PartePlanDAO:DataAccess
    {
        public PartePlanDAO() { }

        public int InsertarPartePlan(PartePlan PartePlan, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Direction = ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (PartePlan.PART_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = PartePlan.PART_Interno;
            param.ParameterName = "PART_Interno";
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = PartePlan.PART_Nombre.ToUpper();
            param0.ParameterName = "PART_Nombre";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if (PartePlan.PART_Origen == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = PartePlan.PART_Origen;
            param1.ParameterName = "PART_Origen";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (PartePlan.PLAN_Interno == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = PartePlan.PLAN_Interno;
            param2.ParameterName = "PLAN_Interno";
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

            //System.Windows.Forms.MessageBox.Show(PartePlan.PART_Interno+ ' ' + PartePlan.PART_Nombre + ' ' + PartePlan.PART_Origen + ' ' + PartePlan.PLAN_Interno.ToString() + ' ' + AUDI_UsuarioEdita);
            int resultado = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                resultado = EjecuteNonQueryOutID("PA_InsertarPartePlan", parametros, "PART_Interno");
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

        public PartePlan ObtenerPartesPorId(PartePlan ObjParte)
        {
            PartePlan Parte = null;
            string StoredProcedure = "PA_ObtenerPartePorID";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = ObjParte.PART_Interno;
            param.ParameterName = "PART_Interno";
            parametros.Add(param);

            //System.Windows.Forms.MessageBox.Show(ObjParte.PART_Interno.ToString());
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                if (dr.Read())
                {
                    try
                    {
                        
                        Parte = new PartePlan();
                        Parte.PART_Interno = (int)dr["PART_Interno"];
                        Parte.PART_Nombre = (string)dr["PART_Nombre"];
                        Parte.PART_NombreExtendido = dr["PART_NombreExtendido"] == System.DBNull.Value ? null : (string)(dr["PART_NombreExtendido"]);
                        Parte.PART_Origen = dr["PART_Origen"] == System.DBNull.Value ? null : (int?)(dr["PART_Origen"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Parte por Origen", ex);
                    }
                    //System.Windows.Forms.MessageBox.Show("more tan try");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Parte por Id", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return Parte;
        }


        public List<PartePlan> ObtenerPartesPorOrigen(int  PART_Origen)
        {
            List<PartePlan> PartesPlan = new List<PartePlan>();
            string StoredProcedure = "PA_ObtenerPartesPorOrigen";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = PART_Origen;
            param.ParameterName = "PART_Origen";
            parametros.Add(param);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    PartePlan PartePlan = null;
                    try
                    {
                        PartePlan = new PartePlan();
                        PartePlan.PART_Interno = (int)dr["PART_Interno"];
                        PartePlan.PART_Nombre = (string)dr["PART_Nombre"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Partes por Origen", ex);
                    }
                    PartesPlan.Add(PartePlan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Partes por Origen", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return PartesPlan;
        }

        public List<PartePlan> ObtenerPartesPorPlan(int PLAN_Interno)
        {
            List<PartePlan> PartesPlan = new List<PartePlan>();
            string StoredProcedure = "PA_ObtenerPartesPorPlan";

            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = PLAN_Interno;
            param.ParameterName = "PLAN_Interno";
            parametros.Add(param);
            //System.Windows.Forms.MessageBox.Show(PLAN_Interno.ToString());
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                //System.Windows.Forms.MessageBox.Show("GO");
                while (dr.Read())
                {
                    PartePlan PartePlan = null;
                    try
                    {
                        PartePlan = new PartePlan();
                        PartePlan.PART_Interno = (int)dr["PART_Interno"];
                        PartePlan.PART_Nombre = (string)dr["PART_Nombre"];
                        PartePlan.PART_Origen = dr["PART_Origen"] == System.DBNull.Value ? null : (int?)(dr["PART_Origen"]);
                        PartePlan.PLAN_Interno = dr["PLAN_Interno"] == System.DBNull.Value ? null : (int?)(dr["PLAN_Interno"]);
                        PartePlan.PART_NombreExtendido = dr["PART_NombreExtendido"] == System.DBNull.Value ? null : (string)(dr["PART_NombreExtendido"]);
                        //System.Windows.Forms.MessageBox.Show(PartePlan.PART_Interno + '-' + PartePlan.PART_Nombre + ' ' + PartePlan.PART_Origen + ' ' + PartePlan.PLAN_Interno);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Partes de Plan de Trabajo a Objecto", ex);
                    }
                    PartesPlan.Add(PartePlan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Partes de Plan", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
            return PartesPlan;
        }

        public int EliminarPartes(PartePlan PartePlan, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = PartePlan.PART_Interno;
            param.ParameterName = "PART_Interno";
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
                result = EjecuteNonQuery("PA_EliminarPartes", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Partes de Plan", ex);
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
