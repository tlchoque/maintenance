<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PaginaMaestra.Master"
    CodeBehind="Equipo.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.Equipos" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.numeric.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Equipo.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/date.format.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="ContentCuerpo" ContentPlaceHolderID="Cuerpo" runat="server">
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
                    <a class="toolbar" href="GestionEquipos.aspx" id="cerrar_pagina">
                    <span class=" icon-cerrar-16"> </span>
                    Cerrar
                    </a>
                    </li>
                </ul>
                	
                </div>
            <!--fin toolbar-->
            <div class="pagetitle">
                <h2>Nuevo Equipo</h2>
            </div>
            <div class="clr"></div>
        </div>
        <!--fin class m-->
    </div>
    <!--fin toolbar-box -->
    <div class="msj-box">
    </div>
    <div id="element-box">
        <div class="m margintop10">
            <form id="frmEquipo" runat="server">
            <asp:HiddenField ClientIDMode="Static" ID="IDEquipo" runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="opcURL" runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="HILO_Fecha" runat="server" />
            <asp:Label ID="LbMsg" runat="server"  ></asp:Label>
            
            <div class="item margintop10">
                <label>Equipo <span class="star">*</span></label>
                <input type="text" id="Nombre" class="mayuscula" style="width:360px;"/>
            </div>
            <div class="item">
                <label>Marca</label>
                <input type="text" id="Marca" class="mayuscula" />
            </div>
            <div class="item">
                <label>Modelo</label>
                <input type="text" id="Modelo" class="mayuscula" />
            </div>
            <div class="item">
                <label>Serie</label>
                <input type="text" id="Serie" class="mayuscula" />
            </div>
            <div class="item">
                <label>Código</label>
                <input type="text" id="Codigo" class="mayuscula" />
            </div>
            <div class="item">
                <label>Año de Fabricación</label>
                <input type="text" id="AnioFabricacion"  maxlength="4"/>
            </div>
            <div class="item">
                <label>Año de Servicio</label>
                <input type="text" id="AnioServicio"  maxlength="4"/>
            </div>
            <div class="item">
                <label>Tipo<span class="star">*</span></label>
                
                <select ID="Tipo"  ClientIDMode="Static" style="width:auto;"  runat="server" >
                    <option value="" class="selectinfo">-seleccione-</option>
                </select>
            </div>
            <div class="item">
                <label>Estado<span class="star">*</span></label>
                <select id="Estado">
                    <option class="selectinfo" value="">-seleccione-</option>
                    <option value="R">RESERVA</option>
                    <option value="S">SERVICIO</option>
                    <option value="F">FUERA DE SERVICIO</option>
                </select>
            </div>
            <div class="item">
                <label>Localizacion<span class="star">*</span></label>
                <select id="Localizacion" style="width:auto;">
                    <option class="selectinfo" value="">-seleccione-</option>
                    
                </select>
            </div>
            <div class="item">
                <label>Descripción</label>
                <textarea id="Descripcion"  readonly="readonly" class="mayuscula" style="width:360px;"></textarea>
            </div>
            </form>
        </div>
    </div>
    <!--fin element-box -->
</div>
    <div  id="msj_box_server" ClientIDMode="Static" runat="server">
    <h1>Este contenido no esta disponible</h1>
    <p>La página que has solicitado no puede mostrarse. Puede que no esté disponible temporalmente, 
    el enlace puede haber expirado o puede que no tengas permiso para visualizarla.</p>
    <p><a href="Panel.aspx">Volver a la página de Inicio</a></p>
</div>
</asp:Content>
