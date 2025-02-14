function SoloIP(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;

    return true;
}
function ValidarIP(obj) {
    //Creamos un objeto 

    valueForm = obj.value;

    // Patron para la ip
    var patronIp = new RegExp("^([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3})$");
    //window.alert(valueForm.search(patronIp));
    // Si la ip consta de 4 pares de números de máximo 3 dígitos
    if (valueForm.search(patronIp) == 0) {
        // Validamos si los números no son superiores al valor 255
        valores = valueForm.split(".");
        if (valores[0] <= 255 && valores[1] <= 255 && valores[2] <= 255 && valores[3] <= 255) {
            //Ip correcta
            obj.style.color = "#000";
            return true;
        }
    }
    //Ip incorrecta



    MostrarMensajeAlerta('La Ip Ingresada ' + obj.value + ' es Incorrecta');
    obj.value = "";
    obj.focus();
    return false;
}



function ValidNum(e) {

    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58) || tecla == 44);
}

function validaRut(Obj) {
    var rut = Obj.value.toString();
    var pos = rut.indexOf('-');
    var verificador = '';
    var cuerpo = '';
    if (pos == -1) {
        verificador = rut.substr(rut.length - 1, 1);
        cuerpo = rut.substr(0, rut.length - 1);
        rut = cuerpo + "-" + verificador;
    }

    var rexp = new RegExp(/^([0-9])+\-([k0-9])+$/);
    if (rut == "") {
        return true;
    }
    if (rut.match(rexp)) {
        try {
            var _RUT = rut.split("-");

            var elRut = _RUT[0];

            var factor = 2;
            var suma = 0;
            var dv;
            for (i = (elRut.length - 1); i >= 0; i--) {

                factor = factor > 7 ? 2 : factor;
                suma += parseInt(elRut.charAt(i)) * parseInt(factor++);

            }
            dv = 11 - (suma % 11);

            if (dv == 11) {
                dv = 0;
            }
            else if (dv == 10) {
                dv = "k";
            }

            if (dv == _RUT[1]) {
                Obj.value = rut;
                return true;
            } else {

                MostrarMensajeAlerta('El rut es incorrecto');
                Obj.value = "";
                Obj.focus();
                return false;
            }
        } catch (e) {
            MostrarMensajeAlerta(e.InnerException + 'Linea' + e.Line);
        }
    } else {
        Obj.value = "";
        Obj.focus();
        MostrarMensajeAlerta('Formato incorrecto. El formato correcto es 12345678-k');

        return false;
    }
}

function SoloRut(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45 && charCode != 107)
        return false;

    return true;
}



function SoloNumeros(evt, texto, decimales, obj) {

    obj.value = obj.value.replace(".", ",");

    var _texto = texto.split(",");


    if (_texto !== null) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode !== 44 && charCode !== 46)
            return false;

        if (_texto.length > 1 && charCode === 44)
            return false;
        if (_texto.length > 1) {
            if (_texto[1] !== null) {
                if (_texto[1].length >= decimales && charCode !== 8 && charCode !== 127)
                    return false;
            }
        }
    }
    return true;
}

function SoloFolios(evt, texto) {
    var _texto = texto.split("-");
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)
        return false;

    return true;
}



function ValidaSoloNumeros() {
    if ((event.keyCode < 48) || (event.keyCode > 57))
        event.returnValue = false;
}



function Formato_Mes_Anho(oText) {
    try {
        var texto, dia, mes, anho;
        texto = oText;
        if (texto != '') {
            while (texto.indexOf('/') != -1) {
                texto = texto.replace("/", "");
            }
            while (texto.indexOf('-') != -1) {
                texto = texto.replace("-", "");
            }
            dia = texto.substring(0, 2);
            mes = texto.substring(2, 4);
            anho = texto.substring(4, 8);
            texto = dia + "/" + mes + "/" + anho;
        }
        return texto;
    } catch (e) {
        alert(e.Message);
    }


}
function ValidaFono(input) {
    var phoneno = /\D*([+56]\d[2-9])(\d{4})(\d{4})\D*/;

    if (input.value === "") { return true; }
    if (input.value.match(phoneno)) {
        return true;
    }
    else {
        MostrarMensajeAlerta("Formato no Válido, Formato Correcto es +5612345678");
        input.value = "";
        return false;
    }
}

function esFechaValida(fecha) {

    fecha.value = Formato_Mes_Anho(fecha.value);

    if (fecha != undefined && fecha.value != "") {
        if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha.value)) {
            MostrarMensajeAlerta("formato de fecha no válido (dd-mm-aaaa)");
            fecha.value = "";
            return false;
        }
        var dia = parseInt(fecha.value.substring(0, 2), 10);
        var mes = parseInt(fecha.value.substring(3, 5), 10);
        var anio = parseInt(fecha.value.substring(6), 10);

        switch (mes) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                numDias = 31;
                break;
            case 4: case 6: case 9: case 11:
                numDias = 30;
                break;
            case 2:
                if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
                break;
            default:
                MostrarMensajeAlerta("Fecha introducida errónea");
                fecha.value = "";
                return false;
        }

        if (dia > numDias || dia === 0) {
            MostrarMensajeAlerta("Fecha introducida errónea");
            fecha.value = "";
            return false;
        }

        var Fecha_aux = fecha.value.split("/");
        var Fecha1 = new Date(parseInt(Fecha_aux[2]), parseInt(Fecha_aux[1] - 1), parseInt(Fecha_aux[0]));

        Hoy = new Date();//Fecha actual del sistema
        if (Fecha1 > Hoy) {
            MostrarMensajeAlerta("No se permite el ingreso de fechas futuras.");
            fecha.value = "";
            return false;
        }



        return true;
    }
}
function esFechaValidaFutura(fecha) {

    fecha.value = Formato_Mes_Anho(fecha.value);

    if (fecha != undefined && fecha.value != "") {
        if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha.value)) {
            MostrarMensajeAlerta("formato de fecha no válido (dd-mm-aaaa)");
            fecha.value = "";
            return false;
        }
        var dia = parseInt(fecha.value.substring(0, 2), 10);
        var mes = parseInt(fecha.value.substring(3, 5), 10);
        var anio = parseInt(fecha.value.substring(6), 10);

        switch (mes) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                numDias = 31;
                break;
            case 4: case 6: case 9: case 11:
                numDias = 30;
                break;
            case 2:
                if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
                break;
            default:
                MostrarMensajeAlerta("Fecha introducida errónea");
                fecha.value = "";
                return false;
        }

        if (dia > numDias || dia === 0) {
            MostrarMensajeAlerta("Fecha introducida errónea");
            fecha.value = "";
            return false;
        }

        return true;
    }
}

function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}

function validarEmail(obj) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(obj.value.toString())) {
        MostrarMensajeAlerta("La dirección de correo " + obj.value + " es incorrecta.");
        obj.value = "";
        return false;
    }
}

function validaUrl(obj) {
    var expr = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + //port
        '(\\?[;&amp;a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i');
    if (!expr.test(obj.value.toString())) {
        MostrarMensajeAlerta("La URL " + obj.value + " es incorrecta.");
        obj.value = "";
        return false;
    }
    else
        return true;
}


function FormatoNumero(number) {
    var decimalSeparator = ",";
    var thousandSeparator = ".";

    // make sure we have a string
    var result = String(number);

    // split the number in the integer and decimals, if any
    var parts = result.split(decimalSeparator);

    // if we don't have decimals, add .00
    if (!parts[1]) {
        parts[1] = "00";
    }

    // reverse the string (1719 becomes 9171)
    result = parts[0].split("").reverse().join("");

    // add thousand separator each 3 characters, except at the end of the string
    result = result.replace(/(\d{3}(?!$))/g, "$1" + thousandSeparator);

    // reverse back the integer and replace the original integer
    parts[0] = result.split("").reverse().join("");
    MostrarMensajeAlerta("La dirección de correo " + parts.join(decimalSeparator) + " es incorrecta.");
    // recombine integer with decimals
    return parts.join(decimalSeparator);
}

function redondeo(numero, decimales) {
    var flotante = parseFloat(numero);
    var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
    return resultado;
}


