using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;
using System.Data;//para DbType

namespace Mantenimiento.DAO
{
    public class NombreActividadDAO:DataAccess
    {
        public List<NombreActividad> ObtenerNombreActividades()
        {
            List<NombreActividad> NombreActividades = new List<NombreActividad>();
            string StoredProcedure = "PA_ObtenerNombreActividad";
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                while (dr.Read())
                {
                    NombreActividad Nombre = null;
                    try
                    {
                        Nombre = new NombreActividad();
                        Nombre.NOMB_Interno = (int)dr["NOMB_Interno"];
                        Nombre.NOMB_Descripcion = (string)(dr["NOMB_Descripcion"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de Nombre a Objecto", ex);
                    }
                    NombreActividades.Add(Nombre);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Nombres", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return NombreActividades;
        }
    }
}
