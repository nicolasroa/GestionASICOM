<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="CierreCondiciones.aspx.cs" Inherits="WorkFlow.Reportes.PlantillasMail.CierreCondiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo02">Cierre de Condiciones de su Crédito Hipotecario</td>
        </tr>
        <tr>
            <td class="ParrafoLegal">Estimado:
                <anthem:Label ID="lblNombreCliente" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                <br />
                Adjunto encontrará la información de su crédito Hipotecario, el cual iniciará el proceso de escrituración, favor confirmar las condiciones en el siguiente Link:<anthem:HyperLink ID="lnkCierreCondiciones" runat="server">[lnkCierreCondiciones]</anthem:HyperLink>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">Participantes</td>
        </tr>
        <tr>
            <td class="tdInfo">
                <anthem:GridView runat="server" ID="gdvParticipantes" AutoGenerateColumns="false" Width="100%"
                    PageSize="5" AllowSorting="True" AutoUpdateAfterCallBack="true">
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
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="tdTitulo02" style="width: 33%">Crédito</td>
                        <td class="tdTitulo02" style="width: 33%">Montos</td>
                        <td class="tdTitulo02">Seguros</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="tdInfo" style="width: 50%">Tipo de Financiamiento</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblTipoFinanciamiento" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Producto</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblProducto" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Objetivo</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblObjetivo" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Destino</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDestino" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Plazo</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblPlazo" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Tasa Mesual</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblTasaMensual" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Periodo de Gracia</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblGracia" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Deuda/Garantía</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDeudaGarantia" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="tdInfo" style="width: 33%">Precio de Venta</td>
                                    <td class="tdInfoData" style="width: 33%">
                                        <anthem:Label ID="lblPrecioVentaUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblPrecioVentaPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Subsidio</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblSubsidioUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblSubsidioPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Bono Integración</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblBonoIntegracionUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblBonoIntegracionPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Bono Captación</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblBonoCaptacionUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblBonoCaptacionPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Monto Solicitado</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblMontoSolicitadoUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblMontoSolicitadoPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Monto Contado (Ahorro/Pie)</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblMontoContadoUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblMontoContadoPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Dividendo Neto</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDividendoNetoUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDividendoNetoPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdInfo">Dividendo con Seguros</td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDividendoConSegurosUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                    <td class="tdInfoData">
                                        <anthem:Label ID="lblDividendoConSegurosPesos" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdInfo" style="vertical-align: top">
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
                                </Columns>
                            </anthem:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="ParrafoLegal">NOTAS:<br />
                - Los valores expresados en pesos fueron calculados con el valor de la UF del Día de hoy
                                        <anthem:Label ID="lblFecha" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    &nbsp;(<anthem:Label ID="lblValorUF" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                    )<br />
                - El monto del Primer Dividendo Puede variar debido a los reajustes de los meses de gracia y primas de los seguros<br />
                - Este correo ha sido generado de manera automática, favor no realizar consultas a esta dirección.</td>
        </tr>
    </table>




</asp:Content>
