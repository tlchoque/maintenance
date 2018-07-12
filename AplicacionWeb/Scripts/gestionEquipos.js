/// <reference path="jquery-2.0.0-vsdoc.js" />
var img = '<img src="img/ajax-loader.gif" />';
var imgok = '<img src="img/ajax_ok.png" />';
var error;
var ordenar = "";
var total_paginas = 0;
var pagina = 1;
var campo = "";
var orden = "";
var estadoEquipo = new Array();
var tiposEquipo = new Array();
estadoEquipo["R"] = "RESERVA"; estadoEquipo["S"] = "SERVICIO"; estadoEquipo["F"] = "FUERA DE SERVICIO";
//var TamanioPagina = 0;
$(document).ready(function (e) {

    //    $(document).tooltip({
    //        track: true
    //    });
    // $("#listEquipos th span").tooltip({ disabled: true });
    calcularTotalPaginas();
    TiposEquipo();
    mostrarLista(1);

    $("#TamanioPagina").change(function () {
        calcularTotalPaginas();
        mostrarLista(pagina);
    });
    $("#frmfiltro").submit(function (e) {
        e.preventDefault();
        if ($("#stringFiltro").val().length >= 2) {//2 caracteres minimo
            mostrarListaFiltrada(1);
        }
        if ($("#stringFiltro").val() == "") {
            calcularTotalPaginas();
            limpiarMsjBox();
            mostrarLista(1);
        }
    });
    $("#restablecer").click(function (e) {//
        
        limpiarBoxFrm("#frmfiltro :input");
        $("#TamanioPagina").val(20);
        calcularTotalPaginas();
        limpiarMsjBox();
        mostrarLista(1);
    });
    $("#eliminar").click(function (e) {
        e.preventDefault();

        var IDs = implodeobj($("#listEquipos .checkboxID:checked"));
        if (IDs == "") {
            showError("No se ha seleccionado ningún registro");
            return false;
        } else {
            limpiarMsjBox();
            var res = EliminarMultiplesRegistros(IDs);
            if (res > 0) {
                calcularTotalPaginas();
                mostrarLista(pagina);
            }
        }
    });
    $("#asociar").click(function (e) {
        e.preventDefault();
        var IDs = implodeobj($("#listEquipos .checkboxID:checked"));
        if (IDs == "") {
            showError("No se ha seleccionado ningún registro");
            return false;
        } else {
            window.location = "AsociarPlan.aspx?opc=equ&id=" + IDs;
        }
    });
    // ordenar por
    //    $("#listEquipos th span").click(function () {
    //        if ($(this).hasClass("desc")) {
    //            $("#listEquipos th span").removeClass("desc").removeClass("asc");
    //            $(this).addClass("asc");
    //            // ordenar = "&orderby=" + $(this).attr("rel") + " asc";
    //            campo = $(this).attr("rel");
    //            orden = "asc";
    //        } else {
    //            $("#listEquipos th span").removeClass("desc").removeClass("asc")
    //            $(this).addClass("desc");
    //            //ordenar = "&orderby=" + $(this).attr("rel") + " desc";
    //            campo = $(this).attr("rel");
    //            orden = "desc";
    //        }
    //        mostrarLista(1);
    //    });

});                        //fin document ready
function mostrarLista(nro_actual) {

    pagina = nro_actual;
    var dato = new Object();
    
    dato.Pagina = nro_actual;
    dato.OrdenarPor = campo;
    dato.Orden = orden;
    dato.TamanioPagina = $("#TamanioPagina").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionEquipos.aspx/listaEquipos",
        async: false,
        cache: false,
        success: function (data) {
            equipos = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
          
            if (equipos!=null) {

                $.each(equipos, function (i, item) {
                    // for (item in equipos) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.EQUI_Interno;
                    
                    html += '<tr ' + css + '>'
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>'
                    html += '<td class="izq"><a href="Equipo.aspx?opc=editar&id=' + encodeURIComponent(item.LOCA_ID) + '" >' + item.EQUI_Nombre + '</a></td>'
                    html += '<td>' + item.EQUI_Marca + '</td>'
                    html += '<td>' + item.EQUI_Modelo + '</td>'
                    html += '<td>' + item.EQUI_Serie + '</td>'
                    html += '<td>' + item.EQUI_Codigo + '</td>'
//                    html += '<td>' + item.EQUI_AnioFabricacion + '</td>'
//                    html += '<td>' + item.EQUI_AnioServicio + '</td>'
                    html += '<td>' + tiposEquipo[item.TIPO_Interno] + '</td>'
                    html += '<td>' + estadoEquipo[item.EQUI_Estado] + '</td>'
                    html += '<td>' + item.LOCA_NombreExtendido + '</td>'
                    html += '</tr>';
                    //pagina = item.pagina; total_paginas = item.total_paginas;
                    i++;
                    //}
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="9" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.'+
                    ' Elija otra página</td></tr>';
                    //mostrarLista(pagina-1)
                    paginarEquipos(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                    paginarEquipos(0, 0);
                }
            } else {

                paginarEquipos(pagina, total_paginas);
            }
            $("#listEquipos tbody").html(html);
            $("#num").html(num);
            trHover();
            //eventosLuegoDeCargar();
        } //fin de succes
    });       //fin de ajax
} //fin mostrar lista
function mostrarListaFiltrada(nro_actual) {
    pagina = nro_actual;
    var dato = new Object();
    dato.TamanioPagina = $("#TamanioPagina").val();
    dato.Pagina = nro_actual;
    dato.StringFiltro = $("#stringFiltro").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionEquipos.aspx/ListaFiltradaPorString",
        async: false,
        cache: false,
        success: function (data) {
            var equipos = data.d;

            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (equipos != null) {

                $.each(equipos, function (i, item) {
                    // for (item in equipos) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.EQUI_Interno;
                    
                    html += '<tr ' + css + '>';
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>';
                    html += '<td class="izq"><a href="Equipo.aspx?opc=editar&id=' + encodeURIComponent(item.LOCA_ID) + '" >' + item.EQUI_Nombre + '</a></td>';
                    html += '<td>' + item.EQUI_Marca + '</td>';
                    html += '<td>' + item.EQUI_Modelo + '</td>';
                    html += '<td>' + item.EQUI_Serie + '</td>';
                    html += '<td>' + item.EQUI_Codigo + '</td>';
                    html += '<td>' + tiposEquipo[item.TIPO_Interno] + '</td>';
                    html += '<td>' + estadoEquipo[item.EQUI_Estado] + '</td>';
                    html += '<td>' + item.LOCA_NombreExtendido + '</td>';
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                paginarEquipos(0, 0);

            } else {

                paginarEquipos(pagina, 1);
            }
            $("#listEquipos tbody").html(html);
            $("#num").html(num);
            $("#total").html(num);
            trHover();
        } //fin de succes
    });
} //fin mostrarListaFiltrada
function TiposEquipo() {
    
    $.ajax({
        
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionEquipos.aspx/TiposEquipo",
        async: false,
        cache: false,
        success: function (data) {
            var tipos;

            tipos = data.d;
            $.each(tipos, function (i, item) {
                
                tiposEquipo[item.TIPO_Interno] = item.TIPO_Nombre;
            });
        } //fin success
    });        //fin ajax
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
        url: "GestionEquipos.aspx/EliminarMultiplesRegistrosEquipo",
        async: false,
        cache: false,
        success: function (data) {

            res = parseInt(data.d, 10);
            if (res > 0) {
                showMensaje("Se eliminaron " + res + " registros");
            } else {
                showError("No se eliminó ningun registro");
            }
        } //fin success
    });      //fin ajax
    return res;
}
function ObtenerTotalRegistros() {
    var total = 0;
    $.ajax({
        
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionEquipos.aspx/TotalEquipos",
        async: false,
        cache: false,
        success: function (data) {

            total = parseInt(data.d, 10);
        } //fin success
    });  //fin ajax
    return total;
}
//function eventosLuegoDeCargar() {
//    $("#listEquipos tr td a.activo").each(function (i) {
//        $(this).click(function (e) {
//            var id = $(this).attr("rel");
//            var accion = $(this).attr("href").replace(/^.*#/, '');
//            $.post("sql/activarDesactivarPersonal.php", { id: id, accion: accion }, function (data) {
//                if (data == "ok") {
//                    mostrarLista(pagina);
//                } else {
//                    showError(data);
//                }
//            }); //fin de post
//        });
//    });
//    $("#allchecked").removeAttr('checked');
//    $("#allchecked").click(function () {
//        if ($(this).is(":checked")) {
//            $("#listEquipos tbody input[type='checkbox']").attr('checked', true);
//        } else {
//            $("#listEquipos tbody input[type='checkbox']").removeAttr('checked');
//        }
//    });
//}
function calcularTotalPaginas() {
    var total = parseInt(ObtenerTotalRegistros());

    total_paginas = Math.ceil(total / parseInt($("#TamanioPagina").val(), 10));
    $("#total").html(total);

}
function paginarEquipos(pagina1, total_paginas1) {
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
    $("#pager").pager({ pagenumber: pageclickednumber, pagecount: nro_pags,buttonClickCallback: PageClick });
    pagina = nro_actual;
    mostrarLista(nro_actual);
} //fin de la funcion pageClick
//$(document).ready(function (e) {
//    $("#btnInsert").click(function (e) {
//        e.preventDefault(); //necesario para desabilitar el comportamiento del boton asp
//        var dato = new Object();
//        dato.marca = $("#txtMarca").val();
//        dato.precio = $("#txtPrecio").val();
//        /*var jsonText = JSON.stringify(dato);
//        alert(jsonText);*/
//        /*$.ajax({
//            type: 'POST',
//            url: 'WebServices/WSAuto.asmx/InsertarAuto',
//            data: JSON.stringify(dato),
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'json',
//            async: false,
//            cache: false,
//            success: function (msg) {

//                $('#mensaje').html(msg.d);
//            }
//        });*/

//        $.ajax({
//            type: 'POST',
//            url: 'Default.aspx/InsertarAuto',
//            data: JSON.stringify(dato),
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'json',
//            async: false,
//            cache: false,
//            success: function (msg) {

//                $('#mensaje').html(msg.d);
//            }
//        });

//        /* $.post("Request/RequestAuto.aspx", { marca: marca }, function (data) {
//        $('#mensaje').html(data);
//        });*/
//    });

//});
