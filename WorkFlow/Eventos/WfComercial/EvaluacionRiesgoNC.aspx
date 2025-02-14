<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="EvaluacionRiesgoNC.aspx.cs" Inherits="WorkFlow.Eventos.EvaluacionRiesgoNC" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/ResumenResolucion.ascx" TagPrefix="uc1" TagName="ResumenResolucion" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<%@ Register Src="~/DatosGenerales/EstadoSituacion.ascx" TagPrefix="uc1" TagName="EstadoSituacion" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>


<%--<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CargarParticipantes() {
            try {
                $('#' + '<%=btnIngresoDeuda.ClientID%>').trigger("click");
            } catch (e) {
                alert(e.message);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">
        <div class="col-lg-8 col-md-8 col-sm-8">

            <ul class="nav small nav-tabs">
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class=""><a href="#Inversionistas" data-toggle="tab" aria-expanded="false">Inversionistas</a></li>
                <li class=""><a href="#IngresoDeuda" data-toggle="tab" aria-expanded="false" onclick="CargarParticipantes();">Estado de Situación</a></li>
                <li class="active"><a href="#Resumen" data-toggle="tab" aria-expanded="true" onclick="CargarParticipantes();">Resumen y Resolución</a></li>
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
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnGenerarFicha" runat="server" Visible="false" CssClass="btn-sm btn-success" AutoUpdateAfterCallBack="True" EnableCallBack="False" OnClick="btnGenerarFicha_Click"><span class="glyphicon glyphicon-file"></span> Ficha de Evaluación Crediticia</anthem:LinkButton>
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
       
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
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="ddlTipoPersona">Inversionistas</label>
                                        <anthem:DropDownList ID="ddlInversionistas" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="ddlTipoPersona">Tasa de Endoso</label>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtTasaEndoso" onKeyPress="return SoloNumeros(event,this.value,4,this);" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row text-center">
                                <div class="col-md-12">
                                    <anthem:LinkButton ID="btnAgregarInversionista" runat="server" class="btn-sm btn-primary" OnClick="btnAgregarInversionista_Click"><span class="glyphicon glyphicon-floppy-save"></span> Agregar Inversionista</anthem:LinkButton>
                                </div>
                            </div>

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
                                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnEliminarInversionista" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                        ToolTip="Eliminar Inversionista" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarInversionista_Click" />
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
        <div class="tab-pane fade" id="IngresoDeuda">
            <anthem:LinkButton ID="btnIngresoDeuda" runat="server" OnClick="btnIngresoDeuda_Click"></anthem:LinkButton>
            <uc1:EstadoSituacion runat="server" ID="EstadoSituacion" />
        </div>
        <div class="tab-pane fade active in" id="Resumen">
            <uc1:ResumenResolucion runat="server" ID="ResumenResolucion" />
            <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
        </div>
    </div>
    <%--Ingreso Motivo del Rechazo--%>

    <button type="button" class="btn-primary btn-sm" data-toggle="modal" data-target="#modalMotivoRechazo" style="visibility: hidden" id="btnIngresarMotivoRechazo">
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
