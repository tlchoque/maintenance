
var $tree = null;
$(document).ready(function () {
    $tree = $("#ArbolLocalizacion");

    $("#mmenu input").click(function () {
        switch (this.id) {
            case "search":
                $("#ArbolLocalizacion").jstree("search", document.getElementById("text").value);
                break;
            case "text": break;
            default:
                $("#ArbolLocalizacion").jstree(this.id);
                break;
        }
    });

    $("#clear_search").click(function () {
        $("#text").val("");
    })

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
                            "url": "GestionLocalizaciones.aspx/ObtenerNodoPrincipal",
                            "data": function () {
                                return '{"id" : 1 }';
                            },
                            "success": function (tree) {
                                return tree.d;
                            }
                        }
                    },
                    "search": {
                        "ajax": {
                            "url": "GestionLocalizaciones.aspx/ObtenerLocalizacionesLike",
                            // You get the search string as a parameter
                            "data": function (str) {
                                return {
                                    "Nombre": str
                                };
                            }
                        }
                    },
                    "types": {
                        'types': {
                            'default': {
                                'icon': {
                                    'image': '/scripts/themes/location.png'
                                },
                                'valid_children': 'default'
                            }
                        }
                    },
                    "ui": {
                        "initially_select": ["1"]
                    },
                    core: { "animation": 0 },
                    contextmenu: {
                        items: function ($node) {
                            return {
                                createItem: {
                                    "label": "Agregar Localizacion",
                                    "action": function (obj) { this.create(obj); }
                                },
                                renameItem: {
                                    "label": "Cambiar Nombre",
                                    "action": function (obj) { this.rename(obj); }
                                },
                                deleteItem: {
                                    "label": "Eliminar Localizacion",
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
                            url: "GestionLocalizaciones.aspx/InsertarNodo",
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
                            url: "GestionLocalizaciones.aspx/InsertarNodo",
                            success: function (r) {
                                if (r.d == 0) {
                                    $.jstree.rollback(data.rlbk);
                                }
                            }
                        })
                    })
                    .bind("remove.jstree", function (e, data) {
                        if (data.rslt.obj.attr("id") != "1") {
                            data.rslt.obj.each(function () {
                                var nodo = new Object();
                                nodo.id = parseInt(this.id);
                                //alert(id)
                                $.ajax({
                                    data: JSON.stringify(nodo),
                                    type: 'POST',
                                    dataType: "json",
                                    contentType: "application/json;",
                                    url: "GestionLocalizaciones.aspx/EliminarNodosPorPadre",
                                    success: function (r) {
                                        if (r.d > 0) {
                                            return true;
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