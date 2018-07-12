<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true"
    CodeBehind="GestionEquipos.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.GestionEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Page.ResolveUrl("~/Styles/Pager.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.pager.es.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/gestionEquipos.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <div id="ContenidoPagina" ClientIDMode="Static" runat="server">
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                    <li><a  href="#" id="asociar"><span class="icon-asociar-16"></span>Asociar</a></li>
                    <li><a  href="Equipo.aspx?opc=nuevo"><span class="icon-new-16"></span>Nuevo</a></li>
                    <li><a  href="#" id="eliminar"><span class="icon-delete-16"></span>Eliminar</a></li>
                </ul>
            </div>
            <!--fin toolbar-->
            <div class="pagetitle">
                <h2>Gestión de Equipos</h2>
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
            <form id="frmfiltro" runat="server">
<div class="floatIzq">
<label>
                Filtro:
            </label>
            <input type="text" name="stringFiltro" id="stringFiltro" class="textbox" 
            title="Escriba el nombre, marca,modelo,serie o código del equipo" />
            <input type="submit" name="submit" id="buscar" value="Buscar" />
            <input type="button" value="Restablecer" id="restablecer" />
</div>
<div class="floatDer">
<label>Mostrar #</label>
    <select id="TamanioPagina" class="selectbox" style="width: auto;">
        <option value="5">05</option>
        <option value="10">10</option>
        <option value="15">15</option>
        <option value="20" selected="selected">20</option>
        <option value="25">25</option>
        <option value="50">50</option>
        <option value="75">75</option>
        <option value="100">100</option>
    </select>
</div>
<div class="clr"> </div>            
            <div class=" margintop10">
                <span>Mostrando <b id="num"></b> de <b id="total"></b> registros</span>
            </div>
            <table class="stabla " id="listEquipos" width="100%">
                <thead>
                    <tr >
                        <th width="3%">
                            <input type="checkbox" id="allchecked" onchange="MarcarCheckbox(this.id,'listEquipos');" />
                        </th>
                        <th width="22%" >
                            <span >Equipo</span>
                        </th>
                        <th width="10%">
                            <span>Marca</span>
                        </th>
                        <th width="9%">
                            <span >Modelo</span>
                        </th>
                        <th width="9%">
                            Serie
                        </th>
                        <th width="9%">
                            Código
                        </th>
                        <th width="9%">
                            <span rel="Tipo">Tipo</span>
                        </th>
                        <th width="9%">
                            <span>Estado</span>
                        </th>
                        <th width="20%" >
                            <span>Localización</span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <asp:Label ID="LbMsg" runat="server" Text=""></asp:Label>
            <div align="center" style="border: 1px solid #ccc;">
                <div id="pager">
                </div>
                <div class="clr">
                </div>
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
