// JavaScript Document
/// <reference path="jquery-2.0.0-vsdoc.js" />
function MarcarCheckbox(id, cID) {//esta funcion se pone en el disparador onchange del check padre
    //id: desmarca y marca todo los checkbox
    //pID: contenedor de los checkbox
    $("#" + cID + " :checkbox").attr('checked', $('#' + id).is(':checked'));
}
function MarcarCheckboxClass(id) {
    //id: id del checbox cliqueado
    //se marcaran los checkbox que tienen una classe .id
    $("." + id + ":checkbox").attr('checked', $('#' + id).is(':checked'));
}
function ConvertirDateTimeAString(jsonDate,formato) {
    //jsonDate --> "/Date(1245398693390)/"; 
    var re = /-?\d+/;
    var m = re.exec(jsonDate);
    //return eval(jsonDate.slice(1, -1));
    return dateFormat(parseInt(m[0]), formato); //necesita libreria  
}
function CamposNumericos(inputs) {
    $(inputs).numeric();//se necesita el plugin jquery numeric
}
function showInfo(txt) {
    $(".msj-box").show().html(txt).delay(4000).fadeOut(500);
}
function showMensaje(txt) {
    $(".msj-box").show().html('<div class="ok"><p>' + txt + '</p></div>');
}
function showError(txt) {
    $(".msj-box").show().html('<div class="error"><p>' + txt + '</p></div>');
}
//mensajes para el popup
function showInfoPopupOK(txt) {
    $(".msj-box-popup").show().html('<div class="ok"><p>' + txt + '</p></div>').delay(4000).fadeOut(500);
}
function showInfoPopupCargando(txt) {
    $(".msj-box-popup").show().html('<div style="text-align:center">' + txt + '</div>');
}
function showMensajePopup(txt) {
    $(".msj-box-popup").show().html('<div class="ok"><p>' + txt + '</p></div>');
}
function showErrorPopup(txt) {
    $(".msj-box-popup").show().html('<div class="error"><p>' + txt + '</p></div>');
}
//
function limpiarMsjBox() {
    $(".msj-box").html("").hide();
}
function CamposRequeridosVacios(inputs) {
    var error = false;
    $(inputs).each(function (i) {
        if ($(this).val() == "") {
            $(this).addClass("bgerror");
            error = true;
        } else {
            $(this).removeClass("bgerror");
        }
    });
    return error;
}
function DesmarcarCamposVaciosLLenados() {
    $("select, input, textarea").each(function (i) {
        $(this).change(function () {
            $(this).removeClass("bgerror");
        });
    });
}
function quitarBgerror() {
    $("input select textarea").each(function (i) {
        if ($(this).val() != "")
            $(this).removeClass("bgerror");
    });
}
function trHover() {
    $('.trhover').hover(
	function () {
	    $(this).css("background-color", "#E1EFC9");
	},
	function () {
	    $(this).css("background-color", "#FFF");
	});
    $('.row0').hover(
	function () {
	    $(this).css("background-color", "#D7EBFF");
	},
	function () {
	    $(this).css("background-color", "#f9f9f9");
	});
    $('.row1').hover(
	function () {
	    $(this).css("background-color", "#D7EBFF");
	},
	function () {
	    $(this).css("background-color", "#eeeeee");
	});
}
function fechaUI(selectorFecha, selectorMostrarTextoFecha) {
    
    $.datepicker.setDefaults({ dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'], dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre'], monthNamesShort: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre'], dateFormat: 'dd/mm/yy'
    });
    $(selectorFecha).datepicker("option", "showAnim", "slideDown");
    $(selectorFecha).datepicker({
        altField: selectorMostrarTextoFecha,
        altFormat: "DD, d MM, yy",
        changeMonth: true,
        changeYear: true
    });
    $(selectorFecha).attr("readonly", "readonly");    
}
function limpiarBoxFrm(inputs) {
    $(inputs).each(function () {
        $(this).removeClass("bgerror");
        var type = this.type;
        var tag = this.tagName.toLowerCase();
        if (type == 'text')
            this.value = "";
        if (tag == 'select')
            this.selectedIndex = 0;
        if (tag == 'textarea')
            this.value = "";
    });
}
function limpiarTodosLosCampos() {
    $("input, select, textarea").each(function () {
        $(this).removeClass("bgerror");
        var type = this.type;
        var tag = this.tagName.toLowerCase();
        if (type == 'text')
            this.value = "";
        if (tag == 'select')
            this.selectedIndex = 0;
        if (tag == 'textarea')
            this.value = "";
    });
}
//para las fechas de nacimiento funciones
function getFechaNacimiento(inputDia, inputMes, inputAnio) {
    var anio = inputAnio.val();
    var mes = inputMes.val();
    var dia = inputDia.val();
    var fecha;
    if (anio == 0 || mes == 0 || dia == 0) {
        return false;
    }
    else {
        fecha = anio + "-" + mes + "-" + dia; //formato mysql
        return fecha;
    }
}
function llenarSelectDia(inputDia){
    for(var i=1;i<=31;i++){
        $(inputDia).append("<option value='"+i+"'>"+i+"</option>");
    }
}
function SetDiasDelMesSegunFechaActual(inputDia) {//selected por defecto el ultimo dia
    var ahora = new Date();
    var m = ahora.getMonth();
    var anio = ahora.getYear();
    
    $(inputDia).find("option[value='31']").remove();
    $(inputDia).find("option[value='30']").remove();
    $(inputDia).find("option[value='29']").remove();
    m = m + 1; //porque m:0->11
    m = m.toString();
    switch (m) {
        case '1': case '3': case '5': case '7': case '8': case '10':
        case '12':
            $(inputDia).append("<option value='29'>29</option>");
            $(inputDia).append("<option value='30'>30</option>");
            $(inputDia).append("<option value='31' selected='selected'>31</option>");
            break;
        case '4': case '6': case '9':
        case '11':
            $(inputDia).append("<option value='29'>29</option>");
            $(inputDia).append("<option value='30' selected='selected'>30</option>");
            break;
        case '2':
            if (esBisiesto(anio)) {
                $(inputDia).append("<option value='29' selected='selected'>29</option>");
            }
            else {
                $(inputDia).find("option[value='29']").remove();
                $(inputDia).find("option[value='28']").attr('selected', 'selected');
            }

            break;
    }
}
//cambios para los meses de febrero
function changeFechaNacimiento(inputDia, inputMes, inputAnio) {
    $(inputMes).change(function () {
        var m = $(this).val();
        $(inputDia).find("option[value='31']").remove();
        $(inputDia).find("option[value='30']").remove();
        $(inputDia).find("option[value='29']").remove();
        switch (m) {
            case '1': case '3': case '5': case '7': case '8': case '10':
            case '12':
                $(inputDia).append("<option value='29'>29</option>");
                $(inputDia).append("<option value='30'>30</option>");
                $(inputDia).append("<option value='31'>31</option>");
                break;
            case '4': case '6': case '9':
            case '11':
                $(inputDia).append("<option value='29'>29</option>");
                $(inputDia).append("<option value='30'>30</option>");
                break;
            case '2':
                addDropDiasFebrero(inputDia, inputMes, inputAnio);
                break;
        }
    }); //fin change el mes
    $(inputAnio).change(function () {
        addDropDiasFebrero(inputDia, inputMes, inputAnio);
    });
} //fin chanfge fecha de nacimiento

//comprobar y añadir dia 29 a febrero
function addDropDiasFebrero(inputDia, inputMes, inputAnio) {
    if ($(inputMes).val() == 2) {
        if (esBisiesto($(inputAnio).val())) {
            $(inputDia).append("<option value='29'>29</option>");
        }
        else {
            $(inputDia).find("option[value='29']").remove();
        }
    }
}
//comprobar si un año es bisiesto
function esBisiesto(year) {
    year = parseInt(year,10);
    //alert(year);
    return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? true : false;
}
function implodeobj(x) {
    var r;
    if (x.length != 0) {
        r = x.eq(0).val();
        x.each(function (i) {
            if (i != 0) r += "|" + x.eq(i).val();
        });
    }
    else r = "";
    return (r);
}
function hora() {
    if (!document.layers && !document.all && !document.getElementById)
        return false;
    var Digital = new Date();
    var hours = Digital.getHours();
    var minutes = Digital.getMinutes();
    var seconds = Digital.getSeconds();
    var dn = "AM";
    if (hours > 12) {
        dn = "PM";
        hours = hours - 12;
    }
    if (hours == 0)
        hours = 12;
    if (minutes <= 9)
        minutes = "0" + minutes;
    if (seconds <= 9)
        seconds = "0" + seconds;
    //change font size here to your desire
    myclock = " " + hours + ":" + minutes + ":" + seconds + " " + dn;
    if (document.layers) {
        document.layers.liveclock.document.write(myclock);
        document.layers.liveclock.document.close();
    }
    else if (document.all)
        liveclock.innerHTML = myclock;
    else if (document.getElementById)
        document.getElementById("liveclock").innerHTML = myclock;
    setTimeout("hora()", 1000);
}
//--------------Funciones para el popup
function MostrarPopup(selectorCapa) {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $(selectorCapa).show();
}

//jQuery.fn.showForm = function () {
//    $("#fondo").fadeTo(0, 0.6);
//    $("#fondo").height($(document).height());
//    $(this).show();
//};

function OcultarPopup() {
    $(".mensaje").hide();
    $("#fondo").hide();
}
function OcultarConClick() {
    $(".closeClick").click(function () {
        $(".mensaje").hide();
        $("#fondo").hide();
    });
}
function OcultarPopupConESC() {
    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            OcultarPopup();
        }
    });
}
