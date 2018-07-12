using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using Mantenimiento.Entidades;
using System.Data;//para DbType
namespace Mantenimiento.DAO
{
    public class UsuarioDAO:DataAccess
    {
        public UsuarioDAO()
        {
        }
        public List<Usuario> ObtenerCualquierPaginaUsuarios(int TamanioPagina, int NumeroPagina)
        {
            //@TamanioPagina int,
            //@NumeroPagina int
            List<Usuario> usuarios = new List<Usuario>();

            string StoredProcedure = "PA_ObtenerCualquierPaginaUsuarios";

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
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {

                    Usuario usuario = null;

                    try
                    {
                        usuario = new Usuario();
                        usuario.USUA_Interno = (int)dr["USUA_Interno"];
                        usuario.USUA_Usuario = (string)(dr["USUA_Usuario"]);
                        usuario.USUA_Nombre =  (string)(dr["USUA_Nombre"]);
                        usuario.USUA_Apellido = (string)(dr["USUA_Apellido"]);
                        usuario.USUA_Direccion = dr["USUA_Direccion"] == System.DBNull.Value ? null : (string)(dr["USUA_Direccion"]);
                        usuario.USUA_Correo = dr["USUA_Correo"] == System.DBNull.Value ? null : (string)(dr["USUA_Correo"]);
                        usuario.USUA_Activo = (Boolean)(dr["USUA_Activo"]);
                        usuario.GRUP_Interno = dr["GRUP_Interno"] == System.DBNull.Value ? null : (int?)(dr["GRUP_Interno"]);
                        usuario.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        usuario.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de usuario a Objecto", ex);
                    }
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return usuarios;
        }

        public List<Usuario> ObtenerCualquierPaginaUsuariosFiltradoPorString(int TamanioPagina, int NumeroPagina, string str)
        {
            
            List<Usuario> usuarios = new List<Usuario>();

            string StoredProcedure = "PA_ObtenerCualquierPaginaUsuariosFiltradoPorString";

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
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, parametros);
                while (dr.Read())
                {
                    Usuario usuario = null;

                    try
                    {
                        usuario = new Usuario();
                        usuario.USUA_Interno = (int)dr["USUA_Interno"];
                        usuario.USUA_Usuario = (string)(dr["USUA_Usuario"]);
                        usuario.USUA_Nombre = (string)(dr["USUA_Nombre"]);
                        usuario.USUA_Apellido = (string)(dr["USUA_Apellido"]);
                        usuario.USUA_Direccion = dr["USUA_Direccion"] == System.DBNull.Value ? null : (string)(dr["USUA_Direccion"]);
                        usuario.USUA_Correo = dr["USUA_Correo"] == System.DBNull.Value ? null : (string)(dr["USUA_Correo"]);
                        usuario.USUA_Activo = (Boolean)(dr["USUA_Activo"]);
                        usuario.GRUP_Interno = dr["GRUP_Interno"] == System.DBNull.Value ? null : (int?)(dr["GRUP_Interno"]);
                        usuario.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        usuario.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de usuario a Objecto", ex);
                    }
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios - cualquier pagina", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return usuarios;
        }

        public Usuario ObtenerUsuarioPorID(Usuario _usuario)
        {

            Usuario usuario = null;

            string StoredProcedure = "PA_ObtenerUsuarioPorID";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = _usuario.USUA_Interno;
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
                        usuario = new Usuario();
                        usuario.USUA_Interno = (int)dr["USUA_Interno"];
                        usuario.USUA_Usuario = (string)(dr["USUA_Usuario"]);
                        usuario.USUA_Nombre = (string)(dr["USUA_Nombre"]);
                        usuario.USUA_Apellido = (string)(dr["USUA_Apellido"]);
                        usuario.USUA_Direccion = dr["USUA_Direccion"] == System.DBNull.Value ? null : (string)(dr["USUA_Direccion"]);
                        usuario.USUA_Correo = dr["USUA_Correo"] == System.DBNull.Value ? null : (string)(dr["USUA_Correo"]);
                        usuario.USUA_Activo = (Boolean)(dr["USUA_Activo"]);
                        usuario.GRUP_Interno = dr["GRUP_Interno"] == System.DBNull.Value ? null : (int?)(dr["GRUP_Interno"]);
                        usuario.AUDI_UsuarioCrea = (int)(dr["AUDI_UsuarioCrea"]);
                        usuario.AUDI_FechaCrea = (DateTime)(dr["AUDI_FechaCrea"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de usuario a Objecto", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos del usuario", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return usuario;
        }

        public int ObtenerTotalUsuarios()
        {
            string StoredProcedure = "PA_ObtenerTotalUsuarios";
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
                throw new Exception("Error al obtener la cantidad de registros de la tabla Usuarios", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int ActivarUsuario(Usuario Usuario, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Usuario.USUA_Interno;
            param.ParameterName = "USUA_Interno";
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
                result = EjecuteNonQuery("PA_ActivarUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al activar usuario", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int DesactivarUsuario(Usuario Usuario, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Usuario.USUA_Interno;
            param.ParameterName = "USUA_Interno";
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
                result = EjecuteNonQuery("PA_DesactivarUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar usuario", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int EliminarUsuario(Usuario Usuario, int AUDI_UsuarioEdita)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = Usuario.USUA_Interno;
            param.ParameterName = "USUA_Interno";
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
                result = EjecuteNonQuery("PA_EliminarUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }

        public int InsertarUsuario(Usuario Usuario, int? AUDI_Usuario)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.Direction = System.Data.ParameterDirection.InputOutput;
            param.DbType = System.Data.DbType.Int32;
            if (Usuario.USUA_Interno == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = Usuario.USUA_Interno;
            param.ParameterName = "USUA_Interno";

            parametros.Add(param);

            DbParameter param0 = dpf.CreateParameter();
            if (Usuario.USUA_Usuario == null)
                param0.Value = System.DBNull.Value;
            else
                param0.Value = Usuario.USUA_Usuario;
            param0.ParameterName = "USUA_Usuario";
            parametros.Add(param0);

            DbParameter param1 = dpf.CreateParameter();
            if (Usuario.USUA_Nombre == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = Usuario.USUA_Nombre.ToUpper();
            param1.ParameterName = "USUA_Nombre";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (Usuario.USUA_Apellido == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = Usuario.USUA_Apellido.ToUpper();
            param2.ParameterName = "USUA_Apellido";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            if (Usuario.USUA_Direccion == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = Usuario.USUA_Direccion;
            param3.ParameterName = "USUA_Direccion";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (Usuario.USUA_Correo == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = Usuario.USUA_Correo;
            param4.ParameterName = "USUA_Correo";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            param5.Value = Usuario.USUA_Activo;
            param5.ParameterName = "USUA_Activo";
            param.DbType = System.Data.DbType.Boolean;
            parametros.Add(param5);

            //6
            DbParameter param6 = dpf.CreateParameter();
            if (Usuario.GRUP_Interno == null)
                param6.Value = System.DBNull.Value;
            else
                param6.Value = Usuario.GRUP_Interno;
            param6.ParameterName = "GRUP_Interno";
            param.DbType = System.Data.DbType.Int32;
            parametros.Add(param6);

            DbParameter param7 = dpf.CreateParameter();
            if (Usuario.USUA_Contrasenia == null)
                param7.Value = System.DBNull.Value;
            else
                param7.Value = Usuario.USUA_Contrasenia;
            param7.ParameterName = "USUA_Contrasenia";
            parametros.Add(param7);

            DbParameter param8 = dpf.CreateParameter();
            param8.Value = AUDI_Usuario;
            param8.ParameterName = "AUDI_Usuario";
            parametros.Add(param8);

           

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQueryOutID("PA_InsertarUsuario", parametros, "USUA_Interno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Usuario", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public int EditarPerfil(Usuario Usuario)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            #region PARAMETROS
            DbParameter param = dpf.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.Value = Usuario.USUA_Interno;
            param.ParameterName = "USUA_Interno";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            if (Usuario.USUA_Nombre == null)
                param1.Value = System.DBNull.Value;
            else
                param1.Value = Usuario.USUA_Nombre.ToUpper();
            param1.ParameterName = "USUA_Nombre";
            parametros.Add(param1);

            DbParameter param2 = dpf.CreateParameter();
            if (Usuario.USUA_Apellido == null)
                param2.Value = System.DBNull.Value;
            else
                param2.Value = Usuario.USUA_Apellido.ToUpper();
            param2.ParameterName = "USUA_Apellido";
            parametros.Add(param2);

            DbParameter param3 = dpf.CreateParameter();
            if (Usuario.USUA_Direccion == null)
                param3.Value = System.DBNull.Value;
            else
                param3.Value = Usuario.USUA_Direccion;
            param3.ParameterName = "USUA_Direccion";
            parametros.Add(param3);

            DbParameter param4 = dpf.CreateParameter();
            if (Usuario.USUA_Correo == null)
                param4.Value = System.DBNull.Value;
            else
                param4.Value = Usuario.USUA_Correo;
            param4.ParameterName = "USUA_Correo";
            parametros.Add(param4);

            DbParameter param5 = dpf.CreateParameter();
            if (Usuario.USUA_Contrasenia == null)
                param5.Value = System.DBNull.Value;
            else
                param5.Value = Usuario.USUA_Contrasenia;
            param5.ParameterName = "USUA_Contrasenia";
            parametros.Add(param5);

            DbParameter param6 = dpf.CreateParameter();
            param6.Value = Usuario.USUA_Interno;//esta editando el mismo usuario
            param6.ParameterName = "AUDI_Usuario";
            parametros.Add(param6);

            #endregion
            int result = 0;
            DbCommand cmd = dpf.CreateCommand();
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();


                result = EjecuteNonQuery("PA_EditarPerfilUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar datos", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return result;
        }
        public  Boolean ExisteNombreDeAcceso(string NombreAcceso)
        {

            Boolean result = false;

            string StoredProcedure = "PA_ExisteNombreDeAcceso";

            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            param.Value = NombreAcceso;
            param.ParameterName = "USUA_Usuario";
            parametros.Add(param);

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                if (EjecuteEscalar(StoredProcedure, parametros) != null)
                    result = true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el escalar", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return result;
        }

        public int Autenticar(Usuario usuario)
        {
            List<DbParameter> parametros = new List<DbParameter>();

            DbParameter param = dpf.CreateParameter();
            if(usuario.USUA_Usuario == null) param.Value=System.DBNull.Value;
            else param.Value = usuario.USUA_Usuario;
            param.ParameterName = "USUA_Usuario";
            parametros.Add(param);

            DbParameter param1 = dpf.CreateParameter();
            if (usuario.USUA_Contrasenia == null) param1.Value = System.DBNull.Value;
            else param1.Value = usuario.USUA_Contrasenia;
            param1.ParameterName = "USUA_Contrasenia";
            parametros.Add(param1);

            string StoredProcedure = "PA_Autenticar";
            int result = 0;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                result = Convert.ToInt32(EjecuteEscalar(StoredProcedure, parametros));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el scalar - Autenticar", ex);
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
