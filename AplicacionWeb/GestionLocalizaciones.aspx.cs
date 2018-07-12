using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mantenimiento.ReglasNegocio;
using Mantenimiento.Entidades;
using System.Web.Services;//para [WebMethod]
using Mantenimiento.AplicacionWeb.clases;

namespace Mantenimiento.AplicacionWeb
{
    public partial class GestionLocalizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static List<Nodo> ObtenerNodoPrincipal(string id)
        {
            List<Nodo> ArrayNodo = new List<Nodo>();
            Nodo NodoP = new Nodo();
            NodoP.data = "ELECTROSUR";
            NodoP.state = "false";
            NodoP.attr = new AtributoNodo { id = id, selected = false };
            NodoP.children = ObtenerNodos(NodoP);
            ArrayNodo.Add(NodoP);
            return ArrayNodo;
        }

        public static List<Nodo> ObtenerNodos(Nodo Nodo)
        {
            List<Nodo> ArrayNodo = new List<Nodo>();
            ControlLocalizacion ControlLocalizacion = new ControlLocalizacion();
            List<LocalizacionS> Localizaciones = ControlLocalizacion.ObtenerLocalizacionesPorOrigen(int.Parse(Nodo.attr.id));
            if (Localizaciones.Count() != 0)
            {
                foreach (LocalizacionS Localizacion in Localizaciones)
                {
                    Nodo _Nodo = new Nodo();
                    _Nodo.data = Localizacion.LOCA_Nombre;
                    _Nodo.state = "false";
                    _Nodo.attr = new AtributoNodo { id = Localizacion.LOCA_Interno.ToString(), selected = true };
                    _Nodo.children = ObtenerNodos(_Nodo);
                    ArrayNodo.Add(_Nodo);
                }
                return ArrayNodo;
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static int InsertarNodo(int? idnodo, string name, int idnodopadre, Boolean op)
        {
            LocalizacionS Localizacion = new LocalizacionS();
            ControlLocalizacion ControlLocalizacion = new ControlLocalizacion();
            Localizacion.LOCA_Nombre = name;
            if (op)
            {
                Localizacion.LOCA_Interno = null;
                Localizacion.LOCA_Origen = idnodopadre;
                return ControlLocalizacion.InsertarLocalizacion(Localizacion, 1, null);
            }
            else
            {
                Localizacion.LOCA_Interno = idnodo;
                Localizacion.LOCA_Origen = idnodopadre;
                return ControlLocalizacion.InsertarLocalizacion(Localizacion, null, 1);
            }
        }

        [WebMethod]
        public static int EliminarNodosPorPadre(int id)
        {
            ControlLocalizacion ControlLocalizacion = new ControlLocalizacion();
            LocalizacionS PartePadre = new LocalizacionS(id);
            return ControlLocalizacion.EliminarNodosPorPadre(PartePadre, 1);
        }

        [WebMethod]
        public List<LocalizacionS> ObtenerLocalizacionesLike(string Nombre)
        {
            ControlLocalizacion ControlLocalizacion = new ControlLocalizacion();
            return ControlLocalizacion.ObtenerLocalizacionesLike(Nombre);
        }
    }
}