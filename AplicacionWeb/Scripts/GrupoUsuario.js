/// <reference path="jquery-2.0.0-vsdoc.js" />
$(document).ready(function (e) {
    CargarTareasPorModulo();
    //para el acordeon
    $('.acordeon dd').hide();
    $('.acordeon dt').first().addClass('activo');
    $('.acordeon dt dd').first().slideUp();
    $('.acordeon dt').first().next().slideDown();
    $('.acordeon dt').click(function () {
        if ($(this).hasClass('activo')) {
            $(this).removeClass('activo');
            $(this).next().slideUp();
        } else {
            $('.acordeon dt').removeClass('activo');
            $(this).addClass('activo');
            $('.acordeon dd').slideUp();
            $(this).next().slideDown();
        }
    });

    //FIN ACORDEON
    //eventos tareas
    $("#allTareas").click(function (e) {
        e.preventDefault();
        $("#Tareas :checkbox").attr('checked', true);
    });
    $("#noneTareas").click(function (e) {
        e.preventDefault();
        $("#Tareas :checkbox").attr('checked', false);
    });
    $(".selectCheck").click(function (e) {
        e.preventDefault();
        //alert($(this).parent('dd').html());
        var idboton = $(this).attr("id");
        $("#checkTareas" + idboton + " :checkbox").attr('checked', true);
    });

    $(".noneCheck").click(function (e) {
        e.preventDefault();
        //alert($(this).parent('dd').html());
        var idboton = $(this).parent().children(".selectCheck").attr("id");
        $("#checkTareas" + idboton + " :checkbox").attr('checked', false);
    });
    //
    if ($("#IDGrupo").val() != "" && $("#opcURL").val() == "editar") {
        $(".pagetitle h2").html("Datos de Grupo");
        $("#nuevo").parent("li").hide();
        $("#guardar_nuevo").parent("li").hide();
        ObtenerDatosDeGrupo();
        ObtenerTareasDeGrupo();
    }
    DesmarcarCamposVaciosLLenados();
    //    $("input[name=Bloquear]:radio").click(function (e) {
    //        e.preventDefault();
    //        alert($(this).val());
    //    });

    $("#nuevo").click(function (e) {
        e.preventDefault();
        $("#IDGrupo").val("");
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
            if (GuardarGrupo() > 0) {
                ObtenerDatosDeGrupo();
                ObtenerTareasDeGrupo();
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

            if (GuardarGrupo() > 0) {
                location.href = "GestionGrupos.aspx";
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

            if (GuardarGrupo() > 0) {
                limpiarTodosLosCampos();
            }
        }
    });

});                 //fin document


//----------funciones de grupo
function GuardarGrupo() {
    var ID = 0;
    var grupo = new Object();
    grupo.GRUP_Interno = $("#IDGrupo").val();
    grupo.GRUP_Nombre = $("#NombreGrupo").val();
    grupo.GRUP_Descripcion = $("#Descripcion").val();
    grupo.GRUP_Activo = $("input[name='Bloquear']:checked").val();
    grupo.GRUP_Tareas = implodeobj($("#Tareas :checked"));
    //alert(grupo.GRUP_Tareas);
    $.ajax({
        data: JSON.stringify(grupo),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GrupoUsuario.aspx/GuardarGrupo",
        async: false,
        cache: false,
        success: function (data) {
            ID = parseInt(data.d);

            if (ID > 0) {
                showMensaje("Datos guardados correctamente");
                $("#IDGrupo").val(ID);

            } else {
                showError("No se insertó el grupo");
                //$("#IDGrupo").val("");
            }
        } //fin success
    }); //fin de ajax
    return ID;
} //fin guardar grupo
function ObtenerDatosDeGrupo() {
    var grupo = new Object();
    grupo.GRUP_Interno = $("#IDGrupo").val();
    $.ajax({
        data: JSON.stringify(grupo),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GrupoUsuario.aspx/ObtenerGrupo",
        async: false,
        cache: false,
        success: function (data) {
            var item = data.d;

            $("#IDGrupo").val(item.GRUP_Interno);
            $("#NombreGrupo").val(item.GRUP_Nombre);
            $("#Descripcion").val(item.GRUP_Descripcion);

            if (item.GRUP_Activo == true)
                $("input[name='Bloquear'][value='true']").attr("checked", "checked");
            else
                $("input[name='Bloquear'][value='false']").attr("checked", "checked");

            $("#UsuarioCreador").val(item.UsuarioCreador);

            if (item.AUDI_FechaCrea != null)
                $("#FechaRegistro").val(ConvertirDateTimeAString(item.AUDI_FechaCrea, "dd/mm/yyyy HH:MM:ss.l"));
            else
                $("#FechaRegistro").val("");

            
        }
    });    //fin de ajax
}
//tareas
function CargarTareasPorModulo() {

    $.ajax({

        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GrupoUsuario.aspx/TareasPorModulo",
        async: false,
        cache: false,
        success: function (data) {
            var html = "";
            var tareas = data.d;
            if (tareas != null) {
                var modulo = "";
                var aux = 0;
                $.each(tareas, function (i, item) {
                    //m.MODU_Interno,t.TARE_Interno,m.MODU_Nombre,TARE_NombreCorto,t.TARE_Nombre,t.TARE_Descripcion 
                    if (modulo != item.MODU_Interno) {
                        if (aux != 0) {
                            html += "</tr></table></dd>";
                            aux = 0;
                        }
                        html += "<dt>Tareas: " + item.MODU_Nombre + "</dt>";
                        modulo = item.MODU_Interno;

                        html += "<dd>";
                        html += "<div class='alinear-right marginBottom5'><input type='button' value='Seleccionar todo' class='btnTransparente selectCheck' id='" + item.MODU_Interno + "'/>";
                        html += "<input type='button' value='Limpiar selección' class='btnTransparente noneCheck'/></div>";
                        html += "<table class='TablaInvisible' id='checkTareas" + item.MODU_Interno + "'><tr>";
                                            
                     }
                    if (modulo == item.MODU_Interno) {
                        if (aux % 3 == 0) {
                            html += "</tr><tr>";
                        }
                        html += "<td width='33%'>";
                        html += "<input type='checkbox' value=" + item.TARE_Interno + " />";
                        html += "<label>" + item.TARE_Nombre + "</label>";
                        html += "</td>";
                        aux++;
                    }
                }); //fin de each

            } //fin de if tareas!=null
            if (html != "") {
                html += "</tr></table></dd>";
                $("#Tareas").html(html);
            } else {
                $("#Tareas").html("No  hay tareas registradas en la base de datos");
            }
            
        }
    });                            //fin de ajax
}
function ObtenerTareasDeGrupo() {
    var grupo = new Object();
    grupo.GRUP_Interno = $("#IDGrupo").val();
    $.ajax({
        data: JSON.stringify(grupo),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "GrupoUsuario.aspx/ObtenerTareasDeGrupo",
        async: false,
        cache: false,
        success: function (data) {
            var html = "";
            var tareas = data.d;
            if (tareas != null) {
                var modulo = "";
                var aux = 0;
                $.each(tareas, function (i, item) {
                    $("#Tareas [value='" + item.TARE_Interno + "']").attr("checked", true);
                }); //fin de each

            } //fin de if tareas!=null
        }
    });//fin de ajax
}