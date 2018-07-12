using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class Usuario
    {
        #region Propiedades
        private int? _USUA_Interno;
        public int? USUA_Interno
        {
            get { return _USUA_Interno; }
            set { _USUA_Interno = value; }
        }

        private string _USUA_Usuario;
        public string USUA_Usuario
        {
            get { return _USUA_Usuario; }
            set { _USUA_Usuario = value; }
        }

        private string _USUA_Contrasenia;
        public string USUA_Contrasenia
        {
            get { return _USUA_Contrasenia; }
            set { _USUA_Contrasenia = value; }
        }

        private string _USUA_Nombre;
        public string USUA_Nombre
        {
            get { return _USUA_Nombre; }
            set { _USUA_Nombre = value; }
        }

        private string _USUA_Apellido;
        public string USUA_Apellido
        {
            get { return _USUA_Apellido; }
            set { _USUA_Apellido = value; }
        }

        private string _USUA_Direccion;
        public string USUA_Direccion
        {
            get { return _USUA_Direccion; }
            set { _USUA_Direccion = value; }
        }

        private string _USUA_Correo;
        public string USUA_Correo
        {
            get { return _USUA_Correo; }
            set { _USUA_Correo = value; }
        }

        private Boolean _USUA_Activo;
        public Boolean USUA_Activo
        {
            get { return _USUA_Activo; }
            set { _USUA_Activo = value; }
        }

        private int? _GRUP_Interno;
        public int? GRUP_Interno
        {
            get { return _GRUP_Interno; }
            set { _GRUP_Interno = value; }
        }

        private string _GRUP_Nombre;
        public string GRUP_Nombre
        {
            get { return _GRUP_Nombre; }
            set { _GRUP_Nombre = value; }
        }

        private int _AUDI_UsuarioCrea;
        public int AUDI_UsuarioCrea
        {
            get { return _AUDI_UsuarioCrea; }
            set { _AUDI_UsuarioCrea = value; }
        }

        private string _UsuarioCreador;
        public string UsuarioCreador
        {
            get { return _UsuarioCreador; }
            set { _UsuarioCreador = value; }
        }

        private DateTime _AUDI_FechaCrea;
        public DateTime AUDI_FechaCrea
        {
            get { return _AUDI_FechaCrea; }
            set { _AUDI_FechaCrea = value; }
        }

        private DateTime? _HIIN_FechaIngreso;//Para el ultimo ingreso del usuario
        public DateTime? HIIN_FechaIngreso
        {
            get { return _HIIN_FechaIngreso; }
            set { _HIIN_FechaIngreso = value; }
        }

        private string _HIIN_IPacceso;
        public string HIIN_IPacceso
        {
            get { return _HIIN_IPacceso; }
            set { _HIIN_IPacceso = value; }
        }

        private string _USUA_ID;
        public string USUA_ID
        {
            get { return _USUA_ID; }
            set { _USUA_ID = value; }
        }


        #endregion
        #region Contructores
        public Usuario()
        {
        }
        public Usuario(int? USUA_Interno, string USUA_Usuario, string USUA_Nombre, string USUA_Apellido, string USUA_Direccion, string USUA_Correo, Boolean USUA_Activo, int? GRUP_Interno)
        {
            this.USUA_Interno = GRUP_Interno;
            this.USUA_Usuario = USUA_Usuario;
            this.USUA_Nombre = USUA_Nombre;
            this.USUA_Apellido = USUA_Apellido;
            this.USUA_Direccion = USUA_Direccion;
            this.USUA_Correo = USUA_Correo;
            this.USUA_Activo = USUA_Activo;
            this.GRUP_Interno = GRUP_Interno;
        }
        #endregion
    }
}
