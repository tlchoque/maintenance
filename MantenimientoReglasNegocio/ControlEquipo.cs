using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable

namespace Mantenimiento.ReglasNegocio
{
    public class ControlEquipo
    {
        public ControlEquipo()
        {
        }
#region PARA EL PAGINADO

        public List<Equipo> ObtenerCualquierPaginaEquipos(int TamanioPagina, int NumeroPagina)
        {
            EquipoDAO DataEquipo = new EquipoDAO();
            
            
            List<Equipo> Equipos = DataEquipo.ObtenerCualquierPaginaEquipos(TamanioPagina,NumeroPagina);
            List<Equipo> EquiposLocalizacion = new List<Equipo>();
            foreach (Equipo objEquipo in Equipos)
            {
                Equipo equipo = new Equipo();
                equipo = objEquipo;
                //agregamos la localizacion al equipo

                LocalizacionEquipo localizacion = DataEquipo.ObtenerUltimaLocalizacionEquipo(equipo);
                if (localizacion != null)
                {
                    equipo.LOCA_Interno = localizacion.LOCA_Interno;
                    equipo.LOCA_Nombre = localizacion.LOCA_Nombre;
                    equipo.LOCA_NombreExtendido = localizacion.LOCA_NombreExtendido;
                }
                else
                {
                    equipo.LOCA_Interno = null;
                    equipo.LOCA_Nombre = null;
                    equipo.LOCA_NombreExtendido = null;
                }
                EquiposLocalizacion.Add(equipo);

            }
            return EquiposLocalizacion;
        }

        public int ObtenerTotalRegistrosAproximadoEquipos()//esto es para tablas grandes
        {
            EquipoDAO DataEquipo = new EquipoDAO();
            return DataEquipo.ObtenerTotalRegistrosAproximadoEquipos();
        }
        public int ObtenerTotalRegistros()
        {
            EquipoDAO DataEquipo = new EquipoDAO();
            return DataEquipo.ObtenerTotalRegistros();
        }
#endregion
        #region para el filtrado
        public List<Equipo> ObtenerCualquierPaginaEquiposFiltradoPorString(int TamanioPagina, int NumeroPagina, string str)
        {
            EquipoDAO DataEquipo = new EquipoDAO();


            List<Equipo> Equipos = DataEquipo.ObtenerCualquierPaginaEquiposFiltradoPorString(TamanioPagina, NumeroPagina,str);
            List<Equipo> EquiposLocalizacion = new List<Equipo>();
            foreach (Equipo objEquipo in Equipos)
            {
                Equipo equipo = new Equipo();
                equipo = objEquipo;
                //agregamos la localizacion al equipo

                LocalizacionEquipo localizacion = DataEquipo.ObtenerUltimaLocalizacionEquipo(equipo);
                if (localizacion != null)
                {
                    equipo.LOCA_Interno = localizacion.LOCA_Interno;
                    equipo.LOCA_Nombre = localizacion.LOCA_Nombre;
                    equipo.LOCA_NombreExtendido = localizacion.LOCA_NombreExtendido;
                }
                else
                {
                    equipo.LOCA_Interno = null;
                    equipo.LOCA_Nombre = null;
                    equipo.LOCA_NombreExtendido = null;
                }
                EquiposLocalizacion.Add(equipo);

            }
            return EquiposLocalizacion;
        }
        #endregion
        public int InsertarEquipo(Equipo equipo, int? AUDI_Usuario)
        {
            EquipoDAO DataEquipo = new EquipoDAO();
            return DataEquipo.InsertarEquipo(equipo, AUDI_Usuario);
        }
        public Equipo ObtenerEquipoPorID(Equipo equipo)
        {
            EquipoDAO DataEquipo = new EquipoDAO();
            Equipo ObjEquipo = DataEquipo.ObtenerEquipoPorID(equipo);
            LocalizacionEquipo localizacion = DataEquipo.ObtenerUltimaLocalizacionEquipo(equipo);
            if (localizacion != null)
            {
                ObjEquipo.LOCA_Interno = localizacion.LOCA_Interno;
                ObjEquipo.LOCA_Nombre = localizacion.LOCA_Nombre;
                ObjEquipo.LOCA_NombreExtendido = localizacion.LOCA_NombreExtendido;
                ObjEquipo.HILO_Fecha = localizacion.HILO_Fecha;
            }
            else
            {
                ObjEquipo.LOCA_Interno = null;
                ObjEquipo.LOCA_Nombre = null;
                ObjEquipo.LOCA_NombreExtendido = null;
                ObjEquipo.HILO_Fecha = null;
            }
            return ObjEquipo;
        }
        public int EliminarEquipo(Equipo equipo, int AUDI_UsuarioEdita){
            EquipoDAO DataEquipo = new EquipoDAO();
            return DataEquipo.EliminarEquipo(equipo,AUDI_UsuarioEdita);
        }
    }
}
