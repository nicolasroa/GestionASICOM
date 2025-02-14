<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="RecepcionAntecedentes.aspx.cs" Inherits="WorkFlow.Eventos.RecepcionAntecedentes" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CargarParticipantes() {
            try {
                $('#' + '<%=btnAntecedentes.ClientID%>').trigger("click");
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
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Antecedentes" data-toggle="tab" aria-expanded="true" onclick="CargarParticipantes();">Antecedentes</a></li>
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
        <div class="col-lg-4"></div>
        <div class="col-lg-4 col-md-6 col-sm-6 text-right"></div>
        <div class="col-lg-4 col-md-6 col-sm-6 text-right">
            <anthem:LinkButton ID="btnImprimir" runat="server" AutoUpdateAfterCallBack="true" EnableCallBack="False" OnClick="btnImprimir_Click" class="btn-sm btn-info"><span class="glyphicon glyphicon-file"></span> Documentación Requerida</anthem:LinkButton>
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
        <div class="tab-pane fade active in" id="Antecedentes">

            <div class="row">
                <div class="col-lg-12">
                <anthem:LinkButton ID="btnAntecedentes" runat="server" OnClick="btnAntecedentes_Click"></anthem:LinkButton>
                    <h5>
                        <span class="titulo">
                            <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Antecedentes de los Participantes
                        </span>
                        <br>
                    </h5>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Participante</h3>
                    </div>
                    <div class="panel-body">
                        <anthem:GridView runat="server" ID="gvParticipantesAntecedentes" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                                <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                            ToolTip="Seleccionar" TextDuringCallBack="Buscando Registro..." OnClick="btnSeleccionar_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </anthem:GridView>
                    </div>

                </div>
                    </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarDocumentos" runat="server" CssClass="btn-sm btn-primary" Text="Guardar Revisión de Documentos"
                        OnClick="btnGuardarDocumentos_Click" TextDuringCallBack="Guardando..." />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDocumentos" runat="server" Text="Cancelar Revisión de Documentos" CssClass="btn-sm btn-primary"
                        OnClick="btnCancelarDocumentos_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Documentación Requerida para
                            <anthem:Label ID="lblParticipanteAntecedentes" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></h3>
                    </div>
                    <div class="panel-body">
                        <anthem:GridView runat="server" ID="gvDocumentosRequeridos" AutoGenerateColumns="false" Width="100%"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:TemplateField HeaderText="Validación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:RadioButtonList ID="rblEstadoDocumento" runat="server" AutoUpdateAfterCallBack="true" RepeatDirection="Horizontal"></anthem:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DescripcionDocumento" HeaderText="Documento" />
                                <asp:BoundField DataField="FechaValidacion" HeaderText="Fecha de Solicitud" NullDisplayText="Sin Validación" />

                            </Columns>
                        </anthem:GridView>
                    </div>
                </div>
                    </div>
            </div>


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
                    <anthem:TextBox ID="txtMotivoRechazo" AutoUpdateAfterCallBack="true" CssClass="form-control" runat="server"></anthem:TextBox>
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
