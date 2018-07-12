/// <reference path="jquery-2.0.0-vsdoc.js" />
$(document).ready(function (e) {
    //    #menu li:hover ul {
    //	visibility: visible;
    //    }

    //    #menu li:hover a {
    //	background-color: #dbe4e9;
    //	border-left: 1px solid #025a8d;
    //	border-right: 1px solid #025a8d;
    //	margin: 0;
    //}
    $("#menu li li").css("height", "20px");
    $("#menu > li").hover(
    //        $(this).children("a").css({ "background-color": "#dbe4e9",
    //            "border-left": "1px solid #025a8d", "border-right": "1px solid #025a8d", "margin": "0"
    //        });
        function () {
            $(this).find('ul:first').css({ visibility: "visible" });
            $(this).find("li:first").hover(
            function () {
                //alert("li");
                
                $(this).children("ul:first").css({ visibility: "visible", left: "100%","margin-top": "1px" });
            },
		    function () {
		        $(this).children('ul:first').css({ visibility: "hidden" });
		    });
        },
		function () {
		    $(this).children('ul:first').css({ visibility: "hidden" });
		});
    //    #menu li:hover li:hover ul {
    //	visibility: visible;
    //	left: 100%;
    //    }
    //    $("#menu li li").hover(function () {
    //        $(this).children("ul").css({ visibility: "visible",left: "100%"});
    //    }, function () {
    //        $(this).children("ul").css({ visibility: "hidden" });
});        //fin document
function menuHover(menu_li) {
    menu_li.hover(
        function () {
            menu_li.find("ul:first").css({ visibility: "visible", left: "100%" });
        },
		function () {
		    menu_li.find('ul:first').css({ visibility: "hidden" });
		});
}