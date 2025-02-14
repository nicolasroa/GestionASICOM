<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="SolicitudPreAprobacion.aspx.cs" Inherits="WorkFlow.Eventos.SolicitudPreAprobacion" %>

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

        <div class="col-lg-8 col-md-8 col-sm-12">
            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class="active"><a href="#Credito" data-toggle="tab" aria-expanded="true">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class=""><a href="#GastosOperacionales" data-toggle="tab" aria-expanded="false">Gastos Operacionales</a></li>
                <li class=""><a href="#Antecedentes" data-toggle="tab" aria-expanded="false" onclick="CargarParticipantes();">Antecedentes</a></li>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12">
            <div class='form-group'>
                <div class="col-lg-7 col-md-7 col-sm-12">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12">
                    <anthem:LinkButton ID="btnAccion" Visible="false" runat="server" EnabledDuringCallBack="False" TextDuringCallBack="Procesando Evento"  AutoUpdateAfterCallBack="true" OnClick="btnAccion_Click"></anthem:LinkButton>
                </div>
            </div>
    </div>
    </div>
    <div class="row spacenav">

        <div class="col-lg-4"></div>
        <div class="col-lg-4 col-md-6 col-sm-6 text-right"></div>
        <div class="col-lg-4 col-md-6 col-sm-6 text-right">
            <anthem:LinkButton ID="btnImprimir" runat="server" AutoUpdateAfterCallBack="true" EnableCallBack="False" OnClick="btnImprimir_Click" class="btn-sm btn-info"><span class="glyphicon glyphicon-file"></span>Documentación Requerida</anthem:LinkButton>
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span>  Carpeta Digital</anthem:LinkButton>
        </div>

    </div>

    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Resumen">
            <uc1:DatosResumen runat="server" ID="DatosResumen" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade active in" id="Credito">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>
        <div class="tab-pane fade" id="Participantes">
           
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
        </div>
        <div class="tab-pane fade" id="Propiedades">
            
            <uc1:DatosPropiedad runat="server" ID="DatosPropiedad" />
        </div>
        <div class="tab-pane fade" id="GastosOperacionales">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos Solicitud</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="txtRutCliente">Rut Solicitante</label>
                                        <anthem:TextBox ID="txtRutSolicitante" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class='form-group'>
                                        <label for="txtNombreSolicitante">Nombre Solicitante</label>
                                        <anthem:TextBox ID="txtNombreSolicitante" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="txtDestino">Destino</label>
                                        <anthem:TextBox ID="txtDestino" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class='form-group'>
                                        <label for="chkIndDfl2" class="control-label"></label>
                                        <anthem:CheckBox ID="chkIndDfl2" AutoUpdateAfterCallBack="true" class="control-label" runat="server" Text="Beneficio DFL-2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Provisión de Gastos Operacionales</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <anthem:GridView runat="server" ID="gvGastosOperacionales" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                                            PageSize="10" AllowSorting="True"
                                            AutoUpdateAfterCallBack="true">
                                            <RowStyle CssClass="GridItem" />
                                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                            <AlternatingRowStyle CssClass="GridAtlItem" />
                                            <Columns>
                                                <asp:BoundField DataField="DescripcionTipoGasto" HeaderText="Tipo de Gasto" />
                                                <asp:TemplateField HeaderText="Quién Paga?" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle />
                                                    <ItemTemplate>
                                                        <anthem:DropDownList Width="150px" ID="ddlQuienPaga" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cómo Paga?" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle />
                                                    <ItemTemplate>
                                                        <anthem:DropDownList Width="150px" ID="ddlComoPaga" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DescripcionMoneda" HeaderText="Tipo de Moneda" />
                                                <asp:TemplateField HeaderText="Valor UF" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle />
                                                    <ItemTemplate>
                                                        <anthem:TextBox Width="100px" ID="txtValorUF" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ValorPesos" HeaderText="Monto en Pesos" DataFormatString="{0:C}" />
                                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/icons/Grabar.png"
                                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
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

                </div>
            </div>

        </div>
        <div class="tab-pane fade" id="Antecedentes">

            <div class="row">
                <div class="col-lg-12">
                <anthem:LinkButton ID="btnAntecedentes" runat="server" OnClick="btnAntecedentes_Click"></anthem:LinkButton>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Seleccione un Participante</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-lg-12">
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
                    </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center panel-pading">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarDocumentos" runat="server" CssClass="btn-primary btn-sm" Text="Guardar Revisión de Documentos"
                        OnClick="btnGuardarDocumentos_Click" TextDuringCallBack="Guardando..." />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDocumentos" runat="server" Text="Cancelar Revisión de Documentos" CssClass="btn-primary btn-sm"
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
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <anthem:GridView runat="server" ID="gvDocumentosRequeridos" AutoGenerateColumns="false" Width="100%"
                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Solicitar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDocumentoValidado" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DescripcionDocumento" HeaderText="Documento" />
                                        <asp:BoundField DataField="FechaValidacion" HeaderText="Fecha de Solicitud" NullDisplayText="No Solicitado" />

                                    </Columns>
                                </anthem:GridView>


                            </div>
                            <div class="col-lg-1"></div>
                        </div>

                    </div>
                </div>
                    </div>

            </div>


        </div>
    </div>



    <%--Ingreso Motivo del Rechazo--%>

    <button type="button" class="btn-primary btn-sm" data-toggle="modal" data-target="#modalMotivoRechazo" style="visibility: hidden" id="btnIngresarMotivoRechazo">
    </button>

    <div class="modal fade" id="modalMotivoRechazo" tabindex="-1" role="dialog" aria-labelledby="modalRechazoLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-content-client">
                <div class="modal-header">
                    <h4 class="modal-title" id="modalRechazoLabel">Anulación</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <div class='form-group'>
                                <label for="txtMotivoRechazo" class="text-left">Motivo</label>
                                <anthem:TextBox ID="txtMotivoRechazo" AutoUpdateAfterCallBack="true" CssClass="form-control" runat="server"></anthem:TextBox>
                            </div>
                        </div>



                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn-secondary btn-sm" data-dismiss="modal">Cerrar</button>
                        <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGrabarMotivoRechazo" CssClass="btn-success btn-sm" runat="server" Text="Grabar"
                            OnClick="btnGrabarMotivoRechazo_Click" data-dismiss="modal" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
