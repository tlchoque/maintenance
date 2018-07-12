<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="GestionPlanes.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.GestionPlanes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Ventana.css" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Styles/Pager.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.pager.es.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/GestionPlanes.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <div id="fondo">
    </div>
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                    <li><a href="#" id="nuevo"><span class=" icon-new-16"></span>Nuevo </a></li>
                    <li><a href="#" id="editar"><span class=" icon-editar-16"></span>Editar Actividades</a></li>
                    <li><a href="#" id="copiar"><span class=" icon-replicar-16"></span>Replicar</a></li>
                    <li><a href="#" id="eliminar"><span class=" icon-delete-16">
                    </span>Eliminar</a></li>
                </ul>
            </div>
            <div class="pagetitle">
                <h2>Gestión de Planes</h2>
            </div>
            <div class="clr"></div>
        </div>
        <!--fin class m-->
    </div>
    <div class="msj-box">
    </div>
    <div id="element-box">
        <div class="m margintop10">
            <div class="floatIzq">
                <form id="frmfiltro" runat="server">
                <label>Filtro:</label>
                            <input type="text" name="stringFiltro" id="stringFiltro" class="textbox" 
                            title="Escriba el nombre, marca,modelo,serie o código del equipo" />
            
                            <input type="submit" name="submit" id="buscar" value="Buscar" />
                            <input type="button" value="Restablecer" id="restablecer" />
                </form>
                </div>
                <div class="floatDer">
                <label>
                                Mostrar #
                            </label>
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
            <table class="stabla " id="listaPlanes" width="100%">
                <thead>
                    <tr >
                        <th width="3%">
                            <input type="checkbox" id="allchecked" onchange="MarcarCheckbox(this.id,'listaPlanes');" />
                        </th>
                        <th width="47%" >
                            <span >Descripción</span>
                        </th>
                        <th width="30%">
                            <span >Régimen</span>
                        </th>
                        <th width="20%">
                            <span >Unidad Lecturas</span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <asp:Label ID="LbMsg" runat="server" Text=""></asp:Label>
            <div align="center" style="border: 1px solid #ccc;">
                <div id="pager"></div>
                <div class="clr"></div>
            </div>
        </div>
    </div>


    <div class="mensaje" id="divPlan">
	<div class="mtop">
    	<h2 id="tituloExamen">PLAN DE TRABAJO</h2>
        <div class="close closeForm" id="divCerrar"></div>
    </div>
	<div class="mcenter">
		    <input type="hidden" id="Interno" value=""/>
   		    <div class="itemc">
            	<label>Descripcción:</label>                
                <textarea  cols=""rows="3" id="txtDescripcion" class="mayuscula" style="width:200px"></textarea>
            </div>
            <div class="itemc">
            	<label>Régimen:</label>
                <select id="lstRegimen">
                  <option value="" class="selectinfo">--seleccione--</option>
                  <option value="F">Fechas</option>
                  <option value="L">Lecturas</option>
                </select>
            </div>
            <div class="itemc" id="divUnidadLecturas">
            	<label>Unidad Lecturas:</label>
                <input type="text"name="columna" id="txtUnidadLecturas"/>
            </div>       
            <div class="resultAjaxForm" id="divMensaje" >
            	<div id="load"></div>
            </div>
           	<div class="controles">
            	<input type="submit" class="boton" name="agregar" id="btnAgregar" value="Agregar" title="Guardar" />
                <input type="reset" class="boton closeForm" value="Cancelar" title="Borrar campos" />
            </div>     
    </div>
   	<div class="mbotton"></div>
    </div>

    <div class="mensaje" id="divCopia">
	<div class="mtop">
    	<h2 id="H1">NUEVO PLAN DE TRABAJO</h2>
        <div class="close closeForm" id="divCerrar2"></div>
    </div>
	<div class="mcenter">
		    <input type="hidden" id="InternoCopia" value=""/>
   		    <div class="itemc">
            	<label>Nombe Plan:</label>                
                <textarea  cols=""rows="3" id="txtNombreCopia" class="mayuscula" style="width:200px"></textarea>
            </div>
            
           	<div class="controles">
            	<input type="submit" class="boton" name="agregar" id="btnCopiar" value="Copiar" title="Guardar" />
                <input type="reset" class="boton closeForm" value="Cancelar" title="Borrar campos" />
            </div>     
    </div>
   	<div class="mbotton"></div>
    </div>
</asp:Content>
