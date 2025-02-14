<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ActualizarSolicitud.aspx.cs" Inherits="WorkFlow.Aplicaciones.ActualizarSolicitud" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">
        <div class="col-lg-12">

            <ul class="nav small nav-tabs">
                <li class="active"><a href="#Resumen" data-toggle="tab" aria-expanded="true">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class=""><a id="TabAsignacion" runat="server" href="#Portero" data-toggle="tab" aria-expanded="false">Asignación</a></li>
                <li class=""><a href="#Eventos" data-toggle="tab" aria-expanded="false">Eventos</a></li>
                <li class=""><a id="TabGGOO" href="#GGOO" runat="server" data-toggle="tab" aria-expanded="false">Gastos Operacionales</a></li>
                <li class=""><a id="TabDocumentos" href="#Documentos" runat="server" data-toggle="tab" aria-expanded="false">Reimpresión de Documentos</a></li>
            </ul>
        </div>
        </div>
    <div class="row spacenav">
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open" aria-hidden="true"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>

    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade active in" id="Resumen">
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
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade" id="Portero">
            <div class="row">
                <div class="col-md-12">
                    <h5>
                        <span class="titulo">
                            <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Asignación
                        </span>
                    </h5>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Comercial</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <label for="AddlEjecutivoComercial" class="control-label">Ejecutivo Comercial</label>
                                    <anthem:DropDownList ID="AddlEjecutivoComercial" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                                    <anthem:LinkButton ID="btnActualizarEjecutivoComercial" runat="server" class="btn-sm btn-success" OnClick="btnActualizarEjecutivoComercial_Click"><span class="glyphicon glyphicon-floppy-save"></span> Actualizar Asignación</anthem:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-primary" id="DivSupervisoresFormalizacion" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">Formalización</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="AddlJefeProducto" class="control-label">Jefe de Producto</label>
                                    <anthem:DropDownList ID="AddlJefeProducto" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                                    <anthem:LinkButton ID="LinkButton2" runat="server" class="btn-sm btn-success" OnClick="btnActualizarJefeProducto_Click"><span class="glyphicon glyphicon-floppy-save"></span> Actualizar Asignación</anthem:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <label for="AddlVisador" class="control-label">Visador</label>
                                    <anthem:DropDownList ID="AddlVisador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                                    <anthem:LinkButton ID="LinkButton3" runat="server" class="btn-sm btn-success" OnClick="btnActualizarVisador_Click"><span class="glyphicon glyphicon-floppy-save"></span> Actualizar Asignación</anthem:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary" id="DivAsignacionFlujoFormalizacion" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Asignación Fabrica</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12 small">

                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabFormalizacion" class="control-label">Fábrica Asistentes</label>
                                    <anthem:DropDownList ID="AddlFabFormalizacion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabFormalizacion_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>


                                </div>
                                <div class='form-group'>
                                    <label for="AddlFormalizador" class="control-label">Asistente</label>
                                    <anthem:DropDownList ID="AddlFormalizador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <%-- <div class="col-md-2 col-lg-2 ">
                                <div class='form-group'>
                                    <label for="AddlFabFormalizacionGestion" class="control-label">Fábrica Formalización de Gestión</label>
                                    <anthem:DropDownList ID="AddlFabFormalizacionGestion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabFormalizacionGestion_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>

                                </div>
                                <div class='form-group'>
                                    <label for="AddlFormalizadorGestion" class="control-label">Formalizador</label>
                                    <anthem:DropDownList ID="AddlFormalizadorGestion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>--%>
                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabAbogados" class="control-label">Fábrica Abogados</label>
                                    <anthem:DropDownList ID="AddlFabAbogados" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabAbogados_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class='form-group'>
                                    <label for="AddlAbogados" class="control-label">Abogado</label>
                                    <anthem:DropDownList ID="AddlAbogados" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabNotarias" class="control-label">Fábrica Notarías</label>
                                    <anthem:DropDownList ID="AddlFabNotarias" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabNotarias_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class='form-group'>
                                    <label for="AddlNotaria" class="control-label">Notaría</label>
                                    <anthem:DropDownList ID="AddlNotaria" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-lg-3">
                                <div class='form-group'>
                                    <label for="AddlFabTasadores" class="control-label">Fábrica Tasadores</label>
                                    <anthem:DropDownList ID="AddlFabTasadores" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="AddlFabTasadores_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class='form-group'>
                                    <label for="AddlTasador" class="control-label">Tasador</label>
                                    <anthem:DropDownList ID="AddlTasador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarAsignacion" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
                                OnClick="btnGuardarAsignacion_Click" TextDuringCallBack="Guardando..." />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="Eventos">
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
                                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle />
                                                <ItemTemplate>
                                                    <anthem:DropDownList ID="ddlEstadoEvento" OnSelectedIndexChanged="ddlEstadoEvento_SelectedIndexChanged" AutoCallBack="True" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DescripcionMalla" HeaderText="Malla" />
                                            <asp:BoundField DataField="DescripcionEtapa" HeaderText="Etapa" />
                                            <asp:BoundField DataField="DescripcionEvento" HeaderText="Evento">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" />
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
        <div class="tab-pane fade" id="GGOO">
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
                                        <anthem:CheckBox ID="chkIndDfl2" AutoUpdateAfterCallBack="true" class="control-label" runat="server" Text="DFL-2" />
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
                                                        <anthem:TextBox ID="TextBox1" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-9">
                                                    <div class='form-group'>
                                                        <label for="txtNombreSolicitante">Nombre Solicitante</label>
                                                        <anthem:TextBox ID="TextBox2" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <div class='form-group'>
                                                        <label for="txtDestino">Destino</label>
                                                        <anthem:TextBox ID="TextBox3" runat="server" class="form-control" Enabled="false" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="chkIndDfl2" class="control-label"></label>
                                                        <anthem:CheckBox ID="CheckBox1" AutoUpdateAfterCallBack="true" CssClass="checkbox-inline" runat="server" Text="DFL-2" />
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
                                <div class="col-md-12">
                                    <anthem:GridView runat="server" ID="gvGastosOperacionales" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowSorting="True"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
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
        <div class="tab-pane fade" id="Documentos">
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <anthem:LinkButton ID="btnImprimirOrden" runat="server" CssClass="btn-sm btn-success" OnClick="btnImprimirOrden_Click" EnableCallBack="False"><span class="glyphicon glyphicon-cloud-download"></span> Orden de Escrituración</anthem:LinkButton>
                </div>
                <div class="col-md-3">
                    <anthem:LinkButton ID="btnImprimirHojaResumen" runat="server" CssClass="btn-sm btn-success" OnClick="btnImprimirHojaResumen_Click" EnableCallBack="False"><span class="glyphicon glyphicon-cloud-download"></span> Hoja Resumen</anthem:LinkButton>
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
