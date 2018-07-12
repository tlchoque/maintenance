using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;


namespace Mantenimiento.ReglasNegocio
{
    public class ControlActividadR
    {
        public ControlActividadR() { }

        public List<ActividadR> ObtenerActividadesPorParte(int PART_Interno)
        {
            ActividadRDAO DataActividad = new ActividadRDAO();
            return DataActividad.ObtenerActividadesParte(PART_Interno);
        }

        public int InsertarActividadR(ActividadR Actividad, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            ActividadRDAO DataActividad = new ActividadRDAO();
            return DataActividad.InsertarActividad(Actividad, AUDI_UsuarioCrea, AUDI_UsuarioEdita);
        }

        public int EdliminarActividadR(ActividadR Actividad, int AUDI_UsuarioEdita)
        {
            ActividadRDAO DataActividad = new ActividadRDAO();
            return DataActividad.EliminarActividad(Actividad, AUDI_UsuarioEdita);
        }

        public ActividadR ObtenerActividadPorId(ActividadR Actividad)
        {
            ActividadRDAO DataActividad = new ActividadRDAO();
            return DataActividad.ObtenerActividadPorId(Actividad);
        }
    }
}
