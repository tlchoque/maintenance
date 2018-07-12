using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class LocalizacionS
    {
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

        private int? _LOCA_Origen;

        public int? LOCA_Origen
        {
            get { return _LOCA_Origen; }
            set { _LOCA_Origen = value; }
        }

        private string _LOCA_EstadoMantenimiento;

        public string LOCA_EstadoMantenimiento
        {
            get { return _LOCA_EstadoMantenimiento; }
            set { _LOCA_EstadoMantenimiento = value; }
        }

        private int? _PLAN_Interno;

        public int? PLAN_Interno
        {
            get { return _PLAN_Interno; }
            set { _PLAN_Interno = value; }
        }

        public LocalizacionS() { }

        public LocalizacionS(int LOCA_Interno) 
        {
            this.LOCA_Interno = LOCA_Interno;
        }

        public LocalizacionS(int? LOCA_Interno, string LOCA_Nombre, string LOCA_NombreExtendido, int? LOCA_Origen)
        {
            this.LOCA_Interno = LOCA_Interno;
            this.LOCA_Nombre = LOCA_Nombre;
            this.LOCA_NombreExtendido = LOCA_NombreExtendido;
            this.LOCA_Origen = LOCA_Origen;
        }

        public LocalizacionS(int? LOCA_Interno, string LOCA_Nombre, string LOCA_NombreExtendido, int LOCA_Origen)
        {
            this.LOCA_Interno = LOCA_Interno;
            this.LOCA_Nombre = LOCA_Nombre;
            this.LOCA_NombreExtendido = LOCA_NombreExtendido;
            this.LOCA_Origen = LOCA_Origen;
        }
    }
}
