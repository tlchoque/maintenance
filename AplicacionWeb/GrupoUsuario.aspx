<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="GrupoUsuario.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.GrupoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Page.ResolveUrl("~/Styles/MiAcordeon.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/GrupoUsuario.js") %>" type="text/javascript" ></script>
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
                    <a class="toolbar" href="#" id="nuevo">
                    <span class=" icon-new-16"></span>Nuevo 
                    </a>
                    </li>
                	<li class="button">
                    <a class="toolbar" href="#" id="guardar">
                    <span class=" icon-guardar-16"> </span>
                    Guardar
                    </a>
                    </li>
                    <li class="button">
                    <a class="toolbar" href="#" id="guardar_cerrar">
                    <span class=" icon-guardar-cerrar-16"> </span>
                    Guardar y cerrar
                    </a>
                    </li>
                    <li class="button">
                    <a class="toolbar" href="#" id="guardar_nuevo">
                    <span class=" icon-guardar-nuevo-16"> </span>
                    Guardar y nuevo
                    </a>
                    </li>
                    <li class="button">
                    <a class="toolbar" href="GestionGrupos.aspx" id="cerrar_pagina">
                    <span class=" icon-cerrar-16"> </span>
                    Cerrar
                    </a>
                    </li>
            </ul>
        </div>
        <!--fin toolbar-->
        <div class="pagetitle icon-32-grupos-add">
            <h2>Nuevo Grupo</h2>                
        </div>
        <div class="clr"></div>
    </div>
    <!--fin class m-->
</div>
<div class="msj-box"></div>
<div id="element-box">
    <div class="capa_float_left">
    <div class="m margintop10">
        <form id="frmUsuario" runat="server">
        <asp:HiddenField ClientIDMode="Static" ID="IDGrupo" runat="server"/>
        <asp:HiddenField ClientIDMode="Static" ID="opcURL" runat="server"/>
        <asp:Label ID="LbMsg" runat="server"  ></asp:Label>
        <div class="item margintop10">
            <label>Nombre Grupo <span class="star">*</span></label>
            <input type="text" id="NombreGrupo" class="mayuscula requerido" style="width:360px;"/>
        </div>      
        <div class="item">
            <label style="margin-top:30px;">Descripción <span class="star">*</span></label>
            <textarea id="Descripcion" class="requerido" style="width:360px;" rows="4" cols="1"></textarea>
        </div>
        <div class="item">
            <label>Grupo Activo</label>
            <input type="radio" name="Bloquear" value="false" style="width:30px;" checked="checked"/>No
            <input type="radio" name="Bloquear" value="true" style="width:30px;"/>Sí
        </div>
        <div class="item">
            <label>Fecha Registro</label>               
            <input type="text" readonly="readonly" class="readonly" id="FechaRegistro"/>
        </div>
        <div class="item">
            <label>Usuario Creador</label>               
            <input type="text" readonly="readonly" class="readonly" id="UsuarioCreador"/>
        </div>
        </form>
    </div>
    </div>
    <div class="capa_float_right">
        <h3 class="tituloH3">Asignar tareas para el Grupo</h3>
        <div class='alinear-right margintop10'>
                <input type='button' value='Seleccionar todas las tareas' class='btnTransparente' id="allTareas"/>
                <input type='button' value='Limpiar selección' class='btnTransparente' id="noneTareas"/>
        </div>
        <dl class="acordeon" id="Tareas">
            
		    <!--<dt>Modulo 1</dt>
		    <dd>
                <input type='button' value='Seleccionar todo'/>
                <input type='button' value='Limpiar selección'/>
            Tareas - Modulo 1
            </dd>
            <dt>Modulo 2</dt>
		    <dd>Tareas - Modulo 2</dd>-->
            
	    </dl>	
    </div>
    <div class="clr"></div>
</div>
</div><!--fin contenido pagina-->
<div  id="msj_box_server" ClientIDMode="Static" runat="server">
    <h1>Este contenido no esta disponible</h1>
    <p>La página que has solicitado no puede mostrarse. Puede que no esté disponible temporalmente, 
    el enlace puede haber expirado o puede que no tengas permiso para visualizarla.</p>
    <p><a href="Panel.aspx">Volver a la página de Inicio</a></p>
</div>

</asp:Content>
