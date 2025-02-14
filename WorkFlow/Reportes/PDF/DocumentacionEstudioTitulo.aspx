<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="DocumentacionEstudioTitulo.aspx.cs" Inherits="WorkFlow.Reportes.PDF.DocumentacionEstudioTitulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 79%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divContenido" runat="server" style="position: relative; width: 100%; height: 100%; margin: 0 auto;">
        <div class="ParrafoLegal" style="width: 80%; margin-right: auto; margin-left: auto; margin-top: 100px;">
            &nbsp;<p style="text-align: right;"><%=DateTime.Now.ToLongDateString()%></p>
            <br />
            <p style="text-align: center;"><strong>Checklist Antecedentes Legales</strong></p>

            <table>
                <tr>
                    <td style="width: 20%">Cliente
                    </td>
                    <td style="width: 2%">:
                    </td>
                    <td style="width: 78%">
                        <anthem:Label ID="lblNombreDeudor" runat="server"></anthem:Label>
                    </td>
                    <td class="auto-style1">Solicitud</td>
                    <td style="width: 78%">:</td>
                    <td style="width: 78%">
                        <anthem:Label ID="lblSolicitud" runat="server"></anthem:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">Ejecutivo:</td>
                    <td style="width: 2%">:</td>
                    <td style="width: 78%">
                        <anthem:Label ID="lblNombreEjecutivo" runat="server"></anthem:Label>
                    </td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="width: 78%">&nbsp;</td>
                    <td style="width: 78%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <div>
                <anthem:GridView ID="gvDocumentosRequeridos" runat="server" AutoGenerateColumns="False"
                    Width="100%">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>

                        <asp:BoundField DataField="DescripcionDocumento" HeaderText="Listado de Documentación" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left" />

                    </Columns>
                </anthem:GridView>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="ContenedorPDFDatos" style="text-align: right;">
                Firma: ___________________________
            </div>

        </div>
    </div>
</asp:Content>
