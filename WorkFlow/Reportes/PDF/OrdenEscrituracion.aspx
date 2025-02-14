<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="OrdenEscrituracion.aspx.cs" Inherits="WorkFlow.Reportes.PDF.OrdenEscrituracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 26%;
        }

        .auto-style2 {
            width: 100%;
        }

        .auto-style3 {
            width: 50%;
            height: 21px;
        }

        .auto-style4 {
            width: 20%;
            height: 21px;
        }

        .auto-style5 {
            width: 30%;
            height: 21px;
        }
        .auto-style6 {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="Contenedor">
        <table class="ContenedorTabla">
            <tr>
                <td style="width: 25%; text-align: center;">
                    <img id="imgLogoCertificado" runat="server" alt="" style="width: 120px;" />
                </td>
                <td class="auto-style1"></td>
                <td style="width: 35%">
                    <table class="auto-style2">
                        <tr>
                            <td style="width: 40%; text-align: left">SOLICITUD N°
                                <anthem:Label ID="lblNumeroSolicitud" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 60%; text-align: right">OPERACION N°
                                <anthem:Label ID="lblNumeroOperacion" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="CeldaTitulo">
                        VALIDO PARA MES:
                        <anthem:Label ID="lblMesEscritura" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>Departamento Operaciones Hipotecarias
                </td>
                <td class="auto-style1"></td>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="Contenedor">
        <div class="TituloEscritura">
            ORDEN DE ESCRITURACION
            <anthem:Label ID="lblDescripcionProducto" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
        </div>
    </div>
    <div class="Contenedor">
        <table class="ContenedorTabla">
            <tr>
                <td>Abogado
                </td>
                <td>:
                </td>
                <td>
                    <anthem:Label ID="lblAbogado" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                </td>
                <td>Sucursal
                </td>
                <td>:
                </td>
                <td>
                    <anthem:Label ID="lblSucursal" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="Contenedor">
        <div class="SubTitulo">
            DATOS SOLICITANTE
        </div>
    </div>
    <div id="lstParticipantes" runat="server" class="ContenedorDatos">
    </div>
    <div class="Contenedor">
        <div class="SubTitulo">
            DATOS DEL CREDITO
        </div>
    </div>
    <div class="ContenedorDatos">
        <table class="ContenedorTabla">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%; border-collapse: collapse; border: 1px solid black;">
                        <tr>
                            <td style="width: 50%; border: 1px solid black; text-align: center;"></td>
                            <td style="width: 20%; border: 1px solid black; text-align: center;">UF
                            </td>
                            <td style="width: 30%; border: 1px solid black; text-align: center;">PESOS
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">PRECIO COMPRA/VENTA 
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblCompraVentaUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblCompraVentaPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">MONTO CONTADO (AHORRO/PIE)
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoContadoUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoContadoPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <anthem:Label ID="lblSubsidio" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoSubsidioUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoSubsidioPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">BONO INTEGRACÍÓN</td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoBonoIntegracionUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoBonoIntegracionPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">BONO CAPTACIÓN</td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoBonoCaptacionUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMontoBonoCaptacionPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">MUTUO HIPOTECARIO
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMutuoHipotecarioUF" runat="server"></anthem:Label>
                            </td>
                            <td style="border: 1px solid black; text-align: right;">
                                <anthem:Label ID="lblMutuoHipotecarioPesos" runat="server"></anthem:Label>
                            </td>
                        </tr>
                    </table>
                </td>

                <td style="width: 50%; vertical-align: top;">
                    <table style="width: 100%">
                        <tr>
                            <td class="auto-style3">PLAZO DEL CRÉDITO</td>
                            <td style="text-align: center; border-bottom: 1px solid black;" class="auto-style4">
                                <anthem:Label ID="lblPlazoGracia" runat="server"></anthem:Label>
                            </td>
                            <td style="padding: 0px 0px 0px 10px;" class="auto-style5">MESES</td>
                        </tr>
                        <tr>
                            <td style="width: 50%;">DIVIDENDOS A PAGAR</td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblPlazo" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;">MESES</td>
                        </tr>
                        <tr>
                            <td>FECHA 1º DIVIDENDO</td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblFechaPrimerDividendo" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;"></td>
                        </tr>
                        <tr>
                            <td>DIVIDENDO 1 AL&nbsp;
                                <anthem:Label ID="lblCantidadDividendosMenos1" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblValorDividendo" runat="server" Text=""></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;">CADA UNO 
                            </td>
                        </tr>
                        <tr>
                            <td>ÚLTIMO DIVIDENDO</td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblValorUltimoDividendo" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>CON EXCEPCION DEL MES
                            </td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblMesExcepcion" runat="server" Text=""></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;">CADA AÑO
                            </td>
                        </tr>
                        <tr>
                            <td>MESES DE GRACIA</td>
                            <td style="width: 20%; text-align: center; border-bottom: 1px solid black;">
                                <anthem:Label ID="lblMesesGracia" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 30%; padding: 0px 0px 0px 10px;">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="ContenedorDatos">
        <div>VALOR DE LA UF AL 
            <anthem:Label ID="lblFechaParidad" runat="server"></anthem:Label>
            (
            <anthem:Label ID="lblValorUF" runat="server"></anthem:Label>)</div>
        <div>
            EL PLAZO DEL MUTUO HIPOTECARIO SERA DE
            <anthem:Label ID="lblAños" runat="server"></anthem:Label>
            &nbsp;AÑOS.
        </div>
        <div>
            LOS DIVIDENDOS SE PAGARAN DENTRO DE LOS
            <anthem:Label ID="lblPrimeroDias" runat="server"></anthem:Label>
            PRIMEROS DIAS DE CADA MES.
        </div>
        <div>
            CREDITO CON COSTO DE PREPAGO (ELIMINAR PARTE CLAUSULA QUE NO CORRESPONDE)
        </div>

        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; margin-top: 0px; vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3" class="TablaTitulo">TASAS Y DIVIDENDOS
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Tasa Anual
                            </td>
                            <td style="width: 2%">:
                            </td>
                            <td style="width: 68%">
                                <anthem:Label ID="lblTasaAnual" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Valor Didivendo UF
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblValorDividendoUF" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Spread
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblSpread" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Costo de Fondo
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblCostoFondo" runat="server"></anthem:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3" class="TablaTitulo">DATOS 
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">Objetivo del Crédito
                            </td>
                            <td style="width: 2%">:
                            </td>
                            <td style="width: 68%">
                                <anthem:Label ID="lblObjetivo" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Producto
                            </td>
                            <td class="auto-style6">:
                            </td>
                            <td class="auto-style6">
                                <anthem:Label ID="lblProducto" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>CAE
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblCAE" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Costo Total
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblCostoTotal" runat="server"></anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Cuenta corriente N°
                            </td>
                            <td>:
                            </td>
                            <td>
                                <anthem:Label ID="lblCuentaCorriente" runat="server"></anthem:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="11" class="TablaTitulo">PROTOCOLIZACIÓN DE SERIE
                                <anthem:Label ID="lblSerie" runat="server"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 5%">Notaría</td>
                            <td style="width: 2%">:
                            </td>
                            <td style="width: 19%">
                                <anthem:Label ID="lblNotariaProtocolizacion" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 5%">Fecha</td>
                            <td style="width: 2%">:
                            </td>
                            <td style="width: 19%">
                                <anthem:Label ID="lblFechaProtocolizacion" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 5%">Repertorio</td>
                            <td style="width: 2%">:
                            </td>
                            <td style="width: 19%">
                                <anthem:Label ID="lblRepertorioProtocolizacion" runat="server"></anthem:Label>
                            </td>
                            <td style="width: 19%">
                                Nro. Protocolización</td>
                            <td style="width: 19%">
                                <anthem:Label ID="lblNumeroProtocolizacion" runat="server"></anthem:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="Contenedor">
        <div class="SubTitulo">
            DATOS DE PROPIEDADES
        </div>
    </div>
    <div class="ContenedorDatos">
        <div class="SubTituloDatos">
            PROPIEDADES DE COMPRA/VENTA
        </div>
        <div>
            <div id="lstPropiedadesCompraventa" runat="server" class="ContenedorDatos">
            </div>
        </div>
        <div class="SubTituloDatos">
            PROPIEDADES EN GARANTIA
        </div>
        <div id="lstPropiedades" runat="server" class="ContenedorDatos">
        </div>
    </div>

    <br />
    <div class="Contenedor">
        <div class="SubTitulo">
            DATOS DE SEGUROS
        </div>
        <div>
            <anthem:GridView ID="agvSeguros" runat="server" AutoGenerateColumns="False"
                Width="100%">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:BoundField DataField="DescripcionSeguro" HeaderText="Seguro" />
                    <asp:BoundField DataField="PrimaMensual" HeaderText="Prima Mensual" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="PrimaAnual" HeaderText="Prima Total" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="DescripcionCompañia" HeaderText="Compañia de Seguros" />
                </Columns>
            </anthem:GridView>
        </div>
    </div>



</asp:Content>
