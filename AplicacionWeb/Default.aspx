<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mantenimiento.AplicacionWeb.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>:: ELECTROSUR</title>
    <meta name="author" content="cima" />
    <link rel="shortcut icon" href="" type="image/x-icon" />
    <link href="Styles/Plantilla.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Scripts/json3.min.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script>
</head>
<body>
	<div id="header" >
    	<span class="title"><a href="Default.aspx">Sistema de Mantenimiento - Transmisión</a></span>
    </div><!--fin header-->
    <div id="cuerpo">
      <div id="element-box" class="login">
          <div class="m wbg">
              <h1>Conexión a la administración de SMT</h1>      
          <div class="msj-box marginbottom10"></div>
          <asp:Label ID="label" runat="server"></asp:Label>
          <div id="section-box">
            <div class="m">
            	<form id="form" runat="server" >
                  <div class="item margintop20">
                      <label  style="width:60px;">Usuario:</label>
                      <input type="text" name="user" id="user" />
                      <div class="errorlogin">Ingrese su usuario</div>
                  </div>
                  <div class="item">
                      <label  style="width:60px;">Contraseña:</label>
                      <input type="password" name="pass" id="pass" />
                      <div class="errorlogin">Ingrese su contraseña</div>
                  <div style="display:none; text-align:center; color:#FF8040; font-weight:bold; padding-top:10px;" id="ingresando"> </div>
                  </div>
                  
                  <div class="submit-login">
                    <input type="submit"  value="Conectar" class="boton"/>
                  </div>
                </form>
            <div class="clr"></div>
            </div><!--class="m"-->
          </div><!--fin section-box-->
  
            <p style="text-align:justify;">Use un nombre de usuario y contraseña válidos para obtener acceso al sistema <b>SMT</b>.</p>
            <div id="lock"></div>
            <div class="clr"></div>
          </div><!--fin class m wbg-->
        </div><!--fin class login-->
          <noscript>
				¡Advertencia! Para poder realizar operaciones correctamente 
                desde la administración, debe tener habilitado JavaScript.
          </noscript>
    </div><!--fin cuerpo-->
    <div class="clr"></div>
    <div id="pie">
		<p class="copyright">Sistema de Mantenimiento - Transmisión<br />
            ©2013 <a href="#">ELECTROSUR</a></p>
    </div>
</body>
</html>
