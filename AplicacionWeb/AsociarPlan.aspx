<%@ Page  Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AsociarPlan.aspx.cs" 
Inherits="Mantenimiento.AplicacionWeb.AsociarPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Page.ResolveUrl("~/Styles/cupertino/jquery-ui-1.10.3.custom.min.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-1.9.1.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-ui-1.10.3.custom.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/AsociarPlan.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/Funciones.js") %>" type="text/javascript"></script>
    <style type="text/css">
	.demo-description {
		clear: both;
		padding: 12px;
		font-size: 1.3em;
		line-height: 1.4em;
	}
	.ui-draggable, .ui-droppable {
		background-position: top;
	}
	.custom-combobox {
		position: relative;
		display: inline-block;
	}
	.custom-combobox-toggle {
		position: absolute;
		top: 0;
		bottom: 0;
		margin-left: -1px;
		padding: 0;
		/* support: IE7 */
		*height: 1.7em;
		*top: 0.1em;
	}
	.custom-combobox-input {
		margin: 0;
		padding: 0.3em;
	}	
	.ui-autocomplete {
	position: absolute;
	top: 0;
	left: 0;
	cursor: default;
	}
	.ui-tooltip {
	padding: 8px;
	position: absolute;
	z-index: 9999;
	max-width: 300px;
	-webkit-box-shadow: 0 0 5px #aaa;
	box-shadow: 0 0 5px #aaa;
	}
	body .ui-tooltip {
		border-width: 2px;
	}    
	.ui-autocomplete {
    max-height: 200px;
    overflow-y: auto;   /* prevent horizontal scrollbar */
    overflow-x: hidden; /* add padding to account for vertical scrollbar */
    z-index:1000 !important;
    }
    .divTabla
    {
    	padding:0px;
        width: 80%;
        float:left;
        position: relative;
    }
    .divTabla table 
    {
    border:1px solid;
    max-width: 100%;
    width: 100% !important;
    }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <div id="toolbar-box">
        <div class="submenu-box">
            <div class="toolbar-list">
                <ul>
                	<li><a href="#" id="asociar"><span class=" icon-guardar-16"></span>Asociar</a></li>
                    <li><a href="#" id="guardar_cerrar"><span class=" icon-guardar-cerrar-16"></span>Asociar y cerrar</a></li>
                    <li><a href="GestionEquipos.aspx" id="cerrar_pagina"><span class=" icon-cerrar-16"></span>Cerrar</a></li>
                </ul>
                	
            </div>
            <div class="pagetitle">
                <h2>Asociar PLan</h2>
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
            <form id="frmAsociar" runat="server">
            <asp:HiddenField ClientIDMode="Static" ID="opcURL" runat="server"/> 
            <asp:HiddenField ClientIDMode="Static" ID="IDs" runat="server"/>
            <div class="item margintop10">
                <label>Plan<span class="star">*</span></label>
                <div class="ui-widget" id="cboPlan">
                <select id="combobox" ClientIDMode="Static" runat="server" class="combo" >
                    <option value="" class="selectinfo">-seleccione-</option>
                </select>
                </div>
            </div>
            <div class="item" id="divPlanDescripcion">
                <label id="prueba">Descripcion</label>
                <div class="divTabla">
                    <table class="stabla" id="TablaPlan">
                        <thead>
                                <tr >
                                    <th width="24%">
                                        <span>Parte Plan</span>
                                    </th>
                                    <th width="40%">
                                        <span>Actividad</span>
                                    </th>
                                    <th width="12%" colspan="2">
                                        <span>Frecuencia</span>
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
            <br clear="all"/>
            <div class="item margintop10">
                <label>Equipos</label>
                <div class="divTabla">
                    <table class="stabla" id="TablaItems">
                    <thead>
                            <tr>
                                <th width="60%">
                                    <span>Descripcion</span>
                                </th>
                                <th width="40%">
                                    <span>Localizacion</span>
                                </th>
                            </tr>
                     </thead>
                     <tbody>
                     </tbody>
                </table>
                </div>
            </div>
            <br clear="all"/>
            </form>
        </div>
    </div>    
</asp:Content>