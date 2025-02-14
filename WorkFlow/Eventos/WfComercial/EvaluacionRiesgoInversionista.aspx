<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="EvaluacionRiesgoInversionista.aspx.cs" Inherits="WorkFlow.Eventos.WfComercial.EvaluacionRiesgoInversionista" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/ResumenResolucion.ascx" TagPrefix="uc1" TagName="ResumenResolucion" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<%--<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">
        <div class="col-lg-8 col-md-8 col-sm-12">
            <ul class="nav small nav-tabs">
                <li class=""><a href="#ResumenCredito" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class=""><a href="#Inversionistas" data-toggle="tab" aria-expanded="false">Inversionistas</a></li>
                <li class="active"><a href="#Resumen" data-toggle="tab" aria-expanded="true">Resumen y Resolución</a></li>
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
        <div class="tab-pane fade" id="ResumenCredito">
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
        <div class="tab-pane fade" id="Inversionistas">
            <div class="row">
                <div class="col-lg-4"></div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Inversionistas</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <anthem:GridView runat="server" ID="gvSolicitudInversionistas" AutoGenerateColumns="false" Width="100%"
                                        PageSize="5" AllowSorting="True"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionInversionista" HeaderText="Inversionista" />
                                            <asp:BoundField DataField="TasaEndoso" HeaderText="Tasa de Endoso" DataFormatString="{0:F2}" />
                                            <asp:TemplateField HeaderText="Estado de la Evaluación" ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle />
                                                <ItemTemplate>
                                                    <anthem:DropDownList ID="ddlEstadoEvaluacion" OnSelectedIndexChanged="ddlEstadoEvaluacion_SelectedIndexChanged" AutoCallBack="True" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4"></div>

            </div>
        </div>
        <div class="tab-pane fade active in" id="Resumen">
            <uc1:ResumenResolucion runat="server" ID="ResumenResolucion" />
        </div>
    </div>
    <%--Ingreso Motivo del Rechazo--%>

    <button type="button" class="btn-sm btn-primary" data-toggle="modal" data-target="#modalMotivoRechazo" style="visibility: hidden" id="btnIngresarMotivoRechazo">
    </button>

    <div class="modal fade" id="modalMotivoRechazo" tabindex="-1" role="dialog" aria-labelledby="modalRechazoLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-content-client">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalRechazoLabel">Motivo del Rechazo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <anthem:DropDownList ID="ddlMotivoRechazo" AutoUpdateAfterCallBack="true" CssClass="form-control" runat="server"></anthem:DropDownList>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn-secondary btn-sm" data-dismiss="modal">Cerrar</button>
                    <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGrabarMotivoRechazo" CssClass="btn-success btn-sm" runat="server" Text="Grabar"
                        OnClick="btnGrabarMotivoRechazo_Click" data-dismiss="modal" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
