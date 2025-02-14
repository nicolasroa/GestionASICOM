<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosFichaEvaluacion.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosFichaEvaluacion" %>
<div style="position: relative; width: 100%; height: 100%; margin: 0 auto;">


    <div class="row">
        <div class="col-lg-4">
            <div class='form-group'>
                <label for="ddlTipoPersona">Valor de la UF</label>
                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblValorUF" CssClass="form-control" runat="server" UpdateAfterCallBack="True"></anthem:Label>
            </div>
        </div>
        <div class="col-lg-4">

            <div class='form-group'>
                <label for="lblEjecutivoNombre">Ejecutivo de Negocio Hipotecario</label>
                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoNombre" CssClass="form-control" runat="server"></anthem:Label>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Participantes</h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Flujos Mensuales</h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <div id="DivFlujosMensuales" runat="server"></div>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

  

    <div class="row">
     
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Inmuebles</h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <anthem:GridView ID="gvPropiedades" runat="server" AutoGenerateColumns="False"
                                Width="100%" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                                    <asp:BoundField DataField="DescripcionAntiguedad" HeaderText="Antigüedad" />
                                    <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                                </Columns>
                            </anthem:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos del Crédito</h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="tdInfo">Tipo de Financiamiento</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblTipoFinanciamiento" runat="server"></anthem:Label></td>
                                    <td class="tdInfo">Destino</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblDestino" runat="server"></anthem:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Producto</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblProducto" runat="server"></anthem:Label></td>
                                    <td class="tdInfo">Plazo</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblPlazo" runat="server"></anthem:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Objetivo</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblObjetivo" runat="server"></anthem:Label></td>
                                    <td class="tdInfo">Tasa Mensual</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblTasa" runat="server"></anthem:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Precio de Venta</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblPrecioVenta" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblPrecioVentaPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                                    <td class="tdInfo">Meses de Gracia</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblGracia" runat="server"></anthem:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Monto Contado</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoContado" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoContadoPesos" runat="server"></anthem:Label>)</td>
                                    <td class="tdInfo">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblNombreSubsidio" runat="server"></anthem:Label></td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoSubsidio" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoSubsidioPesos" runat="server"></anthem:Label>)</td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Monto del Crédito</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoCredito" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoCreditoPesos" runat="server"></anthem:Label>)</td>
                                    <td class="tdInfo">Bono Integración</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoIntegracion" runat="server" UpdateAfterCallBack="True"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoIntegracionPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">% de Financiamiento</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblPorcFinanciamiento" runat="server"></anthem:Label></td>
                                    <td class="tdInfo">Bono Captación</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoCaptacion" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoCaptacionPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Dividendo Neto</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoNeto" runat="server" UpdateAfterCallBack="True"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoNetoPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                                    <td class="tdInfo">Dividendo Con Seguros</td>
                                    <td class="tdInfoData">
                                        <anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoConSeguros" runat="server" UpdateAfterCallBack="True"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoConSegurosPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
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
                                    <asp:BoundField DataField="DescripcionCompañia" HeaderText="Compañía" />

                                </Columns>
                            </anthem:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   


</div>
