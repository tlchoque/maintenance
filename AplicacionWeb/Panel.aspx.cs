using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mantenimiento.AplicacionWeb
{
    public partial class Panel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["ID_SESSION"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            //{
            //    if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            //    {
            //        //REDIRECCIONAMOS AL LOGIN
            //        Response.Redirect("~/Default.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Default.aspx");
            //}
        }
    }
}