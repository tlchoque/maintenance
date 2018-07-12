using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class PartePlan
    {
        private int? _PART_Interno;

        public int? PART_Interno
        {
            get { return _PART_Interno; }
            set { _PART_Interno = value; }
        }

        private string _PART_Nombre;

        public string PART_Nombre
        {
            get { return _PART_Nombre; }
            set { _PART_Nombre = value; }
        }

        private string _PART_NombreExtendido;

        public string PART_NombreExtendido
        {
            get { return _PART_NombreExtendido; }
            set { _PART_NombreExtendido = value; }
        }

        private int? _PART_Origen;

        public int? PART_Origen
        {
            get { return _PART_Origen; }
            set { _PART_Origen = value; }
        }

        private int? _PLAN_Interno;

        public int? PLAN_Interno
        {
            get { return _PLAN_Interno; }
            set { _PLAN_Interno = value; }
        }

        public PartePlan() { }

        public PartePlan(int? PART_Interno)
        {
            this.PART_Interno = PART_Interno;
        }

        private List<ActividadR> _PART_Actividades;

        public List<ActividadR> PART_Actividades
        {
            get { return _PART_Actividades; }
            set { _PART_Actividades = value; }
        }

        public PartePlan(int? PART_Interno, string PART_Nombre, int? PART_Origen)
        {
            this.PART_Interno = PART_Interno;
            this.PART_Nombre = PART_Nombre;
            this.PART_Origen = PART_Origen;
        }

        public PartePlan(int? PART_Interno, string PART_Nombre, string PART_NombreExtendido, int? PART_Origen, int? PLAN_Interno)
        {
            this.PART_Interno = PART_Interno;
            this.PART_Nombre = PART_Nombre;
            this.PART_NombreExtendido = PART_NombreExtendido;
            this.PART_Origen = PART_Origen;
            this.PLAN_Interno = PLAN_Interno;
        }

        public PartePlan(int? PART_Interno, string PART_NombreExxtendido, List<ActividadR> PART_Actividades)
        {
            this.PART_Interno = PART_Interno;            
            this.PART_NombreExtendido = PART_NombreExtendido;
            this.PART_Actividades = PART_Actividades;
        }
    }
}
