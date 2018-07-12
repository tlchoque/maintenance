using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class LocalizacionEquipo
    {
        private int? _EQUI_Interno;
        public int? EQUI_Interno
        {
            get { return _EQUI_Interno; }
            set { _EQUI_Interno = value; }
        }

        private int? _LOCA_Interno;
        public int? LOCA_Interno
        {
            get { return _LOCA_Interno; }
            set { _LOCA_Interno = value; }
        }

        private string _LOCA_Nombre;
        public string LOCA_Nombre
        {
            get { return _LOCA_Nombre; }
            set { _LOCA_Nombre = value; }
        }

        private string _LOCA_NombreExtendido;
        public string LOCA_NombreExtendido
        {
            get { return _LOCA_NombreExtendido; }
            set { _LOCA_NombreExtendido = value; }
        }
        
        private DateTime? _HILO_Fecha;
        public DateTime? HILO_Fecha
        {
            get { return _HILO_Fecha; }
            set { _HILO_Fecha = value; }
        }

        public LocalizacionEquipo()
        {
            
        }
        public LocalizacionEquipo(int? EQUI_Interno, int? LOCA_Interno, DateTime? HILO_Fecha, string LOCA_Nombre, string LOCA_NombreExtendido)
        {
            this.EQUI_Interno = EQUI_Interno;
            this.LOCA_Interno = LOCA_Interno;
            this.HILO_Fecha = HILO_Fecha;
            this.LOCA_Nombre = LOCA_Nombre;
            this.LOCA_NombreExtendido = LOCA_NombreExtendido;
              
        }

    }
}
