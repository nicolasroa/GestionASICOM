<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ConfirmarGastosOperacionales.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.ConfirmarGastosOperacionales" %>

<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
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
                <li class="active"><a href="#GastosOperacionales" data-toggle="tab" aria-expanded="true">Gastos Operacionales</a></li>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12">
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
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success form-group"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
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
        <div class="tab-pane fade active in" id="GastosOperacionales">
            <div class="row">
                <div class="col-lg-6">
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
                                <div class="col-lg-9">
                                    <div class='form-group'>
                                        <label for="txtNombreSolicitante">Nombre Solicitante</label>
                                        <anthem:TextBox ID="txtNombreSolicitante" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class='form-group'>
                                        <label for="txtDestino">Destino</label>
                                        <anthem:TextBox ID="txtDestino" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="chkIndDfl2" class="control-label"></label>
                                        <anthem:CheckBox ID="chkIndDfl2" AutoUpdateAfterCallBack="true" CssClass="checkbox-inline" runat="server" Text="DFL-2" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Resumen de Cuenta</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtRutCliente">Monto Provisionado</label>
                                        <anthem:TextBox ID="txtMontoProvisionado" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtNombreSolicitante">Monto Utilizado</label>
                                        <anthem:TextBox ID="txtMontoUtilizado" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="txtDestino">Monto Disponible</label>
                                        <anthem:TextBox ID="txtMontoDisponible" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
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

                                                <asp:TemplateField HeaderText="Valor UF" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle />
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><%# Eval("DescripcionMoneda")%></span>
                                                            <anthem:TextBox Width="100px" ID="txtValorUF" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ValorPesos" HeaderText="Monto en Pesos" DataFormatString="{0:C}" />
                                                <asp:BoundField DataField="ValorPagado" HeaderText="Monto Pagado" DataFormatString="{0:C}" />
                                                 <asp:TemplateField HeaderText="Solicitar Provisión" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle />
                                                    <ItemTemplate>
                                                        <anthem:CheckBox ID="chkProvisionSolicitada" Text="Solicitar" CssClass="checkbox-inline" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
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
    </div>

</asp:Content>
