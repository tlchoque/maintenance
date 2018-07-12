using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class HistorialIngreso
    {
        private int? _HIIN_Interno;
        public int? HIIN_Interno
        {
            get { return _HIIN_Interno; }
            set { _HIIN_Interno = value; }
        }

        private int? _USUA_Interno;
        public int? USUA_Interno
        {
            get { return _USUA_Interno; }
            set { _USUA_Interno = value; }
        }

        private DateTime? _HIIN_FechaIngreso;
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

        public HistorialIngreso()
        {
        }
        public HistorialIngreso(int? HIIN_Interno, int USUA_Interno, DateTime? HIIN_FechaIngreso,string HIIN_IPacceso)
        {
            this.HIIN_Interno = HIIN_Interno;
            this.USUA_Interno = USUA_Interno;
            this.HIIN_FechaIngreso = HIIN_FechaIngreso;
            this.HIIN_IPacceso = HIIN_IPacceso;
        }
    }
}
