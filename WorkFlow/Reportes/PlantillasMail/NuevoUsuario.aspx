<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="WorkFlow.Reportes.PlantillasMail.NuevoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style1 {
        padding: 3px;
        font-size: 14px;
        color: #333333;
        font-family: Arial;
        text-align: justify;
        height: 24px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">





    <table style="width:100%;">
        <tr>
            <td>
                    <img id="imgLogoCertificado" runat="server" alt="" style="width: 120px;" /></td>
        </tr>
        <tr>
            <td class="tdTitulo01">Registro de Nuevo Usuario del Sistema</td>
        </tr>
        <tr>
            <td class="Texto14N">Estimado/a
                <anthem:Label ID="lblNombre" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="ParrafoLegal">Informamos que se ha creado un usuario en el sistema WorkFlowHipotecario asociado a este Correo Electrónico</td>
        </tr>
        <tr>
            <td class="tdTitulo02">Datos del Usuario:</td>
        </tr>
        <tr>
            <td class="ParrafoLegal">Nombre de Usuario:
                <anthem:Label ID="lblNombreUsuario" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Clave Temporal:
                <anthem:Label ID="lblClave" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="ParrafoLegal">Favor acceder al siguiente sitio para acceder:
                <anthem:HyperLink ID="lnkRutaSitio" runat="server" AutoUpdateAfterCallBack="true">Sistema WorkFlow Hipotecario</anthem:HyperLink>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>





</asp:Content>
