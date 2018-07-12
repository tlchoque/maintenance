using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;//para la clase Equipo; agregar la referencia
using System.Data;//para DbType

namespace Mantenimiento.DAO
{
    public class EquipoDAO:DataAccess
    {
        public EquipoDAO()
        {
        }

#region METODOS PARA PAGINAR
        public List<Equipo> ObtenerCualquierPaginaEquipos(int TamanioPagina,int NumeroPagina)
        {
            //@TamanioPagina int,
            //@NumeroPagina int
            List<Equipo> Equipos = new List<Equipo>();

            string StoredProcedure = "PA_ObtenerCualquierPaginaEquipos";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = TamanioPagina;
            param.ParameterName = "TamanioPagina";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = NumeroPagina;
            param1.ParameterName = "NumeroPagina";
            parametros.Add(param1);

            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                
                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    
                    Equipo equipo = null;
                    
                    try
                    {
                        equipo = new Equipo();
                        equipo.EQUI_Interno = (int)dr["EQUI_Interno"]; 
                        equipo.EQUI_Nombre = dr["EQUI_Nombre"] == System.DBNull.Value ? null : (string)(dr["EQUI_Nombre"]);
                        equipo.EQUI_Marca = dr["EQUI_Marca"] == System.DBNull.Value ? null : (string)(dr["EQUI_Marca"]);
                        equipo.EQUI_Modelo = dr["EQUI_Modelo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Modelo"]);
                        equipo.EQUI_Serie = dr["EQUI_Serie"] == System.DBNull.Value ? null : (string)(dr["EQUI_Serie"]);
                        equipo.EQUI_Codigo = dr["EQUI_Codigo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Codigo"]);

                        if (dr["EQUI_AnioFabricacion"] == System.DBNull.Value) equipo.EQUI_AnioFabricacion = null;
                        else equipo.EQUI_AnioFabricacion = (int)(dr["EQUI_AnioFabricacion"]);

                        if (dr["EQUI_AnioServicio"] == System.DBNull.Value) equipo.EQUI_AnioServicio = null;
                        else equipo.EQUI_AnioServicio = (int)(dr["EQUI_AnioServicio"]);

                        equipo.EQUI_Estado = dr["EQUI_Estado"] == System.DBNull.Value ? null : (string)(dr["EQUI_Estado"]);
                        equipo.EQUI_Descripcion = dr["EQUI_Descripcion"] == System.DBNull.Value ? null : (string)(dr["EQUI_Descripcion"]);
                        equipo.TIPO_Interno = (int)(dr["TIPO_Interno"]);

                        
                        
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Equipo a Objecto", ex);
                    }
                    Equipos.Add(equipo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de equipos - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return Equipos;
        }

        public int ObtenerTotalRegistrosAproximadoEquipos()
        {
            string StoredProcedure = "PA_TotalRegistrosAproximadoEquipo";
            int result = 0;
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                result = (int)EjecuteEscalar(StoredProcedure, null);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad de registros de la tabla Equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public int ObtenerTotalRegistros(){
            string StoredProcedure = "PA_TotalRegistrosEquipo";
            int result = 0;
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                result = (int)EjecuteEscalar(StoredProcedure, null);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cantidad de registros de la tabla Equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        
#endregion
        #region metodos para el filtro
        public List<Equipo> ObtenerCualquierPaginaEquiposFiltradoPorString(int TamanioPagina, int NumeroPagina, string str)
        {
            //@TamanioPagina int,
            //@NumeroPagina int
            List<Equipo> Equipos = new List<Equipo>();

            string StoredProcedure = "PA_ObtenerCualquierPaginaEquiposFiltradoPorString";

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

            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {

                    Equipo equipo = null;

                    try
                    {
                        equipo = new Equipo();
                        equipo.EQUI_Interno = (int)dr["EQUI_Interno"];
                        equipo.EQUI_Nombre = dr["EQUI_Nombre"] == System.DBNull.Value ? null : (string)(dr["EQUI_Nombre"]);
                        equipo.EQUI_Marca = dr["EQUI_Marca"] == System.DBNull.Value ? null : (string)(dr["EQUI_Marca"]);
                        equipo.EQUI_Modelo = dr["EQUI_Modelo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Modelo"]);
                        equipo.EQUI_Serie = dr["EQUI_Serie"] == System.DBNull.Value ? null : (string)(dr["EQUI_Serie"]);
                        equipo.EQUI_Codigo = dr["EQUI_Codigo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Codigo"]);

                        if (dr["EQUI_AnioFabricacion"] == System.DBNull.Value) equipo.EQUI_AnioFabricacion = null;
                        else equipo.EQUI_AnioFabricacion = (int)(dr["EQUI_AnioFabricacion"]);

                        if (dr["EQUI_AnioServicio"] == System.DBNull.Value) equipo.EQUI_AnioServicio = null;
                        else equipo.EQUI_AnioServicio = (int)(dr["EQUI_AnioServicio"]);

                        equipo.EQUI_Estado = dr["EQUI_Estado"] == System.DBNull.Value ? null : (string)(dr["EQUI_Estado"]);
                        equipo.EQUI_Descripcion = dr["EQUI_Descripcion"] == System.DBNull.Value ? null : (string)(dr["EQUI_Descripcion"]);
                        equipo.TIPO_Interno = (int)(dr["TIPO_Interno"]);



                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Equipo a Objecto", ex);
                    }
                    Equipos.Add(equipo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de equipos - página filtrada", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return Equipos;
        }
        #endregion

        public LocalizacionEquipo ObtenerUltimaLocalizacionEquipo(Equipo equipo)
        {
            LocalizacionEquipo localizacion = null;
            string StoredProcedure = "PA_ObtenerUltimaLocalizacionEquipo";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.Value = equipo.EQUI_Interno;
            param.ParameterName = "EQUI_Interno";
            parametros.Add(param);

            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                if (dr.Read())
                {

                    try
                    {
                        localizacion = new LocalizacionEquipo();
                        localizacion.LOCA_Interno = (int)dr["LOCA_Interno"];
                        localizacion.LOCA_Nombre = dr["LOCA_Nombre"] == System.DBNull.Value ? null : (string)(dr["LOCA_Nombre"]);
                        localizacion.LOCA_NombreExtendido = dr["LOCA_NombreExtendido"] == System.DBNull.Value ? null : (string)(dr["LOCA_NombreExtendido"]);
                        if(dr["HILO_Fecha"] == System.DBNull.Value)
                            localizacion.HILO_Fecha = null;
                        else
                            localizacion.HILO_Fecha = (DateTime)dr["HILO_Fecha"];

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Localizacion del Equipo a Objecto", ex);
                    }

                }//fin if
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la última localizacion del equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return localizacion;
        }
        public int InsertarEquipo(Equipo Equipo, int? AUDI_Usuario)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            
            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.Direction = System.Data.ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (Equipo.EQUI_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = Equipo.EQUI_Interno;
            param.ParameterName = "EQUI_Interno";
            
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = Equipo.EQUI_Nombre.ToUpper();
            if (Equipo.EQUI_Nombre == null)
                param0.Value = System.DBNull.Value;
            else
                param0.Value = Equipo.EQUI_Nombre.ToUpper();
            param0.ParameterName = "EQUI_Nombre";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if(Equipo.EQUI_Marca==null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = Equipo.EQUI_Marca.ToUpper();
            param1.ParameterName = "EQUI_Marca";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (Equipo.EQUI_Modelo == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = Equipo.EQUI_Modelo.ToUpper();
            param2.ParameterName = "EQUI_Modelo";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            if (Equipo.EQUI_Serie == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = Equipo.EQUI_Serie.ToUpper();
            param3.ParameterName = "EQUI_Serie";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (Equipo.EQUI_Codigo == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = Equipo.EQUI_Codigo.ToUpper();
            param4.ParameterName = "EQUI_Codigo";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            if (Equipo.EQUI_AnioFabricacion == null)
                param5.Value = System.DBNull.Value;
            else
                param5.Value = Equipo.EQUI_AnioFabricacion;
            param5.ParameterName = "EQUI_AnioFabricacion";
            parametros.Add(param5);
            
            //6
            DbParameter param7 = dpf.CreateParameter();
            if (Equipo.EQUI_AnioServicio == null)
                param7.Value = System.DBNull.Value;
            else
                param7.Value = Equipo.EQUI_AnioServicio;
            param7.ParameterName = "EQUI_AnioServicio";
            parametros.Add(param7);

            DbParameter param8 = dpf.CreateParameter();
            param8.Value = Equipo.EQUI_Estado;
            param8.ParameterName = "EQUI_Estado";
            parametros.Add(param8);

            DbParameter param9 = dpf.CreateParameter();
            param9.Value = Equipo.EQUI_Descripcion.ToUpper();
            param9.ParameterName = "EQUI_Descripcion";
            parametros.Add(param9);

            DbParameter param10 = dpf.CreateParameter();
            param10.Value = Equipo.TIPO_Interno;
            param10.ParameterName = "TIPO_Interno";
            param10.DbType = System.Data.DbType.Int32;
            parametros.Add(param10);

            DbParameter param11 = dpf.CreateParameter();
            if (AUDI_Usuario == null)
                param11.Value = System.DBNull.Value;
            else
                param11.Value = AUDI_Usuario;
            param11.ParameterName = "AUDI_Usuario";
            parametros.Add(param11);

            DbParameter param12 = dpf.CreateParameter();
            if (Equipo.LOCA_Interno == null)
                param12.Value = System.DBNull.Value;
            else
                param12.Value =Equipo.LOCA_Interno;
            param12.ParameterName = "LOCA_Interno";
            parametros.Add(param12);

            DbParameter param13 = dpf.CreateParameter();
            if (Equipo.HILO_Fecha == null)
                param13.Value = System.DBNull.Value;
            else
                param13.Value = Equipo.HILO_Fecha;
            param13.ParameterName = "HILO_Fecha";
            parametros.Add(param13);

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQueryOutID("PA_InsertarEquipo", parametros,"EQUI_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            //if (result > 0)
            //{
            //    return (int)cmd.Parameters["EQUI_Interno"].Value;
            //}
            //else
            //    return 0;
            return result;
        }
        public Equipo ObtenerEquipoPorID(Equipo ObjEquipo)
        {
            Equipo equipo = null;
            string StoredProcedure = "PA_ObtenerEquipoPorID";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = ObjEquipo.EQUI_Interno;
            param.ParameterName = "EQUI_Interno";
            parametros.Add(param);


            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                if (dr.Read())
                {

                    try
                    {
                        equipo = new Equipo();
                        equipo.EQUI_Interno = (int)dr["EQUI_Interno"];
                        equipo.EQUI_Nombre = dr["EQUI_Nombre"] == System.DBNull.Value ? null : (string)(dr["EQUI_Nombre"]);
                        equipo.EQUI_Marca = dr["EQUI_Marca"] == System.DBNull.Value ? null : (string)(dr["EQUI_Marca"]);
                        equipo.EQUI_Modelo = dr["EQUI_Modelo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Modelo"]);
                        equipo.EQUI_Serie = dr["EQUI_Serie"] == System.DBNull.Value ? null : (string)(dr["EQUI_Serie"]);
                        equipo.EQUI_Codigo = dr["EQUI_Codigo"] == System.DBNull.Value ? null : (string)(dr["EQUI_Codigo"]);

                        if (dr["EQUI_AnioFabricacion"] == System.DBNull.Value) equipo.EQUI_AnioFabricacion = null;
                        else equipo.EQUI_AnioFabricacion = (int)(dr["EQUI_AnioFabricacion"]);

                        if (dr["EQUI_AnioServicio"] == System.DBNull.Value) equipo.EQUI_AnioServicio = null;
                        else equipo.EQUI_AnioServicio = (int)(dr["EQUI_AnioServicio"]);

                        equipo.EQUI_Estado = dr["EQUI_Estado"] == System.DBNull.Value ? null : (string)(dr["EQUI_Estado"]);
                        equipo.EQUI_Descripcion = dr["EQUI_Descripcion"] == System.DBNull.Value ? null : (string)(dr["EQUI_Descripcion"]);
                        equipo.TIPO_Interno = (int)(dr["TIPO_Interno"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Equipo a Objecto", ex);
                    }
                    
                }//fin if
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return equipo;
        }
        public int EditarEquipo(Equipo Equipo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            
            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.Value = Equipo.EQUI_Interno;
            param.ParameterName = "EQUI_Interno";
            
            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            param0.Value = Equipo.EQUI_Nombre;
            param0.ParameterName = "EQUI_Nombre";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = Equipo.EQUI_Marca;
            param1.ParameterName = "EQUI_Marca";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            param2.Value = Equipo.EQUI_Modelo;
            param2.ParameterName = "EQUI_Modelo";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            param3.Value = Equipo.EQUI_Serie;
            param3.ParameterName = "EQUI_Serie";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            param4.Value = Equipo.EQUI_Codigo;
            param4.ParameterName = "EQUI_Codigo";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            param5.Value = Equipo.EQUI_AnioFabricacion;
            param5.ParameterName = "EQUI_AnioFabricacion";
            parametros.Add(param5);

            //6
            DbParameter param7 = dpf.CreateParameter();
            param7.Value = Equipo.EQUI_AnioServicio;
            param7.ParameterName = "EQUI_AnioServicio";
            parametros.Add(param7);

            DbParameter param8 = dpf.CreateParameter();
            param8.Value = Equipo.EQUI_Estado;
            param8.ParameterName = "EQUI_Estado";
            parametros.Add(param8);

            DbParameter param9 = dpf.CreateParameter();
            param9.Value = Equipo.EQUI_Descripcion;
            param9.ParameterName = "EQUI_Descripcion";
            parametros.Add(param9);

            DbParameter param10 = dpf.CreateParameter();
            param10.Value = Equipo.EQUI_Interno;
            param10.ParameterName = "TIPO_Interno";
            parametros.Add(param10);

            DbParameter param11 = dpf.CreateParameter();
            param11.Value = AUDI_UsuarioEdita;
            param11.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param11);

            #endregion
            int result = 0;
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_EditarEquipo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el Equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int EliminarEquipo(Equipo Equipo, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Equipo.EQUI_Interno;
            param.ParameterName = "EQUI_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            param1.Value = AUDI_UsuarioEdita;
            param1.ParameterName = "AUDI_UsuarioEdita";
            parametros.Add(param1);

            int result = 0;
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                result = EjecuteNonQuery("PA_EliminarEquipo", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar equipo", ex);
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
