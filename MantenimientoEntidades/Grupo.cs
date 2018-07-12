using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class Grupo
    {
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

        private string _GRUP_Descripcion;
        public string GRUP_Descripcion
        {
            get { return _GRUP_Descripcion; }
            set { _GRUP_Descripcion = value; }
        }

        private Boolean? _GRUP_Activo;
        public Boolean? GRUP_Activo
        {
            get { return _GRUP_Activo; }
            set { _GRUP_Activo = value; }
        }
        

        private int? _AUDI_UsuarioCrea;
        public int? AUDI_UsuarioCrea
        {
            get { return _AUDI_UsuarioCrea; }
            set { _AUDI_UsuarioCrea = value; }
        }

        private string _GRUP_ID;
        public string GRUP_ID
        {
            get { return _GRUP_ID; }
            set { _GRUP_ID = value; }
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

        private String _GRUP_Tareas;
        public String GRUP_Tareas
        {
            get { return _GRUP_Tareas; }
            set { _GRUP_Tareas = value; }
        }

        public Grupo()
        {
        }
        public Grupo(int? GRUP_Interno,string GRUP_Nombre, string GRUP_Descripcion, Boolean? GRUP_Activo)
        {
            this.GRUP_Interno = GRUP_Interno;
            this.GRUP_Nombre = GRUP_Nombre;
            this.GRUP_Descripcion = GRUP_Descripcion;
            this.GRUP_Activo = GRUP_Activo;
        }
    }
}
