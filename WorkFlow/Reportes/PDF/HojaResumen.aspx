<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="HojaResumen.aspx.cs" Inherits="WorkFlow.Reportes.PDF.HojaResumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table style="width: 100%; font-family: arial, Helvetica, sans-serif; column-span: none">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td rowspan="3">
                            <img id="imgLogoCertificado" runat="server" alt="" style="width: 120px;" /></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>SOLICITUD N°
                            <anthem:Label ID="lblNumeroSolicitud" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                        </td>
                        <td>OPERACION N°
                            <anthem:Label ID="lblNumeroOperacion" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border-width: thin; font-size: 25px; text-align: center; border-style: groove;">Hoja Resumen de Crédito Hipotecario</td>
        </tr>
        <tr>
            <td style="border-style: groove; width: 100%;">
                <table style="width: 100%;">
                    <tr>
                        <td>Nombre:<anthem:Label ID="lblNombreCliente" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                        <td rowspan="2" style="font-size: 35px; text-align: right;">CAE:
                                        <anthem:Label ID="lblCae" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Fecha:
                            <anthem:Label ID="lblFecha" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">I. Producto Principal</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%;">Monto Líquido del Crédito (UF)</td>
                        <td>
                            <anthem:Label ID="lblMontoCredito" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Plazo del Crédito (Años)</td>
                        <td>
                            <anthem:Label ID="lblPlazo" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Costo Total del Crédito (UF)</td>
                        <td aria-atomic="False">
                            <anthem:Label ID="lblCTC" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Valor de la Cuota 1 a
                            <anthem:Label ID="lblDividendosMenos1" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                        <td>
                            <anthem:Label ID="lblValorCuota" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Valor Última Cuota</td>
                        <td>
                            <anthem:Label ID="lblUltimoValorCuota" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>CAE (%)</td>
                        <td>
                            <anthem:Label ID="lblCAE2" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Modalidad del Credito</td>
                        <td class="auto-style1">
                            <anthem:Label ID="lblModalidad" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Tasa de Interés Aplicada</td>
                        <td>
                            <anthem:Label ID="lblTasa" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label> (Fija)</td>
                    </tr>
                    <tr>
                        <td>Monto Bruto del Crédito (UF)</td>
                        <td>
                            <anthem:Label ID="lblMontoBruto" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Periodo de Gracia</td>
                        <td>
                            <anthem:Label ID="lblGracia" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Monto Nominal (UF)</td>
                        <td>
                            <anthem:Label ID="lblMontoNominal" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Garantías Adicionales a la Hipoteca Asociada</td>
                        <td>NO</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">II. Gastos Asociados al Otorgamiento del Crédito</td>
        </tr>
        <tr>
            <td>
                <anthem:GridView ID="gvGastosOperacionales" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="DescripcionTipoGasto" HeaderText="Tipo de Gasto" />
                        <asp:BoundField DataField="DescripcionProvisiona" HeaderText="Provisiona" NullDisplayText="-- No Ingresado --" />
                        <asp:BoundField DataField="Valor" HeaderText="Monto" />
                        <asp:BoundField DataField="ValorPesos" HeaderText="Monto en Pesos" DataFormatString="{0:C}" />
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">III. Seguros Asociados al Crédito Hipotecario y Gastos o Cargos por Productos o Servicios Voluntariamente Contratados</td>
        </tr>
        <tr>
            <td>
                <anthem:GridView ID="gvSegurosContratados" runat="server" AutoGenerateColumns="False"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="DescripcionSeguro" HeaderText="Seguro" />
                        <asp:BoundField DataField="MontoAsegurado" HeaderText="Monto Asegurado" DataFormatString="{0:F4}" />
                        <asp:BoundField DataField="TasaMensual" HeaderText="Tasa Mensual" DataFormatString="{0:F4}" />
                        <asp:BoundField DataField="PrimaMensual" HeaderText="Prima Mensual" DataFormatString="{0:F4}" />
                        <asp:BoundField DataField="PrimaAnual" HeaderText="Prima Anual" DataFormatString="{0:F4}"/>
                        <asp:BoundField DataField="DescripcionCompañia" HeaderText="Compañía" />
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">IV. Condiciones de Prepago</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%;">Cargo Prepago</td>
                        <td>Mes y medio de interés</td>
                    </tr>
                    <tr>
                        <td>Plazo Aviso Prepago</td>
                        <td>3 días hábiles desde su solicitud</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">V. Costos por Atrasos</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%;">Interés Moratorio</td>
                        <td>Tasa Máxima Convencional</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">Gastos de Cobranza</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td colspan="2">Honorarios</td>
                                </tr>
                                <tr>
                                    <td>Hasta UF 10</td>
                                    <td>9%</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&gt; UF10 &lt; UF 50</td>
                                    <td class="auto-style2">6%</td>
                                </tr>
                                <tr>
                                    <td>Exceda UF 50</td>
                                    <td>3%</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">Los porcentajes se aplican una vez transcurridos los 20 días de mora o simple retardo del pago</td>
        </tr>
        <tr>
            <td style="font-size: 18px; border-style: groove;">Advertencias</td>
        </tr>
        <tr>
            <td>(1) En estos valores no se considera la eventual sobreprima que la Compañía de Seguros pueda cobrar al deudor y/o fiador o codeudor solidario. (2) Los gastos operacionales informados precedentemente podrían sufrir variaciones respecto de los aranceles cobrados por el Conservador de Bienes Raíces y Notaria que corresponda. (3) Condiciones del crédito se mantienen en el evento que este se contrate sin seguros voluntarios. (4) Monto Líquido del crédito (UF) según cláusula_____, Plazo del crédito (años) según cláusula_____, Valor cuota del crédito (UF) según cláusula __________ Tasa de interés aplicada (%) según cláusula _____, Cargo de Prepago según cláusula_____ Interés moratorio según cláusula______, Gastos de Cobranza según cláusula______. Costo Total del crédito (UF), Carga anual equivalente(%), Modalidad de crédito según lo informado en anexo 1 de detalle de cargos y comisiones. (5) El Crédito Hipotecario de que da cuenta esta Hoja Resumen, requiere del consumidor contratante 
                            don(ña) 
                                        <anthem:Label ID="lblNombreCliente2" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                , Patrimonio o Ingresos futuros suficientes para pagar su costo total de  
                                        <anthem:Label ID="lblCTC2" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                , cuya cuota mensual es de 
                                        <anthem:Label ID="lblValorCuota2" AutoUpdateAfterCallBack="True" runat="server" UpdateAfterCallBack="True"></anthem:Label>
                &nbsp;durante todo el periodo del crédito. Los créditos con periodos de gracia o la posibilidad de traslado de dividendos podrían estar asociados a un costo.</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="border-bottom-style: groove">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: center; width: 40%">Firma Cliente</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>





</asp:Content>
