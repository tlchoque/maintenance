/// <reference path="jquery-2.0.0-vsdoc.js" />
//-------------------para el select de localizaciones
var localizaciones = null;
var charTab = "-";
var numCharTab = 1;
$(document).ready(function (e) {
    ObtenerLocalizaciones();
    if ($("#IDEquipo").val() != "" && $("#opcURL").val() == "editar") {
        $(".pagetitle h2").html("Datos de Equipo");
        $("#nuevo").parent("li").hide();
        $("#guardar_nuevo").parent("li").hide();
        ObtenerDatosDeEquipo();
    }
    $("#Nombre,#Marca,#Modelo,#Serie,#Codigo").change(function () {
        var descripcion = $("#Nombre").val() + " " +
                            $("#Marca").val() + " " +
                            $("#Modelo").val() + " " +
                            $("#Serie").val() + " " +
                            $("#Codigo").val();
        $("#Descripcion").val(descripcion);
    });
    DesmarcarCamposVaciosLLenados(); //desmarcamos los campos marcados que se van llenando
    CamposNumericos("#AnioFabricacion,#AnioServicio"); //mandamos los camppos que seran numericos


    $("#nuevo").click(function (e) {
        e.preventDefault();
        //alert("kk");
        $("#IDEquipo").val("");
        $("#HILO_Fecha").val("");
        limpiarMsjBox();
        limpiarTodosLosCampos();
    });
    $("#guardar").click(function (e) {
        e.preventDefault();
        if (CamposRequeridosVacios("#Nombre,#Estado,#Tipo,#Localizacion")) {//si estan vacios
            showError("Los campos marcados son obligatorios");
            return false;
        } else {
            limpiarMsjBox();
            quitarBgerror();
            if (GuardarEquipo() > 0) {
                ObtenerDatosDeEquipo();
            }

        }
    });
    $("#guardar_cerrar").click(function (e) {
        e.preventDefault();
        if (CamposRequeridosVacios("#Nombre,#Estado,#Tipo,#Localizacion")) {//si estan vacios
            showError("Los campos marcados son obligatorios");
            return false;
        } else {
            GuardarEquipo();
            //window.history.back();
            location.href = "GestionEquipos.aspx";
        }
    });
    $("#guardar_nuevo").click(function (e) {
        e.preventDefault();
        if (CamposRequeridosVacios("#Nombre,#Estado,#Tipo,#Localizacion")) {//si estan vacios
            showError("Los campos marcados son obligatorios");
            return false;
        } else {
            GuardarEquipo();
            //limpiarMsjBox();
            limpiarTodosLosCampos();
        }
    });

});  //fin document


//----------funciones de equipo
function GuardarEquipo() {
    var ID = 0;
    var equipo = new Object();
    equipo.EQUI_Interno = $("#IDEquipo").val();
    equipo.EQUI_Nombre = $("#Nombre").val();
    equipo.EQUI_Marca = $("#Marca").val();
    equipo.EQUI_Modelo = $("#Modelo").val();
    equipo.EQUI_Serie = $("#Serie").val();
    equipo.EQUI_Codigo = $("#Codigo").val();
    equipo.EQUI_AnioFabricacion = $("#AnioFabricacion").val();
    equipo.EQUI_AnioServicio = $("#AnioServicio").val();
    equipo.EQUI_Estado = $("#Estado").val();
    equipo.EQUI_Descripcion = $("#Descripcion").val();
    equipo.TIPO_Interno = $("#Tipo").val();
    equipo.LOCA_Interno = $("#Localizacion").val();
    equipo.HILO_Fecha = $("#HILO_Fecha").val();
    $.ajax({
        data: JSON.stringify(equipo),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Equipo.aspx/GuardarEquipo",
        async: false,
        cache: false,
        success: function (data) {
            ID = parseInt(data.d);
            
            if (ID > 0) {
                showMensaje("Datos guardados correctamente");
                $("#IDEquipo").val(ID);
            } else {
                showError("No se insertó el usuario");
                //$("#IDEquipo").val("");
            }
        }//fin success
    }); //fin de ajax
    return ID;
} //fin guardar equipo
function ObtenerDatosDeEquipo() {
    var equipo = new Object();
    equipo.EQUI_Interno = $("#IDEquipo").val();
    $.ajax({
        data: JSON.stringify(equipo),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Equipo.aspx/ObtenerEquipo",
        async: false,
        cache: false,
        success: function (data) {
            var item = data.d;

            $("#IDEquipo").val(item.EQUI_Interno);
            $("#Nombre").val(item.EQUI_Nombre);
            $("#Marca").val(item.EQUI_Marca);
            $("#Modelo").val(item.EQUI_Modelo);
            $("#Serie").val(item.EQUI_Serie);
            $("#Codigo").val(item.EQUI_Codigo);
            $("#AnioFabricacion").val(item.EQUI_AnioFabricacion);
            $("#AnioServicio").val(item.EQUI_AnioServicio);
            $("#Tipo").val(item.TIPO_Interno);
            $("#Estado").val(item.EQUI_Estado);
            $("#Descripcion").val(item.EQUI_Descripcion);
            $("#Localizacion").val(item.LOCA_Interno);
            if (item.HILO_Fecha != null)
                $("#HILO_Fecha").val(ConvertirDateTimeAString(item.HILO_Fecha, "dd/mm/yyyy HH:MM:ss.l"));
            else
                $("#HILO_Fecha").val("");
            //alert(ConvertirDateTimeAString(item.HILO_Fecha, "dd/mm/yyyy HH:MM:ss.l"));
            //alert(dateFormat(Date(), "dd/mm/yyyy HH:MM:ss"));
        }
    });  //fin de ajax
   
}

function ObtenerLocalizaciones() {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        url: "Equipo.aspx/Localizaciones",
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
