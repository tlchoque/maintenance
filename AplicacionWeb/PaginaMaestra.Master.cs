using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mantenimiento.AplicacionWeb
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_SESSION"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    //REDIRECCIONAMOS AL LOGIN
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            //Page.RegistrarRedireccionarCuandoFinaliceLaSesion();
        }
    }
}