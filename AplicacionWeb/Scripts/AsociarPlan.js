
var tipoActividad = new Array();
tipoActividad["PV"] = "Preventivo"; tipoActividad["PD"] = "Predictivo";
var unidadFrecuencia = new Array();
unidadFrecuencia["M"] = "Meses"; unidadFrecuencia["S"] = "Semanas"; unidadFrecuencia["D"] = "Días";
$(function () {
    if ($("#opcURL").val() == "equi") {
        //$(".pagetitle h2").html("Datos de Equipo");
        MostrarEquipos();
    }

    $("#combobox").change(function () {
        //alert($("#combobox").val());
    })

    $("#asociar").click(function (e) {
        e.preventDefault();
        if (CamposRequeridosVacios("#combobox")) {
            showError("Los campos marcados son obligatorios");
            return false;
        }
        else {
            limpiarMsjBox();
            Asociar();
        }
    });

    $("#combobox").combobox({
        select: function (event, ui) {
            MostrarPlanCompleto(ui.item.value);
        }
    });

    $('body').keypress(function (event) {
        //prevenir recarga de pagina por combobox 
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            return false;
        }
    });
});
(function ($) {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
					.addClass("custom-combobox")
					.insertAfter(this.element);
            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },
        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
					value = selected.val() ? selected.text() : "";
            this.input = $('<input style="width:360px;">')
					.appendTo(this.wrapper)
					.val(value)
					.attr("title", "")
					.addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: $.proxy(this, "_source")
					})
					.tooltip({
					    tooltipClass: "ui-state-highlight"
					});

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                },
                autocompletechange: "_removeIfInvalid"
            });
        },
        _createShowAllButton: function () {
            var input = this.input,
					wasOpen = false;

            $("<a>")
					.attr("tabIndex", -1)
					.attr("title", "Mostrar Opciones")
					.tooltip()
					.appendTo(this.wrapper)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("custom-combobox-toggle ui-corner-right")
					.mousedown(function () {
					    wasOpen = input.autocomplete("widget").is(":visible");
					})
					.click(function () {
					    input.focus();

					    // Close if already visible
					    if (wasOpen) {
					        return;
					    }
					    // Pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					});
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },
        _removeIfInvalid: function (event, ui) {
            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }
            // Search for a match (case-insensitive)
            var value = this.input.val(),
					valueLowerCase = value.toLowerCase(),
					valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });
            // Found a match, nothing to do
            if (valid) {
                return;
            }
            // Remove invalid value
            this.input
					.val("")
					.attr("title", value + " no encontro ningún elemento")
					.tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.data("ui-autocomplete").term = "";
        },
        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);

function MostrarPlanCompleto(idPlan) {
    var i, j;
    var dato = new Object();
    dato.PLAN_Interno = idPlan;
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "AsociarPlan.aspx/ObtenerParteActividades",
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
                            //html += '<td>' + tipoActividad[subitem.ACRU_Tipo] + '</td>'
                            html += '<td>' + subitem.ACRU_ConCorte + '</td>'
                            html += '<td>' + subitem.ACRU_ConMedicion + '</td>'
                            html += '</tr>';
                        });
                    }
                });
            }
            if (html == '') {
                html = '<tr><td colspan="6" align="center">No hay actividades en el plan</td></tr>';
            }
            $("#TablaPlan tbody").html(html);
            trHover();
        }
    });
}

function MostrarEquipos() {
    $.ajax({
        data: "",
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "AsociarPlan.aspx/ObtenerEquipos",
        async: false,
        cache: false,
        success: function (data) {
            equipos = data.d;
            var html = '';
            if (equipos.length > 0) {
                html+='<thead><tr><th width="60%"><span>Descripcion</span></th><th width="40%"><span>Localizacion</span></th></tr></thead>'
                html+='<tbody></tbody>'
                $.each(equipos, function (i, item) {
                    var css = "";
                    if ((i + 1) % 2 == 0) css = 'class="row1"';
                    else css = 'class="row0"';
                    var id = item.EQUI_Interno;
                    i = 0;
                    html += '<tr ' + css + '>'
                    html += '<td class="izq">' + item.EQUI_Descripcion + '</td>'
                    html += '<td class="izq"> ' + item.LOCA_NombreExtendido + '</td>'
                    html += '</tr>';
                    i++;
                });
            }
            if (html == '') {
                html = '<tr><td colspan="4" align="center">No se encontraron registros..</td></tr>';
            }
            $("#TablaItems").html(html);
            trHover();
        }
    });
}

function Asociar() {
    var dato = new Object();
    dato.PLAN_Interno = $("#combobox").val();
    $.ajax({
        data: JSON.stringify(dato),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "AsociarPlan.aspx/InsertarMantenimientoInicial",
        async: false,
        cache: false,
        success: function (data) {
            if (data.d > 0) {
                showMensaje("Se asociaron los items al Plan");
            } else {
                showError("No se asociaron los items al Plan");
            }
        }
    })
}