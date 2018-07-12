using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class GrupoDAO:DataAccess
    {
        public GrupoDAO()
        {
        }
        public int ObtenerTotalGrupos()
        {
            
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = Convert.ToInt32(EjecuteEscalar("PA_ObtenerTotalGrupos", null));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de los grupos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public List<Grupo> ObtenerGrupos(int TamanioPagina, int NumeroPagina)
        {
            List<Grupo> grupos = new List<Grupo>();
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = TamanioPagina;
            param.ParameterName = "TamanioPagina";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = NumeroPagina;
            param1.ParameterName = "NumeroPagina";
            parametros.Add(param1);
            string StoredProcedure = "PA_ObtenerGrupos";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    Grupo grupo = null;
                    try
                    {
                        grupo = new Grupo();
                        grupo.GRUP_Interno = (int)dr["GRUP_Interno"];
                        grupo.GRUP_Nombre = (string)(dr["GRUP_Nombre"]);
                        grupo.GRUP_Descripcion = (string)(dr["GRUP_Descripcion"]);
                        grupo.GRUP_Activo = (Boolean)(dr["GRUP_Activo"]);
                        grupo.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        grupo.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de grupo a Objecto", ex);
                    }
                    grupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de grupos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return grupos;
        }
        public List<Grupo> ObtenerTodosGrupos()
        {
            List<Grupo> grupos = new List<Grupo>();
            
            string StoredProcedure = "PA_ObtenerTodosGrupos";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                while (dr.Read())
                {
                    Grupo grupo = null;
                    try
                    {
                        grupo = new Grupo();
                        grupo.GRUP_Interno = (int)dr["GRUP_Interno"];
                        grupo.GRUP_Nombre = (string)(dr["GRUP_Nombre"]);
                        grupo.GRUP_Descripcion = (string)(dr["GRUP_Descripcion"]);
                        grupo.GRUP_Activo = (Boolean)(dr["GRUP_Activo"]);
                        grupo.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        grupo.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de grupo a Objecto", ex);
                    }
                    grupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los grupos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return grupos;
        }
        public List<Grupo> ObtenerGruposFiltradoPorNombre(int TamanioPagina, int NumeroPagina,string str)
        {
            List<Grupo> grupos = new List<Grupo>();
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
            string StoredProcedure = "PA_ObtenerGruposFiltradoPorNombre";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    Grupo grupo = null;
                    try
                    {
                        grupo = new Grupo();
                        grupo.GRUP_Interno = (int)dr["GRUP_Interno"];
                        grupo.GRUP_Nombre = (string)(dr["GRUP_Nombre"]);
                        grupo.GRUP_Descripcion = (string)(dr["GRUP_Descripcion"]);
                        grupo.GRUP_Activo = (Boolean)(dr["GRUP_Activo"]);
                        grupo.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        grupo.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de grupo a Objecto", ex);
                    }
                    grupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de grupos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return grupos;
        }

        public Grupo ObtenerGrupoPorID(Grupo grup)
        {
            Grupo grupo = null;

            string StoredProcedure = "PA_ObtenerGrupoPorID";
            List<DbParameter> parametros = new List<DbParameter>();
            DbParameter param = dpf.CreateParameter();
            param.Value = grup.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
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
                        grupo = new Grupo();
                        grupo.GRUP_Interno = (int)dr["GRUP_Interno"];
                        grupo.GRUP_Nombre = (string)(dr["GRUP_Nombre"]);
                        grupo.GRUP_Descripcion = (string)(dr["GRUP_Descripcion"]);
                        grupo.GRUP_Activo = (Boolean)(dr["GRUP_Activo"]);
                        grupo.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        grupo.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de grupo a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de grupos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return grupo;
        }

        public int InsertarGrupo(Grupo grupo,int AUDI_Usuario)
        {
            List<DbParameter> parametros = new List<DbParameter>();
    
            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.Direction = System.Data.ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (grupo.GRUP_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            if (grupo.GRUP_Descripcion == null)
                param0.Value = System.DBNull.Value;
            else
                param0.Value = grupo.GRUP_Descripcion;
            param0.ParameterName = "GRUP_Descripcion";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if (grupo.GRUP_Activo == null)
                param1.Value = false;
            else
                param1.Value = grupo.GRUP_Activo;
            param1.ParameterName = "GRUP_Activo";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.Value = grupo.GRUP_Nombre.ToUpper();
            param2.ParameterName = "GRUP_Nombre";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.Value = AUDI_Usuario;
            param3.ParameterName = "AUDI_Usuario";
            parametros.Add(param3);

            

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQueryOutID("PA_InsertarGrupo", parametros, "GRUP_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int EliminarGrupo(Grupo grupo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            
            param.DbType = System.Data.DbType.Int32;
            param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param.DbType = System.Data.DbType.Int32;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_EliminarGrupo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public int ActivarGrupo(Grupo grupo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            
            param.DbType = System.Data.DbType.Int32;
            param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param.DbType = System.Data.DbType.Int32;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_ActivarGrupo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al activar el grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public int DesactivarGrupo(Grupo grupo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param.DbType = System.Data.DbType.Int32;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_DesactivarGrupo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar el grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        //tareas del grupo
        public List<Tarea> ObtenerTareasPorModulo()
        {
            List<Tarea> tareas = new List<Tarea>();
            
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader("PA_ObtenerTareasPorModulo", null);
                while (dr.Read())
                {
                    Tarea tarea = null;
                    try
                    {
                        //m.MODU_Interno,t.TARE_Interno,m.MODU_Nombre,t.TARE_Nombre,t.TARE_Descripcion
                        tarea = new Tarea();
                        tarea.MODU_Interno = (int)dr["MODU_Interno"];
                        tarea.TARE_Interno = (int)(dr["TARE_Interno"]);
                        tarea.MODU_Nombre = (string)(dr["MODU_Nombre"]);
                        tarea.TARE_Nombre = (string)(dr["TARE_Nombre"]);
                        tarea.TARE_Descripcion = (string)(dr["TARE_Descripcion"]);
                        tarea.TARE_NombreCorto = (string)(dr["TARE_NombreCorto"]); 
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de tarea a Objecto", ex);
                    }
                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de tareas", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return tareas;
        }
        public List<TareaGrupo> ObtenerTareasDeGrupo(Grupo grupo)
        {
            List<TareaGrupo> tareasGrupo = new List<TareaGrupo>();
            List<DbParameter> parametros = new List<DbParameter>();


            DbParameter param = dpf.CreateParameter();

            param.DbType = System.Data.DbType.Int32;
            param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader("PA_ObtenerTareasDeGrupo", parametros);
                while (dr.Read())
                {
                    TareaGrupo tareaGrupo = null;
                    try
                    {
                        //TARE_Interno,GRUP_Interno
                        tareaGrupo = new TareaGrupo();
                        tareaGrupo.TARE_Interno = (int)dr["TARE_Interno"];
                        tareaGrupo.GRUP_Interno = (int)(dr["GRUP_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de tarea grupo a Objecto", ex);
                    }
                    tareasGrupo.Add(tareaGrupo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de tareas del grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return tareasGrupo;
        }
        public int InsertarTareasDelGrupo(TareaGrupo tareaGrupo)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            
            DbParameter param = dpf.CreateParameter();
            
            param.DbType = System.Data.DbType.Int32;
            param.Value = tareaGrupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.DbType = System.Data.DbType.Int32;
            param1.Value = tareaGrupo.TARE_Interno;
            param1.ParameterName = "TARE_Interno";
            parametros.Add(param1);

            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_InsertarTareasDeGrupo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el tareas del grupo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public int EliminarTareasDeGrupo(Grupo grupo)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            
            DbParameter param = dpf.CreateParameter();

            param.DbType = System.Data.DbType.Int32;
            param.Value = grupo.GRUP_Interno;
            param.ParameterName = "GRUP_Interno";
            parametros.Add(param);

            
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_EliminarTareasDeGrupo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la tarea del grupo", ex);
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
