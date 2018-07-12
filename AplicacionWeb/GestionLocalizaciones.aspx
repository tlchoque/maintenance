<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="GestionLocalizaciones.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.GestionLocalizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <%--<script src="<%= Page.ResolveUrl("~/Scripts/jquery.cookie.js") %>" type="text/javascript"></script>--%>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.hotkeys.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.jstree.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/GestionLocalizaciones.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                    <li><a href="#" id="eliminar"><span class="icon-asociar-16"></span>Asociar Plan</a></li>
                </ul>
            </div>
            <div class="pagetitle">
                <h2>Gestión Localizaciones</h2>
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
            <form id="frmLocalizacion" runat="server" >
                <div id="mmenu" style="height:30px;">
                    <label>Filtro:</label>
                    <input type="text" id="text" value="" class="textbox" />
                    <input type="button" id="search" value="Buscar" />
                    <input type="button" id="clear_search" value="Reestablecer" />                    
                </div>
                <br />
                <div id="ArbolLocalizacion">
                </div>
            </form>
        </div>
    </div>
</asp:Content>
