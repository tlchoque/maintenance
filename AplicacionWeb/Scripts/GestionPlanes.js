var img = '<img src="img/ajax-loader.gif" />';
var imgok = '<img src="img/ajax_ok.png" />';
var error;
var tipoRegimen = new Array();
tipoRegimen["F"] = "Fechas"; tipoRegimen["L"] = "Lecturas";
$(document).ready(function (e) {
    $div = $("#divUnidadLecturas");
    $divVentana = $("#divPlan");
    //mostrarLista();
    calcularTotalPaginas();

    mostrarListaP(1);
    $div.hide();
    DesmarcarCamposVaciosLLenados();


    $("#TamanioPagina").change(function () {
        calcularTotalPaginas();
        mostrarListaP(pagina);
    });
    $("#frmfiltro").submit(function (e) {
        e.preventDefault();
        //alert("hi");
        if ($("#stringFiltro").val().length >= 2) {//2 caracteres minimo
            mostrarListaFiltrada(1);
        }
        if ($("#stringFiltro").val() == "") {
            calcularTotalPaginas();
            limpiarMsjBox();
            mostrarListaP(1);
        }
    });

    $("#restablecer").click(function (e) {//

        limpiarBoxFrm("#frmfiltro :input");
        $("#TamanioPagina").val(20);
        calcularTotalPaginas();
        limpiarMsjBox();
        mostrarListaP(1);
    });


    $("#nuevo").live("click", function (e) {
        e.preventDefault();
        $("#Interno").val("");
        ReiniciarFormulario();
        $div.hide();
        showForm();
    })

    $("#editar").live("click", function (e) {
        e.preventDefault();
        var contarchecked = $("#listaPlanes input[type=checkbox]:checked").length;
        if (contarchecked == 1) {
            var id = $('#listaPlanes input[type=checkbox]:checked').val();
            window.location = "plan.aspx?opc=editar&id=" + id;
        }
        else
            showError("Debe seleccionar un registro");
    })

    $("#copiar").live("click", function (e) {
        e.preventDefault();
        var contarchecked = $("#listaPlanes input[type=checkbox]:checked").length;
        if (contarchecked == 1) {
            var id = $('#listaPlanes input[type=checkbox]:checked').val();
            $("#InternoCopia").val(id);
            $("#divCopia").showForm();
        }
        else
            showError("Debe seleccionar solo un registro");
    })

    $("#btnAgregar").live("click", function (e) {
        if (CamposRequeridosVacios("#txtDescripcion,#lstRegimen")) {
            $("#divMensaje").html('<div class="error">Complete los campos<div>');
            return false;
        }
        GuardarPlan();
    })

    $("#btnCopiar").live("click", function (e) {
        if (CamposRequeridosVacios("#txtNombreCopia")) {
            $("#divMensaje").html('<div class="error">Complete los campos<div>');
            return false;
        }
        CopiarPlan();
    })

    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            hideForm();
            hideConfirm();
        }
    });

    $("#lstRegimen").change(function () {
        if ($(this).val() == "L") $div.show();
        else {
            $div.hide();
            $("#txtUnidadLecturas").val("");
        }
    })

    $("#eliminar").click(function (e) {
        e.preventDefault();
        var IDs = implodeobj($("#listaPlanes .checkboxID:checked"));
        if (IDs == "") {
            showError("No se ha seleccionado ningún registro");
            return false;
        } else {
            limpiarMsjBox();
            var res = EliminarMultiplesRegistros(IDs);
            if (res > 0) {
                //mostrarLista();
                calcularTotalPaginas();
                mostrarListaP(pagina);
            }
        }
    });

    $(".closeForm").click(function (e) {
        hideForm();
        hideConfirm();
    })

    $(".closeConfirm").click(function (e) {
        hideConfirm();
    })
});

function EventosVentana(id) {
    showForm();
    CargarPlanFormulario(id);
}

function mostrarListaP(nro_actual) {
    pagina = nro_actual;
    var dato = new Object();
    dato.Pagina = nro_actual;
    dato.TamanioPagina = $("#TamanioPagina").val();

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/listaPlanesP",
        async: false,
        cache: false,
        success: function (data) {
            var Planes = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (Planes != null) {
                $.each(Planes, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    if (!item.PLAN_UnidadLecturas) item.PLAN_UnidadLecturas = "";
                    var id = item.PLAN_Interno;
                    html += '<tr ' + css + '>'
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>'
                    html += '<td class="izq"><a href="javascript:void(0)" onclick="EventosVentana(' + id + ')" id="' + id + '">' + item.PLAN_Descripcion + '</a></td>'
                    html += '<td>' + tipoRegimen[item.PLAN_Regimen] + '</td>'
                    html += '<td>' + item.PLAN_UnidadLecturas + '</td>'
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="4" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.' +
                    ' Elija otra página</td></tr>';
                    paginarPlanes(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                    paginarPlanes(0, 0);
                }
            } else {

                paginarPlanes(pagina, total_paginas);
            }
            $("#listaPlanes tbody").html(html);
            $("#num").html(num);
            trHover();
        } //fin de succes
    });                //fin de ajax
}


function mostrarListaFiltrada(nro_actual) {
    pagina = nro_actual;
    var dato = new Object();
    dato.Pagina = nro_actual;
    dato.TamanioPagina = $("#TamanioPagina").val();
    dato.stringFiltro = $("#stringFiltro").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/listaPlanesFiltradoPorString",
        async: false,
        cache: false,
        success: function (data) {
            var Planes = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (Planes != null) {
                $.each(Planes, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    if (!item.PLAN_UnidadLecturas) item.PLAN_UnidadLecturas = "";
                    var id = item.PLAN_Interno;
                    html += '<tr ' + css + '>'
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>'
                    html += '<td class="izq"><a href="javascript:void(0)" onclick="EventosVentana(' + id + ')" id="' + id + '">' + item.PLAN_Descripcion + '</a></td>'
                    html += '<td>' + tipoRegimen[item.PLAN_Regimen] + '</td>'
                    html += '<td>' + item.PLAN_UnidadLecturas + '</td>'
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="4" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.' +
                    ' Elija otra página</td></tr>';
                    paginarPlanes(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                    paginarPlanes(0, 0);
                }
            } else {

                paginarPlanes(pagina, total_paginas);
            }
            $("#listaPlanes tbody").html(html);
            $("#num").html(num);
            trHover();
        } //fin de succes
    });
} //fin funcion mostrarListaFiltrada
function mostrarLista() {
    $.ajax({
        data: "",
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/listaPlanes",
        async: false,
        cache: false,
        success: function (data) {
            planes = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (planes.length > 0) {
                $.each(planes, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    if (!item.PLAN_UnidadLecturas) item.PLAN_UnidadLecturas = "";
                    var id = item.PLAN_Interno;
                    i = 0;
                    html += '<tr ' + css + '>'
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>'
                    html += '<td class="izq"><a href="javascript:void(0)" onclick="EventosVentana(' + id + ')" id="' + id + '">' + item.PLAN_Descripcion + '</a></td>'
                    html += '<td>' + tipoRegimen[item.PLAN_Regimen] + '</td>'
                    html += '<td>' + item.PLAN_UnidadLecturas + '</td>'
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {
                html = '<tr><td colspan="4" align="center">No se encontraron registros..</td></tr>';
            }
            $("#listaPlanes tbody").html(html);
            trHover();
        } 
    });    
} 

function GuardarPlan() {
    var plan = new Object();
    plan.PLAN_Interno = $("#Interno").val();
    plan.PLAN_Descripcion = $("#txtDescripcion").val();
    plan.PLAN_Regimen = $("#lstRegimen").val();
    if ($("#txtUnidadLecturas").val() == "")
        plan.PLAN_UnidadLecturas = null;
    else
        plan.PLAN_UnidadLecturas = $("#txtUnidadLecturas").val();

    $.ajax({
        data: JSON.stringify(plan),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/GuardarPlan",
        async: false,
        cache: false,
        success: function (data) {
            var ID = parseInt(data.d);  
            if (ID > 0) {
                $("#divMensaje").html('<div class="ok"> ' + img + ' actualizando lista...<div>');
                hideForm();
                //mostrarLista();
                calcularTotalPaginas();
                mostrarListaP(pagina);
                ReiniciarFormulario();
            }
            else {
                $("#divMensaje").html('<div class="error">Ocurrió un error: ' + data.Message + '<div>');
                hideForm();
            }
        }
    });             //fin de ajax
}

function CargarPlanFormulario(id) {
    ReiniciarFormulario();
    var colIndex = 0;
    var $tr = $("#" + id).parent().parent();
    $tr.find('td').each(function () {
        if (colIndex == 1) {
            $("#txtDescripcion").val($(this).text());
        }
        else if (colIndex == 2) {
            if ($(this).text() == "Fechas") {
                $("#lstRegimen").val("F");
                $div.hide();
            }
            else {
                $("#lstRegimen").val("L");
                $div.show();
            }
        }
        else if (colIndex == 3) {
            $("#txtUnidadLecturas").val($(this).text());
        }
        colIndex++; 
    })
    $("#btnAgregar").val("Actualizar");
    $("#Interno").val(id);
}

function ReiniciarFormulario() {
    $('.mcenter :input').each(function () {
        $(this).removeClass("bgerror");
        var type = this.type;
        var tag = this.tagName.toLowerCase();
        if (type == 'text' || type == 'password' || tag == 'textarea')
            this.value = "";
        if (tag == 'select')
            this.selectedIndex = 0;
        if (type == 'checkbox') {
            this.checked = false;
            this.disabled = false;
        }
    });
    $div.hide();
    $("#btnAgregar").val("Agregar");
    $("#divMensaje").html("");
}

function EliminarMultiplesRegistros(IDs) {
    var res = 0;
    var dato = new Object();
    dato.IDs = IDs;

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/EliminarPlanes",
        async: false,
        cache: false,
        success: function (data) {
            res = parseInt(data.d, 10);
            if (res > 0) {
                showMensaje("Se eliminaron " + res + " registros");
            } else {
                showError("No se eliminó ningun registro");
            }
        } 
    });      
    return res;
}

function CopiarPlan() {
    var Plan = new Object();
    Plan.PLAN_Interno = $("#InternoCopia").val();    
    Plan.PLAN_Descripcion = $("#txtNombreCopia").val();
    $.ajax({
        data: JSON.stringify(Plan),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/CopiarPlan",
        async: false,
        cache: false,
        success: function (data) {
            res = parseInt(data.d);
            if (res > 0) {
                hideForm();
                //mostrarLista();
                calcularTotalPaginas();
                mostrarListaP(pagina);
                showMensaje("Se replicó el Plan");
            } else {
                showError("No se replicó el Plan");
            }
        } 
    }); 
}

//function ValidarPlan() {
//    var ok = true;
//    $("#txtDescripcion").each(function () {
//        if ($(this).val() == "") {
//            $(this).addClass("ierror3");
//            ok = false;
//        }
//        else {
//            $(this).removeClass("ierror3");
//        }
//    });

//    $("#lstRegimen").each(function () {
//        if ($(this).val() == 0) {
//            $(this).addClass("ierror3");
//            ok = false;
//        }
//        else {
//            $(this).removeClass("ierror3");
//        }
//    });
//    if (ok) return true;
//    else return false;
//}

function showForm() {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $divVentana.show();
}

jQuery.fn.showForm = function () {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $(this).show();
};	

function showConfirm() {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $divVentana.show();
}

function hideForm() {
    $(".mensaje").hide();
    $("#fondo").hide();
}

function hideConfirm() {
    $divVentana.hide();
    $("#fondo").hide();
}

function calcularTotalPaginas() {
    var total = parseInt(ObtenerTotalRegistros());
    total_paginas = Math.ceil(total / parseInt($("#TamanioPagina").val(), 10));
    $("#total").html(total);
}

function ObtenerTotalRegistros() {
    var total = 0;
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionPlanes.aspx/TotalPlanes",
        async: false,
        cache: false,
        success: function (data) {
            total = parseInt(data.d, 10);
        } //fin success
    });  //fin ajax
    return total;
}

function paginarPlanes(pagina1, total_paginas1) {
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
    mostrarListaP(nro_actual);
} //fin de la funcion pageClick