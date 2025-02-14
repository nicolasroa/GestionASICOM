<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="SimuladorHipotecario.aspx.cs" Inherits="WorkFlow.Aplicaciones.SimuladorHipotecario" %>


<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        function AvanzaCliente() {
            var btnAvanzaCliente = document.getElementById('<%=btnAvanzaClientesSup.ClientID%>')
            btnAvanzaCliente.click();
        }

        function MostrarInformacionGeneral() {
            var lnkGeneral = document.getElementById('<%=lnkGeneral.ClientID%>')
            lnkGeneral.click();
        }
        function MostrarInformacionCredito() {
            var lnkCredito = document.getElementById('<%=lnkCredito.ClientID%>')
            lnkCredito.click();
        }

        function MostrarInformacionCliente() {
            var lnkCliente = document.getElementById('<%=lnkCliente.ClientID%>')
            lnkCliente.click();
        }


        function MostrarSimulacionMinvu(Visible) {
            if (Visible == "true") {
                $('#DivSimulacionMinvu').show();
            }
            else {
                $('#DivSimulacionMinvu').hide();
            }

        }


        function MostrarSimulacionFlexible(Visible) {
            if (Visible == "true") {
                $('#DivSimulacionFlexible').show();
            }
            else {
                $('#DivSimulacionFlexible').hide();
            }

        }

        function ActualizaMontos(campo) {
            try {

                var PrecioVenta = document.getElementById('<%=txtPrecioVenta.ClientID%>');
                var Subsidio = document.getElementById('<%=txtMontoSubsidio.ClientID%>');
                var BonoIntegracion = document.getElementById('<%=txtBonoIntegracion.ClientID%>');
                var BonoCaptacion = document.getElementById('<%=txtBonoCaptacion.ClientID%>');
                var MontoContado = document.getElementById('<%=txtMontoContado.ClientID%>');
                var MontoCredito = document.getElementById('<%=txtMontoCredito.ClientID%>');
                var PorcFinan = document.getElementById('<%=txtPorcentajeFinanciamiento.ClientID%>');

                var PrecioVentaP = document.getElementById('<%=txtPrecioVentaPesos.ClientID%>');
                var SubsidioP = document.getElementById('<%=txtMontoSubsidioPesos.ClientID%>');
                var BonoIntegracionP = document.getElementById('<%=txtBonoIntegracionPesos.ClientID%>');
                var BonoCaptacionP = document.getElementById('<%=txtBonoCaptacionPesos.ClientID%>');
                var MontoContadoP = document.getElementById('<%=txtMontoContadoPesos.ClientID%>');
                var MontoCreditoP = document.getElementById('<%=txtMontoCreditoPesos.ClientID%>');
                var ValorUF = document.getElementById('<%= Master.FindControl("txtValorUF").ClientID%>').value;

                if (campo === 'PrecioVenta') {

                }

                if (PrecioVenta.value === '') {
                    //MensajeEnControl(PrecioVenta.id, "Dato Obligatorio");
                    PrecioVenta.value = '';
                    Subsidio.value = '';
                    BonoIntegracion.value = '';
                    BonoCaptacion.value = '';
                    MontoContado.value = '';
                    MontoCredito.value = '';
                    PorcFinan.value = '';

                    return;
                }
                PrecioVenta.value = PrecioVenta.value.replace(',', '.')
                if (campo === 'MontoContado') {
                    MontoCredito.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - MontoContado.value.replace(',', '.') - Subsidio.value.replace(',', '.') - BonoIntegracion.value.replace(',', '.') - BonoCaptacion.value.replace(',', '.')), 4);
                    PorcFinan.value = redondeo((MontoCredito.value.replace(',', '.') * 100) / PrecioVenta.value.replace(',', '.'), 2);
                }

                if (campo === 'MontoCredito') {
                    MontoContado.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - MontoCredito.value.replace(',', '.') - Subsidio.value.replace(',', '.') - BonoIntegracion.value.replace(',', '.') - BonoCaptacion.value.replace(',', '.')), 4);
                    PorcFinan.value = redondeo((MontoCredito.value.replace(',', '.') * 100) / PrecioVenta.value.replace(',', '.'), 2);
                }

                if (campo === 'MontoSubsidio') {
                    MontoCredito.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - MontoContado.value.replace(',', '.') - Subsidio.value.replace(',', '.') - BonoIntegracion.value.replace(',', '.') - BonoCaptacion.value.replace(',', '.')), 4);
                    PorcFinan.value = redondeo((MontoCredito.value.replace(',', '.') * 100) / PrecioVenta.value.replace(',', '.'), 2);
                }
                if (campo === 'BonoIntegracion') {
                    MontoCredito.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - MontoContado.value.replace(',', '.') - Subsidio.value.replace(',', '.') - BonoIntegracion.value.replace(',', '.') - BonoCaptacion.value.replace(',', '.')), 4);
                    PorcFinan.value = redondeo((MontoCredito.value.replace(',', '.') * 100) / PrecioVenta.value.replace(',', '.'), 2);
                }

                if (campo === 'BonoCaptacion') {
                    MontoCredito.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - MontoContado.value.replace(',', '.') - Subsidio.value.replace(',', '.') - BonoIntegracion.value.replace(',', '.') - BonoCaptacion.value.replace(',', '.')), 4);
                    PorcFinan.value = redondeo((MontoCredito.value.replace(',', '.') * 100) / PrecioVenta.value.replace(',', '.'), 2);
                }
                if (PorcFinan.value > 80) {
                    PorcFinan.style.background = "red";
                    PorcFinan.style.color = "white"
                }
                else {
                    PorcFinan.style.background = "white";
                    PorcFinan.style.color = "black"
                }


                if (campo === 'PorcentajeFinanciamiento') {

                    if (PorcFinan.value > 100 || PorcFinan.value < 1) {
                        MensajeEnControl(PorcFinan.id, 'Porcentaje de Financiemiento debe ser entre 1% y 100%');
                        return;
                    }

                    MontoCredito.value = redondeo(parseFloat(((PrecioVenta.value.replace(',', '.') - Subsidio.value.replace(',', '.') - MontoContado.value.replace(',', '.')) * PorcFinan.value.replace(',', '.')) / 100), 4);
                    MontoContado.value = redondeo(parseFloat(PrecioVenta.value.replace(',', '.') - Subsidio.value.replace(',', '.') - MontoCredito.value.replace(',', '.')), 4);
                }
                SubsidioP.value = parseFloat(Subsidio.value.replace(',', '.') * ValorUF.replace(',', '.'));
                SubsidioP.value = redondeo(SubsidioP.value, 0);
                SubsidioP.value = '$ ' + Intl.NumberFormat().format(SubsidioP.value);

                BonoIntegracionP.value = parseFloat(BonoIntegracion.value.replace(',', '.') * ValorUF.replace(',', '.'));
                BonoIntegracionP.value = redondeo(BonoIntegracionP.value, 0);
                BonoIntegracionP.value = '$ ' + Intl.NumberFormat().format(BonoIntegracionP.value);

                BonoCaptacionP.value = parseFloat(BonoCaptacion.value.replace(',', '.') * ValorUF.replace(',', '.'));
                BonoCaptacionP.value = redondeo(BonoCaptacionP.value, 0);
                BonoCaptacionP.value = '$ ' + Intl.NumberFormat().format(BonoCaptacionP.value);


                PrecioVentaP.value = parseFloat(PrecioVenta.value.replace(',', '.') * ValorUF.replace(',', '.'));
                PrecioVentaP.value = redondeo(PrecioVentaP.value, 0);
                PrecioVentaP.value = '$ ' + Intl.NumberFormat().format(PrecioVentaP.value);


                MontoContadoP.value = parseFloat(MontoContado.value.replace(',', '.') * ValorUF.replace(',', '.'));
                MontoContadoP.value = redondeo(MontoContadoP.value, 0);
                MontoContadoP.value = '$ ' + Intl.NumberFormat().format(MontoContadoP.value);


                MontoCreditoP.value = parseFloat(MontoCredito.value.replace(',', '.') * ValorUF.replace(',', '.'));
                MontoCreditoP.value = redondeo(MontoCreditoP.value, 0);
                MontoCreditoP.value = '$ ' + Intl.NumberFormat().format(MontoCreditoP.value);


                PorcFinan.value = PorcFinan.value.replace('.', ',');
                Subsidio.value = Subsidio.value.replace('.', ',');
                BonoIntegracion.value = BonoIntegracion.value.replace('.', ',');
                BonoCaptacion.value = BonoCaptacion.value.replace('.', ',');
                PrecioVenta.value = PrecioVenta.value.replace('.', ',');
                MontoContado.value = MontoContado.value.replace('.', ',');
                MontoCredito.value = MontoCredito.value.replace('.', ',');

            } catch (e) {
                MostrarMensajeError(e.message);
            }
        }
        function redondeo(numero, decimales) {
            var flotante = parseFloat(numero);
            var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
            return resultado;
        }

        $(document).ready(function () {

            var navListItems = $('div.setup-panel div a'),
                allWells = $('.setup-content'),
                allNextBtn = $('.nextBtn');
            allBackBtn = $('.backBtn');

            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                    $item = $(this);

                if (!$item.hasClass('disabled')) {
                    navListItems.removeClass('btn-primary').addClass('btn-default');
                    $item.addClass('btn-primary');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });
            allNextBtn.click(function () {
                var curStep = $(this).closest(".setup-content"),
                    curStepBtn = curStep.attr("id"),
                    nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
                    curInputs = curStep.find("input[type='text'],input[type='url']"),
                    isValid = true;

                $(".form-group").removeClass("has-error");
                for (var i = 0; i < curInputs.length; i++) {
                    if (!curInputs[i].validity.valid) {
                        isValid = false;
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                }

                if (isValid)
                    nextStepWizard.removeAttr('disabled').trigger('click');
            });
            allBackBtn.click(function () {
                try {


                    var curStep = $(this).closest(".setup-content"),
                        curStepBtn = curStep.attr("id"),
                        backStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().prev().children("a"),
                        curInputs = curStep.find("input[type='text'],input[type='url']"),
                        isValid = true;

                    $(".form-group").removeClass("has-error");
                    for (var i = 0; i < curInputs.length; i++) {
                        if (!curInputs[i].validity.valid) {
                            isValid = false;
                            $(curInputs[i]).closest(".form-group").addClass("has-error");
                        }
                    }

                    if (isValid)
                        backStepWizard.removeAttr('disabled').trigger('click');

                } catch (e) {
                    alert(e.Message);
                }
            });
            $('div.setup-panel div a.btn-primary').trigger('click');
        });









    </script>
    <anthem:HiddenField ID="fechaproceso" runat="server" />
    <anthem:HiddenField ID="hdnRut" runat="server" AutoUpdateAfterCallBack="True" />
    <anthem:HiddenField ID="hdnTipoPersona" runat="server" />

    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<anthem:Image ID="Image1" runat="server" src='../Img/icons/favicon_xsmall.png' />--%>
                    Simulador Ejecutivo
                </span>


            </h4>
           <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos del Solicitante</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Rut</h5>
                                <anthem:Label ID="alblRutCompleto" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>

                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Nombre</h5>
                                <anthem:Label ID="alblNombreCompleto" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Mail</h5>
                                <anthem:Label ID="alblMail" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>

                        <div class="col-lg-1 small">
                            <anthem:Button ID="btnModificarCliente" runat="server" CssClass="btn-sm btn-success" Text="Modificar" OnClick="btnModificarCliente_Click" />

                        </div>
                        <div class="col-lg-2 small">
                            <anthem:LinkButton ID="btHistorialSimulaciones" runat="server" class="btn btn-sm btn-primary" OnClick="btHistorialSimulaciones_Click"><span class="glyphicon glyphicon-search"></span> Simulaciones Realizadas</anthem:LinkButton>
                        </div>
                    </div>
                    <div class="row">                        
                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Celular</h5>
                                <anthem:Label ID="alblCelular" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Fijo</h5>
                                <anthem:Label ID="alblFono" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 small">
                            <div class="form-group">
                                <h5>Fecha Nacimiento</h5>
                                <anthem:Label ID="alblFechaNacimiento" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                            </div>
                        </div>
                        <div class="col-lg-3"></div>
                    </div>

                </div>
            </div>

        </div>

    </div>
    <div class="stepwizard">
        <div class="stepwizard-row setup-panel">
            <div class="stepwizard-step">
                <a id="lnkCliente" runat="server" href="#step-1" type="button" class="btn btn-primary btn-circle" enable="false">1</a>
                <p>
                    Solicitante
                </p>
            </div>
            <div class="stepwizard-step">
                <a id="lnkGeneral" runat="server" href="#step-2" type="button" class="btn btn-default btn-circle" disabled="true">2</a>
                <p>
                    Información General
                </p>
            </div>
            <div class="stepwizard-step">
                <a id="lnkCredito" runat="server" href="#step-3" type="button" class="btn btn-default btn-circle" disabled="true">3</a>
                <p>
                    Información del Crédito
                </p>
            </div>
            <div class="stepwizard-step">
                <a href="#step-4" type="button" class="btn btn-default btn-circle" disabled="true">4</a>
                <p>
                    Resultado de la Simulación
                </p>
            </div>
        </div>
    </div>

    <div class="row setup-content" id="step-1">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-md-6  text-center">
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnAvanzaClientesSup" class="btn btn-primary nextBtn pull-right" runat="server" Style="visibility: hidden" />
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-lg-3">
                </div>
                <div class="col-lg-6 text-center">
                    <div class='form-group'>
                        <label for="txtRutCliente" class="col-lg-4 control-label">Rut Cliente</label>
                        <div class="col-lg-4">
                            <anthem:TextBox ID="txtRutCliente" runat="server" class="form-control" onchange="validaRut(this);" MaxLength="13" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <anthem:Button ID="btnBuscarCliente" runat="server" CssClass="btn-sm btn-success" Text="Buscar" OnClick="btnBuscarCliente_Click" AutoUpdateAfterCallBack="True" EnabledDuringCallBack="False" />
                            <anthem:Button ID="btnLimpiar" runat="server" CssClass="btn-sm btn-success" Text="Limpiar" AutoUpdateAfterCallBack="True" EnabledDuringCallBack="False" OnClick="btnLimpiar_Click" />
                        </div>

                    </div>
                </div>
                <div class="col-lg-3">
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6  text-center">
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnAvanzaClienteInf" class="btn btn-primary nextBtn pull-right" runat="server" Style="visibility: hidden" />
                </div>
            </div>
        </div>
    </div>
    <div class="row setup-content" id="step-2">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-md-6 text-center">
                    <button class="btn-sm btn-primary backBtn pull-left" type="button">
                        Atrás</button>
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnSiguienteGeneralSup" class="btn-sm btn-primary nextBtn pull-right" runat="server" Text="Siguiente" OnClick="btnSiguienteGeneral_Click" />
                </div>
            </div>
            <hr />
            
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos Generales</h3>
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="col-md-2 small">
                            <label for="ddlSucursal">Sucursal</label>
                            <anthem:DropDownList ID="ddlSucursal" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2 small">
                            <label for="ddlEjecutivo">Ejecutivo</label>
                            <anthem:DropDownList ID="ddlEjecutivo" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2 small">
                            <label for="ddlCooperativa">Instituciones</label>
                            <anthem:DropDownList ID="ddlCooperativa" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2 small">
                            <label for="ddlInmobiliaria">Inmobiliaria</label>
                            <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2 small">
                            <label for="ddlProyecto">Proyecto</label>
                            <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2 small">
                            <label for="ddlComuna">Comuna</label>
                            <anthem:DropDownList ID="ddlComuna" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                        </div>


                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6 text-center">
                    <button class="btn-sm btn-primary backBtn pull-left" type="button">
                        Atrás</button>
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnSiguienteGeneralInf" class="btn-sm btn-primary nextBtn pull-right" runat="server" Text="Siguiente" OnClick="btnSiguienteGeneral_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="row setup-content" id="step-3">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-md-6 text-center">
                    <button class="btn-sm btn-primary backBtn pull-left" type="button">
                        Atrás</button>
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnSiguientgeCreditoSup" class="btn-sm btn-primary nextBtn pull-right" runat="server" Text="Siguiente" OnClick="btnSiguientgeCredito_Click" />
                </div>
            </div>
            <hr />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos del Crédito</h3>
                </div>
                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-4 col-lg-3 small">
                            <div class="form-group">
                                <label for="ddlTipoFinanciamiento" class="control-label">Tipo Financiamiento</label>
                                <anthem:DropDownList ID="ddlTipoFinanciamiento" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoFinanciamiento_SelectedIndexChanged"></anthem:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlProducto" class="control-label">Producto</label>
                                <anthem:DropDownList ID="ddlProducto" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged"></anthem:DropDownList>
                            </div>

                            <div class='form-group'>
                                <label for="ddlObjetivo" class="control-label">Objetivo</label>
                                <anthem:DropDownList ID="ddlObjetivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlObjetivo_SelectedIndexChanged"></anthem:DropDownList>
                            </div>

                            <div class='form-group'>
                                <label for="ddlDestino" class="control-label">Destino</label>
                                <anthem:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5 col-lg-6 small ">

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtPrecioVenta">Precio de Venta</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPrecioVenta" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('PrecioVenta');" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPrecioVentaPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:DropDownList ID="ddlSubsidio" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlSubsidio_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoSubsidio" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('MontoSubsidio');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoSubsidioPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtBonoIntegracion">Bono Integración</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoIntegracion" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('BonoIntegracion');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoIntegracionPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtBonoCaptacion">Bono Captación</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoCaptacion" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('BonoCaptacion');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoCaptacionPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtMontoContado">Monto Contado (Ahorro/Pie)</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoContado" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('MontoContado');" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoContadoPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" onchange="ActualizaMontos('MontoContado');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtMontoCredito">Monto Crédito</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCredito" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('MontoCredito');" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCreditoPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" onchange="ActualizaMontos('MontoCredito');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtPrecioVenta">% de Financiamiento</label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPorcentajeFinanciamiento" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontos('PorcentajeFinanciamiento');" runat="server" Enabled="False"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3 col-lg-3 small ">

                            <div class="row">

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="ddlMesesGracia" class="control-label">Meses de Gracia</label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:DropDownList ID="ddlMesesGracia" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="ddlPlazo" class="control-label">Plazo</label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:DropDownList ID="ddlPlazo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlPlazo_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="ddlPlazo" class="control-label">Plazo Flexible</label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:DropDownList ID="ddlPlazoFlexible" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:CheckBox ID="chkIndTasaEspecial" AutoUpdateAfterCallBack="true" AutoCallBack="true" class="form-check-input" runat="server" Text="Tasa Especial" OnCheckedChanged="chkIndTasaEspecial_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaEspecial" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server" Enabled="False"></anthem:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:CheckBox ID="chIndDfl2" AutoUpdateAfterCallBack="true" class="form-check-input" runat="server" Text="DFL-2" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <anthem:CheckBox ID="chkViviendaSocial" runat="server" class="form-check-input" Text="Vivienda Social" AutoUpdateAfterCallBack="True" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Seguros</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Inmueble</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="ddlTipoInmueble" class="control-label">Tipo Inmueble</label>
                                        <anthem:DropDownList ID="ddlTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoInmueble_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="ddlAntiguedad" class="control-label">Antigüedad</label>
                                        <anthem:DropDownList ID="ddlAntiguedad" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguro de Incendio</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="ddlSeguroIncendio">Poliza Incendio</label>
                                        <anthem:DropDownList ID="ddlSeguroIncendio" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlSeguroIncendio_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtMontoAsegurado">Monto Asegurado</label>
                                        <anthem:TextBox ID="txtMontoAsegurado" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTasaSeguroInc">Tasa Seguro</label>
                                        <anthem:TextBox ID="txtTasaSeguroInc" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguro de Desgravamen</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="txtNroCodeudores">Nro. Codeudores</label>
                                        <anthem:TextBox ID="txtNroCodeudores" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="ddlSeguroDesgravamen">Poliza Desgravamen</label>
                                        <anthem:DropDownList ID="ddlSeguroDesgravamen" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlSeguroDesgravamen_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTasaSeguroDes">Tasa Seguro</label>
                                        <anthem:TextBox ID="txtTasaSeguroDes" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguro de Cesantía</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="ddlSeguroCesantia">Seguro Cesantía</label>
                                        <anthem:DropDownList ID="ddlSeguroCesantia" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlSeguroCesantia_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTasaSeguroCes">Tasa Seguro</label>
                                        <anthem:TextBox ID="txtTasaSeguroCes" runat="server" class="form-control input-sm" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6 text-center">
                    <button class="btn-sm btn-primary backBtn pull-left" type="button">
                        Atrás</button>
                </div>
                <div class="col-md-6 text-center">
                    <anthem:Button ID="btnSiguientgeCreditoInf" class="btn-sm btn-primary nextBtn pull-right" runat="server" Text="Siguiente" OnClick="btnSiguientgeCredito_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row setup-content" id="step-4">
        <div class="col-lg-12">
            <div class="row">
                <button class="btn-sm btn-primary backBtn pull-left" type="button">
                    Atrás</button>
            </div>
            <hr />
            <div class="row center">

                <div class="col-md-1">
                </div>
                <div class="col-md-2">
                    <anthem:Button ID="btnGGOO" runat="server" CssClass="btn btn-sm btn-success" Text="Gastos Operacionales" OnClick="btnGGOO_Click" />
                </div>
                <div class="col-md-2">
                    <anthem:Button ID="btnSimular" runat="server" CssClass="btn btn-sm btn-success" Text="  Simular  " OnClick="btnSimular_Click" />
                </div>
                <div class="col-md-2">
                    <anthem:Button ID="btnImprimir" runat="server" CssClass="btn btn-sm btn-success" Text="Imprimir Reporte Simulación" OnClick="btnImprimir_Click" EnableCallBack="False" TextDuringCallBack="Generando Reporte" PreCallBackFunction="waitingDialog.show('Generando Resporte');" PostCallBackFunction="" />
                </div>
                <div class="col-md-2">
                    <anthem:LinkButton ID="btnEnviarSimulacion" CssClass="btn btn-sm btn-success" runat="server" OnClick="btnEnviarSimulacion_Click" EnabledDuringCallBack="False" TextDuringCallBack="Enviando Correo..."><span class="glyphicon glyphicon-send "></span> Enviar Simulación Via Mail</anthem:LinkButton>
                </div>
                <div class="col-md-2">
                    <anthem:Button ID="btnGenerarSolicitud" runat="server" CssClass="btn btn-sm btn-success" Text="Generar Solicitud de Crédito" OnClick="btnGenerarSolicitud_Click" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea generar la Solicitud de Crédito?',this);" AutoUpdateAfterCallBack="True" EnabledDuringCallBack="False" TextDuringCallBack="Generando Solicitud..." />
                </div>
                <div class="col-md-1">
                </div>
            </div>

            <hr />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Resultado de la Simulación</h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-12 small">
                            <anthem:GridView runat="server" ID="gvSimulacion" AutoGenerateColumns="false" Width="100%"
                                PageSize="5" AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                    <asp:BoundField DataField="TasaAnual" HeaderText="Tasa" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="SegDesgravamen" HeaderText="Seg. Desgravamen" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegIncendio" HeaderText="Seg. Incendio" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegCesantiaMinvu" HeaderText="Seg. Cesantía" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoUF" HeaderText="Dividendo Sin Seguros UF" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoPesos" HeaderText="Dividendo Sin Seguros $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="DividendoTotal" HeaderText="Dividendo Final UF" />
                                    <asp:BoundField DataField="DividendoTotalPesos" HeaderText="Dividendo Final $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="CAE" HeaderText="CAE" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="RentaMininaRequerida" HeaderText="Renta Mínima Requerida" DataFormatString="{0:C}" />

                                </Columns>
                            </anthem:GridView>
                          
                        </div>

                    </div>
                      <div class="row" id="DivSimulacionFlexible">
                        <div class="col-md-12">
                            <h5>Detalle de Dividendo Flexible</h5>
                              <anthem:GridView runat="server" ID="gvSimulacionFlexible" AutoGenerateColumns="false" Width="100%"
                                PageSize="5" AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                    <asp:BoundField DataField="TasaAnual" HeaderText="Tasa" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="SegDesgravamen" HeaderText="Seg. Desgravamen" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegIncendio" HeaderText="Seg. Incendio" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegCesantiaMinvu" HeaderText="Seg. Cesantía" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoUF" HeaderText="Dividendo Sin Seguros UF" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoPesos" HeaderText="Dividendo Sin Seguros $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="DividendoTotal" HeaderText="Dividendo Final UF" />
                                    <asp:BoundField DataField="DividendoTotalPesos" HeaderText="Dividendo Final $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="CAE" HeaderText="CAE" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="RentaMininaRequerida" HeaderText="Renta Mínima Requerida" DataFormatString="{0:C}" />

                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>
                    <div class="row" id="DivSimulacionMinvu">
                        <div class="col-md-12">
                            <h5>Detalle de Dividendos por pago antes del día 10 del mes (Subvención mensual por pago oportuno)</h5>
                            <anthem:GridView runat="server" ID="gvSimulacionMinvu" AutoGenerateColumns="false" Width="100%"
                                PageSize="5" AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                    <asp:BoundField DataField="TasaAnual" HeaderText="Tasa" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="SegDesgravamen" HeaderText="Seg. Desgravamen" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegIncendio" HeaderText="Seg. Incendio" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="SegCesantiaMinvu" HeaderText="Seg. Cesantía" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoMinvuUf" HeaderText="Dividendo Minvu Sin Seguros UF" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoMinvuPesos" HeaderText="Dividendo Sin Seguros $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="DividendoMinvuTotal" HeaderText="Dividendo Final UF" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="DividendoMinvuTotalPesos" HeaderText="Dividendo Final $" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="CAEMinvu" HeaderText="CAE" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="CTCMinvu" HeaderText="CTC" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="RentaMininaRequeridaMinvu" HeaderText="Renta Mínima Requerida" DataFormatString="{0:C}" />

                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
