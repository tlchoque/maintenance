using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class HistorialAR
    {
        private int? _HIAR_Interno;

        public int? HIAR_Interno
        {
            get { return _HIAR_Interno; }
            set { _HIAR_Interno = value; }
        }

        private DateTime? _HIAR_FechaProgramado;

        public DateTime? HIAR_FechaProgramado
        {
            get { return _HIAR_FechaProgramado; }
            set { _HIAR_FechaProgramado = value; }
        }

        private string _HIAR_Ejecutor;

        public string HIAR_Ejecutor
        {
            get { return _HIAR_Ejecutor; }
            set { _HIAR_Ejecutor = value; }
        }

        private DateTime? _HIAR_FechaEjecutado;

        public DateTime? HIAR_FechaEjecutado
        {
            get { return _HIAR_FechaEjecutado; }
            set { _HIAR_FechaEjecutado = value; }
        }

        private DateTime? _HIAR_FechaEjecucionAnterior;
        public DateTime? HIAR_FechaEjecucionAnterior
        {
            get { return _HIAR_FechaEjecucionAnterior; }
            set { _HIAR_FechaEjecucionAnterior = value; }
        }

        private DateTime? _HIAR_SiguienteFecha;

        public DateTime? HIAR_SiguienteFecha
        {
            get { return _HIAR_SiguienteFecha; }
            set { _HIAR_SiguienteFecha = value; }
        }

        private string _HIAR_Observacion;

        public string HIAR_Observacion
        {
            get { return _HIAR_Observacion; }
            set { _HIAR_Observacion = value; }
        }

        private string _HIAR_Estado;

        public string HIAR_Estado
        {
            get { return _HIAR_Estado; }
            set { _HIAR_Estado = value; }
        }

        private int? _ACRU_Interno;

        public int? ACRU_Interno
        {
            get { return _ACRU_Interno; }
            set { _ACRU_Interno = value; }
        }

        private int? _LOCA_Interno;

        public int? LOCA_Interno
        {
            get { return _LOCA_Interno; }
            set { _LOCA_Interno = value; }
        }

        private int? _EQUI_Interno;

        public int? EQUI_Interno
        {
            get { return _EQUI_Interno; }
            set { _EQUI_Interno = value; }
        }

        private int? _PERI_Interno;

        public int? PERI_Interno
        {
            get { return _PERI_Interno; }
            set { _PERI_Interno = value; }
        }

        public HistorialAR() 
        {
            //this.HIAR_Interno = null;
            //this.HIAR_FechaProgramado = null;
            //this.HIAR_Ejecutor = null;
            //this.HIAR_FechaEjecutado = null;
            //this.HIAR_SiguienteFecha = null;
            //this.HIAR_Observacion = null;
            //this.HIAR_Estado = null;
            //this.ACRU_Interno = null;
            ////this.LOCA_Interno = null;
            //this.EQUI_Interno = null;
            //this.PERI_Interno = null;
        }

        public HistorialAR(int? HIAR_Interno,DateTime? HIAR_FechaProgramado,string HIAR_Ejecutor,DateTime? HIAR_FechaEjecutado,
            DateTime? HIAR_SiguienteFecha,string HIAR_Observacion,string HIAR_Estado, int? ACRU_Interno,int? LOCA_Interno,
            int? EQUI_Interno, int? PERI_Interno)
        {
            this.HIAR_Interno = HIAR_Interno;
            this.HIAR_FechaProgramado = HIAR_FechaProgramado;
            this.HIAR_Ejecutor = HIAR_Ejecutor;
            this.HIAR_FechaEjecutado = HIAR_FechaEjecutado;
            this._HIAR_SiguienteFecha = HIAR_SiguienteFecha;
            this.HIAR_Observacion = HIAR_Observacion;
            this.HIAR_Estado = HIAR_Estado;
            this.ACRU_Interno = ACRU_Interno;
            this.LOCA_Interno = LOCA_Interno;
            this.EQUI_Interno = EQUI_Interno;
            this.PERI_Interno = PERI_Interno;
        }
    }
}
