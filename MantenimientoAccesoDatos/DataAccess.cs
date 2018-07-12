using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;//para ConfigurationManager (agregar referencia)
using System.Data.Common;//para DbProviderFactory, DbParameter
using System.Data;//para CommandType

namespace Mantenimiento.DAO
{
    public abstract class DataAccess
    {
        public DataAccess()
        {
        }
        private static string globalConectionString;//este campo estatico puede ver se de otros lugares, sin importar
        //si no tengo una instancia creada de mi clase DataAcces
        //a este campo la encapsulamos: Refactorizar
        public static string GlobalConectionString
        {
            get
            {
                if (globalConectionString == null)
                    globalConectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                return DataAccess.globalConectionString;
            }
            set { DataAccess.globalConectionString = value; }
        }

        public static string Provider
        {
            get { return ConfigurationManager.ConnectionStrings["conn"].ProviderName; }//para que funcione esto hay que configurar el archivo: Web.config
        }
        public static DbProviderFactory dpf
        {
            get { return DbProviderFactories.GetFactory(Provider); }
        }

        //---------------
        private static DbConnection connection;

        public static DbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = dpf.CreateConnection();
                    //connection = new SqlConnection(GlobalConectionString);
                    Connection.ConnectionString = GlobalConectionString;
                    if (connection == null)
                    {
                        throw new Exception("Coneccion a la base de datos falló");
                    }
                }
                return connection;
            }
            /* set
             {
                 connection = value;
             }*/
        }
        //
        //-----------

        protected int EjecuteNonQuery(string StoredProcedure, List<DbParameter> Parametros)
        {
            int result = 0;
            try
            {


                using (DbCommand cmd = dpf.CreateCommand())
                {

                    cmd.Connection = Connection;
                    cmd.CommandText = StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;//utilizaremos procedimientos almacenados
                    
                    if (Parametros != null)
                    {
                        foreach (DbParameter param in Parametros)
                            cmd.Parameters.Add(param);
                    }

                    result = cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el Query", ex);
            }

            return result;
        }

        protected DbDataReader EjecuteReader(string StoredProcedure, List<DbParameter> Parametros)
        {
            DbDataReader result = null;
            try
            {

                //Connection.ConnectionString = GlobalConectionString;
                using (DbCommand cmd = dpf.CreateCommand())
                {

                    cmd.Connection = Connection;
                    cmd.CommandText = StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (Parametros != null)
                    {
                        foreach (DbParameter param in Parametros)//Ejecute reader
                            cmd.Parameters.Add(param);
                    }
                    //Connection.Open();
                    result = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Reader", ex);
            }

            return result;


        }

        protected object EjecuteEscalar(string StoredProcedure, List<DbParameter> Parametros)
        {
            object result = null;
            try
            {

                
                using (DbCommand cmd = dpf.CreateCommand())
                {

                    cmd.Connection = Connection;
                    cmd.CommandText = StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;//utilizaremos procedimientos almacenados

                    if (Parametros != null)
                    {
                        foreach (DbParameter param in Parametros)
                            cmd.Parameters.Add(param);
                    }

                    result = cmd.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Escalar", ex);
            }

            return result;
        }

        protected int EjecuteNonQueryOutID(string StoredProcedure, List<DbParameter> Parametros, string NombreCampoID)
        {
            int result = 0;
            try
            {
                using (DbCommand cmd = dpf.CreateCommand())
                {
                    cmd.Connection = Connection;
                    cmd.CommandText = StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (Parametros != null)
                    {
                        foreach (DbParameter param in Parametros)
                            cmd.Parameters.Add(param);
                    }
                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        result = (int)cmd.Parameters[NombreCampoID].Value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el Query", ex);
            }
            return result;
        }
    }//fin clase
}
