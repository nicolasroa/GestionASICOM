<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="EnvioAdjunto.aspx.cs" Inherits="WorkFlow.Reportes.PlantillasMail.EnvioAdjunto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: 11px;
            color: #333333;
            font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            font-weight: bold;
            text-align: left;
            border-style: solid;
            border-width: 1px 1px 1px 1px;
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo02">Estimado(a):
                <anthem:Label ID="lblNombreCliente" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                <br />
                <br />
                Nos es grato informarle que adjunto a este correo encontrará
                <anthem:Label ID="lblNombreAdjunto" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>Atentamente<br />
                <img id="imgLogoCertificado" src="#" runat="server" alt="" style="width: 110px;" /></td>
        </tr>
        <tr>
            <td class="ParrafoLegal">NOTA: Este correo ha sido generado de manera automática, favor no realizar consultas a esta dirección.</td>
        </tr>
    </table>
</asp:Content>
