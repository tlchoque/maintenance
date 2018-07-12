var img = '<img src="img/ajax-loader.gif" />';
var imgok = '<img src="img/ajax_ok.png" />';
var error;
var tipoActividad = new Array();
tipoActividad["PV"] = "Preventivo"; tipoActividad["PD"] = "Predictivo";
var unidadFrecuencia = new Array();
unidadFrecuencia["M"] = "Meses"; unidadFrecuencia["S"] = "Semanas"; unidadFrecuencia["D"] = "Días";
var $tree = null;
var PLAN_Interno = null;
var $divMedicion = null;
var $idConfirmacion = null;
$(document).ready(function (e) {
    $divMedicion = $("#divMedicion");
    $divMedicion.hide();
    $tree = $("#Arbol");
    PLAN_Interno = $("#PLAN_Interno").val();
    $idConfirmacion = $("#idConfirmacion");
    var ParteInterno = $("#PART_Interno").val();
    DesmarcarCamposVaciosLLenados();
    CamposNumericos("#txtFrecuencia"); 
    MostrarPlanCompleto();

    $("#btnNuevo").click(function (e) {
        e.preventDefault();
        $("#ACRU_Interno").val("");
        limpiarMsjBox();
        ReiniciarFormulario();
        showForm();
    });

    $("#btnGuardar").click(function (e) {
        if (CamposRequeridosVacios("#Actividad,#lstTipo,#txtFrecuencia,#lstUnidadFrecuencia")) {
            showError("Los campos marcados son obligatorios");
            return false;
        }
        var n = $tree.jstree('get_selected').attr('id');
        GuardarActividad(n);
    })

    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            hideForm();
            hideConfirmacion();
        }
    });

    $("#chkConMedicion").click(function (e) {
        if ($(this).is(':checked')) {
            $divMedicion.show();
        }
        else {
            $("#chkConMedicion").val("");
            $("#txtUnidadMedicion").val("");
            $divMedicion.hide();
        }
    })

    $("#btnDelRegistro").click(function () {
        $("#load2").html('<div class="ok"> ' + img + ' eliminando familiar...<div>');
        EliminarActividad();
    })


    $(".closeForm").click(function (e) {
        hideForm();
    })

    $(".closeConfirm").click(function (e) {
        hideConfirmacion();
    })

    $('#container').layout({
        defaults: {
            fxName: "slide"
			       , fxSpeed: "slow"
			       , spacing_closed: 14
			       , initClosed: false
        }
			    , center: {
			        fxName: "none"
			       , spacing_closed: 1
			    }
			    , south: {
			        fxName: "none"
	               , size: 275
			       , spacing_closed: 8
			    }
			    , west: {
			        fxName: "none"
			       , size: 370
			       , minSize: 280
			       , spacing_closed: 8
			    }
    });

    $tree.bind("loaded.jstree", function (event, data) {
        $tree.jstree("open_all");
    });
    $tree
                .jstree({
                    "plugins": [
			            "themes", "json_data", "ui", "crrm", "dnd", "search", "types", "hotkeys", "contextmenu"],
                    "json_data": {
                        "ajax": {
                            "type": "POST",
                            "dataType": "json",
                            "contentType": "application/json;",
                            "url": "Plan.aspx/ObtenerNodoPrincipal",
                            "data": function () {
                                return '{"id" : ' + PLAN_Interno + ' }';
                            },
                            "success": function (tree) {
                                return tree.d;
                            }
                        }
                    },
                    "types": {
                        'types': {
                            'default': {
                                'icon': {
                                    'image': '/scripts/themes/icon1.png'
                                },
                                'valid_children': 'default'
                            }
                        }
                    },
                    "ui": {
                        "initially_select": [ParteInterno]
                    },
                    core: { "animation": 0 },
                    contextmenu: {
                        items: function ($node) {
                            return {
                                createItem: {
                                    "label": "Agregar Parte",
                                    "action": function (obj) { this.create(obj); }
                                },
                                renameItem: {
                                    "label": "Cambiar Nombre",
                                    "action": function (obj) { this.rename(obj); }
                                },
                                deleteItem: {
                                    "label": "Eliminar Parte",
                                    "action": function (obj) { this.remove(obj); }
                                }
                            };
                        }
                    }
                })
                    .bind("create.jstree", function (e, data) {
                        var nodo = new Object();
                        nodo.idnodo = null;
                        nodo.name = data.rslt.name;
                        nodo.idnodopadre = parseInt(data.rslt.parent.attr("id"));
                        nodo.op = true;
                        $.ajax({
                            data: JSON.stringify(nodo),
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json;",
                            url: "Plan.aspx/InsertarNodo",
                            success: function (r) {
                                if (r.d != 0) {
                                    $(data.rslt.obj).attr("id", r.d);
                                }
                                else {
                                    $.jstree.rollback(data.rlbk);
                                }
                            }
                        })
                    })
                    .bind("rename.jstree", function (e, data) {
                        var nodo = new Object();
                        nodo.idnodo = parseInt(data.rslt.obj.attr("id"));
                        nodo.name = data.rslt.new_name;
                        nodo.idnodopadre = parseInt(data.rslt.obj.parent().parent().attr("id"));
                        nodo.op = false;
                        $.ajax({
                            data: JSON.stringify(nodo),
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json;",
                            url: "Plan.aspx/InsertarNodo",
                            success: function (r) {
                                if (r.d == 0) {
                                    $.jstree.rollback(data.rlbk);
                                }
                                else
                                    MostrarPlanCompleto();
                            }
                        })
                    })
                    .bind("remove.jstree", function (e, data) {
                        if (data.rslt.obj.attr("id") != ParteInterno) {
                            data.rslt.obj.each(function () {
                                var nodo = new Object();
                                nodo.id = parseInt(this.id);
                                //alert(id)
                                $.ajax({
                                    data: JSON.stringify(nodo),
                                    type: 'POST',
                                    dataType: "json",
                                    contentType: "application/json;",
                                    url: "Plan.aspx/EliminarNodosPorPadre",
                                    success: function (r) {
                                        if (r.d > 0) {
                                            MostrarPlanCompleto();
                                            //data.inst.refresh();
                                        }
                                    }
                                });
                            });
                        }
                        else
                            $.jstree.rollback(data.rlbk);
                    })
                    .bind("select_node.jstree", function (event, data) {
                        MostrarLista(data.rslt.obj.attr("id"));
                    })
    		        .delegate("a", "click", function (event, data) { event.preventDefault(); })
                    .bind("refresh.jstree", function (event, data) {
                        tree.jstree("open_all");
                    });
})

function MostrarLista(idParte) {
    var dato = new Object();
    var parte = new Object();
    parte.PART_Interno = parseInt(idParte);
    $.ajax({
        data: JSON.stringify(parte),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Plan.aspx/ObtenerActividadesPorParte",
        async: false,
        cache: false,
        success: function (data) {
            actividades = data.d;
            var html = '';
            var num = 0;
            if (actividades.length > 0) {
                $.each(actividades, function (i, item) {
                    var css = "";
                    num = num + 1;
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.ACRU_Interno;
                    i = 0;
                    if (item.ACRU_ConCorte == true) item.ACRU_ConCorte = "Si";
                    else item.ACRU_ConCorte = "No";
                    if (item.ACRU_ConMedicion == true) item.ACRU_ConMedicion = "Si";
                    else item.ACRU_ConMedicion = "No";
                    if (item.ACRU_UnidadMedicion==null) item.ACRU_UnidadMedicion = "";
                    html += '<tr ' + css + '>'
                    html += '<td class="izq"><a href="javascript:void(0)" rel="'+item.NOMB_Interno+'" onclick="EditarActividad(' + id + ')" id="' + id + '">' + item.NOMB_Descripcion + '</a></td>'
                    html += '<td>' + item.ACRU_Frecuencia + '</td>'
                    html += '<td>' + unidadFrecuencia[item.ACRU_UnidadFrecuencia] + '</td>'
                    html += '<td>' + item.ACRU_ConCorte + '</td>'
                    html += '<td>' + item.ACRU_ConMedicion + '</td>'
                    html += '<td>' + item.ACRU_UnidadMedicion + '</td>'
                    html += '<td><a href="javascript:void(0)" onclick="MostrarConfirmacion(' + id + ')">Eli</a></td>'
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {
                html = '<tr><td colspan="7" align="center">No se encontraron registros..</td></tr>';
            }
            $("#listaActividades tbody").html(html);
            trHover();
        } 
    });
}

function EditarActividad(id) {
    limpiarMsjBox();
    ReiniciarFormulario();
    CargarActividad(id);
    showForm();
}

function MostrarConfirmacion(id) {
    limpiarMsjBox();
    var colIndex = 0;
    var $tr = $("#" + id).parent().text();
    $("#load").html("");
    $("#eNombre").html($tr);
    $idConfirmacion.val(id);
    showConfirmacion();
}

function EliminarActividad() {
    var idParte = $tree.jstree('get_selected').attr('id');
    var actividad = new Object();
    actividad.id = parseInt($idConfirmacion.val());
    $.ajax({
        data: JSON.stringify(actividad),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Plan.aspx/EliminarActividad",
        async: false,
        cache: false,
        success: function (data) {
            $("#btnDelRegistro").removeAttr('disabled');
            if (data.d > 0) {
                $("#load").html('<div class="ok"> ' + img + ' actualizando lista...<div>');
                MostrarPlanCompleto();
                MostrarLista(idParte);
                hideConfirmacion();
            }
            else {
                $("#load").html('<div class="error">Ocurrió un error: ' + data.Message + '<div>');
            }
        }
    });  
}

function GuardarActividad(idParte) {    
    var actividad = new Object();
    var jConCorte, jConMedicion;
    actividad.ACRU_Interno = $("#ACRU_Interno").val();
    actividad.ACRU_Descripcion = "";
    actividad.ACRU_Tipo = $("#lstTipo").val();
    if ($("#chkConCorte").is(':checked')) jConCorte = true;
    else jConCorte = false;
    actividad.ACRU_ConCorte = jConCorte;
    if ($("#chkConMedicion").is(':checked')) jConMedicion = true;
    else jConMedicion = false;
    actividad.ACRU_ConMedicion = jConMedicion;
    if ($("#txtUnidadMedicion").val() == "")
        actividad.ACRU_UnidadMedicion = null; 
    else
        actividad.ACRU_UnidadMedicion = $("#txtUnidadMedicion").val();
    actividad.ACRU_Frecuencia = $("#txtFrecuencia").val();
    actividad.ACRU_UnidadFrecuencia = $("#lstUnidadFrecuencia").val();
    actividad.PART_Interno = parseInt(idParte);
    actividad.NOMB_Interno = $("#Actividad").val();
    $.ajax({
        data: JSON.stringify(actividad),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Plan.aspx/GuardarActvidad",
        async: false,
        cache: false,
        success: function (data) {
            $("#btnGuardar").removeAttr('disabled');
            if (data.d > 0) {
                showMensaje("Datos guardados correctamente");
                ReiniciarFormulario();
                MostrarLista(idParte);
                MostrarPlanCompleto();
                hideForm();                
            }
            else {
                showError("No se insertó la Actividad");
            }
        }
    });
}

function MostrarPlanCompleto() {
    var i, j;
    $.ajax({
        data: "",
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Plan.aspx/ObtenerParteActividades",
        async: false,
        cache: false,
        success: function (data) {
            plan = data.d;
            var html = '';
            var j = 0;
            if (plan.length > 0) {
                $.each(plan, function (i, item) {
                    if (item.PART_Actividades) {
                        var css = "";
                        if ((j + 1) % 2 == 0) css = 'class="row1"';
                        else css = 'class="row0"';
                        j++;
                        var rowspan = item.PART_Actividades.length + 1;
                        html += '<tr ' + css + '>'
                        html += '<td rowspan="' + rowspan + '" class="izq">' + item.PART_NombreExtendido + '</td>'
                        html += '</tr>';
                        $.each(item.PART_Actividades, function (j, subitem) {
                            if (subitem.ACRU_ConCorte == true) subitem.ACRU_ConCorte = "Si";
                            else subitem.ACRU_ConCorte = "No";
                            if (subitem.ACRU_ConMedicion == true) subitem.ACRU_ConMedicion = "Si";
                            else subitem.ACRU_ConMedicion = "No";
                            if (subitem.ACRU_UnidadMedicion == null) subitem.ACRU_UnidadMedicion = "";
                            html += '<tr ' + css + '>'
                            html += '<td class="izq">' + subitem.NOMB_Descripcion + '</td>'
                            html += '<td>' + subitem.ACRU_Frecuencia + '</td>'
                            html += '<td>' + unidadFrecuencia[subitem.ACRU_UnidadFrecuencia] + '</td>'
                            html += '<td>' + tipoActividad[subitem.ACRU_Tipo] + '</td>'
                            html += '<td>' + subitem.ACRU_ConCorte + '</td>'
                            html += '<td>' + subitem.ACRU_ConMedicion + '</td>'
                            html += '</tr>';
                        });
                    }
                });
            }
            if (html == '') {
                html = '<tr><td colspan="7" align="center">No hay actividades en el plan</td></tr>';
            }
            $("#listaParteActividades tbody").html(html);
            trHover();
        }
    });
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
    $divMedicion.hide();
    $("#btnGuardar").val("Agregar");
    $("#divMensaje").html("");
}

function CargarActividad(id) {
    var actividad = new Object();
    actividad.ACRU_Interno = id;
    $.ajax({
        data: JSON.stringify(actividad),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Plan.aspx/ObtenerActividadPorId",
        async: false,
        cache: false,
        success: function (data) {
            var item = data.d;
            $("#Actividad").val(item.NOMB_Interno);
            $("#lstTipo").val(item.ACRU_Tipo);
            $("#txtFrecuencia").val(item.ACRU_Frecuencia);
            $("#lstUnidadFrecuencia").val(item.ACRU_UnidadFrecuencia);
            $("#chkConCorte").prop('checked', item.ACRU_ConCorte);
            $("#chkConMedicion").prop('checked', item.ACRU_ConMedicion);
            $("#txtUnidadMedicion").val(item.ACRU_UnidadMedicion);
        }
    });  

    if ($("#chkConMedicion").is(':checked')) {
        $divMedicion.show();
    }
    else $divMedicion.hide();
    $("#btnGuardar").val("Actualizar");
    $("#ACRU_Interno").val(id);
}

function showForm() {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $("#divActividad").show();
}

function showConfirmacion() {
    $("#fondo").fadeTo(0, 0.6);
    $("#fondo").height($(document).height());
    $("#divConfirmacion").show();
}

function hideForm() {
    $("#divActividad").hide();
    $("#fondo").hide();
}

function hideConfirmacion() {
    $("#divConfirmacion").hide();
    $("#fondo").hide();
}