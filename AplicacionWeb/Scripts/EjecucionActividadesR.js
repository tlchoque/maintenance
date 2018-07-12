/// <reference path="jquery-2.0.0-vsdoc.js" />
var img = '<img src="img/ajax-loader.gif" />';
var total_paginas = 0;
var pagina = 1;
//-------------------para el select de localizaciones
var localizaciones = null;
var charTab = "-";
var numCharTab = 1;
//--para el filtro
$(document).ready(function (e) {
    //para el popup
    ObtenerLocalizaciones();
    $("#ConfigurarP").click(function (e) {
        e.preventDefault();
        MostrarPopup("#ConfigPeriodo");
    });
    OcultarPopup();
    OcultarConClick();
    OcultarPopupConESC();
    //--para el dia del mes en el popup de configuracion
    llenarSelectDia("#DiaMes"); //llenamos el combo
    SetDiasDelMesSegunFechaActual("#DiaMes");
    //---
    //para el periodo
    GetPeriodo();
    $("#saveConfigPeriodo").click(function (e) {
        e.preventDefault();
        if (EditarPeriodo() > 0) {
            GetPeriodo();
            SetTituloTabla();
            calcularTotalPaginas();
            mostrarLista(1);
            OcultarPopup();
        }
    });

    $("#TamanioPagina").change(function () {
        calcularTotalPaginas();
        mostrarLista(pagina);
    });
    $("#Localizacion").change(function () {
        calcularTotalPaginas();
        mostrarLista(pagina);
    });
    //para cargar los datos
    SetTituloTabla();
    calcularTotalPaginas();
    mostrarLista(1);
    //guardar fechas next
    $("#guardarFechasNext").click(function (e) {
        e.preventDefault();
        var res = EditarFechasSiguientes();
        if (res > 0 && res != false) {
            mostrarLista(pagina);
            showMensaje("Se editaron " + res + " fechas del próximo mantenimiento");
        }
    });
    $("#EjecutarActividades").click(function (e) {
        e.preventDefault();
        var res = EjecutarActividades();
        if (res > 0 && res != false) {
            mostrarLista(pagina);
            showMensaje("Se Ejecutaron " + res + " actividad(es) rutinaria(s)");
        } else {
            showMensaje("No se ejecutó ninguna actividad");
        }
    });
});                       //fin de document
function mostrarLista(nro_actual) {
    limpiarMsjBox();
    pagina = nro_actual;
    var dato = new Object();
    dato.NumeroPagina = nro_actual;
    dato.TamanioPagina = $("#TamanioPagina").val();
    dato.PPRO_Periodo = $("input[name='periodo']:checked").val();
    dato.PPRO_DiaSemana = $("#DiaSemana").val();
    dato.PPRO_DiaMes = $("#DiaMes").val();
    dato.LOCA_Interno = $("#Localizacion").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/ActividadesParaSerEjecutadas",
        async: false,
        cache: false,
        success: function (data) {
            var actividades = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);

            if (actividades != null) {
                var actividad = "";

                $.each(actividades, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) {
                        css = 'class="row1"';
                        css2 = 'row1';
                    } else { css = 'class="row0"'; css2 = 'row0'; }
                    if (actividad != item.NOMB_Interno) {
                        html += '<tr class="subItem ' + css2 + '"><td class="center">';
                        html += '<input type="checkbox" id="Actividad' + item.NOMB_Interno + '" onchange="MarcarCheckboxClass(this.id);"/>';
                        html += '</td>';
                        html += '<td colspan="7">' + item.NOMB_Descripcion + '</td></tr>';
                        aux = 0;
                        actividad = item.NOMB_Interno;

                    }

                    var idHAR = item.HIAR_Interno;
                    var idAR = item.ACRU_Interno;
                    html += '<tr ' + css + '>';
                    html += '<td></td><td><input type="checkbox" value="' + idHAR + '" class="checkboxID Actividad' + item.NOMB_Interno + '"/>';
                    html += '<input type="hidden" class="hiddenAR" value="'+idAR+'"/>';
                    html += '</td>';
                    var EquiInm = item.EQUI_Descripcion;
                    var htmlLocalizacionEquipo = "";
                    if (item.EQUI_Interno == null) {
                        EquiInm = item.LOCA_NombreExtendido
                    } else {
                        htmlLocalizacionEquipo = '<p class="smallsub">(<span>Sub-Estación</span>: ' + item.EQUI_LocalizacionExtendida + ')</p>';
                    }
                    html += '<td class="izq"><a href="#?opc=editar&id=' + encodeURIComponent(idHAR + ';' + idAR) + '" >' + EquiInm + '</a>' + htmlLocalizacionEquipo + '</td>';
                    html += '<td>' + ConvertirDateTimeAString(item.HIAR_FechaEjecucionAnterior, "dd/mm/yyyy") + '</td>';
                    var frecuencia = "";
                    switch (item.ACRU_UnidadFrecuencia) {
                        case 'M': frecuencia = "Mes(es)"; break;
                        case 'S': frecuencia = "Semana(s)"; break;
                        case 'D': frecuencia = "Día(s)"; break;
                    }

                    html += '<td>' + item.ACRU_Frecuencia + ' ' + frecuencia + '</td>';
                    var fechaProg = item.HIAR_FechaProgramado;
                    if (fechaProg == null)
                        fechaProg = null;
                    else
                        fechaProg = ConvertirDateTimeAString(fechaProg, "dd/mm/yyyy");
                    html += '<td>' + fechaProg + '</td>';
                    html += '<td><input class="boxFecha fechaEjecucion" value="' + fechaProg + '" name="' + idHAR + '" /></td>';
                    var dias = parseInt(item.HIAR_Retrazo, 10);
                    if (dias < 0)
                        html += '<td></td>';
                    else
                        html += '<td>' + dias + ' día(s)</td>';
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="9" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.' +
                    ' Elija otra página</td></tr>';
                    paginarLista(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No hay actividades para ejecutar entre estas fechas</td></tr>';
                    paginarLista(0, 0);
                }
            } else {

                paginarLista(pagina, total_paginas);
            }
            $("#listRegistros tbody").html(html);
            $("#num").html(num);
            //eventos
            trHover();
            fechaUI(".boxFecha", "");
            FechaModificada();
            // $(".boxFecha").datepicker();

        } //fin de succes
    });                                       //fin de ajax
} //fin funcion mostrarLista
//para guardar las fechas
function EditarFechasSiguientes() {
    var res = 0;
    var dato = new Object();
    dato.FechasSiguientes = implodeFechasSiguientesEditadas($(".fechaNextEdit"));
    //alert(dato.FechasSiguientes);
    if (dato.FechasSiguientes == "") {
        showError("No se editó ninguna fecha");
        return false;
    }
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/EditarProximasFechas",
        async: false,
        cache: false,
        success: function (data) {
            res = parseInt(data.d, 10);

        } //fin success
    });   //fin ajax
    return res;
}
function EjecutarActividades() {
    var res = 0;
    var dato = new Object();
    dato.actividadesR = implodeActividadesParaEjecutar($("#listRegistros .checkboxID:checked"));
    //alert(dato.FechasSiguientes);
    if (dato.actividadesR == "") {
        showError("No se Ejecutó ninguna actividad. Elija las actividades rutinarias a ejecutar");
        return false;
    }
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/EjecutarActividades",
        async: false,
        cache: false,
        success: function (data) {
            res = parseInt(data.d, 10);

        } //fin success
    });   //fin ajax
    return res;
}
function calcularTotalPaginas() {
    var total = parseInt(ObtenerTotalRegistros());
    total_paginas = Math.ceil(total / parseInt($("#TamanioPagina").val(), 10));
    $("#total").html(total);
}
function ObtenerTotalRegistros() {
    var total = 0;
    var dato = new Object();
    dato.PPRO_Periodo = $("input[name='periodo']:checked").val();
    dato.PPRO_DiaSemana = $("#DiaSemana").val();
    dato.PPRO_DiaMes = $("#DiaMes").val();
    dato.LOCA_Interno = $("#Localizacion").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/TotalRegistrosParaEjecutar",
        async: false,
        cache: false,
        success: function (data) {
            total = parseInt(data.d, 10);
        } //fin success
    });  //fin ajax
    return total;
}
//para el fltro por localizacion
function ObtenerLocalizaciones() {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/Localizaciones",
        async: false,
        cache: false,
        success: function (data) {
            //variable global localizaciones
            localizaciones = data.d;
        }
    });   //fin ajax
    //obtenemos el select arbol
    if (localizaciones != null) {

        ArbolSelectLocalizaciones(1);
    }
} //fin ObtenerLocalizaciones
function ArbolSelectLocalizaciones(padre) {
    var IDLoc = null;
    var NomLoc = null;
    $.each(localizaciones, function (i, item) {
        if (item.LOCA_Origen == padre) {
            var tab = "";
            for (i = 1; i <= numCharTab; i++)
                tab = tab + " " + charTab;
            $("#Localizacion").append("<option value='" + item.LOCA_Interno + "'>" + tab + item.LOCA_Nombre + "</option>");
            numCharTab++;
            ArbolSelectLocalizaciones(item.LOCA_Interno);
            numCharTab--;
        }
    });

}
function GetPeriodo() {
    $.ajax({

        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/Periodo",
        async: false,
        cache: false,
        success: function (data) {
            item = data.d;
            if (item != null) {
                $("input[name='periodo'][value='" + item.PPRO_Periodo + "']").attr("checked", "checked");
                $("#DiaSemana").val(item.PPRO_DiaSemana);
                $("#DiaMes").val(item.PPRO_DiaMes);
            } else {
                showError("El periodo no está correctamente configurado en la base de datos");
            }
        } //fin success
    });     //fin ajax
}
function EditarPeriodo() {
    var res = 0;
    var dato = new Object();
    dato.PPRO_Periodo = $("input[name='periodo']:checked").val();
    dato.PPRO_DiaSemana = $("#DiaSemana").val();
    dato.PPRO_DiaMes = $("#DiaMes").val();
    showInfoPopupCargando(img);
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/EditarPeriodo",
        async: false,
        cache: false,
        success: function (data) {
            res = parseInt(data.d, 10);
            if (res <= 0) {
                showErrorPopup("Error al guardar los cambios");
            } else {
                showInfoPopupOK("Se guardaron los cambios correctamente");
            }
        } //fin success
    });        //fin ajax
    return res;
}
function SetTituloTabla() {
    var res = 0;
    var dato = new Object();
    dato.PPRO_Periodo = $("input[name='periodo']:checked").val();
    dato.PPRO_DiaSemana = $("#DiaSemana").val();
    dato.PPRO_DiaMes = $("#DiaMes").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "EjecucionActividadesRutinarias.aspx/ObtenerTituloTabla",
        async: false,
        cache: false,
        success: function (data) {
            $("#TituloTabla").html(data.d);
        } //fin success
    });         //fin ajax
    return res;
}
//cuando se edita la fecha siguiente
function FechaModificada() {
    $(".boxFecha").each(function (i) {
        $(this).change(function () {
            $(this).addClass("boxModificado");
            $(this).addClass("fechaNextEdit");
        });
    });
}
function implodeActividadesParaEjecutar(x) {
    var r;
    if (x.length != 0) {
        r = x.eq(0).val() + ";" + x.eq(0).parent().children("input.hiddenAR").val() +";" + $(".fechaEjecucion[name='" + x.eq(0).val() + "']").val();
        x.each(function (i) {
            if (i != 0) r += "|" + x.eq(i).val() + ";" + x.eq(i).parent().children("input.hiddenAR").val() + ";" + $(".fechaEjecucion[name='" + x.eq(i).val() + "']").val();
        });
    }
    else r = "";
    return (r);
}
function paginarLista(pagina1, total_paginas1) {
    nro_pags = total_paginas1;
    nro_pags = parseInt(nro_pags);
    nro_actual = pagina1;
    nro_pags = parseInt(nro_pags);
    $("#pager").pager({ pagenumber: nro_actual, pagecount: nro_pags, buttonClickCallback: PageClick });
}
function PageClick(pageclickednumber) {
    nro_pags = total_paginas;
    nro_actual = pageclickednumber;
    nro_pags = parseInt(nro_pags);
    $("#pager").pager({ pagenumber: pageclickednumber, pagecount: nro_pags, buttonClickCallback: PageClick });
    pagina = nro_actual;
    mostrarLista(nro_actual);
} //fin de la funcion pageClick