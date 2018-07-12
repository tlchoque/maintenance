using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.Web.UI;
namespace Mantenimiento.AplicacionWeb.clases
{
    public class MessageBoxConfirm
    {
        private static Hashtable m_executingPages = new Hashtable();
        private MessageBoxConfirm()
        { 
            
        }
        
        public static void Show(string sMessage)
        {

            //Si esta es la primera vez que una página ha llamado a este metodo entonces
            if (!m_executingPages.Contains(HttpContext.Current.Handler))
            {
                // Attempt to cast HttpHandler as a Page.
                //Intentar lanzar HttpHandler como página.
                Page executingPage = HttpContext.Current.Handler as Page;
                if (executingPage != null)
                {
                    //Crear una cola, para una o más mensajes.
                    Queue messageQueue = new Queue();
                    // agregamos un mensaje a la cola
                    messageQueue.Enqueue(sMessage);

                    //Agregamos nuestra cola de mensaje a la tabla hash
                    //Usamos nuestra pagina de referencia (IHttpHandler) como clave-llave
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue);

                    // Wire up Unload event so that we can inject 
                    // some JavaScript for the alerts.
                    //Cablear descargar evento para que podamos inyectar
                    //algo de JavaScript para las alertas.
                    executingPage.Unload += new EventHandler(ExecutingPage_Unload);

                    // ****
                    // **** I think there is something wron with UNLOAD, but don't know what to use instead
                    //Creo que hay algo mal con la descarga, pero no sé lo que debo usar en su lugar.
                }
            }
            else
            {
                // If were here then the method has allready been 
                // called from the executing Page.
                // We have allready created a message queue and stored a
                // reference to it in our hastable.

                //Si estuviera aquí, entonces el método ya ha sido llamado desde la página de ejecución.
                //Ya hemos creado una cola de mensajes y almacena una referencia a él en nuestra tabla hash.
                Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
                // Añade tu mensaje a la cola
                queue.Enqueue(sMessage);
            }
            
        }


        // Our page has finished rendering so lets output the
        // JavaScript to produce the alert's

        //Nuestra página ha terminado de representar asi que vamos a dejar que  JavaScript reproduzca las alertas
        private static void ExecutingPage_Unload(object sender, EventArgs e)
        {
            // Get our message queue from the hashtable
            //Recibe nuestra cola de mensajes de la tabla hash
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
            if (queue != null)
            {
                StringBuilder sb = new StringBuilder();

                // ¿Cuántos mensajes se han registrado?
                int iMsgCount = queue.Count;
                // Use StringBuilder to build up our client slide JavaScript.
                //Utilizar StringBuilder para construir nuestra JavaScript del lado del cliente.
                sb.Append("<script language='javascript'>");
                // Loop round registered messages
                //bucle de mensajes registrados
                string sMsg;
                while (iMsgCount-- > 0)
                {
                    sMsg = (string)queue.Dequeue();
                    sMsg = sMsg.Replace("\n", "\\n");
                    sMsg = sMsg.Replace("\"", "'");


                    sb.Append(@"var answer = ");

                    sb.AppendLine(@"confirm( """ + sMsg + @""" );");


                    sb.AppendLine("if(answer){");
                    sb.AppendLine("var home_url = String(this.location).split(\"?\")");
                    sb.AppendLine("this.location = home_url[0].concat(\"?\",\"true\")}");
                    sb.AppendLine("else{");
                    sb.AppendLine("var home_url = String(this.location).split(\"?\") ");
                    sb.AppendLine("this.location = home_url[0].concat(\"?\",\"false\")}");

                }

                sb.Append(@"</script>");
                // Were done, so remove our page reference from the hashtable
                // Se realizaron, asi removemos la referencia de página de la tabla hash
                m_executingPages.Remove(HttpContext.Current.Handler);
                // Write the JavaScript to the end of the response stream.
                //Escribe el JavaScript para el final de la secuencia de respuesta.
                HttpContext.Current.Response.Write(sb.ToString());
            }
        }
    }
}