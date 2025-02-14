<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="PreparacionInversionista.aspx.cs" Inherits="WorkFlow.Eventos.PreparacionInversionista" %>

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
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">
            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#PrepInversionista" data-toggle="tab" aria-expanded="true">Preparación Inversionista</a></li>
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
        <div class="tab-pane fade active in" id="PrepInversionista">

            <div class="row">
                <div class="col-md-12">
                    <h6>
                        <span class="titulo">
                            <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Preparación Inversionista
                        </span>
                        <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                            <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
                        </span>
                        <br>
                    </h6>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <label for="txtTasaCredito" class="control-label">Tasa del Crédito</label>
                </div>
                <div class="col-lg-3">
                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaCredito" onKeyPress="return SoloNumeros(event,this.value,2,this);" runat="server"></anthem:TextBox>
                </div>

                <div class="col-lg-3">
                    <label for="txtPlazoCredito" class="control-label">Plazo del Crédito</label>
                </div>
                <div class="col-lg-3">
                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtPlazoCredito" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <label for="txtMontoCredito" class="control-label">Monto del Crédito</label>
                </div>
                <div class="col-lg-3">
                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoCredito" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                </div>
                <div class="col-lg-3">
                    <label for="txtGracia" class="control-label">Meses de gracia</label>
                </div>
                <div class="col-lg-3">
                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtGracia" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <label for="ddlInversionista" class="control-label">Inversionista</label>
                </div>
                <div class="col-lg-3">
                    <anthem:DropDownList ID="ddlInversionista" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                </div>
                <div class="col-lg-3">
                    <label for="txtTasaEndoso" class="control-label">Tasa de Endoso</label>
                </div>
                <div class="col-lg-3">
                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaEndoso" onKeyPress="return SoloNumeros(event,this.value,2,this);" runat="server"></anthem:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12 col-lg-12 small center">
                    <anthem:Button ID="btnAgregaInversionista" runat="server" Text="Agregar Inversionista" AutoUpdateAfterCallBack="True" CssClass="btn-sm btn-success" OnClick="btnAgregaInversionista_Click" />
                </div>
            </div>


            <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
        </div>

    </div>

</asp:Content>
