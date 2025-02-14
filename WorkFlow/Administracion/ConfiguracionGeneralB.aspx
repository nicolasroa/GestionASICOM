<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="ConfiguracionGeneralB.aspx.cs" Inherits="WebSite.Administracion.ConfiguracionGeneralB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Configuración General
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span class="busqueda text-left">Datos al
                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblFechaActualizacion" runat="server"></anthem:Label>
                    </span>
                    <span class="busqueda TextoDerecha">
                        Ambiente: <anthem:Label AutoUpdateAfterCallBack="true" ID="lblAmbiente" runat="server"></anthem:Label>

                    </span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Validación de Usuario</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class='form-group'>
                                                <label for="txtTamanioClave" class="text-left">Tamaño Mínimo de la Clave</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTamanioClave" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class='form-group'>
                                                <label for="txtIntentos" class="text-left">Cantidad de Intentos Fallidos</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtIntentos" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class='form-group'>
                                                <label for="txtPlazoValidez" class="text-left">Vigencia de la Clave (Días)</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPlazoValidez" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class='form-group'>
                                                <label for="txtNotificacion" class="text-left">Notificar Caducidad de la Clave (Días)</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNotificacion" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Envío de Correo</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtUsuarioCorreo" class="text-left">Usuario</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtUsuarioCorreo" onblur="validarEmail(this);"
                                                    runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtClaveCorreo" class="text-left">Clave</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtClaveCorreo"
                                                    runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtCorreo" class="text-left">Dirección de Correo</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtCorreo" runat="server" onblur="validarEmail(this);"></anthem:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtServidorEntradaCorreo" class="text-left">Conexión Segura</label>
                                                <anthem:CheckBox ID="chkConexion" runat="server" AutoUpdateAfterCallBack="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtServidorSalidaCorreo" class="text-left">Servidor de Salida</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtServidorSalidaCorreo" runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtNotificacion" class="text-left">Puerto de Salida</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPuertoCorreo" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Datos Generales</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtUrlSistema" class="text-left">Dirección del Sistema</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtUrlSistema" onBlur="validaUrl(this)"
                                                    runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="chkEnvioMail" class="text-left">Envío de Mail</label>
                                                <anthem:CheckBox ID="chkEnvioMail" runat="server" AutoUpdateAfterCallBack="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class='form-group'>
                                                <label for="txtDescuentoTMC" class="text-left">Descuento a TMC</label>
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescuentoTMC" runat="server" onKeyPress="return SoloNumeros(event,this.value,6,this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary" TextDuringCallBack="Guardando..."
                                OnClick="btnGuardar_Click" Text="Guardar" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hfId" runat="server" />
</asp:Content>
