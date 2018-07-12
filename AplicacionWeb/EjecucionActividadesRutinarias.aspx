<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="EjecucionActividadesRutinarias.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.EjecucionActividadesRutinarias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Page.ResolveUrl("~/Styles/Pager.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Styles/Ventana.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Styles/cupertino/jquery-ui-1.10.3.custom.min.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.ui.core.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.ui.widget.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.ui.datepicker.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.pager.es.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/EjecucionActividadesR.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/date.format.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
<div id="fondo">
</div>
<div id="ContenidoPagina" ClientIDMode="Static" runat="server">
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                    <li><a  href="#" title="Ejecutar Actividades" id="EjecutarActividades"><span class="icon-16-programar-act"></span>Ejecutar</a></li>
                    <li class="divider"></li>
                    <li><a href="#" title="Configurar Periodo" id="ConfigurarP"><span class="icon-16-config-date"></span>Periodo</a></li>
                </ul>
            </div>
            <!--fin toolbar-->
            <div class="pagetitle icon-32-progra-act">
                <h2>Ejecución de Actividades Rutinarias - Actividades Programadas</h2>
            </div>
            <div class="clr"></div>
        </div>
        <!--fin class m-->
    </div>
    <!--fin toolbar-box -->
    <div class="msj-box">
    </div>
    <asp:Label ID="lblMsj" runat="server"></asp:Label>
    <div id="element-box">
        <div class="m margintop10">
            <form id="frmfiltro" runat="server">
            <div class="floatIzq">
            <h2 id="TituloTabla"></h2>
            <%--<label>
                            Filtro:
                        </label>
                        <input type="text" name="stringFiltro" id="stringFiltro" class="textbox" 
                        title="Escriba el nombre de la actividad" />
                        <input type="submit" name="submit" id="buscar" value="Buscar" />
                        <input type="button" value="Restablecer" id="restablecer" />--%>
                
                        
            </div>
            <div class="floatDer">
            
            <select id="Localizacion" class="selectbox" style="width:auto;">
                    <option class="selectinfo" value=""> Sub-Estación: Todas</option>                   
                </select>
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
            <table class="stabla " id="listRegistros" width="100%">
                <thead>
                    <tr >
                        <th width="2%">
                            <input type="checkbox" id="allchecked" onchange="MarcarCheckbox(this.id,'listRegistros');" />
                        </th>
                        <th width="2%"></th>
                        <th width="46%" >
                            Actividades Programadas
                        </th>
                        <th width="19%">
                            Fecha Última Ejecución
                        </th>
                        <th width="7%">
                            Frecuencia
                        </th>
                        <th width="19%">
                            Fecha Programado                          
                        </th>
                        <th width="19%">
                            Fecha de Ejecución
                            <%--<a href="#" title="Editar Fechas de Ejecución" class="Guardar" id="guardarFechasNext"></a> --%>                          
                        </th>
                        <th width="4%">
                            Días Retraso
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
<div class="mensaje" id="ConfigPeriodo">
	<div class="mtop">
    	<h2 id="H1">PERIODO PROGRAMACION ACTIVIDADES</h2>
        <div class="close closeClick"></div>
    </div>
	<div class="mcenter">
		<input type="radio" value="Diario" name="periodo" /><label>Diario - De hoy a hoy</label><br />
        <input type="radio" value="Semanal" name="periodo" checked="checked"/><label>Semanal - De hoy al siguiente </label>
        <select id="DiaSemana">
            <option value="0">Domingo</option>
            <option value="1">Lunes</option>
            <option value="2">Martes</option>
            <option value="3">Miércoles</option>
            <option value="4">Jueves</option>
            <option value="5">Viernes</option>
            <option value="6">Sábado</option>
        </select><br />
        <%--<input type="radio" value="" name="periodo"/><label>Mensual - De hoy a fin de mes</label><br />--%>
        <input type="radio" value="Mensual" name="periodo"/><label>Mensual - De hoy al día</label>
        <select id="DiaMes">
        </select>
        <label> de cada mes</label>                
        <div class="controles">
            <input type="button" class="boton" name="" id="saveConfigPeriodo" value="Guardar Periodo" title="" />          
        </div><br />
        <div class="msj-box-popup"></div>     
    </div>
   	<div class="mbotton"></div>
    </div>
<div  id="msj_box_server" ClientIDMode="Static" runat="server">
    <h1>Este contenido no esta disponible</h1>
    <p>La página que has solicitado no puede mostrarse. Puede que no esté disponible temporalmente, 
    el enlace puede haber expirado o puede que no tengas permiso para visualizarla.</p>
    <p><a href="Panel.aspx">Volver a la página de Inicio</a></p>
</div>
</asp:Content>
