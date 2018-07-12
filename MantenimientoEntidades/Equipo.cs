using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class Equipo
    {
        //EQUI_Interno int,
        //EQUI_Nombre varchar(80),
        //EQUI_Marca varchar(30),
        //EQUI_Modelo varchar(30),
        //EQUI_Serie varchar(20),
        //EQUI_Codigo varchar(10),
        //EQUI_AnioFabricacion int,
        //EQUI_AnioServicio int,
        //EQUI_Estado char(1),
        //EQUI_Descripcion varchar(200),
        //TIPO_Interno int

        #region Propiedades
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

        private string _EQUI_Marca;
        public string EQUI_Marca
        {
            get { return _EQUI_Marca; }
            set { _EQUI_Marca = value; }
        }

        private string _EQUI_Modelo;
        public string EQUI_Modelo
        {
            get { return _EQUI_Modelo; }
            set { _EQUI_Modelo = value; }
        }

        private string _EQUI_Serie;
        public string EQUI_Serie
        {
            get { return _EQUI_Serie; }
            set { _EQUI_Serie = value; }
        }

        private string _EQUI_Codigo;
        public string EQUI_Codigo
        {
            get { return _EQUI_Codigo; }
            set { _EQUI_Codigo = value; }
        }

        private int? _EQUI_AnioFabricacion;
        public int? EQUI_AnioFabricacion
        {
            get { return _EQUI_AnioFabricacion; }
            set { _EQUI_AnioFabricacion = value; }
        }

        private int? _EQUI_AnioServicio;
        public int? EQUI_AnioServicio
        {
            get { return _EQUI_AnioServicio; }
            set { _EQUI_AnioServicio = value; }
        }

        private string _EQUI_Estado;
        public string EQUI_Estado
        {
            get { return _EQUI_Estado; }
            set { _EQUI_Estado = value; }
        }

        private string _EQUI_Descripcion;
        public string EQUI_Descripcion
        {
            get { return _EQUI_Descripcion; }
            set { _EQUI_Descripcion = value; }
        }

        private int _TIPO_Interno;
        public int TIPO_Interno
        {
            get { return _TIPO_Interno; }
            set { _TIPO_Interno = value; }
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

        private string _LOCA_ID;
        public string LOCA_ID
        {
            get { return _LOCA_ID; }
            set { _LOCA_ID = value; }
        }

        #endregion

        #region CONSTRUCTORES
        public Equipo() { }
        public Equipo(int EQUI_Interno)
        {
            this.EQUI_Interno = EQUI_Interno;
        }
        public Equipo(int? EQUI_Interno,string EQUI_Nombre,string EQUI_Marca,string EQUI_Modelo,string EQUI_Serie,
            string EQUI_Codigo, int? EQUI_AnioFabricacion,int? EQUI_AnioServicio,string EQUI_Estado,
            string EQUI_Descripcion, int TIPO_Interno)
        {
            this.EQUI_Interno = EQUI_Interno;
            this.EQUI_Nombre = EQUI_Nombre;
            this.EQUI_Marca = EQUI_Marca;
            this.EQUI_Modelo = EQUI_Modelo;
            this.EQUI_Serie = EQUI_Serie;
            this.EQUI_Codigo = EQUI_Codigo;
            this.EQUI_AnioFabricacion = EQUI_AnioFabricacion;
            this.EQUI_AnioServicio = EQUI_AnioServicio;
            this.EQUI_Estado = EQUI_Estado;
            this.EQUI_Descripcion = EQUI_Descripcion;
            this.TIPO_Interno = TIPO_Interno;
        }
        public Equipo(int? EQUI_Interno, string EQUI_Nombre, string EQUI_Marca, string EQUI_Modelo, string EQUI_Serie,
            string EQUI_Codigo, int? EQUI_AnioFabricacion, int? EQUI_AnioServicio, string EQUI_Estado,
            string EQUI_Descripcion, int TIPO_Interno, int? LOCA_Interno)
        {
            this.EQUI_Interno = EQUI_Interno;
            this.EQUI_Nombre = EQUI_Nombre;
            this.EQUI_Marca = EQUI_Marca;
            this.EQUI_Modelo = EQUI_Modelo;
            this.EQUI_Serie = EQUI_Serie;
            this.EQUI_Codigo = EQUI_Codigo;
            this.EQUI_AnioFabricacion = EQUI_AnioFabricacion;
            this.EQUI_AnioServicio = EQUI_AnioServicio;
            this.EQUI_Estado = EQUI_Estado;
            this.EQUI_Descripcion = EQUI_Descripcion;
            this.TIPO_Interno = TIPO_Interno;
            this.LOCA_Interno = LOCA_Interno;
        }
        public Equipo(int? EQUI_Interno, string EQUI_Nombre, string EQUI_Marca, string EQUI_Modelo, string EQUI_Serie,
            string EQUI_Codigo, int? EQUI_AnioFabricacion, int? EQUI_AnioServicio, string EQUI_Estado,
            string EQUI_Descripcion, int TIPO_Interno, int? LOCA_Interno, string LOCA_Nombre, string LOCA_NombreExtendido)
        {
            this.EQUI_Interno = EQUI_Interno;
            this.EQUI_Nombre = EQUI_Nombre;
            this.EQUI_Marca = EQUI_Marca;
            this.EQUI_Modelo = EQUI_Modelo;
            this.EQUI_Serie = EQUI_Serie;
            this.EQUI_Codigo = EQUI_Codigo;
            this.EQUI_AnioFabricacion = EQUI_AnioFabricacion;
            this.EQUI_AnioServicio = EQUI_AnioServicio;
            this.EQUI_Estado = EQUI_Estado;
            this.EQUI_Descripcion = EQUI_Descripcion;
            this.TIPO_Interno = TIPO_Interno;
            this.LOCA_Interno = LOCA_Interno;
            this.LOCA_Nombre = LOCA_Nombre;
            this.LOCA_NombreExtendido = LOCA_NombreExtendido;
        }
        #endregion
    }
}
