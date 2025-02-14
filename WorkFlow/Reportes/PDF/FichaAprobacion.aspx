<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="FichaAprobacion.aspx.cs" Inherits="WorkFlow.Reportes.PDF.FichaAprobacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }

        .auto-style2 {
            font-size: 11px;
            color: #333333;
            font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            font-weight: bold;
            text-align: left;
            border-style: solid;
            border-width: 1px 1px 1px 1px;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="position: relative; width: 100%; height: 100%; margin: 0 auto;">
        <table style="width: 100%;">
            <tr>
                <td class="Titulo">
                    <table style="width: 100%;">
                        <tr>
                           
                            <td style="width: 60%">Ficha de Evaluación Crediticia</td>
                            <td style="width: 20%" class="Contenedor">Solicitud N°
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblSolicitud" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                    </table>

                </td>

            </tr>
            <tr>
                <td class="tdInfoData">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%">Valor de la UF
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblValorUF" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                            <td style="text-align: center">
                                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoNombre" runat="server"></anthem:Label>&nbsp;(<anthem:Label AutoUpdateAfterCallBack="true" ID="lblEjecutivoMail" runat="server"></anthem:Label>)</td>
                            <td class="auto-style1" style="text-align: right; width: 20%;">
                                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblFecha" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <div id="DivParticipantes" runat="server"></div>
            </tr>
            <tr>
                <div id="DivFlujosMensuales" runat="server"></div>
            </tr>
            <tr>
                <div id="DivActivosSolicitud" runat="server"></div>
            </tr>
            <tr>
                <div id="DivPasivosSolicitud" runat="server"></div>
            </tr>
            <tr>
                <td class="tdTitulo02">Datos del Inmueble</td>
            </tr>
            <tr>
                <td>
                    <anthem:GridView ID="gvPropiedades" runat="server" AutoGenerateColumns="False"
                        Width="100%">
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
                </td>
            </tr>
            <%--<tr>
                <td class="tdTitulo02">Detalle de Seguros</td>
            </tr>
            <tr>
                <td class="tdFormulario01">
                    <anthem:GridView ID="gvSeguros" runat="server" AutoGenerateColumns="False"
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
                            <asp:BoundField DataField="DescripcionCompañia" HeaderText="Compañía" />

                        </Columns>
                    </anthem:GridView>
                </td>
            </tr>--%>

            <tr>
                <td class="tdTitulo02">Resumen</td>
            </tr>

            <tr>
                <td class="tdFormulario01">

                    <table style="width: 100%;">
                        <tr>
                            <td class="tdTitulo02" style="width: 50%;">Patrimonio</td>
                            <td class="tdTitulo02" style="width: 50%; vertical-align: top;">Indicadores</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="tdInfo">Pasivos Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="true" ID="lblTotalPasivosPrincipalMO" runat="server"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalPasivosPrincipalPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Activos Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalActivosPrincipalMO" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalActivosPrincipalPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Patrimonio Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblPatrimonioPrincipal" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblPatrimonioPrincipalPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Pasivos Complementados</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalPasivosComplementados" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalPasivosComplementadosPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Activos Complementados</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalActivosComplementados" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblTotalActivosComplementadosPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Patrimonio Complementado</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblPatrimonioComplementado" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblPatrimonioComplementadoPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Renta Necesaria</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaNecesaria" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaNecesariaPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Renta Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaPrincipal" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaPrincipalPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Renta Codeudor</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaCodeudor" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaCodeudorPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">RentaComplementada</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaComplementada" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblRentaComplementadaPesos" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top;">
                                <table style="width: 100%;">

                                    <tr>
                                        <td class="tdInfo">Dividendo/Renta Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoRentaPrincipal" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Dividendo/Renta Codeudor</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoRentaCodeudor" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Dividendo/Renta Complementada</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblDividendoRentaComplementada" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Carga Financiera Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCargaFinancieraPrincipal" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Carga Financiera Complementada</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCargaFinancieraComplementada" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Carga Financiera Hipotecaria Deudor Principal</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCargaFinancieraHipotecariaPrincipal" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Carga Financiera Hipotecaria Codeudor</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCargaFinancieraHipotecariaCodeudor" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdInfo">Carga Financiera Hipotecaria Complementada</td>
                                        <td class="tdInfoData">
                                            <anthem:Label AutoUpdateAfterCallBack="True" ID="lblCargaFinancieraHipotecariaComplementada" runat="server" UpdateAfterCallBack="True"></anthem:Label></td>
                                        <td class="tdInfoData">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>

            <%--<tr>
                <td class="tdTitulo02">Observaciones</td>
            </tr>
            <tr>
                <td>
                    <anthem:GridView ID="gvObservaciones" runat="server" AutoGenerateColumns="False"
                        Width="100%">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="ResponsableIngreso" HeaderText="Responsable Ingreso" />
                            <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Comentario" ItemStyle-Font-Bold="true" />
                        </Columns>
                    </anthem:GridView>
                </td>
            </tr>--%>
        </table>
    </div>
</asp:Content>
