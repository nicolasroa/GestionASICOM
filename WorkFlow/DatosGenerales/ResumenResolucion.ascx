<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResumenResolucion.ascx.cs" Inherits="WorkFlow.DatosGenerales.ResumenResolucion" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<script>

</script>



<div class="row">
    <div class="col-lg-8">

        <ul class="nav small nav-tabs2">
            <li class="active"><a href="#tabResumen" data-toggle="tab" aria-expanded="true">Resumen</a></li>
            <li class=""><a href="#tabEstadoSituacion" data-toggle="tab" aria-expanded="false">Estado de Situación</a></li>
            <li class=""><a href="#tabParticipantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
        </ul>
    </div>
    <div class="col-md-4">
        <h6>
            <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
            </span>
            <br>
        </h6>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <div id="tabControlResumen" class="tab-content">

            <div class="tab-pane fade active in" id="tabResumen">
                <div class="row">
                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-xs-12 small">

                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Crédito</h3>
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblTipoFinanciamiento">Tipo de Financiamiento</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblTipoFinanciamiento" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblProducto">Producto</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblProducto" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblObjetivo">Objetivo</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblObjetivo" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblDestino">Destino</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblDestino" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblPlazo">Plazo</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblPlazo" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblTasa">Tasa Mensual</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblTasa" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="lblGracia">Periodo de Gracia</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="lblGracia" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <label for="alblMtoDeudaGarantiaMO">Deuda/Garantia</label>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                        <anthem:Label ID="alblMtoDeudaGarantiaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-xs-12 small">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Montos</h3>
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblValorVentaMO">Precio de Venta</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblValorVentaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblValorVentaCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblValorVentaMO">Subsidio</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoSubsidioMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoSubsidioCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblMontoBonoIntegracionMO">Bono Integración</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoBonoIntegracionMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoBonoIntegracionCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblMontoBonoCaptacionMO">Bono Captación</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoBonoCaptacionMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoBonoCaptacionCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblValorVentaMO">Monto Solicitado</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoSolicitadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMontoSolicitadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblPagoContadoMO">Monto Contado (Ahorro/Pie)</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblPagoContadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblPagoContadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblMtoDividendoNetoMO">Dividendo Neto</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMtoDividendoNetoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMtoDividendoNetoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <label for="lblMtoDividendoConSeguroMO">Dividendo con Seguros</label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMtoDividendoConSeguroMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                        <anthem:Label ID="lblMtoDividendoConSeguroCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 col-xs-12 small">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Seguros</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <anthem:GridView ID="gvSeguros" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="table table-condensed">
                                            <RowStyle CssClass="GridItem" />
                                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                            <AlternatingRowStyle CssClass="GridAtlItem" />
                                            <Columns>
                                                <asp:BoundField DataField="DescripcionSeguro" HeaderText="Seguro" />
                                                <asp:BoundField DataField="TasaMensual" HeaderText="Tasa Mensual" DataFormatString="{0:F4}" />
                                                <asp:BoundField DataField="PrimaMensual" HeaderText="Prima Mensual" DataFormatString="{0:F4}" />
                                                <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" />
                                            </Columns>
                                        </anthem:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>


                </div>




                <div class="row small">
                    <div class="col-lg-12">
                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Patrimonio</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblTotalPasivosPrincipalMO">Pasivos Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalPasivosPrincipalMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalPasivosPrincipalCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblTotalActivosPrincipalMO">Activos Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalActivosPrincipalMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalActivosPrincipalCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblPatrimonioPrincipal">Patrimonio Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblPatrimonioPrincipal" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblPatrimonioPrincipalCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblTotalPasivosComplementadoMO">Pasivos Complementados</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalPasivosComplementadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalPasivosComplementadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblTotalActivosComplementadoMO">Activos Complementados</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalActivosComplementadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblTotalActivosComplementadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblPatrimonioComplementado">Patrimonio Complementado</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblPatrimonioComplementado" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblPatrimonioComplementadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblMtoRentaNecesariaMO">Renta Necesaria</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblMtoRentaNecesariaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblMtoRentaNecesariaCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblRentaPrincipal">Renta Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaPrincipal" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaPrincipalCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblRentaPrincipal">Renta Codeudor</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaCodeudor" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaCodeudorCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                            <label for="lblRentaComplementada">Renta Complementada</label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaComplementada" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                            <anthem:Label ID="lblRentaComplementadaCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Indicadores</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblDividendoRentaPrincipalMO">Dividendo/Renta Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblDividendoRentaPrincipalMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblDividendoRentaCodeudorlMO">Dividendo/Renta Codeudor</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblDividendoRentaCodeudorlMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblDividendoRentaComplementadaMO">Dividendo/Renta Complementada</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblDividendoRentaComplementadaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblCargaFinancieraPrincipalMO">Carga Financiera Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblCargaFinancieraPrincipalMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblCargaFinancieraComplementadaMO">Carga Financiera Complementada</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblCargaFinancieraComplementadaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblCargaFinancieraHipotecariaPrincipalMO">Carga Financiera Hipotecaria Deudor Principal</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblCargaFinancieraHipotecariaPrincipalMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblCargaFinancieraHipotecariaCodeudorMO">Carga Financiera Hipotecaria Codeudor</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblCargaFinancieraHipotecariaCodeudorMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-8 col-lg-8 col-md-8 col-xs-8">
                                            <label for="lblCargaFinancieraHipotecariaPrincipalMO">Carga Financiera Hipotecaria Complementada</label>
                                        </div>
                                        <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4">
                                            <anthem:Label ID="lblCargaFinancieraHipotecariaComplementadaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="tab-pane fade" id="tabEstadoSituacion">
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8 small">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Activos</h3>
                            </div>
                            <div class="panel-body small">

                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Deudor</label>
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Codeudor</label>
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Fiador/Aval</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                </div>
                                <div id="DivActivosSolicitud" runat="server"></div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                        <label>Total Activos</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalActivosDeudor" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalActivosDeudorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalActivosCodeudor" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalActivosCodeudorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalActivosAval" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalActivosAvalUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                        <label>TOTAL COMPLEMENTADO</label>
                                    </div>
                                    <div class="col-xl-5 col-lg-5 col-md-5 col-xs-5 text-center">
                                        <anthem:Label ID="lblTotalActivosComplementados" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4 text-center">
                                        <anthem:Label ID="lblTotalActivosComplementadosUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8 small">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Pasivos</h3>
                            </div>
                            <div class="panel-body small">

                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3">
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Deudor</label>
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Codeudor</label>
                                    </div>
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-center">
                                        <label>Fiador/Aval</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <label>$</label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <label>UF</label>
                                    </div>
                                </div>
                                <div id="DivPasivosSolicitud" runat="server"></div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                        <label>Total Pasivos</label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalPasivosDeudor" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalPasivosDeudorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalPasivosCodeudor" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalPasivosCodeudorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center">
                                        <anthem:Label ID="lblTotalPasivosAval" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center">
                                        <anthem:Label ID="lblTotalPasivosAvalUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left">
                                        <label>TOTAL COMPLEMENTADO</label>
                                    </div>
                                    <div class="col-xl-5 col-lg-5 col-md-5 col-xs-5 text-center">
                                        <anthem:Label ID="lblTotalPasivosComplementados" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-xs-4 text-center">
                                        <anthem:Label ID="lblTotalPasivosComplementadosUF" AutoUpdateAfterCallBack="true" runat="server" CssClass="control-label"></anthem:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
            </div>


            <div id="tabParticipantes" class="tab-pane fade">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Resumen de Participantes</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 col-lg-12 ">
                                <div class="form-group">
                                    <anthem:GridView runat="server" ID="gdvParticipantes" AutoGenerateColumns="false" Width="100%"
                                        PageSize="5" AllowSorting="True" AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Participación" />
                                            <asp:BoundField DataField="RutCompleto" HeaderText="Rut" />
                                            <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Completo" />
                                            <asp:BoundField DataField="AntiguedadLaboral" HeaderText="Antigüedad Laboral" />
                                            <asp:BoundField DataField="EdadPlazo" HeaderText="Edad + Plazo" />
                                            <asp:BoundField DataField="RentaPromedio" HeaderText="Renta Promedio" DataFormatString="{0:C}" />
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                            </div>
                        </div>
                        <div id="DivParticipantes" runat="server"></div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</div>





