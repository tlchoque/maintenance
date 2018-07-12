/// <reference path="jquery-2.0.0-vsdoc.js" />
$(document).ready(function (e) {

    ObtenerDatosDeUsuario();
    DesmarcarCamposVaciosLLenados(); //desmarcamos los campos marcados que se van llenando
    $("#guardar").click(function (e) {
        e.preventDefault();
        if (CamposRequeridosVacios(".requerido")) {//si estan vacios
            showError("Los campos marcados son obligatorios");
            return false;
        } else {
            limpiarMsjBox();
            quitarBgerror();
            if ($("#contrasenia1").val() != "" || $("#contrasenia2").val() != "") {
                if (ValidarContrasenias("#contrasenia1", "#contrasenia2") == true) {
                    $("input[type='password']").val("");
                    return false;
                }
            }
            if (GuardarUsuario() > 0) {
                ObtenerDatosDeUsuario();
                $("input[type='password']").val("");
            }
        }
    });

});           //fin document


//----------funciones de usuario
function GuardarUsuario() {
    var ID = 0;
    var usuario = new Object();
    usuario.USUA_Interno = $("#IDUsuario").val();
    usuario.USUA_Apellido = $("#Apellido").val();
    usuario.USUA_Nombre = $("#Nombre").val();
    usuario.USUA_Usuario = $("#Usuario").val();
    usuario.USUA_Direccion = $("#Direccion").val();
    usuario.USUA_Correo = $("#Email").val();
    usuario.USUA_Contrasenia = $("#contraseniaActual").val();
    usuario.USUA_ContraseniaNueva = $("#contrasenia1").val();
    $.ajax({
        data: JSON.stringify(usuario),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Perfil.aspx/GuardarUsuario",
        async: false,
        cache: false,
        success: function (data) {
            ID = parseInt(data.d);

            if (ID > 0) {
                showMensaje("Datos actualizados correctamente");
            } else if (ID == -1) {
                showError("Contraseña Actual Incorrecta. Se hace diferencia entre mayúsculas y minúsculas. Vuelva a escribirla");
            } else {
                showError("Error al actualizar los datos");
            }
        } //fin success
    });  //fin de ajax
    return ID;
} //fin guardar usuario
function ObtenerDatosDeUsuario() {
    var usuario = new Object();
    usuario.USUA_Interno = $("#IDUsuario").val();
    $.ajax({
        data: JSON.stringify(usuario),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Perfil.aspx/ObtenerUsuario",
        async: false,
        cache: false,
        success: function (data) {
            var item = data.d;

            $("#IDUsuario").val(item.USUA_Interno);
            $("#Apellido").val(item.USUA_Apellido);
            $("#Nombre").val(item.USUA_Nombre);
            $("#Usuario").val(item.USUA_Usuario);
            $("#Direccion").val(item.USUA_Direccion);
            $("#Email").val(item.USUA_Correo);
        }
    });    //fin de ajax
}
function ValidarContrasenias(input1, input2) {
    var error = false;
    if ($("#IDUsuario").val() == "") {
        if ($(input1).val() == "" || $(input2).val() == "") {
            showError("Escriba una contraseña. Mínimo 6 dígitos");
            return true;
        }
    }
    if ($(input1).val() != $(input2).val()) {
        error = true;
        showError("Las contraseñas nuevas no coinciden. Por favor vuelva a escribirlas. Se hace diferencia entre mayúscula y minúscula");
    } else if ($(input1).val().length < 6) {
        error = true;
        showError("La contrasenia nueva no debe tener menos de 6 dígitos");
    }
    return error;
}