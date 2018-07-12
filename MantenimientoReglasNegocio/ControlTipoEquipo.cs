using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable
namespace Mantenimiento.ReglasNegocio
{
    public class ControlTipoEquipo
    {
        public ControlTipoEquipo()
        {
        }
        public List<TipoEquipo> ObtenerTiposEquipo(){
            TipoEquipoDAO DataTipoEquipo = new TipoEquipoDAO();
            return DataTipoEquipo.ObtenerTiposEquipo();
        }
    }

}
