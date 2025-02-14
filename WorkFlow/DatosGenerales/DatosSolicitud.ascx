<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosSolicitud.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosSolicitud" %>

<script>

    function DesplegarDatosAdicionales(Visible) {
        if (Visible == "1") {
            $('#DivDatosAdicionales').show(0);
        } else {
            $('#DivDatosAdicionales').hide(0);
        }
    }


    function ActualizaMontosPreaprobacion(campo) {
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
            var ValorUF = document.getElementById('<%= txtValorUF.ClientID%>').value;

            if (campo === 'PrecioVenta') {

            }

            if (PrecioVenta.value === '') {
                MensajeEnControl(PrecioVenta.id, "Dato Obligatorio");
                PrecioVenta.value = '';
                Subsidio.value = '';
                BonoIntegracion.value = '';
                BonoCaptacion.value = '';
                MontoContado.value = '';
                MontoCredito.value = '';
                PorcFinan.value = '';

                return;
            }
            // PrecioVenta.value = PrecioVenta.value.replace(',', '.')
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
            alert(e.message);
        }
    }
    function redondeo(numero, decimales) {
        var flotante = parseFloat(numero);
        var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
        return resultado;
    }

</script>
<div class="row">
    <div class="col-md-12">
        <h6>
            <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
            </span>

        </h6>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos de la Solicitud</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xl-2 col-lg-2 col-md-12 col-xs-12 small vr">
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
                        <div id="DivDatosAdicionales">
                            <div class='form-group'>
                                <label for="ddlFinalidad" class="control-label">Finalidad</label>
                                <anthem:DropDownList ID="ddlFinalidad" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                            </div>

                            <div class='form-group'>
                                <label for="ddlUtilidad" class="control-label">Utilidad</label>
                                <anthem:DropDownList ID="ddlUtilidad" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-5 col-lg-5 col-md-12 col-xs-12 small vr">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtPrecioVenta">Precio de Venta</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPrecioVenta" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('PrecioVenta');" runat="server"></anthem:TextBox>

                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPrecioVentaPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <anthem:DropDownList ID="ddlSubsidio" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlSubsidio_SelectedIndexChanged1"></anthem:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoSubsidio" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('MontoSubsidio');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoSubsidioPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtMontoContado">Bono Integración</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoIntegracion" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('BonoIntegracion');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoIntegracionPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtMontoContado">Bono Captación</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoCaptacion" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('BonoCaptacion');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtBonoCaptacionPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtMontoContado">Monto Contado (Ahorro/Pie)</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoContado" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('MontoContado');" runat="server"></anthem:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoContadoPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" onchange="ActualizaMontosPreaprobacion('MontoContado');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtMontoCredito">Monto Crédito</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCredito" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('MontoCredito');" runat="server"></anthem:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCreditoPesos" onKeyPress="return SoloNumeros(event,this.value,0,this);" onchange="ActualizaMontosPreaprobacion('MontoCredito');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtPrecioVenta">% de Financiamiento</label>
                                </div>
                                <div class="col-lg-3">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPorcentajeFinanciamiento" onKeyPress="return SoloNumeros(event,this.value,4,this);" onchange="ActualizaMontosPreaprobacion('PorcentajeFinanciamiento');" runat="server" Enabled="False"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-12 col-xs-12 small vr">
                        <div class='form-group'>
                            <label for="ddlMesesGracia" class="control-label">Meses de Gracia</label>
                            <anthem:DropDownList ID="ddlMesesGracia" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                        </div>
                        <div class='form-group'>
                            <label for="ddlPlazo" class="control-label">Plazo</label>
                            <anthem:DropDownList ID="ddlPlazo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlPlazo_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                        <div class='form-group'>
                            <label for="ddlPlazoFlexible" class="control-label">Plazo Flexible</label>
                            <anthem:DropDownList ID="ddlPlazoFlexible" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                        </div>
                        <div class='form-group'>
                            <label for="txtTasaMensual" class="control-label">Tasa Mensual</label>
                            <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaMensual" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                        </div>
                        <div class='form-group'>
                            <label for="chIndDfl2" class="control-label"></label>
                            <anthem:CheckBox ID="chIndDfl2" AutoUpdateAfterCallBack="true" class="control-label" runat="server" Text="Beneficio DFL-2" />
                        </div>
                        <div class='form-group'>
                            <label for="chkViviendaSocial" class="control-label"></label>
                            <anthem:CheckBox ID="chkViviendaSocial" runat="server" class="form-check-input" Text="Vivienda Social" AutoUpdateAfterCallBack="True" />
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-12 col-xs-12 small">
                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtCAE">CAE</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtCAE" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtCTC">CTC</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtCTC" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtValorCuota">Valor Cuota</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtValorCuota" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtValorDividendo">Dividendo Total</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtValorDividendo" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtValorCuotaFlexible">Valor Cuota Flexible</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtValorCuotaFlexible" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label for="txtValorDividendoFlexible">Dividendo Flexible Total</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtValorDividendoFlexible" ReadOnly="true" runat="server"></anthem:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>









<br />






<div class="row">
    <div class="col-lg-12 text-center">
        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarSolicitud" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
            OnClick="btnGuardarSolicitud_Click" TextDuringCallBack="Guardando..." />
        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarSolicitud" runat="server" Text="Cancelar" CssClass="btn-success btn-sm"
            OnClick="btnCancelarSolicitud_Click" TextDuringCallBack="Cancelando..." />
    </div>
</div>
<%-- Datos PAC Inhabilitados Para las Mutuarias --%>
<div class="row form-horizontal" hidden="hidden">
    <div class="col-md-4 small">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos PAC</h3>
            </div>
            <div class="panel-body center">

                <div class='form-group'>
                    <anthem:CheckBox ID="chkIndPac" AutoUpdateAfterCallBack="true" class="col-lg-4 control-label" runat="server" Text="PAC" />

                    <div class="col-lg-8">
                    </div>
                </div>
                <div class='form-group'>
                    <label for="ddlBancoPac" class="col-lg-4 control-label">Banco</label>
                    <div class="col-lg-8">
                        <anthem:DropDownList ID="ddlBancoPac" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                    </div>
                </div>
                <div class='form-group'>
                    <label for="txtCuentaPac" class="col-lg-4 control-label">Cuenta Corriente</label>
                    <div class="col-lg-8">
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtCuentaPac" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
