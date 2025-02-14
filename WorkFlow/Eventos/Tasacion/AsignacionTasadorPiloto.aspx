<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="AsignacionTasadorPiloto.aspx.cs" Inherits="WorkFlow.Eventos.Tasacion.AsignacionTasadorPiloto" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosInmobiliariaProyecto.ascx" TagPrefix="uc1" TagName="DatosInmobiliariaProyecto" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">
            <ul class="nav small nav-tabs">
                <li class=""><a href="#Observaciones" data-toggle="tab" aria-expanded="false">Bitácora</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class="active"><a href="#CoordinacionTasacion" data-toggle="tab" aria-expanded="true">Coordinación de Tasación</a></li>
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
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Observaciones">
            <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade active in" id="CoordinacionTasacion">
            <div class="row">
                <div class="col-md-12">
                    <uc1:DatosInmobiliariaProyecto runat="server" ID="DatosInmobiliariaProyecto" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Contacto Tasación
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="AddlFabTasadores" class="control-label">Fábrica Tasadores</label>
                                        <anthem:DropDownList ID="AddlFabTasadores" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="AddlFabTasadores_SelectedIndexChanged"></anthem:DropDownList>

                                    </div>

                                </div>

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="AddlTasador" class="control-label">Tasador</label>
                                        <anthem:DropDownList ID="AddlTasador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
