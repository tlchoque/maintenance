using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class Tarea
    {
        //m.MODU_Interno,t.TARE_Interno,m.MODU_Nombre,TARE_NombreCorto,t.TARE_Nombre,t.TARE_Descripcion
        private int _MODU_Interno;
        public int MODU_Interno
        {
            get { return _MODU_Interno; }
            set { _MODU_Interno = value; }
        }

        private int _TARE_Interno;
        public int TARE_Interno
        {
            get { return _TARE_Interno; }
            set { _TARE_Interno = value; }
        }

        private string _MODU_Nombre;
        public string MODU_Nombre
        {
            get { return _MODU_Nombre; }
            set { _MODU_Nombre = value; }
        }

        private string _TARE_Nombre;
        public string TARE_Nombre
        {
            get { return _TARE_Nombre; }
            set { _TARE_Nombre = value; }
        }

        private string _TARE_Descripcion;
        public string TARE_Descripcion
        {
            get { return _TARE_Descripcion; }
            set { _TARE_Descripcion = value; }
        }

        private string _TARE_NombreCorto;
        public string TARE_NombreCorto
        {
            get { return _TARE_NombreCorto; }
            set { _TARE_NombreCorto = value; }
        }
        public Tarea()
        {
        }
    }
}
