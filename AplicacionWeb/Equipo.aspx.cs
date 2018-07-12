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
    public partial class Equipos : System.Web.UI.Page
    {
        static string opc = null;
        static int UsuarioLogeado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogeado = Convert.ToInt32(HttpContext.Current.Session["ID_SESSION"]);
            this.msj_box_server.Visible = false;
            this.ContenidoPagina.Visible = true;
            string IDEquipo = Request.QueryString["id"];
            string opcion = Request.QueryString["opc"];
            if (IDEquipo != null)
            {
                //reemplazamos si el parametro id existe y esta bien escrito
                IDEquipo = Request.QueryString["id"].Replace("_", "+");
                IDEquipo = SeguridadWeb.Desencriptar(IDEquipo);
            }

            if (opcion == "editar" && IDEquipo != "" && IDEquipo != null)
            {
                this.opcURL.Value = "editar";
                opc = "editar";
                this.IDEquipo.Value = IDEquipo;
                LlenarSelectTipoEquipo();
            }
            else if (opcion == "nuevo" && IDEquipo == null)
            {
                this.opcURL.Value = "nuevo";
                opc = "nuevo";
                this.IDEquipo.Value = "";
                LlenarSelectTipoEquipo();
            }
            else
            {
                this.msj_box_server.Visible = true;
                this.ContenidoPagina.Visible = false;
            }
            if (!IsPostBack)
            {
                //LbMsg.Text = GuardarEquipo("", "AA", "ll", "lll", "lll", "kkk", "2012", "2012", "S", "kkkkkkkk", "2","4").ToString();
                //Equipo equipo = ObtenerEquipo("133");
                //LbMsg.Text = equipo.HILO_Fecha.ToString(); 
            }            
                
            
        }
        protected void LlenarSelectTipoEquipo()
        {
            ControlTipoEquipo CTiposEquipo = new ControlTipoEquipo();
            List<TipoEquipo> ListaTiposEquipo = new List<TipoEquipo>();
            ListaTiposEquipo = CTiposEquipo.ObtenerTiposEquipo();
            //this.Tipo.Items.Insert(0, "-seleccione-");
            //this.Tipo.Items[0].Value = "";
            int i = 1;
            foreach (TipoEquipo tip in ListaTiposEquipo)
            {
                this.Tipo.Items.Insert(i, tip.TIPO_Nombre);
                this.Tipo.Items[i].Value = tip.TIPO_Interno.ToString();
                i++;
            }
            
            /*
            //esto funciona tambien
            this.Tipo.DataSource = ListaTiposEquipo;
            this.Tipo.DataValueField = "TIPO_Interno";
            this.Tipo.DataTextField = "TIPO_Nombre";
            this.Tipo.DataBind();*/
        }
        [WebMethod]
        public static int GuardarEquipo(string EQUI_Interno,string EQUI_Nombre,string EQUI_Marca,string EQUI_Modelo,
            string EQUI_Serie,string EQUI_Codigo,string EQUI_AnioFabricacion,string EQUI_AnioServicio,string EQUI_Estado,
                string EQUI_Descripcion, string TIPO_Interno, string LOCA_Interno, string HILO_Fecha)
        {
            Equipo equipo = new Equipo();
            if (EQUI_Interno == "") equipo.EQUI_Interno = null; 
            else equipo.EQUI_Interno = int.Parse(EQUI_Interno);

            equipo.EQUI_Nombre = EQUI_Nombre == "" ? null : EQUI_Nombre;
            equipo.EQUI_Marca = EQUI_Marca == "" ? null : EQUI_Marca;
            equipo.EQUI_Modelo = EQUI_Modelo == "" ? null : EQUI_Modelo;
            equipo.EQUI_Serie = EQUI_Serie == "" ? null : EQUI_Serie;
            equipo.EQUI_Codigo = EQUI_Codigo == "" ? null : EQUI_Codigo;

            if (EQUI_AnioFabricacion == "") equipo.EQUI_AnioFabricacion = null;
            else equipo.EQUI_AnioFabricacion = int.Parse(EQUI_AnioFabricacion);

            if (EQUI_AnioServicio == "") equipo.EQUI_AnioServicio = null;
            else equipo.EQUI_AnioServicio = int.Parse(EQUI_AnioServicio);

            equipo.EQUI_Estado = EQUI_Estado == "" ? null : EQUI_Estado;
            equipo.EQUI_Descripcion = EQUI_Descripcion == "" ? null : EQUI_Descripcion;
            equipo.TIPO_Interno = int.Parse(TIPO_Interno);

            if (LOCA_Interno == "") equipo.LOCA_Interno = null;
            else equipo.LOCA_Interno = int.Parse(LOCA_Interno);

            string example = "23/07/2013 18:36:03.513";
            DateTime d = DateTime.ParseExact(example, "dd/MM/yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            MessageBox.Show(d.ToString());

            if (HILO_Fecha == "") equipo.HILO_Fecha = null;//DateTime.ParseExact(DateInicio, "dd/MM/yyyy", null)
            else equipo.HILO_Fecha = DateTime.ParseExact(HILO_Fecha, "dd/MM/yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            //else equipo.HILO_Fecha = DateTime.Parse(HILO_Fecha);
            //MessageBox.Show("G" + equipo.EQUI_AnioServicio.ToString());
            ControlEquipo CEquipo = new ControlEquipo();
            if (opc == "nuevo" || opc == "editar")
            {
                return CEquipo.InsertarEquipo(equipo, UsuarioLogeado);
            }
            else
                return 0;
            
        }
        [WebMethod]
        public static Equipo ObtenerEquipo(string EQUI_Interno)
        {
            Equipo equipo = new Equipo();
            ControlEquipo CEquipo = new ControlEquipo();
            equipo.EQUI_Interno = int.Parse(EQUI_Interno);
            return CEquipo.ObtenerEquipoPorID(equipo);
        }
        [WebMethod]
        public static List<LocalizacionS> Localizaciones()
        {
            ControlLocalizacion CLoca = new ControlLocalizacion();
            return CLoca.ObtenerLocalizaciones();
        }
    }
}