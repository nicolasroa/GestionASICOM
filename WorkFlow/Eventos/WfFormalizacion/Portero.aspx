<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="Portero.aspx.cs" Inherits="WorkFlow.Eventos.WebForm1" %>

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

        <div class="col-lg-8 col-md-8 col-sm-8">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Portero" data-toggle="tab" aria-expanded="true">Asignación</a></li>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-4">

            <div class='form-group'>
                <div class="col-lg-7 col-md-7 col-sm-12">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12">
                    <anthem:LinkButton ID="btnAccion" Visible="false" runat="server" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);" AutoUpdateAfterCallBack="true" OnClick="btnAccion_Click" EnabledDuringCallBack="false" TextDuringCallBack="Procesando" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"></anthem:LinkButton>
                </div>
            </div>
        </div>
        </div>

        <div class="row spacenav">
        <div class="col-lg-12 text-right" >
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
        <div class="tab-pane fade active in" id="Portero">
            <div class="row">
                <div class="col-md-12">
                    <h5>
                        <span class="titulo">
                            <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Asignación
                        </span>
                    </h5>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">General</h3>
                </div>

                <div class="panel-body">
                    <div class="col-md-12 col-lg-12 small ">
                        <div class='form-group'>
                            <label for="AtxtEjecutivoComercial" class="col-lg-2 control-label">Ejecutivo Comercial</label>
                            <div class="col-lg-2">
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="AtxtEjecutivoComercial" runat="server"></anthem:TextBox>
                            </div>

                            <%--<label for="AddlJefeProducto" class="col-lg-2 control-label">Jefe de Producto</label>
                            <div class="col-lg-2">
                                <anthem:DropDownList ID="AddlJefeProducto" runat="server" CssClass="text-left form-control col-lg-12" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                            </div>

                            <label for="AddlVisador" class="col-lg-2 control-label">Visador</label>
                            <div class="col-lg-2">
                                <anthem:DropDownList ID="AddlVisador" runat="server" CssClass="text-left form-control col-lg-12" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>



            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Asignación Fabrica</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12 small">

                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabFormalizacion" class="control-label">Fábrica Ejecutivos Hipotecarios</label>
                                    <anthem:DropDownList ID="AddlFabFormalizacion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabFormalizacion_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>

                                </div>
                                <div class='form-group'>
                                    <label for="AddlFormalizador" class="control-label">Ejecutivo Hipotecario</label>
                                    <anthem:DropDownList ID="AddlFormalizador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <%-- <div class="col-md-2 col-lg-2 ">
                                <div class='form-group'>
                                    <label for="AddlFabFormalizacionGestion" class="control-label">Fábrica Formalización de Gestión</label>
                                    <anthem:DropDownList ID="AddlFabFormalizacionGestion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabFormalizacionGestion_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>

                                </div>
                                <div class='form-group'>
                                    <label for="AddlFormalizadorGestion" class="control-label">Formalizador</label>
                                    <anthem:DropDownList ID="AddlFormalizadorGestion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>--%>
                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabAbogados" class="control-label">Fábrica Abogados</label>
                                    <anthem:DropDownList ID="AddlFabAbogados" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabAbogados_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class='form-group'>
                                    <label for="AddlAbogados" class="control-label">Abogado</label>
                                    <anthem:DropDownList ID="AddlAbogados" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabNotarias" class="control-label">Fábrica Notarías</label>
                                    <anthem:DropDownList ID="AddlFabNotarias" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabNotarias_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class='form-group'>
                                    <label for="AddlNotaria" class="control-label">Notaría</label>
                                    <anthem:DropDownList ID="AddlNotaria" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabTasadores" class="control-label">Fábrica Tasadores</label>
                                    <anthem:DropDownList ID="AddlFabTasadores" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabTasadores_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
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

</asp:Content>
