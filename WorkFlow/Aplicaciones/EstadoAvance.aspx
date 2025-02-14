<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="EstadoAvance.aspx.cs" Inherits="WorkFlow.Aplicaciones.EstadoAvance" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row navbar-fixed-top" style="background-color: #ffffff">
        <div class="col-lg-9">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Eventos" data-toggle="tab" aria-expanded="true">Eventos</a></li>
            </ul>
        </div>
        <div class="col-lg-2 text-center">
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Resumen">
            <uc1:DatosResumen runat="server" ID="DatosResumen" />
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
        <div class="tab-pane fade active in" id="Eventos">

            <div class="row">
                <div class="col-md-12 small">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Control de Eventos
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <anthem:GridView runat="server" ID="gdvControlEventos" AutoGenerateColumns="False" Width="100%"
                                        PageSize="15" AllowSorting="True" AllowPaging="True"
                                        AutoUpdateAfterCallBack="True" OnPageIndexChanging="gdvControlEventos_PageIndexChanging" UpdateAfterCallBack="True" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                                            <asp:BoundField DataField="DescripcionMalla" HeaderText="Malla" />
                                            <asp:BoundField DataField="DescripcionEtapa" HeaderText="Etapa" />
                                            <asp:BoundField DataField="DescripcionEvento" HeaderText="Evento">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Ingreso" />
                                            <asp:BoundField DataField="FechaTermino" HeaderText="Fecha Termino" NullDisplayText="En proceso" />
                                            <asp:BoundField DataField="UsuarioResponsable" HeaderText="Usuario Responsable" />
                                            <asp:BoundField DataField="UsuarioTermino" HeaderText="Usuario Término" />
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

