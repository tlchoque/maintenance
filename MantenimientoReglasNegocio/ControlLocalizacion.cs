using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable

namespace Mantenimiento.ReglasNegocio
{
    public class ControlLocalizacion
    {
        public ControlLocalizacion() { }

        public List<LocalizacionS> ObtenerLocalizacionesPorOrigen(int LOCA_Interno)
        {
            LocalizacionSDAO LocalizacionDAO = new LocalizacionSDAO();
            return LocalizacionDAO.ObtenerLocalizacionesPorOrigen(LOCA_Interno);
        }

        public List<LocalizacionS> ObtenerLocalizaciones()
        {
            LocalizacionSDAO LocalizacionDAO = new LocalizacionSDAO();
            return LocalizacionDAO.ObtenerLocalizaciones();
        }

        public int InsertarLocalizacion(LocalizacionS Localizacion, int? AUDI_UsuarioCrea, int? AUDI_UsuarioEdita)
        {
            LocalizacionSDAO LocalizacionDAO = new LocalizacionSDAO();
            return LocalizacionDAO.InsertarLocalizacion(Localizacion, AUDI_UsuarioCrea, AUDI_UsuarioEdita);
        }

        public int EliminarNodosPorPadre(LocalizacionS Localizacion, int AUDI_UsuarioEdita)
        {
            LocalizacionSDAO LocalizacionDAO = new LocalizacionSDAO();
            return LocalizacionDAO.EliminarLocalizaciones(Localizacion, AUDI_UsuarioEdita);
        }

        public List<LocalizacionS> ObtenerLocalizacionesLike(string LOCA_Nombre)
        {
            LocalizacionSDAO LocalizacionDAO = new LocalizacionSDAO();
            return LocalizacionDAO.ObtenerLocalizacionesLike(LOCA_Nombre);
        }
    }
}
