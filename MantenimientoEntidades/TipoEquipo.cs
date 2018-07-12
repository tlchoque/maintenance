using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    
    public class TipoEquipo
    {
        //TIPO_Interno, TIPO_Nombre
        private int _TIPO_Interno;
        private string _TIPO_Nombre;

        public int TIPO_Interno
        {
            get { return _TIPO_Interno; }
            set { _TIPO_Interno = value; }
        }
        public string TIPO_Nombre
        {
            get { return _TIPO_Nombre; }
            set { _TIPO_Nombre = value; }
        }
        public TipoEquipo()
        {
        }
        public TipoEquipo(int TIPO_Interno, string TIPO_Nombre)
        {
            this.TIPO_Interno = TIPO_Interno;
            this.TIPO_Nombre = TIPO_Nombre;
        }
    }
}
