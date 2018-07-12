/// <reference path="jquery-2.0.0-vsdoc.js" />
var error;
var img = '<img src="img/ajax-loader.gif" />';
var imgok = '<img src="img/ajax_ok.png" />';
$(document).ready(function () {

    error = $("#form .errorlogin");
    var item = $('#form .item');
    $("#form [name='user']").val("");
    $("#form [name='pass']").val("");
    $("#form [name='user']").focus();
    $("#form").submit(function (e) {
        e.preventDefault();
        error.eq(0).slideUp(200);
        error.eq(1).slideUp(200);
        if (validar()) {
            return false;
        }
        showInfo(img + " Ingresando...");

        //
        var usuario = new Object();
        var respuesta = "";
        usuario.USUA_Usuario = $('#user').val();
        usuario.USUA_Contrasenia = $('#pass').val();
        $.ajax({
            data: JSON.stringify(usuario),
            type: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            url: "Default.aspx/IngresarSistema",
            async: false,
            cache: false,
            success: function (data) {
                data = data.d;
                limpiarMsjBox();
                if (data == "ok") {
                    //showOK("Ingreso satisfactorio");
                    location.href = 'Panel.aspx';
                    //respuesta = data;
                } else {
                    showError(data);
                }
            }
        }); //fin de ajax
        //if(respuesta
        return false;
    }); //fin submit
});     //fin document

function validar() {
    var r = false;
    if ($('#user').val() == "") {
        error.eq(0).slideDown(200);
        r = true;
        $('#user').focus();
    }
    if ($('#pass').val() == '') {
        error.eq(1).slideDown(200);
        if (!r)
            $('#pass').focus();
        r = true;
    }
    return r;
}
function showError(txt) {
    $(".msj-box").show().html('<div class="error"><p>' + txt + '</p></div>');
}
function limpiarMsjBox() {
    $(".msj-box").html("");
}
function showInfo(txt) {
    $(".msj-box").show().html(txt);
}
function showOK(txt) {
    $(".msj-box").show().html('<div class="ok"><p>' + txt + '</p></div>');
}