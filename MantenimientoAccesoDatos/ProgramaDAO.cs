using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class ProgramaDAO:DataAccess
    {
        public ProgramaDAO() { }

        public int InsertarProgramaSemanal(ProgramaSemanal programa)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Direction = System.Data.ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (programa.PERI_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = programa.PERI_Interno;
            param.ParameterName = "PERI_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = System.Data.DbType.Int32;
            param1.Value = programa.PERI_NumSemana;
            param1.ParameterName = "PERI_NumSemana";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.DbType = System.Data.DbType.Int32;
            param2.Value = programa.PERI_Anio;
            param2.ParameterName = "PERI_Anio";
            parametros.Add(param2);

            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQueryOutID("PA_InsertarProgramaSemanal", parametros, "PERI_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el programa", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int ObtenerIDProgramaSemanal(ProgramaSemanal programa)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = System.Data.DbType.Int32;
            param1.Value = programa.PERI_NumSemana;
            param1.ParameterName = "PERI_NumSemana";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.DbType = System.Data.DbType.Int32;
            param2.Value = programa.PERI_Anio;
            param2.ParameterName = "PERI_Anio";
            parametros.Add(param2);
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = Convert.ToInt32(EjecuteEscalar("PA_ObtenerIDProgramaSemanal", parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID Programa", ex);
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
