using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class ActividadR
    {
        #region Propiedades
        private int? _ACRU_Interno;
        public int? ACRU_Interno
        {
            get { return _ACRU_Interno; }
            set { _ACRU_Interno = value; }
        }

        private string _ACRU_Descripcion;
        public string ACRU_Descripcion
        {
            get { return _ACRU_Descripcion; }
            set { _ACRU_Descripcion = value; }
        }

        private string _ACRU_Tipo;
        public string ACRU_Tipo
        {
            get { return _ACRU_Tipo; }
            set { _ACRU_Tipo = value; }
        }

        private Boolean _ACRU_ConCorte;
        public Boolean ACRU_ConCorte
        {
            get { return _ACRU_ConCorte; }
            set { _ACRU_ConCorte = value; }
        }        

        private Boolean _ACRU_ConMedicion;
        public Boolean ACRU_ConMedicion
        {
            get { return _ACRU_ConMedicion; }
            set { _ACRU_ConMedicion = value; }
        }

        private string _ACRU_UnidadMedicion;
        public string ACRU_UnidadMedicion
        {
            get { return _ACRU_UnidadMedicion; }
            set { _ACRU_UnidadMedicion = value; }
        }

        private int _ACRU_Frecuencia;
        public int ACRU_Frecuencia
        {
            get { return _ACRU_Frecuencia; }
            set { _ACRU_Frecuencia = value; }
        }

        private string _ACRU_UnidadFrecuencia;
        public string ACRU_UnidadFrecuencia
        {
            get { return _ACRU_UnidadFrecuencia; }
            set { _ACRU_UnidadFrecuencia = value; }
        }

        private int _PART_Interno;
        public int PART_Interno
        {
            get { return _PART_Interno; }
            set { _PART_Interno = value; }
        }

        private int _NOMB_Interno;
        public int NOMB_Interno
        {
            get { return _NOMB_Interno; }
            set { _NOMB_Interno = value; }
        }

        private string _NOMB_Descripcion;//nombre de la actividad
        public string NOMB_Descripcion
        {
            get { return _NOMB_Descripcion; }
            set { _NOMB_Descripcion = value; }
        }
        #endregion
        
        #region Propiedades Extendidas....................
        private int? _HIAR_Interno;
        public int? HIAR_Interno
        {
            get { return _HIAR_Interno; }
            set { _HIAR_Interno = value; }
        }

        private DateTime? _HIAR_SiguienteFecha;
        public DateTime? HIAR_SiguienteFecha
        {
            get { return _HIAR_SiguienteFecha; }
            set { _HIAR_SiguienteFecha = value; }
        }

        private DateTime? _HIAR_FechaProgramado;
        public DateTime? HIAR_FechaProgramado
        {
            get { return _HIAR_FechaProgramado; }
            set { _HIAR_FechaProgramado = value; }
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

        private string _HIAR_Estado;
        public string HIAR_Estado
        {
            get { return _HIAR_Estado; }
            set { _HIAR_Estado = value; }
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

        private int? _EQUI_Interno;
        public int? EQUI_Interno
        {
            get { return _EQUI_Interno; }
            set { _EQUI_Interno = value; }
        }

        private string _EQUI_Nombre;
        public string EQUI_Nombre
        {
            get { return _EQUI_Nombre; }
            set { _EQUI_Nombre = value; }
        }

        private string _EQUI_Descripcion;
        public string EQUI_Descripcion
        {
            get { return _EQUI_Descripcion; }
            set { _EQUI_Descripcion = value; }
        }

        private string _HIAR_Ejecutor;
        public string HIAR_Ejecutor
        {
            get { return _HIAR_Ejecutor; }
            set { _HIAR_Ejecutor = value; }
        }

        private string _HIAR_Observacion;
        public string HIAR_Observacion
        {
            get { return _HIAR_Observacion; }
            set { _HIAR_Observacion = value; }
        }

        private int? _PERI_Interno;
        public int? PERI_Interno
        {
            get { return _PERI_Interno; }
            set { _PERI_Interno = value; }
        }

        private int _HIAR_Retrazo;//dias de retrazo
        public int HIAR_Retrazo
        {
            get { return _HIAR_Retrazo; }
            set { _HIAR_Retrazo = value; }
        }

        private string _EQUI_LocalizacionExtendida;//lugar donde esta el equipo, que se le va ha hacer elmantenimiento rutinario
        public string EQUI_LocalizacionExtendida
        {
            get { return _EQUI_LocalizacionExtendida; }
            set { _EQUI_LocalizacionExtendida = value; }
        }

        #endregion
        
        #region Constructores
        public ActividadR() { }
        public ActividadR(int ACRU_Interno)
        {
            this.ACRU_Interno = ACRU_Interno;
        }
        public ActividadR(int? ACRU_Interno, string ACRU_Descripcion)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Descripcion = ACRU_Descripcion;
        }
        public ActividadR(int? ACRU_Interno, string ACRU_Descripcion, string ACRU_Tipo,
            Boolean ACRU_ConCorte, Boolean ACRU_ConMedicion, string ACRU_UnidadMedicion, int ACRU_Frecuencia,
            string ACRU_UnidadFrecuencia,int PART_Interno, int NOMB_Interno)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Descripcion = ACRU_Descripcion;
            this.ACRU_Tipo = ACRU_Tipo;
            this.ACRU_ConCorte = ACRU_ConCorte;
            this.ACRU_ConMedicion = ACRU_ConMedicion;
            this.ACRU_UnidadMedicion = ACRU_UnidadMedicion;
            this.ACRU_Frecuencia = ACRU_Frecuencia;
            this.ACRU_UnidadFrecuencia = ACRU_UnidadFrecuencia;
            this.PART_Interno = PART_Interno;
            this.NOMB_Interno = NOMB_Interno;
        }

        public ActividadR(int ACRU_Interno, string ACRU_Descripcion, Boolean ACRU_ConCorte, Boolean ACRU_ConMediciion)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Descripcion = ACRU_Descripcion;
            this.ACRU_ConCorte = ACRU_ConCorte;
            this.ACRU_ConMedicion = ACRU_ConMedicion;
        }

        public ActividadR(int? ACRU_Interno, string ACRU_Descripcion, string ACRU_Tipo,
            Boolean ACRU_ConCorte, Boolean ACRU_ConMedicion, string ACRU_UnidadMedicion, int ACRU_Frecuencia,
            string ACRU_UnidadFrecuencia, int PART_Interno, string NOMB_Descripcion)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Descripcion = ACRU_Descripcion;
            this.ACRU_Tipo = ACRU_Tipo;
            this.ACRU_ConCorte = ACRU_ConCorte;
            this.ACRU_ConMedicion = ACRU_ConMedicion;
            this.ACRU_UnidadMedicion = ACRU_UnidadMedicion;
            this.ACRU_Frecuencia = ACRU_Frecuencia;
            this.ACRU_UnidadFrecuencia = ACRU_UnidadFrecuencia;
            this.PART_Interno = PART_Interno;
            this.NOMB_Descripcion = NOMB_Descripcion;
        }

        public ActividadR(int ACRU_Interno, string ACRU_Descripcion, string ACRU_Tipo,
            Boolean ACRU_ConCorte, Boolean ACRU_ConMedicion, string ACRU_UnidadMedicion, int ACRU_Frecuencia,
            string ACRU_UnidadFrecuencia, int PART_Interno,int NOMB_Interno, string NOMB_Descripcion)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Descripcion = ACRU_Descripcion;
            this.ACRU_Tipo = ACRU_Tipo;
            this.ACRU_ConCorte = ACRU_ConCorte;
            this.ACRU_ConMedicion = ACRU_ConMedicion;
            this.ACRU_UnidadMedicion = ACRU_UnidadMedicion;
            this.ACRU_Frecuencia = ACRU_Frecuencia;
            this.ACRU_UnidadFrecuencia = ACRU_UnidadFrecuencia;
            this.PART_Interno = PART_Interno;
            this.NOMB_Interno = NOMB_Interno;
            this.NOMB_Descripcion = NOMB_Descripcion;
        }

        public ActividadR(int ACRU_Interno, int ACRU_Frecuencia, string ACRU_UnidadFrecuencia)
        {
            this.ACRU_Interno = ACRU_Interno;
            this.ACRU_Frecuencia = ACRU_Frecuencia;
            this.ACRU_UnidadFrecuencia = ACRU_UnidadFrecuencia;
        }
        #endregion
    }
}
