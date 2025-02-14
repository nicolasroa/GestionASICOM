<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="Visado.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.Visado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
    <%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
    <%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
    <%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
    <%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
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
                <li class="active"><a href="#Visado" data-toggle="tab" aria-expanded="true">Visado</a></li>
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
        <div class="tab-pane fade active in" id="Visado">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Seguros Contratados 
                            </h3>
                        </div>
                        <div class="panel-body">
                            <anthem:GridView ID="gvSegurosContratados" runat="server" AutoGenerateColumns="False" AutoUpdateAfterCallBack="true"
                                Width="100%" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionSeguro" HeaderText="Seguro" />
                                    <asp:BoundField DataField="MontoAsegurado" HeaderText="Monto Asegurado" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="TasaMensual" HeaderText="Tasa Mensual" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="PrimaMensual" HeaderText="Prima Mensual" DataFormatString="{0:F4}" />
                                    <asp:BoundField DataField="Beneficiario" HeaderText="Asegurado" />
                                    <asp:BoundField DataField="DescripcionCompañia" HeaderText="Compañía" />
                                    <asp:BoundField DataField="FechaInicioVigencia" HeaderText="Fecha Inicio Vigencia" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="FechaTerminoVigencia" HeaderText="Fecha Término Vigencia" DataFormatString="{0:d}" />
                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>


                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Participantes 
                            </h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <anthem:GridView runat="server" ID="gvParticipantes" AutoGenerateColumns="false" Width="100%"
                                        AllowSorting="True"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Participación" />
                                            <asp:BoundField DataField="RutCompleto" HeaderText="Rut" />
                                            <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Completo" />
                                              <asp:TemplateField HeaderText="Fecha Ingreso DPS" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:Label ID="lblFechaIngresoDPS" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Aprobación DPS" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:Label ID="lblFechaAprobacionDPS" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:BoundField DataField="MontoAseguradoDPS" HeaderText="Monto Asegurado DPS" DataFormatString="{0:F4}" NullDisplayText="Pendiente" />
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Propiedades 
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <anthem:GridView runat="server" ID="gvPropiedades" AutoGenerateColumns="false" Width="100%"
                                        AllowSorting="True"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                                            <asp:BoundField DataField="DescripcionAntiguedad" HeaderText="Antigüedad" />
                                            <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                                            <asp:BoundField DataField="FechaTasacion" HeaderText="Fecha de Tasación" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="NombreEmpresaTasacion" HeaderText="Tasador" />
                                            <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución Carta Resguardo" NullDisplayText="No Aplica" />
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
</asp:Content>
