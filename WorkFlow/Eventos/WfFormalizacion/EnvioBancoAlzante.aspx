<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="EnvioBancoAlzante.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.EnvioBancoAlzante" %>



<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function InicioDatepicker() {
            try {
                $("#<%= txtFechaEnvio.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato
                    maxDate: 0
                });
            } catch (e) {
                alert(e.message);
            }
        }
    </script>

    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Envio" data-toggle="tab" aria-expanded="true">Envío a Banco Alzante</a></li>
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
        <div class="tab-pane fade active in" id="Envio">


            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Envío Carta a Banco Alzante</h3>
                </div>
                <div class="panel-body">
                    <div class='form-group'>
                        <anthem:GridView runat="server" ID="gvGarantias" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True" AutoUpdateAfterCallBack="true"
                            UpdateAfterCallBack="True" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución Resguardo" />
                                <asp:BoundField DataField="FechaEnvioBcoAlzante" HeaderText="Fecha Envío" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
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

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Información de Contacto Para la Carta de Resguardo de: <anthem:Label ID="lblInstitucionCarta" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <label for="txtNombreContacto" class="control-label">Nombre Contacto</label>
                        </div>
                        <div class="col-lg-3">
                            <anthem:TextBox ID="txtNombreContacto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <label for="txtTelefono" class="control-label">Teléfono</label>
                        </div>
                        <div class="col-lg-3">
                            <anthem:TextBox ID="txtTelefono" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <label for="txtEmail" class="control-label">Email</label>
                        </div>
                        <div class="col-lg-3">
                            <anthem:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="name@example.com" onBlur="validarEmail(this)" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <label for="txtFechaEnvio" class="control-label">Fecha Envío</label>
                        </div>
                        <div class="col-lg-3">
                            <anthem:TextBox ID="txtFechaEnvio" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class='form-group'>
                            <br />
                        </div>

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
