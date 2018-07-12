using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class PlanTrabajo
    {
        private int? _PLAN_Interno;

        public int? PLAN_Interno
        {
            get { return _PLAN_Interno; }
            set { _PLAN_Interno = value; }
        }

        private string _PLAN_Descripcion;

        public string PLAN_Descripcion
        {
            get { return _PLAN_Descripcion; }
            set { _PLAN_Descripcion = value; }
        }

        private string _PLAN_Regimen;

        public string PLAN_Regimen
        {
            get { return _PLAN_Regimen; }
            set { _PLAN_Regimen = value; }
        }

        private string _PLAN_UnidadLecturas;

        public string PLAN_UnidadLecturas
        {
            get { return _PLAN_UnidadLecturas; }
            set { _PLAN_UnidadLecturas = value; }
        }

        public PlanTrabajo() { }

        public PlanTrabajo(int PLAN_Interno) 
        {
            this.PLAN_Interno = PLAN_Interno;
        }

        private string _PLAN_ID;
        public string PLAN_ID
        {
            get { return _PLAN_ID; }
            set { _PLAN_ID = value; }
        }

        public PlanTrabajo(int? PLAN_Interno, string PLAN_Descripcion, string PLAN_Regimen, string PLAN_UnidadLecturas)
        {
            this.PLAN_Interno = PLAN_Interno;
            this.PLAN_Descripcion = PLAN_Descripcion;
            this.PLAN_Regimen = PLAN_Regimen;
            this.PLAN_UnidadLecturas = PLAN_UnidadLecturas;
        }
    }
}
