using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable

namespace Mantenimiento.ReglasNegocio
{
    public class ControlNombre
    {
        public ControlNombre()
        {
        }
        public List<NombreActividad> ObtenerNombreActividades(){
            NombreActividadDAO DataNombre = new NombreActividadDAO();
            return DataNombre.ObtenerNombreActividades();
        }
    }

}
