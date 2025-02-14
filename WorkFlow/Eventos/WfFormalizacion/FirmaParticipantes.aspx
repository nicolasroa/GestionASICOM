<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="FirmaParticipantes.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.FirmaParticipantes" %>


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
                $("#<%= txtFechaRepertorioNotarial.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato
                    maxDate: 0
                });
                $('.txtFechaPicker').datepicker({
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
                <li class="active"><a href="#Salida" data-toggle="tab" aria-expanded="true">Registro de Firmas</a></li>
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
        <div class="tab-pane fade active in" id="Salida">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Registro de Firmas</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Escritura</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="txtFechaRepertorioNotarial">Fecha Repertorio</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:TextBox ID="txtFechaRepertorioNotarial" runat="server" AutoUpdateAfterCallBack="True" CssClass="form-control" onblur="esFechaValida(this);"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="txtNroRepertorio">N° Repertorio</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNroRepertorio" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="txtMesEscritura">Mes de Escrituración</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMesEscritura" runat="server" Enabled="false"></anthem:TextBox>
                                              
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Impuesto al mutuo</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="txtFolioFormulario">Folio del Formulario</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFolioFormulario" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="ddlTasaImpuesto">Tasa Impuesto al Mutuo</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:DropDownList ID="ddlTasaImpuesto" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTasaImpuesto_SelectedIndexChanged"></anthem:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label for="txtMontoImpuesto">Monto Impuesto</label>
                                            </div>
                                            <div class="col-lg-6">
                                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtMontoImpuesto" runat="server" Enabled="False"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>


                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Firmas Participantes</h3>
                            </div>
                            <div class="panel-body">


                                <div class="row">
                                    <div class="col-lg-6">
                                        <ul class="nav small nav-tabs">
                                            <li class="active"><a href="#Deudores" data-toggle="tab" aria-expanded="true">Deudores</a></li>
                                            <li class=""><a href="#Vendedor" data-toggle="tab" aria-expanded="false">Vendedor</a></li>
                                        </ul>
                                    </div>

                                    <div id="tabPersona" class="tab-content">
                                        <div class="tab-pane fade active in" id="Deudores">
                                            <div class="row">

                                                <div class="col-lg-12">
                                                    <br />
                                                    <anthem:GridView runat="server" ID="GvDeudores" AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"
                                                        PageSize="20" AllowPaging="True" AllowSorting="True"
                                                        AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" CssClass="table table-condensed">
                                                        <RowStyle CssClass="GridItem" />
                                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                                        <Columns>
                                                            <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Tipo Participación" />
                                                            <asp:BoundField DataField="NombreCliente" HeaderText="Deudor" />
                                                            <asp:BoundField DataField="FechaFirma" HeaderText="Fecha de Firma Registrada"  DataFormatString="{0:d}" NullDisplayText="No Registrada" />
                                                            <asp:TemplateField HeaderText="Fecha Firma Deudor" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle />
                                                                <ItemTemplate>
                                                                    <anthem:TextBox ID="txtFechaFirmaDeudor" class="txtFechaPicker center" runat="server" AutoUpdateAfterCallBack="true" onblur="esFechaValida(this);"></anthem:TextBox>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Guarda Fecha Firma Deudor" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <anthem:ImageButton ID="btnGuardaFirmaDeudor" runat="server" ImageUrl="~/Img/icons/Grabar.png"
                                                                        PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                        ToolTip="Abrir" OnClick="btnGuardaFirmaDeudor_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </anthem:GridView>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="Vendedor">
                                            <div class="row">

                                                <div class="col-lg-12">
                                                    <br />
                                                    <anthem:GridView runat="server" ID="GvVendedor" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                                                        PageSize="20" AllowPaging="True" AllowSorting="True"
                                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                        <RowStyle CssClass="GridItem" />
                                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                                        <Columns>
                                                            <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Tipo Participación" />
                                                            <asp:BoundField DataField="NombreCliente" HeaderText="Deudor" />
                                                            <asp:BoundField DataField="FechaFirma" HeaderText="Fecha de Firma Registrada" DataFormatString="{0:d}" NullDisplayText="No Registrada" />
                                                            <asp:TemplateField HeaderText="Fecha Firma Vendedor" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle />
                                                                <ItemTemplate>
                                                                    <anthem:TextBox ID="txtFechaFirmaVendedor" class="txtFechaPicker center" runat="server" AutoUpdateAfterCallBack="true" onblur="esFechaValida(this);"></anthem:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Guarda Firma Vendedor" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <anthem:ImageButton ID="btnGuardaFirmaVendedor" runat="server" ImageUrl="~/Img/icons/Grabar.png"
                                                                        PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                        ToolTip="Abrir" OnClick="btnGuardaFirmaVendedor_Click" />
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



            </div>
        </div>
    </div>
</asp:Content>
