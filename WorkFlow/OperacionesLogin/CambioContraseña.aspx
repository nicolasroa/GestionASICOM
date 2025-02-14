<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Operaciones.Master" AutoEventWireup="true" CodeBehind="CambioContraseña.aspx.cs" Inherits="WorkFlow.OperacionesLogin.CambioContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <anthem:Panel ID="pnlOperacion" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="lblUsuario">Usuario:</label>
                    <anthem:Label ID="lblUsuario" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="txtClaveActual">Contraseña Actual:</label>
                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveActual" runat="server" CssClass="form-control"
                        TextMode="Password"></anthem:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="ddlPreguntaSeguridad">Pregunta de Seguridad:</label>
                    <anthem:DropDownList ID="ddlPreguntaSeguridad" AutoUpdateAfterCallBack="true" runat="server" CssClass="form-control">
                    </anthem:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="txtRespuesta1">Respuesta:</label>
                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtRespuesta1" runat="server" CssClass="form-control"></anthem:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="txtRespuesta2">Repetir Respuesta:</label>
                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtRespuesta2" runat="server" CssClass="form-control"></anthem:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="txtClaveNueva">Nueva Contraseña:</label>
                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveNueva" runat="server" CssClass="form-control"
                        TextMode="Password"></anthem:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class='form-group'>
                    <label for="txtClaveNueva2">Repetir Contraseña Atual:</label>
                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveNueva2" runat="server" CssClass="form-control"
                        TextMode="Password"></anthem:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCambiarClave" runat="server" CssClass="btn btn-primary" Text="Cambiar Contraseña"
                    OnClick="btnCambiarClave_Click" AutoUpdateAfterCallBack="True" />
            </div>
        </div>
    </anthem:Panel>
    <div class="row">
        <div class="col-md-12 text-center">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelar" runat="server" CssClass="btn btn-primary" OnClick="btnCancelar_Click"
                AutoUpdateAfterCallBack="true" Text="Cancelar" />
        </div>
    </div>


    <anthem:HiddenField ID="hfUsuarioId" runat="server" AutoUpdateAfterCallBack="true" />
</asp:Content>
