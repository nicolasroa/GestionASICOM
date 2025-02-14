<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="GeneracionCartaDeResguardo.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.GeneracionCartaDeResguardo" %>


<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
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
                <li class="active"><a href="#Genera" data-toggle="tab" aria-expanded="true">Generación Documento</a></li>
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
        <div class="tab-pane fade active in" id="Genera">


            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Inmuebles con Hipoteca Vigente</h3>
                </div>
                <div class="panel-body">
                    <div class='form-group'>
                        <anthem:GridView runat="server" ID="gvGarantias" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True" AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo Inmueble" />
                                <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                                <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Alzante" />
                                <asp:TemplateField HeaderText="Seleccionar / Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                            ToolTip="Seleccionar/Editar" TextDuringCallBack="Buscando Registro..." OnClick="btnSeleccionar_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carta Resguardo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnCResguardo" runat="server" ImageUrl="~/Img/IconDocumento/pdf-icon.gif"
                                            ToolTip="PDF Carta de Resguardo" OnClick="btnCResguardo_Click" EnableCallBack="False" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </anthem:GridView>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Seleccione Información de Alzamiento</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtTipoInmuebleDescripcion" class="control-label">Tipo Inmueble</label>
                                </div>
                                <div class="col-lg-6">
                                    <anthem:TextBox ID="txtTipoInmuebleDescripcion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="txtDireccionCompleta" class="control-label">Dirección</label>
                                </div>
                                <div class="col-lg-6">
                                    <anthem:TextBox ID="txtDireccionCompleta" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <label for="ddlInstitucionFinanciera" class="control-label">Institución Alzante</label>
                                </div>
                                <div class="col-lg-6">
                                    <anthem:DropDownList ID="ddlInstitucionFinanciera" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Selección de Apoderados (2)</h3>
                                </div>
                                <div class="panel-body">

                                    <anthem:GridView runat="server" ID="gvApoderados" AutoGenerateColumns="False" Width="50%" HorizontalAlign="Center"
                                        PageSize="5" AllowSorting="True"
                                        AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="NombreUsuario" HeaderText="Apoderado" />
                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkApoderadoSeleccionado" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>

                                </div>
                            </div>
                        </div>

                    </div>
                    <div class='form-group'>
                        <br />
                    </div>
                    <div class="col-md-12 text-center">
                        <anthem:Button ID="AbtnGuardaCartaResguardo" runat="server" Text="Guardar" AutoUpdateAfterCallBack="true" CssClass="btn-sm btn-success" OnClick="AbtnGuardaCartaResguardo_Click" TextDuringCallBack="Guardando..." />
                        &nbsp;<anthem:Button ID="AbtnCancela" runat="server" Text="Cancelar" AutoUpdateAfterCallBack="true" CssClass="btn-sm btn-success" OnClick="AbtnCancela_Click" TextDuringCallBack="Cancelando..." />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
