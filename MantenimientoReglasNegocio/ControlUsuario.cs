using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable
namespace Mantenimiento.ReglasNegocio
{
    public class ControlUsuario
    {
        public ControlUsuario()
        {
        }
        public Usuario ObtenerPerfil(Usuario _usuario)
        {
            UsuarioDAO DataUsuario = new UsuarioDAO();
            //solo mostramos los datos necesarios
            Usuario usuario = DataUsuario.ObtenerUsuarioPorID(_usuario);
            usuario.GRUP_Interno = null;
            return usuario;
        }
        public int EditarPerfilUsuario(Usuario usuario)
        {
            UsuarioDAO DataUsuario = new UsuarioDAO();
            if (usuario.USUA_Contrasenia != null)
            {
                usuario.USUA_Contrasenia = Seguridad.EncriptarContrasenia(usuario);
            }
            return DataUsuario.EditarPerfil(usuario);
        }
        public Usuario ObtenerDatosUsuario(Usuario _usuario)
        {
            UsuarioDAO DataUsuario = new UsuarioDAO();

            Usuario usuario = DataUsuario.ObtenerUsuarioPorID(_usuario);  
            //obtenemos el nombre de usuario creador
            Usuario UsuarioCreador = new Usuario();
            UsuarioCreador.USUA_Interno = usuario.AUDI_UsuarioCrea;
            UsuarioCreador = DataUsuario.ObtenerUsuarioPorID(UsuarioCreador);
            usuario.UsuarioCreador = UsuarioCreador.USUA_Usuario;
            //Obtenemos el nombre del grupo
            if (usuario.GRUP_Interno != null)
            {
                GrupoDAO DataGrupo = new GrupoDAO();
                Grupo grupo = new Grupo();
                grupo.GRUP_Interno = usuario.GRUP_Interno;
                grupo = DataGrupo.ObtenerGrupoPorID(grupo);
                usuario.GRUP_Nombre = grupo.GRUP_Nombre;
            }
            //Obtenemos el ultimo ingreso del usuario al sistema
            HistorialIngresoDAO histIngrDAO = new HistorialIngresoDAO();
            HistorialIngreso histIngr = new HistorialIngreso();
            histIngr.USUA_Interno = usuario.USUA_Interno;
            histIngr = histIngrDAO.ObtenerUltimoIngresoUsuario(histIngr);
            if (histIngr != null)
            {
                usuario.HIIN_FechaIngreso = histIngr.HIIN_FechaIngreso;
                usuario.HIIN_IPacceso = histIngr.HIIN_IPacceso;
            }
            return usuario;
        }
        public List<Usuario> ObtenerCualquierPaginaUsuarios(int TamanioPagina, int NumeroPagina)
        {
            UsuarioDAO DataUsuario = new UsuarioDAO();

            List<Usuario> Usuarios = DataUsuario.ObtenerCualquierPaginaUsuarios(TamanioPagina, NumeroPagina);
            List<Usuario> ListaUsuarios = new List<Usuario>();
            foreach (Usuario objUsuario in Usuarios)
            {
                Usuario usuario = new Usuario();
                usuario = objUsuario;
                //obtenemos el nombre de usuario creador
                Usuario UsuarioCreador = new Usuario();
                UsuarioCreador.USUA_Interno = objUsuario.AUDI_UsuarioCrea;
                UsuarioCreador = DataUsuario.ObtenerUsuarioPorID(UsuarioCreador);
                usuario.UsuarioCreador = UsuarioCreador.USUA_Usuario;
                //Obtenemos el nombre del grupo
                if (objUsuario.GRUP_Interno != null)
                {
                    GrupoDAO DataGrupo = new GrupoDAO();
                    Grupo grupo = new Grupo();
                    grupo.GRUP_Interno = objUsuario.GRUP_Interno;
                    grupo = DataGrupo.ObtenerGrupoPorID(grupo);
                    usuario.GRUP_Nombre = grupo.GRUP_Nombre;
                }
                //Obtenemos el ultimo ingreso del usuario al sistema
                HistorialIngresoDAO histIngrDAO = new HistorialIngresoDAO();
                HistorialIngreso histIngr = new HistorialIngreso();
                histIngr.USUA_Interno = objUsuario.USUA_Interno;
                histIngr = histIngrDAO.ObtenerUltimoIngresoUsuario(histIngr);
                if (histIngr != null)
                {
                    usuario.HIIN_FechaIngreso = histIngr.HIIN_FechaIngreso;
                    //usuario.HIIN_IPacceso = histIngr.HIIN_IPacceso;
                }
                ListaUsuarios.Add(usuario);
            }
            return ListaUsuarios;
        }

        public List<Usuario> ObtenerCualquierPaginaUsuariosFiltradoPorString(int TamanioPagina, int NumeroPagina, string str)
        {
            UsuarioDAO DataUsuario = new UsuarioDAO();

            List<Usuario> Usuarios = DataUsuario.ObtenerCualquierPaginaUsuariosFiltradoPorString(TamanioPagina, NumeroPagina,str);
            List<Usuario> ListaUsuarios = new List<Usuario>();
            foreach (Usuario objUsuario in Usuarios)
            {
                Usuario usuario = new Usuario();
                usuario = objUsuario;
                //obtenemos el nombre de usuario creador
                Usuario UsuarioCreador = new Usuario();
                UsuarioCreador.USUA_Interno = objUsuario.AUDI_UsuarioCrea;
                UsuarioCreador = DataUsuario.ObtenerUsuarioPorID(UsuarioCreador);
                usuario.UsuarioCreador = UsuarioCreador.USUA_Usuario;
                //Obtenemos el nombre del grupo
                if (objUsuario.GRUP_Interno != null)
                {
                    GrupoDAO DataGrupo = new GrupoDAO();
                    Grupo grupo = new Grupo();
                    grupo.GRUP_Interno = objUsuario.GRUP_Interno;
                    grupo = DataGrupo.ObtenerGrupoPorID(grupo);
                    usuario.GRUP_Nombre = grupo.GRUP_Nombre;
                }
                //Obtenemos el ultimo ingreso del usuario al sistema
                HistorialIngresoDAO histIngrDAO = new HistorialIngresoDAO();
                HistorialIngreso histIngr = new HistorialIngreso();
                histIngr.USUA_Interno = objUsuario.USUA_Interno;
                histIngr = histIngrDAO.ObtenerUltimoIngresoUsuario(histIngr);
                if (histIngr != null)
                {
                    usuario.HIIN_FechaIngreso = histIngr.HIIN_FechaIngreso;
                }
                ListaUsuarios.Add(usuario);
            }
            return ListaUsuarios;
        }

        public int ObtenerTotalUsuarios()
        {
            UsuarioDAO D = new UsuarioDAO();
            return D.ObtenerTotalUsuarios();
        }

        public int InsertarUsuario(Usuario usuario,int AUDI_Usuario)
        {
            UsuarioDAO DataUser = new UsuarioDAO();
            if (usuario.USUA_Interno == null)
            {
                //generamos el usuario
                usuario.USUA_Usuario = this.GenerarNombreDeAcceso(usuario);
            }
            if (usuario.USUA_Contrasenia != null)
            {
                usuario.USUA_Contrasenia = Seguridad.EncriptarContrasenia(usuario);
            }
            return DataUser.InsertarUsuario(usuario, AUDI_Usuario);
        }
        protected string GenerarNombreDeAcceso(Usuario usuario)
        {
            string[] Apellidos = usuario.USUA_Apellido.Split(' ');
            Apellidos[0] = Apellidos[0].Trim();


            UsuarioDAO DU = new UsuarioDAO();
            Boolean _bol = true;
            int str = 1;
            string nameaux = "";
            if (Apellidos.Length < 2)
            {
                nameaux = usuario.USUA_Nombre.Substring(0, 1) + Apellidos[0];
            }
            else
            {
                nameaux = usuario.USUA_Nombre.Substring(0, 1) + Apellidos[0] + Apellidos[1].Substring(0, 1);
            }

            string name = nameaux;
            while (_bol)
            {
                _bol = DU.ExisteNombreDeAcceso(name);
                if (_bol)
                {
                    name = nameaux + str.ToString();
                    str++;
                }
            }
            return name;
        }
        public int EliminarUsuario(Usuario usuario, int AUDI_UsuarioEdita)
        {
            UsuarioDAO dataU = new UsuarioDAO();
            return dataU.EliminarUsuario(usuario, AUDI_UsuarioEdita);
        }
        public int ActivarUsuario(Usuario usuario, int AUDI_UsuarioEdita)
        {
            UsuarioDAO dataU = new UsuarioDAO();
            return dataU.ActivarUsuario(usuario, AUDI_UsuarioEdita);
        }
        public int DesactivarUsuario(Usuario usuario, int AUDI_UsuarioEdita)
        {
            UsuarioDAO dataU = new UsuarioDAO();
            return dataU.DesactivarUsuario(usuario, AUDI_UsuarioEdita);
        }
    }
}
