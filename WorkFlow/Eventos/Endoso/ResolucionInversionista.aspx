<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ResolucionInversionista.aspx.cs" Inherits="WorkFlow.Eventos.ResolucionInversionista" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/ResumenResolucion.ascx" TagPrefix="uc1" TagName="ResumenResolucion" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function InicioResolucionInversionista() {

            $("#<%= txtFechaResolucion.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                constrainInput: true, //La entrada debe cumplir con el formato
                maxDate: 0
            });


        }
    </script>
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Resolucion" data-toggle="tab" aria-expanded="true">Resolución Inversionista</a></li>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12">

            <div class='form-group'>
                <div class="col-lg-7">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5">
                    <anthem:LinkButton ID="btnAccion" Visible="false" runat="server" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);" AutoUpdateAfterCallBack="true" OnClick="btnAccion_Click" EnabledDuringCallBack="false" TextDuringCallBack="Procesando" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"></anthem:LinkButton>
                </div>
            </div>
        </div>

    </div>
    <div class="row spacenav">
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Resumen">
            <uc1:DatosResumen runat="server" ID="DatosResumen" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade" id="Credito">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>
        <div class="tab-pane fade" id="Participantes">
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
        </div>
        <div class="tab-pane fade" id="Propiedades">
            <uc1:DatosPropiedad runat="server" ID="DatosPropiedad" />
        </div>
        <div class="tab-pane fade active in" id="Resolucion">
            <div class="row">
                <div class="col-md-12">
                    <h6>
                        <span class="titulo">
                            <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Datos Crédito
                        </span>
                        <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                            <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
                        </span>
                        <br>
                    </h6>
                </div>
            </div>

            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos del Crédito</h3>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class='form-group'>
                                <label for="txtTasa" class="control-label">Tasa del Crédito</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasa" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class='form-group'>
                                <label for="txtPlazoCredito" class="control-label">Plazo del Crédito</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPlazoCredito" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class='form-group'>
                                <label for="txtMontoCredito" class="control-label">Monto del Crédito</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCredito" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class='form-group'>
                                <label for="txtGracia" class="control-label">Meses de gracia</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtGracia" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de Envío a Inversionista</h3>
                    </div>

                    <div class="panel-body">
                        <div class="row">

                            <div class="col-lg-2">
                                <label for="txtTasaEndoso" class="control-label">Tasa del Endoso</label>
                            </div>
                            <div class="col-lg-2">
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaEndoso" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <label for="txtInversionista" class="control-label">Inversionista</label>
                            </div>
                            <div class="col-lg-2">
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtInversionista" runat="server"></anthem:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <label for="txtFechaIngreso" class="control-label">Fecha Ingreso</label>
                            </div>
                            <div class="col-lg-2">
                                <anthem:TextBox ID="txtFechaEnvioAntecedentes" runat="server" AutoUpdateAfterCallBack="True" class="txtFechaPicker" onblur="esFechaValida(this);"></anthem:TextBox>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Resolución Inversionista</h3>
                    </div>

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-lg-3">
                                <label for="txtFechaEnvioAntecedentes" class="control-label">Resolución</label>
                            </div>
                            <div class="col-lg-3">
                                <anthem:DropDownList ID="ddlEstadoEndoso" AutoUpdateAfterCallBack="true" class="form-control" runat="server"></anthem:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <label for="txtFechaResolucion" class="control-label">Fecha de Resolución</label>
                            </div>
                            <div class="col-lg-3">
                                <anthem:TextBox ID="txtFechaResolucion" runat="server" AutoUpdateAfterCallBack="True" class="form-control" onblur="esFechaValida(this);"></anthem:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-lg-12 small center">
                    <anthem:Button ID="btnGrabaResolucion" runat="server" Text="Grabar Resolución" AutoUpdateAfterCallBack="True" CssClass="btn-sm btn-success" OnClick="btnGrabaResolucion_Click" />
                </div>
                <div class="row">
                    <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
                </div>
            </div>
        </div>
        </div>
</asp:Content>
