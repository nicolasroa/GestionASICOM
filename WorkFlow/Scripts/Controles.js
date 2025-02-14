

jQuery(function ($) {

    //Bloquea tecla enter
    $('form').keypress(function (e) {

        var code = null;
        code = (e.keyCode ? e.keyCode : e.which)

        return (code == 13) ? false : true;
    });


    function ObtenerControl(id) {

        if (document.getElementById) {
            return document.getElementById(id);
        }

        else if (document.all) {
            return window.document.all[id];
        }

        else if (document.layers) {
            return window.document.layers[id];
        }
    }

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
            'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);




});



/********************************************************************************************    
function que permite mostrar y ocultar las opciones de usuario
********************************************************************************************/

function MostrarOpcionesUsuario() {
    if ($('.DatosOpciones').attr('style') == 'display: none') {
        $('.DatosOpciones').attr('style', 'display: block');
    }
    else {
        $('.DatosOpciones').attr('style', 'display: none');
    }
}

/********************************************************************************************    
function que permite dar configuracion basica a todos los popus
********************************************************************************************/

function ConfiguracionPopups() {

    elementos = $('div[title=VentanaModal]')


    elementos.each(function (index) {

        var z_Index = index + 999;

        $(this).dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            show: 'clip',
            hide: 'clip',
            zIndex: z_Index,
            overlay: {
                opacity: 0.5,
                background: "black"
            },
            open: function (event, ui) {
                $(this).parent().find('.ui-dialog-titlebar').append('<div class="ui-dialog-titlebar-icon"></div>');
            }

        });
    });

}
/********************************************************************************************
function que permite dar configuracion personalizada a cada popup
********************************************************************************************/
function ConfiguracionPopup(elementoDiv, elementoFrame, url, ancho, alto, titulo) {

    elementoFrame.attr('src', url);
    elementoDiv.dialog('option', 'width', ancho);
    elementoDiv.dialog('option', 'height', alto);
    elementoDiv.dialog('option', 'title', titulo);
    elementoDiv.dialog('open');

}

/********************************************************************************************
verifica que hay un popup disponible y lo abre si hay uno disponible
********************************************************************************************/

function AsignaPopupDisponible(lstFrames, url, ancho, alto, titulo) {

    lstFrames.each(function (index) {

        var SelectorPopup = '#' + $(this).attr('NombreDivPadre');
        var elemento = $(SelectorPopup);

        if (elemento.length > 0) {
            ConfiguracionPopups();
            ConfiguracionPopup(elemento, $(this), url, ancho, alto, titulo);

            return false;
        }
        else {

            if (elemento.dialog('isOpen') == false) {
                elemento.dialog('open');
                elemento.dialog('isOpen') = true;
                $(this).attr('src', url);

                return false;
            }

        }

    });
}
/********************************************************************************************
funcion que permite determinar la ventana donde estan los div de pupus para poder abrirlos
asi tambien cuando encuentra la posición, abre el que encuentra disponible
********************************************************************************************/

function AbrirPopupMain(url, ancho, alto, titulo) {

    var lstFrames = $('.cssFramePopup');
    var ventana;
    var encuentraElmentosPopup = false;

    while (encuentraElmentosPopup == false) {

        if (lstFrames.length > 0) {
            AsignaPopupDisponible(lstFrames, url, ancho, alto, titulo);
            encuentraElmentosPopup = true;
            break;
        }

        ventana = window.parent;

        if (ventana.$('.cssFramePopup').length > 0) {

            Popups = ventana.$('.cssFramePopup');
            encuentraElmentosPopup = true;
        }
        else
            ventana = ventana.window.parent;



    }

}

function AbrirPopup(url) {

    var ancho = 500;
    var alto = 500;
    var titulo = '';

    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}

function AbrirPopup(url, ancho, alto) {

    var titulo = '';
    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}

function AbrirPopup(url, ancho, alto, titulo) {
    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}
function CerrarPopup(ConEvento) {

    var ventanaPopups = window.parent;
    var ElementosPopup = ventanaPopups.$('.cssFramePopup').parent();
    if (navigator.appName != "Microsoft Internet Explorer") {
        if (!ConEvento)
            return false;
        else {
            window.parent.$('.CssCerrarPopup').click();
        }
    }
    if (ElementosPopup.length == 0) {
        while (true) {
            ventanaPopups = ventanaPopups.window.parent;
            ElementosPopup = ventanaPopups.$('.cssFramePopup').parent()
            if (ElementosPopup.length > 0) {
                break;
            }
        }
    }

    for (var i = ElementosPopup.length - 1; i >= 0; i--) {
        var elmentoDivPopup = $(ElementosPopup[i]);
        if (window.parent.$(elmentoDivPopup).dialog('isOpen')) {
            alert(ElementosPopup);
            window.parent.$(elmentoDivPopup).dialog('close');
            break;
        }
    }
    if (!ConEvento)
        return false;
    else {
        window.parent.$('.CssCerrarPopup').click();
    }

}

function ModalTamanoVariable(url, ancho, alto, titulo) {
    $("#divCuerpoModal").width(ancho);
    $("#divCuerpoModal").height(alto);
    $('#divTituloModal').html('<button type="button" id="btnCerrarModal" class="close" data-dismiss="modal">&times;</button> <h3 class="modal-title">' + titulo + '</h3>');
    ancho = ancho - 30;
    alto = alto - 90;
    $("#divContenidoModal").width(ancho);
    $("#divContenidoModal").height(alto);
    $('#divContenidoModal').html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>');

    $('#btnAbrirModal').click();

}

function ModalURL(url, titulo) {
    $('#divTituloModal').html('<button type="button" id="btnCerrarModal" class="close" data-dismiss="modal">&times;</button> <h3 class="modal-title">' + titulo + '</h3>');
    $('#divContenidoModal').html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>');

    $('#btnAbrirModal').click();

}

function OpenModalEvento(url, titulo) {

    
  waitingDialog.show('Cargando... ' + titulo); setTimeout(function () { waitingDialog.hide(); }, 4000);
    $('#divTituloEvento').html('<button type="button" id="btnCerrarModal" class="close" data-dismiss="modal">&times;</button> <h3 class="modal-title">' + titulo + '</h3>');
    $('#divContenidoEvento').html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>');

    $('#btnAbrirModalEvento').click();

}
function OpenModalEventoConDetalle(url, Evento,Etapa,Malla,Solicitud,Responsable,FechaEstimada,FechaAsignacion) {


    waitingDialog.show('Cargando... ' + Evento); setTimeout(function () { waitingDialog.hide(); }, 4000);
    var TituloEvento =
         '<button type="button" id="btnCerrarModal" class="close" data-dismiss="modal">&times;</button>'
        + '<div class="col-lg-12">'
        + '<ul class="breadcrumb">'
        + '<li class="active">Solicitud: '+ Solicitud +'</li>'
        + '<li class="active">Malla: ' + Malla +'</li>'
        + '<li class="active">Etapa: ' + Etapa +'</li>'
        + '<li class="active">Evento: ' + Evento +'</li>'
        + '<li class="floatright">'
        //+ '<span class="label label-warning">Responsable: ' + Responsable +'</span>'
        + '<span class="label label-warning">Fecha de Asignación: ' + FechaAsignacion +'</span>'
        + '<span class="label label-success">Fecha Límite: ' + FechaEstimada +'</span></li>'
        + '</ul>'
        + '</div>'
       



    $('#divTituloEvento').html(TituloEvento);
    $('#divContenidoEvento').html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>');

    $('#btnAbrirModalEvento').click();

}


function CerrarDivModal(Evento) {

    try {
        window.parent.$('#btnCerrarModal').click();
        if (Evento == 1) {
            window.parent.$(".CssCerrarPopup").click();
        }
    } catch (e) {
        alert(e.message);
    }



}

function ModalDiv(IdDiv, titulo) {

    var $dialog = $('#' + IdDiv + '').dialog({
        modal: true,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "explode",
            duration: 500
        },
        title: titulo

    });

    $dialog.dialog('open');

}
function CerrarModal(Id) {

    var Dialog = $('#' + Id + '');
    Dialog.dialog('close');



}

function CerrarVentana() {

    window.parent.$('.CssCerrarPopup').click();
    window.parent.$('#divModalPopup1').dialog('close');

    return false;

}

function CerrarVentanaMenu() {

    window.parent.$('.CssCargarMenu').click();
    window.parent.$('#divModalPopup1').dialog('close');
    return false;

}

function AbrirMensaje(texto) {

    var ancho = 350;
    var alto = 250;
    var titulo = 'prueba ';
    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje2(texto, titulo) {

    var ancho = 350;
    var alto = 250;

    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje(texto, ancho, alto) {

    var titulo = ' ';
    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje(texto, ancho, alto, titulo) {

    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function CargarMensaje(texto, ancho, alto, titulo) {
    $('#lbltextoMensaje').html(texto);
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        height: alto,
        width: ancho,
        title: titulo,
        resizable: false,
        show: 'clip',
        hide: 'clip',
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });
    $('#divMensajes').dialog('open');
}
function CerrarMensaje() {
    $('#divMensajes').dialog('close');
    return false;
}

function MostrarPdfModal(NombreArchivo, ancho, alto, titulo) {


    try {

        var Contenido = '<iframe src="../Repositorio/Pdf/' + NombreArchivo + '" style="width: 100%; height: 100%;" frameborder="0" scrolling="no"><p></p></iframe>';
        document.getElementById("divContenidoPdf").innerHTML = Contenido;
        $('#divTituloPdf').html('<button type="button" id="btnCerrarModal" class="close" data-dismiss="modal">&times;</button> <h3 class="modal-title">' + titulo + '</h3>');
        $('#btnAbrirModalPdf').click();
        return;


    } catch (e) {
        alert(e.Message);
        return false;
    }
}



function MostrarPdfDiv(NombreArchivo, NombreDiv) {
    var pObj = new PDFObject({

        url: "../Repositorio/Pdf/" + NombreArchivo,
        id: "myPDF",
        pdfOpenParams: {
            navpanes: 0,
            toolbar: 0,
            statusbar: 0,
            view: "FitV"
        }
    });
    var msg;
    document.getElementById(NombreDiv).className = "pdf";
    var htmlObj = pObj.embed(NombreDiv);
    return false;
}




function MostrarMensajeError(mensaje) {
    MensajeError(mensaje);
}
function MostrarMensajeInfo(mensaje) {
    MensajeInfo(mensaje);
}
function MostrarMensajeExito(mensaje) {

    MensajeExito(mensaje);
}
function MostrarMensajeAlerta(mensaje) {
    MensajeAlerta(mensaje);
}
function MostrarMensajeConfirmacion(mensaje) {
    return MensajeConfirmacion(mensaje);
}



function MensajeInfo(texto) {
    $("#TipoMensaje").removeClass("alert-info");
    $("#TipoMensaje").removeClass("alert-danger");
    $("#TipoMensaje").removeClass("alert-success");
    $("#TipoMensaje").removeClass("alert-warning");
    $("#Titulo").html("Información");
    $("#Texto").html(texto);
    $("#TipoMensaje").addClass("alert alert-dismissible alert-info");
    $('#MensajesModal').modal("show");
}
function MensajeError(texto) {
    $("#TipoMensaje").removeClass("alert-info");
    $("#TipoMensaje").removeClass("alert-danger");
    $("#TipoMensaje").removeClass("alert-success");
    $("#TipoMensaje").removeClass("alert-warning");
    $("#Titulo").html("Error");
    $("#Texto").html(texto);
    $("#TipoMensaje").addClass("alert alert-dismissible alert-danger");
    $('#MensajesModal').modal("show");
}

function MensajeExito(texto) {
    $("#TipoMensaje").removeClass("alert-info");
    $("#TipoMensaje").removeClass("alert-danger");
    $("#TipoMensaje").removeClass("alert-success");
    $("#TipoMensaje").removeClass("alert-warning");

    $("#Titulo").html("Correcto");
    $("#Texto").html(texto);
    $("#TipoMensaje").addClass("alert alert-dismissible alert-success");
    $('#MensajesModal').modal("show");
}
function MensajeAlerta(texto) {
    $("#TipoMensaje").removeClass("alert-info");
    $("#TipoMensaje").removeClass("alert-danger");
    $("#TipoMensaje").removeClass("alert-success");
    $("#TipoMensaje").removeClass("alert-warning");
    $("#Titulo").html("Alerta");
    $("#Texto").html(texto);
    $("#TipoMensaje").addClass("alert alert-dismissible alert-warning");
    $('#MensajesModal').modal("show");
}

function MensajeConfirmacion(texto, obj) {


    BootstrapDialog.confirm(texto, function (result) {
        if (result) {
            obj.click();
        } else {
            return;
        }
    });
}


function MensajeConfirmacionAnularSolicitud(texto, obj) {


    BootstrapDialog.confirm(texto, function (result) {
        if (result) {
            $("#btnIngresarMotivoRechazo").click();
        } else {
            return;
        }
    });
}

function ConfirmacionConEspera(Confirmacion, texto, obj) {
    $('#lbltextoConfirmacion').html("<p class='messager-confirm'>" + texto + "</p>");
    $('#divConfirmacion').dialog({
        autoOpen: false,
        modal: true,
        title: 'Alerta',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Si": function () {
                obj.click();
                $(this).dialog("close");
                $.blockUI({
                    message: "<p class='tdInfoData'>" + Confirmacion + "</p>",
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#fff',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        color: '#fff'
                    }
                });


            },
            "No": function () { $(this).dialog("close"); }
        }
    });
    $('#divConfirmacion').dialog('open');
}





function tip(obj, txt, maxW) {
    maxW = maxW || 'auto';
    Tipped.create(obj, txt,
        {
            skin: 'blue',
            closeButton: true,
            hook: 'topmiddle',
            maxWidth: maxW,
            shadow: {
                blur: 4,
                offset: { x: 3, y: 3 },
                opacity: .2
            },
            showOn: ['', ''],
            hideOn: ['blur', 'mouseleave']
        });



}


function MensajeEnControl(idcontrol, mensaje) {

    tip("#" + idcontrol + "", mensaje, 350);
    Tipped.show("#" + idcontrol + "");

}


//NOTIFICACIONES

function MostrarNotificacion(mensaje) {


    alertify.prompt("This is a prompt dialog", function (e, str) {
        if (e) {
            alertify.init("You've clicked OK and typed: " + str);
        } else {
            alertify.log("You've clicked Cancel");
        }
    }, "Default Value");
    return false;

}



function AsignarFormatoFecha(idControl) {


    $("#" + idControl).attr('onblur', 'esFechaValida(this);');
    $("#" + idControl).datepicker({
        dateFormat: "dd/mm/yy",
        defaultDate: "+1d",
        changeMonth: true,
        numberOfMonths: 3,
        maxDate: new Date()
    });




}
function LimpiarFormatoFecha(idControl) {
    $("#" + idControl).removeAttr("onblur");
    $("#" + idControl).datepicker("destroy");
}




function popup(lnk, id, Name) {
    BootstrapDialog.confirm({
        title: 'WARNING',
        message: 'Do You Want To Delete <b>' + Name + '</b>',
        type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false
        draggable: true, // <-- Default value is false
        btnCancelLabel: 'Cancel', // <-- Default value is 'Cancel',
        btnOKLabel: 'Ok', // <-- Default value is 'OK',
        btnOKClass: 'btn-warning', // <-- If you didn't specify it, dialog type will be used,
        callback: function (result) {
            // result will be true if button was click, while it will be false if users close the dialog directly.
            if (result) {
                javascript: __doPostBack('grdDemo$ctl02$lnkDelete', '');

            } else {
                BootstrapDialog.closeAll();
            }
        }
    });

}

function FormatoGrid(GridId) {
    $(GridId).DataTable({
        scrollY: '50vh',
        searching: false,
        scrollCollapse: true,
        paging: true

    });
}







