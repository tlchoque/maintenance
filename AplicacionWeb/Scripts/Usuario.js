/// <reference path="jquery-2.0.0-vsdoc.js" />
$(document).ready(function (e) {

    if ($("#IDUsuario").val() != "" && $("#opcURL").val() == "editar") {
        $(".pagetitle h2").html("Datos de Usuario");
        $("#nuevo").parent("li").hide();
        $("#guardar_nuevo").parent("li").hide();
        ObtenerDatosDeUsuario();
    }
    DesmarcarCamposVaciosLLenados(); //desmarcamos los campos marcados que se van llenando
    //    $("input[name=Bloquear]:radio").click(function (e) {
    //        e.preventDefault();
    //        alert($(this).val());
    //    });

    $("#nuevo").click(function (e) {
        e.preventDefault();
        //alert("kk");
        $("#IDUsuario").val("");
        limpiarMsjBox();
        limpiarTodosLosCampos();
    });
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
    $("#guardar_cerrar").click(function (e) {
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
                location.href = "GestionUsuarios.aspx";
            }
        }
    });
    $("#guardar_nuevo").click(function (e) {
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
                limpiarTodosLosCampos();
            }
        }
    });

});           //fin document


//----------funciones de usuario
function GuardarUsuario() {
    var ID = 0;
    var usuario = new Object();
    usuario.USUA_Interno=$("#IDUsuario").val();
    usuario.USUA_Apellido=$("#Apellido").val();
    usuario.USUA_Nombre = $("#Nombre").val();
    usuario.USUA_Usuario = $("#Usuario").val();
    usuario.USUA_Activo = $("input[name='Bloquear']:checked").val();
    usuario.USUA_Direccion=$("#Direccion").val();
    usuario.USUA_Correo=$("#Email").val();
    usuario.GRUP_Interno = $("#Grupo").val();
    usuario.USUA_Contrasenia = $("#contrasenia1").val();

    $.ajax({
        data: JSON.stringify(usuario),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "UsuarioDatos.aspx/GuardarUsuario",
        async: false,
        cache: false,
        success: function (data) {
            ID = parseInt(data.d);

            if (ID > 0) {
                showMensaje("Datos guardados correctamente");
                $("#IDUsuario").val(ID);
                
            } else {
                showError("No se insertó el usuario");
                //$("#IDUsuario").val("");
            }
        } //fin success
    }); //fin de ajax
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
        url: "UsuarioDatos.aspx/ObtenerUsuario",
        async: false,
        cache: false,
        success: function (data) {
            var item = data.d;

            $("#IDUsuario").val(item.USUA_Interno);
            $("#Apellido").val(item.USUA_Apellido);
            $("#Nombre").val(item.USUA_Nombre);
            $("#Usuario").val(item.USUA_Usuario);

            if (item.USUA_Activo == true)
                $("input[name='Bloquear'][value='true']").attr("checked", "checked");
            else
                $("input[name='Bloquear'][value='false']").attr("checked", "checked");
            $("#Direccion").val(item.USUA_Direccion);
            $("#Email").val(item.USUA_Correo);
            $("#Grupo").val(item.GRUP_Interno);
            $("#UsuarioCreador").val(item.UsuarioCreador);

            if (item.HIIN_FechaIngreso != null)
                $("#FechaUltimoIngreso").val(ConvertirDateTimeAString(item.HIIN_FechaIngreso, "dd/mm/yyyy HH:MM:ss.l"));
            else
                $("#FechaUltimoIngreso").val("");

            if (item.AUDI_FechaCrea != null)
                $("#FechaRegistro").val(ConvertirDateTimeAString(item.AUDI_FechaCrea, "dd/mm/yyyy HH:MM:ss.l"));
            else
                $("#FechaRegistro").val("");

            $("#IPacceso").val(item.HIIN_IPacceso);
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
        showError("Las contraseñas no coinciden. Por favor vuelva a escribirlas. Se hace diferencia entre mayúscula y minúscula");
    } else if ($(input1).val().length < 6) {
        error = true;
        showError("La contrasenia no debe tener menos de 6 dígitos");
    }
    return error;
}