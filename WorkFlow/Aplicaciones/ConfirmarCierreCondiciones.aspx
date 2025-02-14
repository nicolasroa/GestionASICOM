<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="ConfirmarCierreCondiciones.aspx.cs" Inherits="WorkFlow.Aplicaciones.ConfirmarCierreCondiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Confirmacion de Cierre de Condiciones de la Solicitud Nº
                        <anthem:Label ID="lblNumeroSolicitud" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h6>
                                <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
                                </span>
                                <anthem:Label ID="lblEstadoCierre" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                            </h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Datos del Participante</h3>
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
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-xs-6 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Crédito</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblTipoFinanciamiento">Tipo de Financiamiento</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblTipoFinanciamiento" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblProducto">Producto</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblProducto" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblObjetivo">Objetivo</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblObjetivo" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblDestino">Destino</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblDestino" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblPlazo">Plazo</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblPlazo" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblTasa">Tasa Mensual</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblTasa" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblGracia">Periodo de Gracia</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="lblGracia" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="alblMtoDeudaGarantiaMO">Deuda/Garantia</label>
                                        </div>
                                        <div class="col-lg-6">
                                            <anthem:Label ID="alblMtoDeudaGarantiaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-xs-6 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Montos</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblValorVentaMO">Precio de Venta</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblValorVentaMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblValorVentaCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblValorVentaMO">Subsidio</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMontoSubsidioMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMontoSubsidioCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblMontoBonoIntegracionMO">Bono Integración</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMontoBonoIntegracionMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMontoBonoIntegracionCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblMontoBonoCaptacionMO">Bono Captación</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMontoBonoCaptacionMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMontoBonoCaptacionCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblValorVentaMO">Monto Solicitado</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMontoSolicitadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMontoSolicitadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblPagoContadoMO">Monto Contado (Ahorro/Pie)</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblPagoContadoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblPagoContadoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblMtoDividendoNetoMO">Dividendo Neto</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMtoDividendoNetoMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMtoDividendoNetoCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <label for="lblMtoDividendoConSeguroMO">Dividendo con Seguros</label>
                                        </div>
                                        <div class="col-lg-4">
                                            <anthem:Label ID="lblMtoDividendoConSeguroMO" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <anthem:Label ID="lblMtoDividendoConSeguroCLP" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-xs-6 small">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguros</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <anthem:GridView ID="gvSeguros" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed"
                                                Width="100%">
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
                    <div class="row">
                        <div class="col-lg-12 text-center">

                            <anthem:LinkButton ID="btnAprobarCondiciones" AutoUpdateAfterCallBack="true" class="btn btn-primary btn-success" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Aprobar las Condiciones de su Crédito?',this);" runat="server" OnClick="btnAprobarCondiciones_Click"><span class="glyphicon glyphicon-ok-circle"></span> Aprobar Condiciones</anthem:LinkButton>
                            <anthem:LinkButton ID="btnRechazarCondiciones" AutoUpdateAfterCallBack="true" class="btn btn-primary btn-danger" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Rechazar las Condiciones de su Crédito?',this);" runat="server" OnClick="btnRechazarCondiciones_Click"><span class="glyphicon glyphicon-ban-circle"></span> Rechazar Condiciones</anthem:LinkButton>
                        </div>
                    </div>


                </div>
            </div>



        </div>
    </div>



</asp:Content>
