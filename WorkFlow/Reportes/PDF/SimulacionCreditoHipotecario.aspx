<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="SimulacionCreditoHipotecario.aspx.cs" Inherits="WorkFlow.Reportes.PDF.SimulacionCreditoHipotecario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="position: relative; width: 100%; height: 100%; margin: 0 auto; page-break-after: always">
        <table style="width: 100%;">
            <tr>
                <td class="Titulo">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%">
                               </td>
                            <td style="width: 60%">Simulación Crédito Hipotecario</td>
                            <td style="width: 20%"></td>
                        </tr>
                    </table>

                </td>

            </tr>
            <tr>
                <td class="tdInfoData">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblValorUF" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                            <td class="auto-style1" style="text-align: right">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblFecha" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdTitulo02">Datos del Cliente</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="tdInfo">Nombre del Cliente</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblNombreCompleto" runat="server"></anthem:Label></td>
                            <td class="tdInfo">Fono</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblFono" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Rut Cliente</td>
                            <td class="tdInfoData">

                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblRutCompleto" runat="server"></anthem:Label></td>
                            <td class="tdInfo">Celular</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblCelular" runat="server"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Mail</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMail" runat="server"></anthem:Label></td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td class="tdTitulo02">Datos del Inmueble</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="tdInfo">Tipo de Inmueble</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblTipoInmueble" runat="server"></anthem:Label></td>
                            <td class="tdInfo">Comuna</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblComuna" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Inmobiliaria</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblInmobiliaria" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                            <td class="tdInfo">Proyecto</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblProyecto" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Institución</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCooperativa" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%; vertical-align: top;">
            <tr>
                <td class="tdTitulo02">Datos del Crédito</td>
            </tr>
            <tr>
                <td>
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
                            <td class="tdInfo">Plazo Seleccionado</td>
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
                            <td class="tdInfo">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblNombreSubsidio" runat="server"></anthem:Label></td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoSubsidio" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoSubsidioPesos" runat="server"></anthem:Label>)</td>
                            <td class="tdInfo">Cantidad de Deudores</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblDeudores" runat="server"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Bono Integración</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoIntegracion" runat="server" UpdateAfterCallBack="True"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoIntegracionPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                            <td class="tdInfo">Seguro de Desgravamen</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblSegDesgravamen" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Bono Captación</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoCaptacion" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="True" ID="lblMontoBonoCaptacionPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label>)</td>
                            <td class="tdInfo">Seguro de Incendio</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblSegIncendio" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Monto Contado (Ahorro/Pie)</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoContado" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoContadoPesos" runat="server"></anthem:Label>)</td>
                            <td class="tdInfo">Seguro de Cesantía</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblSegCesantia" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td class="tdInfo">Monto del Crédito</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoCredito" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblMontoCreditoPesos" runat="server"></anthem:Label>)</td>
                            <td class="tdInfo">% de Financiamiento</td>
                            <td class="tdInfoData">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblPorcFinanciamiento" runat="server"></anthem:Label></td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="tdTitulo02">Detalle de Dividendos</td>
            </tr>
            <tr>
                <td class="tdFormulario01">
                    <anthem:GridView ID="gvDetalleDiv" runat="server" AutoGenerateColumns="False"
                        Width="100%">
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
                            <asp:BoundField DataField="DividendoTotal" HeaderText="Dividendo Final" />
                            <asp:BoundField DataField="DividendoTotalPesos" HeaderText="Dividendo Final $" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="CAE" HeaderText="CAE" DataFormatString="{0:F4}" />
                            <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F4}" />
                            <asp:BoundField DataField="RentaMininaRequerida" HeaderText="Renta Mínima Requerida" DataFormatString="{0:C}" />
                        </Columns>
                    </anthem:GridView>

                </td>
            </tr>

            <tr>
                <td class="tdFormulario01">
                    <div id="DivDividendosMinvu" runat="server" visible="false">
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdTitulo02">Detalle de Dividendos por pago antes del día 10 del mes (Subvención mensual por pago oportuno)</td>
                            </tr>
                            <tr>
                                <td class="tdFormulario01">
                                    <anthem:GridView ID="gvDetalleDivMinvu" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
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
                                            <asp:BoundField DataField="DividendoMinvuUf" HeaderText="Dividendo Sin Seguros UF" DataFormatString="{0:F4}" />
                                            <asp:BoundField DataField="DividendoMinvuPesos" HeaderText="Dividendo Sin Seguros $" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="DividendoMinvuTotal" HeaderText="Dividendo Final UF" DataFormatString="{0:F4}" />
                                            <asp:BoundField DataField="DividendoMinvuTotalPesos" HeaderText="Dividendo Final $" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="CAEMinvu" HeaderText="CAE" DataFormatString="{0:F3}" />
                                            <asp:BoundField DataField="CTCMinvu" HeaderText="CTC" DataFormatString="{0:F4}" />
                                            <asp:BoundField DataField="RentaMininaRequeridaMinvu" HeaderText="Renta Mínima Requerida" DataFormatString="{0:C}" />

                                        </Columns>
                                    </anthem:GridView>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo02" style="width: 30%">Ahorro Pago Oportuno</td>
                                            <td class="auto-style1"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:GridView ID="gvAhorroMinvu" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <RowStyle CssClass="GridItem" />
                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                                        <asp:TemplateField HeaderText="Ahorro UF" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <anthem:Label ID="lblAhorroUF" runat="server" AutoUpdateAfterCallBack="true" Text='<%#  "UF " + string.Format("{0:F4}", (decimal.Parse(Eval("DividendoTotal").ToString()) -decimal.Parse(Eval("DividendoMinvuTotal").ToString()))) %>'></anthem:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Ahorro $" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <anthem:Label ID="lblAhorroPesos" runat="server" AutoUpdateAfterCallBack="true" Text='<%#  string.Format("{0:C}", (decimal.Parse(Eval("DividendoTotalPesos").ToString()) -decimal.Parse(Eval("DividendoMinvuTotalPesos").ToString()))) %>'></anthem:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </anthem:GridView>

                                            </td>
                                            <td class="ParrafoLegal" style="border-width: 2px; border-style: solid; vertical-align: top">La subvención se otorgará segun los siguientes tramos:<br />
                                                a) De un monto equivalente al 20% de cada dividendo, para los deudores cuyo crédito hipotecario sea de hasta 500 Unidades de Fomento.<br />
                                                b) De un monto equivalente al 15% de cada dividendo, para los deudores cuyo crédito hipotecario sea de más de 500 y hasta 900 Unidades de Fomento.<br />
                                                c) De un monto equivalente al 10% de cada dividendo, para los deudores cuyo crédito hipotecario sea de mas de 900 y hasta 1.200 Unidades de Fomento.</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdFormulario01">
                    <table style="width: 100%;">
                        <tr>
                            <td class="tdTitulo02">Gastos Operacionales</td>
                            <td class="tdTitulo02">Datos del Ejecutivo</td>
                        </tr>
                        <tr>
                            <td>
                                <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                                    AutoUpdateAfterCallBack="true">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:BoundField DataField="DescripcionTipoGasto" HeaderText="Tipo Gasto" />
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValor" runat="server" Text='<%# (Eval("Moneda_Id").ToString() == "999") ? string.Format("{0:C}",Eval("Valor")) : string.Format("{0:F4}",Eval("Valor"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor $">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValorPesos" runat="server" Text='<%# string.Format("{0:C}",Eval("ValorPesos"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </anthem:GridView>

                            </td>
                            <td style="vertical-align: top">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="tdInfo">Nombre</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoNombre" runat="server"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Mail</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoMail" runat="server"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Teléfono</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoFono" runat="server"></anthem:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalGGOO" runat="server" CssClass="tdInfoData" UpdateAfterCallBack="True"></anthem:Label>

                            </td>
                            <td style="vertical-align: top">&nbsp;</td>
                        </tr>
                    </table>

                </td>
            </tr>
            </table>
    </div>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="tdTitulo02">Términos y Condiciones</td>
            </tr>
            <tr>
                <td class="ParrafoLegal">
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo1" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo2" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo3" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo4" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo5" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo6" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo7" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo8" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo9" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo10" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo11" runat="server"></anthem:Label>
                    <br />
                    <anthem:Label AutoUpdateAfterCallBack="true" ID="LblParrafo12" runat="server"></anthem:Label>
                </td>
            </tr>
        </table>
    </div>





















</asp:Content>
