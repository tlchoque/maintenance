<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Panel.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.Panel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
<div id="element-box">
    <div class="m margintop10">
        <div class="cpanel-left">
                <div class="icon">
                    <a href="#" >
                        <img src="img/icon-48-reportes.png" width="48" alt=""/>
                        <span>Reportes</span>
                    </a>
                </div>
                <div class="icon">
                    <a href="GestionUsuarios.aspx" >
                        <img src="img/icon-48-user.png"  height="48" alt=""/>
                        <span>Gestor de Usuarios</span>
                    </a>
                </div>
                <div class="icon">
                    <a href="UsuarioDatos.aspx?opc=nuevo" >
                        <img src="img/icon-48-user-add.png"  height="48" alt=""/>
                        <span>Agregar Usuario</span>
                    </a>
                </div>
                <div class="icon">
                    <a href="GestionLocalizaciones.aspx" >
                        <img src="img/icon-48-SE.png" width="48" alt=""/>
                        <span>Sub-Estaciones</span>
                    </a>
                </div>
                <div class="icon">
                    <a href="Perfil.aspx" >
                        <img src="img/icon-48-editar-perfil.png" width="48" alt=""/>
                        <span>Editar Perfil</span>
                    </a>
                </div>
                <div class="icon">
                    <a href="#" >
                        <img src="img/icon-48-config.png" width="48" alt=""/>
                        <span>Configuración del Sistema</span>
                    </a>
                </div>
                <div class="clr"></div>
        </div>
        <div class="cpanel-right">
                Aquí en la derecha podran ir algunos informes resaltantes para el administrador
                <!--<div id="ultimosRegistros"></div>-->
        </div>
        <div class="clr"></div>
    </div><!--fin class m-->
</div>
</asp:Content>
