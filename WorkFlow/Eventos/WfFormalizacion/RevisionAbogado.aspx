<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="RevisionAbogado.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.RevisionAbogado" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function InicioFechas() {
            try {
                $("#<%= txtFechaRevisionAbogado.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0,
                    constrainInput: true //La entrada debe cumplir con el formato
                });
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
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Revision" data-toggle="tab" aria-expanded="true">Revisión</a></li>
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
        <div class="tab-pane fade active in" id="Revision">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Detalle Firma Participantes
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <br />
                                    <anthem:GridView runat="server" ID="GvParticipantes" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center"
                                        PageSize="5" AllowPaging="True" AllowSorting="True"
                                        AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Tipo Participación" />
                                            <asp:BoundField DataField="NombreCliente" HeaderText="Deudor" />
                                            <asp:BoundField DataField="FechaFirma" HeaderText="Fecha de Firma Participante"  DataFormatString="{0:dd-MM-yyyy}" />
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
                            <h3 class="panel-title">Ingreso Fecha Revisión de Abogado</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4 text-center">
                                </div>
                                <div class="col-lg-4 text-center">
                                    <div class='form-group'>
                                        <label for="lblAbogado">Abogado</label>
                                        <anthem:Label ID="lblAbogado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:Label>
                                    </div>
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Notaría</label>
                                        <anthem:DropDownList ID="ddlNotaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <anthem:Label ID="lblFechaRevisionAbogado" for="txtFechaRevisionAbogado" AutoUpdateAfterCallBack="true" class="labelForm" Text="Fecha de Revisión" runat="server"></anthem:Label>
                                        <anthem:TextBox ID="txtFechaRevisionAbogado" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4 text-center">
                                </div>
                            </div>

                        </div>


                    </div>


                </div>
            </div>

        </div>
        </div>
</asp:Content>
