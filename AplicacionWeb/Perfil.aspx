<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Perfil.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/date.format.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
<div id="ContenidoPagina" ClientIDMode="Static" runat="server">
<div id="toolbar-box">
    <div class="submenu-box">
        <div class="toolbar-list">
            <ul>
                <li class="button">
                <a class="toolbar" href="#" id="guardar">
                <span class=" icon-guardar-cerrar-16"> </span>
                Guardar
                </a>
                </li> 
            </ul>
        </div>
        <!--fin toolbar-->
        <div class="pagetitle icon-32-perfil">
            <h2>Mi Perfil</h2>                
        </div>
        <div class="clr"></div>
    </div>
    <!--fin class m-->
</div>
<div class="msj-box"></div>
<div id="element-box">
    <div class="m margintop10">
        <form id="frmUsuario" runat="server">
        <asp:HiddenField ClientIDMode="Static" ID="IDUsuario" runat="server"/>
        <asp:HiddenField ClientIDMode="Static" ID="opcURL" runat="server"/>
        <asp:Label ID="LbMsg" runat="server"  ></asp:Label>
        <div class="item margintop10">
            <label>Nombre <span class="star">*</span></label>
            <input type="text" id="Nombre" class="mayuscula requerido" style="width:360px;"/>
        </div>      
        <div class="item">
            <label>Apellido <span class="star">*</span></label>
            <input type="text" id="Apellido" class="mayuscula requerido" style="width:360px;"/>
        </div>
        
        <div class="item">
            <label>Nombre de Acceso</label>
            <input type="text" readonly="readonly" class="readonly" id="Usuario"/>
        </div>
        <div class="item">
            <label>Contraseña Actual <span class="star">*</span></label>
            <input type="password" id="contraseniaActual" class="requerido" style="width:360px;"/>
        </div>
        <div class="item">
            <label>Contraseña Nueva</label>
            <input type="password" id="contrasenia1" style="width:360px;"/>
        </div>
        <div class="item">
            <label style="margin-top:-5px;">Vuelva escribir la Contraseña Nueva</label>
            <input type="password" id="contrasenia2" style="width:360px;"/>
        </div>
        <div class="item">
            <label>Dirección</label>
            <input type="text" id="Direccion" style="width:360px;"/>
        </div>
        <div class="item">
            <label>E-mail</label>
            <input type="text" id="Email" style="width:360px;"/>
        </div>
        </form>
    </div>
</div>
</div>
<div  id="msj_box_server" ClientIDMode="Static" runat="server">
    <h1>Este contenido no esta disponible</h1>
    <p>La página que has solicitado no puede mostrarse. Puede que no esté disponible temporalmente, 
    el enlace puede haber expirado o puede que no tengas permiso para visualizarla.</p>
    <p><a href="Panel.aspx">Volver a la página de Inicio</a></p>
</div>
</asp:Content>
