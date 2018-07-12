/// <reference path="jquery-2.0.0-vsdoc.js" />
var total_paginas = 0;
var pagina = 1;
var imgActivo = '<img src="img/icono-16-activado.png" title="Activado"/>';
var imgDesactivado = '<img src="img/icono-16-desactivado.png" title="Desactivado"/>';
$(document).ready(function () {
    calcularTotalPaginas();
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

        var IDs = implodeobj($("#listGrupos .checkboxID:checked"));
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
    }); //fin clic eleimanr
    $("#activar").click(function (e) {
        e.preventDefault();

        var IDs = implodeobj($("#listGrupos .checkboxID:checked"));
        if (IDs == "") {
            showError("No se ha seleccionado ningún registro");
            return false;
        } else {
            limpiarMsjBox();
            var res = ActivarVariosGrupos(IDs);
            if (res > 0) {
                calcularTotalPaginas();
                mostrarLista(pagina);
            }
        }
    }); //fin clic activar
    $("#desactivar").click(function (e) {
        e.preventDefault();

        var IDs = implodeobj($("#listGrupos .checkboxID:checked"));
        if (IDs == "") {
            showError("No se ha seleccionado ningún registro");
            return false;
        } else {
            limpiarMsjBox();
            var res = DesactivarVariosGrupos(IDs);
            if (res > 0) {
                calcularTotalPaginas();
                mostrarLista(pagina);
            }
        }
    }); //fin clic activar
});    //fin de $(document).ready
//funciones para la gestion de Grupos
function mostrarLista(nro_actual) {
    pagina = nro_actual;
    var dato = new Object();
    dato.Pagina = nro_actual;
    dato.TamanioPagina = $("#TamanioPagina").val();

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionGrupos.aspx/listaGrupos",
        async: false,
        cache: false,
        success: function (data) {

            var Grupos = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (Grupos != null) {
                $.each(Grupos, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.GRUP_Interno;

                    html += '<tr ' + css + '>';
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>';
                    html += '<td class="izq"><a href="GrupoUsuario.aspx?opc=editar&id=' + encodeURIComponent(item.GRUP_ID) + '" >' + item.GRUP_Nombre + '</a></td>';
                    html += '<td>' + item.GRUP_Descripcion + '</td>';
                    var Activo = imgDesactivado;
                    if (item.GRUP_Activo == true)
                        Activo = imgActivo;
                    html += '<td>' + Activo + '</td>';
                    html += '<td>' + item.UsuarioCreador + '</td>';
                    if (item.AUDI_FechaCrea != null)
                        html += '<td>' + ConvertirDateTimeAString(item.AUDI_FechaCrea, "dd/mm/yyyy") + '</td>';
                    else
                        html += '<td>' + item.AUDI_FechaCrea + '</td>';
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="9" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.' +
                    ' Elija otra página</td></tr>';
                    paginarGrupos(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                    paginarGrupos(0, 0);
                }
            } else {

                paginarGrupos(pagina, total_paginas);
            }
            $("#listGrupos tbody").html(html);
            $("#num").html(num);
            trHover();
        } //fin de succes
    });                //fin de ajax
} //fin funcion mostrarLista
//funcion para lista filtrada
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
        url: "GestionGrupos.aspx/listaGruposFiltradoPorString",
        async: false,
        cache: false,
        success: function (data) {
            var Grupos = data.d;
            var html = '';
            var num = 0;
            $("#allchecked").attr("checked", false);
            if (Grupos != null) {
                $.each(Grupos, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.GRUP_Interno;

                    html += '<tr ' + css + '>';
                    html += '<td><input type="checkbox" value="' + id + '" class="checkboxID"/></td>';
                    html += '<td class="izq"><a href="GrupoUsuario.aspx?opc=editar&id=' + encodeURIComponent(item.GRUP_ID) + '" >' + item.GRUP_Nombre + '</a></td>';
                    html += '<td>' + item.GRUP_Descripcion + '</td>';
                    var Activo = imgDesactivado;
                    if (item.GRUP_Activo == true)
                        Activo = imgActivo;
                    html += '<td>' + Activo + '</td>';
                    html += '<td>' + item.UsuarioCreador + '</td>';
                    if (item.AUDI_FechaCrea != null)
                        html += '<td>' + ConvertirDateTimeAString(item.AUDI_FechaCrea, "dd/mm/yyyy") + '</td>';
                    else
                        html += '<td>' + item.AUDI_FechaCrea + '</td>';
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {

                if (total_paginas > 0) {
                    html = '<tr><td colspan="9" align="center">No hay registros en la página ' + pagina +
                    '. <b>Razón:</b> Fueron eliminados los registros de esta página o se ha cambiado el número de registros por página.' +
                    ' Elija otra página</td></tr>';
                    paginarGrupos(pagina, total_paginas);
                } else {
                    html = '<tr><td colspan="9" align="center">No se encontraron registros..</td></tr>';
                    paginarGrupos(0, 0);
                }
            } else {

                paginarGrupos(pagina, total_paginas);
            }
            $("#listGrupos tbody").html(html);
            $("#num").html(num);
            trHover();
        } //fin de succes
    });  //fin de ajax
} //fin funcion mostrarListaFiltrada
function EliminarMultiplesRegistros(IDs) {
    var res = 0;
    var dato = new Object();
    dato.IDs = IDs;

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionGrupos.aspx/EliminarVariosGrupos",
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
function ActivarVariosGrupos(IDs) {
    var res = 0;
    var dato = new Object();
    dato.IDs = IDs;

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionGrupos.aspx/ActivarVariosGrupos",
        async: false,
        cache: false,
        success: function (data) {

            res = parseInt(data.d, 10);
            if (res > 0) {
                showMensaje("Se activaron " + res + " grupo(s)");
            } else {
                showError("No se activó ningún grupo");
            }
        } //fin success
    });      //fin ajax
    return res;
}
function DesactivarVariosGrupos(IDs) {
    var res = 0;
    var dato = new Object();
    dato.IDs = IDs;

    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GestionGrupos.aspx/DesactivarVariosGrupos",
        async: false,
        cache: false,
        success: function (data) {

            res = parseInt(data.d, 10);
            if (res > 0) {
                showMensaje("Se bloquearon " + res + " grupo(s)");
            } else {
                showError("No se bloqueó ningún grupo");
            }
        } //fin success
    });      //fin ajax
    return res;
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
        url: "GestionGrupos.aspx/TotalGrupos",
        async: false,
        cache: false,
        success: function (data) {
            total = parseInt(data.d, 10);
        } //fin success
    });  //fin ajax
    return total;
}
function paginarGrupos(pagina1, total_paginas1) {
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