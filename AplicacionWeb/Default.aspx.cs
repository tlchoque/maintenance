using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mantenimiento.ReglasNegocio;
using Mantenimiento.Entidades;
using System.Web.Services;//para [WebMethod]

namespace Mantenimiento.AplicacionWeb
{
    public partial class Default : System.Web.UI.Page
    {
        //static string IPCliente = "";
        //static HttpResponse _Response;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Path.EndsWith("Default.aspx", true/*case-insensitive*/, null))
            //{
            //    Response.StatusCode = 301;
            //    Response.StatusDescription = "Moved Permanently";
            //    //Response.Headers.Add("Location", "/");
            //    HttpContext.Current.ApplicationInstance.CompleteRequest(); // end the request
            //}
            //el codigo anterior no funciona en explorer
            if (!IsPostBack)
            {
                //IPCliente=Request.ServerVariables["REMOTE_ADDR"];
                //label.Text = IngresarSistema("admin", "123456");
                //HttpContext.Current.Response.Redirect("~/Panel.aspx");
                
            }
        }
        [WebMethod]
        public  static string IngresarSistema(string USUA_Usuario, string USUA_Contrasenia)
        {
            Usuario usuario = new Usuario();
            //usuario.USUA_Usuario = USUA_Usuario.Replace(";", "").Replace("--", "");
            //usuario.USUA_Contrasenia = USUA_Contrasenia.Replace(";", "").Replace("--", "");
            usuario.USUA_Usuario = USUA_Usuario;
            usuario.USUA_Contrasenia = USUA_Contrasenia;
            string IPCliente= HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            int IDUser = LoginService.Autenticar(usuario, IPCliente);
            if (IDUser>0)
            {
                HttpContext.Current.Session["ID_SESSION"] = IDUser.ToString();

                // createa a new GUID and save into the session
                string guid = Guid.NewGuid().ToString();
                HttpContext.Current.Session["AuthToken"] = guid;
                // now create a new cookie with this guid value
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                //HttpContext.Current.Response.Redirect("~/Panel.aspx");
                //HttpContext.Current.Response.Write("<script>location.href='Panel.aspx';</script>");
               // HttpContext.Current.Response.Redirect("~/Panel.aspx");
                return "ok";
            }
            else
            {
                return "<b>Usuario o Contraseña incorrectos</b>. El usuario y contraseña es susceptible a mayúsculas y minúsculas. ";
            }
        }
    }
}