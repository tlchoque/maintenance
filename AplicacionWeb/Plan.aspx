<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.Plan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/panel/layout-default-latest.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Ventana.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    html{width:		100%;   height:		100%;	}   
    body{width:		98.4%; height:		100%;} 
    form{width:		100%; height:		100%;}
    #cuerpo{width:		99.85%; height:		95%;}
    #element-box{width:		97.25%; height:		93%;}
	.margintop10 {width:		99.25%;	height:		92%;	}
	#container {
		background:	#999;
		height:		92%;
		margin:		0 auto;
		width:		99.5%;
		max-width:	1280px;
		min-width:	1080px;
		_width:		1080px; 
		/* min-width for IE6 */
	}
	.pane {
		display:	none;  
	}
	.inputFrecuencia
	{
		height:11px;
		width:30px;		
	} 
	</style>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/panel/jquery-latest.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/panel/jquery-ui-latest.js") %>" type="text/javascript"></script>    
    <script src="<%= Page.ResolveUrl("~/Scripts/panel/jquery.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.js") %>" type="text/javascript"></script>
    <%--<script src="<%= Page.ResolveUrl("~/Scripts/jquery.cookie.js") %>" type="text/javascript"></script>--%>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.hotkeys.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.jstree.js") %>" type="text/javascript"></script>
     <script src="<%= Page.ResolveUrl("~/Scripts/jquery.numeric.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Plan.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <div id="fondo">
    </div>
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                    <li><a href="GestionPlanes.aspx" id="cerrar_pagina"><span class=" icon-cerrar-16"></span>Cerrar</a></li>
                </ul>
            </div>
            <!--fin toolbar-->
            <div class="pagetitle">
                <h2>Actualizar Plan</h2>
            </div>
            <div class="clr"></div>
        </div>
        <!--fin class m-->
    </div>
    <!--fin toolbar-box -->
    <%--<div class="msj-box">
    </div>--%>
    <form id="frmPlan" runat="server">
    <div id="element-box" >
        <div class="m margintop10" >
            <asp:HiddenField ClientIDMode="Static" ID="PLAN_Interno" runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="PLAN_Descripcion" runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="opcURL" runat="server"/>
            <asp:HiddenField ClientIDMode="Static" ID="PART_Interno" runat="server"/>
	        <div style="overflow: hidden; position: relative;" class="ui-layout-container" id="container">
                <div id="Arbol"  class="pane ui-layout-west ui-layout-pane ui-layout-pane-west"></div>
	            <div style="padding:0px;"class="pane ui-layout-center ui-layout-pane ui-layout-pane-center">
		            <table class="stabla " id="listaActividades" width="100%">
                        <thead>
                            <tr>
                                <th width="45%" >
                                    <span>Actividad</span>
                                </th>
                                <th width="13%" colspan="2">
                                    <span>Frecuencia</span>
                                </th>
                                <th width="11%">
                                    <span>Con Corte</span>
                                </th>
                                <th width="13%">
                                    <span>Con Medición</span>
                                </th>
                                <th width="15%">
                                    <span>Unidad Medición</span>
                                </th>
                                <th width="3%">
                                    <span>Eli</span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div style="position: absolute;left:45%;">
                        <input type="button" value="Agregar" id="btnNuevo" title="Agregar Actividad" />
                    </div>
	            </div>
                
	            <div style="padding:0px;" class="pane ui-layout-south ui-layout-pane ui-layout-pane-south">
                    <table class="stabla " id="listaParteActividades" width="60%">
                        <thead>
                            <tr >
                                <th width="20%">
                                    <span>Parte Plan</span>
                                </th>
                                <th width="32%">
                                    <span>Actividad</span>
                                </th>
                                <th width="12%" colspan="2">
                                    <span>Frecuencia</span>
                                </th>
                                <th width="12%" >
                                    <span>Tipo</span>
                                </th>
                                <th width="12%" >
                                    <span>Con Corte</span>
                                </th>
                                <th width="12%" >
                                    <span>Con Medición</span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>

    <div class="mensaje" id="divActividad">
	<div class="mtop">
    	<h2 id="tituloExamen">ACTIVIDAD RUTINARIA</h2>
        <div class="close closeForm" id="divCerrar"></div>
    </div>
	<div class="mcenter">
	   	<%--<form id="form1" action="">--%>
		    <input type="hidden" id="ACRU_Interno" value=""/>
   		    <div class="itemc">
            	<label>Actividad:</label>                
                <select ID="Actividad"  ClientIDMode="Static" style="width:auto; width:200px"  runat="server" >
                    <option value="" class="selectinfo">-seleccione-</option>
                </select>
            </div>
            <div class="itemc">
            	<label>Tipo Mantenimiento:</label>
                <select id="lstTipo">
                  <option value="" class="selectinfo">--seleccione--</option>
                  <option value="PV">Preventivo</option>
                  <option value="PD">Predictivo</option>
                </select>
            </div>
            <div class="itemc">
            	<label>Frecuencia</label>
                <input type="text"name="columna" id="txtFrecuencia" style="width:30px" />
                <select id="lstUnidadFrecuencia"  style="width:auto">
                  <option value="" class="selectinfo">--seleccione--</option>
                  <option value="M">Meses</option>
                  <option value="S">Semanas</option>
                  <option value="D">Días</option>
                </select>
            </div>  
            <div class="itemc">
            	<label>Con Corte</label>
                <input type="checkbox" id="chkConCorte"/>
            </div>  
            <div class="itemc">
            	<label>Con Medición</label>
                <input type="checkbox" id="chkConMedicion"/>
            </div>
            <div class="itemc" id="divMedicion">
            	<label>Unidad Medición:</label>
                <input type="text"name="columna" id="txtUnidadMedicion"/>
            </div>       
            <%--<div class="resultAjaxForm" id="divMensaje">
            	<div id="load"></div>
            </div>--%>
            <div class="msj-box"></div>
           	<div class="controles">
            	<input type="submit" class="boton" name="agregar" id="btnGuardar" value="Agregar" title="Guardar" />
                <input type="reset" class="boton closeForm" value="Cancelar" title="Borrar campos" />
            </div>         
        <%--</form>--%>
    </div>
   	<div class="mbotton"></div>
    </div>

    <div class="mensaje" id="divConfirmacion" style="margin-top: 150px; top:0%">
	    <div class="mtop">
    	    <h2 id="titulo">ELIMINAR ACTIVIDAD</h2>
            <div class="closeConfirm close"></div>
        </div>
        <div class="mcenter">
    	    <div style="text-align:center">
        	    <h3>¿ESTÁ SEGURO QUE DESEA ELIMINAR LA ACTIVIDAD?</h3>
                <div class="nombreConfirm" id="eNombre">
	            </div>
            </div>
            <%--<div class="resultAjaxForm">
                <div class="load" id="load2">
                </div>
            </div>--%>
            <div class="msj-box"></div>
            <div class="controles">
        	    <input type="hidden" id="idConfirmacion" />
			    <button type="button" class="boton" id="btnDelRegistro">Si</button>
           	    <button type="button" class="boton closeConfirm">No</button>        
            </div>
        </div>
        <div class="mbotton"></div>
    </div>
</asp:Content>
