using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class NombreActividad
    {
        private int _NOMB_Interno;

        public int NOMB_Interno
        {
            get { return _NOMB_Interno; }
            set { _NOMB_Interno = value; }
        }

        private string _NOMB_Descripcion;

        public string NOMB_Descripcion
        {
            get { return _NOMB_Descripcion; }
            set { _NOMB_Descripcion = value; }
        }

        public NombreActividad() { }

        public NombreActividad(int NOMB_Interno, string NOMB_Descripcion)
        {
            this.NOMB_Interno = NOMB_Interno;
            this.NOMB_Descripcion = NOMB_Descripcion;
        }

    }
}
