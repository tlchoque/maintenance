using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;//para DbParameter
using Mantenimiento.Entidades;//para la clase Equipo; agregar la referencia
using System.Data;//para DbType

namespace Mantenimiento.DAO
{
    public class TipoEquipoDAO:DataAccess
    {
        public List<TipoEquipo> ObtenerTiposEquipo()
        {
            List<TipoEquipo> TiposEquipo = new List<TipoEquipo>();
            string StoredProcedure = "PA_ObtenerTiposEquipo";
            try
            {
                //Connection.ConnectionString = GlobalConectionString;
                if (Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();

                DbDataReader dr = EjecuteReader(StoredProcedure, null);
                while (dr.Read())
                {
                    TipoEquipo tipoEquipo = null;
                    try
                    {
                        tipoEquipo = new TipoEquipo();
                        tipoEquipo.TIPO_Interno = (int)dr["TIPO_Interno"];
                        tipoEquipo.TIPO_Nombre = (string)(dr["TIPO_Nombre"]);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error convirtiendo datos de TipoEquipo a Objecto", ex);
                    }
                    TiposEquipo.Add(tipoEquipo);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de Tipos de Equipo", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

            return TiposEquipo;
        }
    }
}
