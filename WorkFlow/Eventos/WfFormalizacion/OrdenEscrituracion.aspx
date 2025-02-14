<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="OrdenEscrituracion.aspx.cs" Inherits="WorkFlow.Eventos.WfFormalizacion.OrdenEscrituracion" %>

<%--<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>--%>
<%--<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>--%>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/ResumenResolucion.ascx" TagPrefix="uc1" TagName="ResumenResolucion" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        function DesplegarDatosParticipante(Visible) {
            try {
                if (Visible === '1') {
                    $('#divDatosParticipante').show(500);
                }
                else {
                    $('#divDatosParticipante').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }

        function DesplegarDatosSubsidio(Visible) {
            try {
                if (Visible === '1') {
                    $('#TabDatosSubsidio').show(500);
                    $('#divDatosSubsidio').show(500);
                }
                else {
                    $('#TabDatosSubsidio').hide(500);
                    $('#divDatosSubsidio').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }


        function DesplegarDatosPropiedad(Visible) {
            try {
                if (Visible === '1') {
                    $('#divDatosPropiedad').show(500);
                }
                else {
                    $('#divDatosPropiedad').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }
        function DesplegarDatosActividadEconominaDeudor(Visible) {
            if (Visible == "1") {
                $('#DatosActividadEconominaDeudor').show(500);
            } else {
                $('#DatosActividadEconominaDeudor').hide(500);
            }
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-9 col-md-9 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <%--<li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>--%>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Seguros" data-toggle="tab" aria-expanded="false">Seguros</a></li>
                <%--<li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>--%>
                <li class="active"><a href="#Instructivo" data-toggle="tab" aria-expanded="true">Instructivo</a></li>
                <li class=""><a href="#divDatosSubsidio" id="TabDatosSubsidio" data-toggle="tab" aria-expanded="false">Datos del Subsidio</a></li>
                <li class=""><a href="#DestinoFondos" data-toggle="tab" aria-expanded="false">Destino de Fondos</a></li>
                <li class=""><a href="#ResumenAprobacion" data-toggle="tab" aria-expanded="false">Resumen Aprobación</a></li>
            </ul>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-12">

            <div class='form-group'>
                <div class="col-lg-7">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5">
                    <anthem:LinkButton ID="btnAccion" Visible="false" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnAccion_Click"></anthem:LinkButton>
                </div>
            </div>
        </div>

    </div>
    <div class="row spacenav">
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnGrabarDatos" runat="server" TextDuringCallBack="Grabando Datos..." EnabledDuringCallBack="false" AutoUpdateAfterCallBack="true" OnClick="btnGrabarDatos_Click" CssClass="btn-sm btn-info"><span class="glyphicon glyphicon-save"></span> Grabar Datos</anthem:LinkButton>
            <a href="#" class="btn-sm btn-info" data-toggle="modal" data-target="#ModalDocumentos"><span class="glyphicon glyphicon-cloud-download"></span>Documentación</a>
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
        <%-- <div class="tab-pane fade" id="Credito">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>--%>
        <div class="tab-pane fade" id="Participantes">
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
        </div>
        <div class="tab-pane fade" id="Seguros">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
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
                    <div class="panel panel-info">
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
                                            <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnModificarParticipante" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                        ToolTip="Modificar" OnClick="btnModificarParticipante_Click" TextDuringCallBack="Buscando Registro..." />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-info" id="divDatosParticipante">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Datos del Participante
                        <anthem:Label ID="lblNombreParticipante" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="row">

                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="ddlTipoParticipacion">Tipo Participación</label>
                                                                <anthem:DropDownList ID="ddlTipoParticipacion" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" Enabled="false" OnSelectedIndexChanged="ddlTipoParticipacion_SelectedIndexChanged"></anthem:DropDownList>
                                                            </div>
                                                            <div class='form-group'>
                                                                <label for="txtPorcentajeDeuda">% de Deuda</label>
                                                                <anthem:TextBox ID="txtPorcentajeDeuda" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                            <div class='form-group'>
                                                                <label for="txtPorcentajeDominio">% de Dominio</label>
                                                                <anthem:TextBox ID="txtPorcentajeDominio" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                            <div class='form-group'>
                                                                <label for="txtPorcentajeDesgravamen">% de Desgravamen</label>
                                                                <anthem:TextBox ID="txtPorcentajeDesgravamen" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnTextChanged="txtPorcentajeDesgravamen_TextChanged"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">

                                                            <div class="panel panel-info">
                                                                <div class="panel-heading">
                                                                    <h3 class="panel-title">Seguro de Desgravamen</h3>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class='form-group'>
                                                                        <label for="ddlSeguroDesgravamen">Póliza</label>
                                                                        <anthem:DropDownList ID="ddlSeguroDesgravamen" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroDesgravamen_SelectedIndexChanged"></anthem:DropDownList>
                                                                    </div>
                                                                    <div class='form-group'>
                                                                        <label for="txtTasaSeguroDesgravamen">Tasa Seguro</label>
                                                                        <anthem:TextBox ID="txtTasaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                    <div class='form-group'>
                                                                        <label for="txtPrimaSeguroDesgravamen">Prima Mensual Seguro</label>
                                                                        <anthem:TextBox ID="txtPrimaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class="panel panel-info">
                                                                <div class="panel-heading">
                                                                    <h3 class="panel-title">Seguro de Cesantía</h3>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class='form-group'>
                                                                        <label for="ddlSeguroCesantia">Póliza</label>
                                                                        <anthem:DropDownList ID="ddlSeguroCesantia" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroCesantia_SelectedIndexChanged"></anthem:DropDownList>
                                                                    </div>
                                                                    <div class='form-group'>
                                                                        <label for="txtTasaSeguroCesantia">Tasa Seguro</label>
                                                                        <anthem:TextBox ID="txtTasaSeguroCesantia" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                    <div class='form-group'>
                                                                        <label for="txtPrimaSeguroCesantia">Prima Mensual Seguro</label>
                                                                        <anthem:TextBox ID="txtPrimaSeguroCesantia" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 text-center">
                                                            <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarDatosGenerales" class="btn-sm btn-primary" OnClick="btnGuardarDatosGenerales_Click" TextDuringCallBack="Guardando..." runat="server"><span class="glyphicon glyphicon-floppy-disk"></span>Actualizar Participante</anthem:LinkButton>
                                                            <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDatosGenerales" class="btn-sm btn-warning" OnClick="btnCancelarDatosGenerales_Click" TextDuringCallBack="Cancelando..." runat="server"><span class="glyphicon glyphicon-trash"></span>Cancelar Actualización</anthem:LinkButton>
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

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
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
                                            <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución Alzamiento" NullDisplayText="No Aplica" />
                                            <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnModificarProp" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                        ToolTip="Modificar" OnClick="btnModificarProp_Click" TextDuringCallBack="Buscando Registro..." />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-info" id="divDatosPropiedad">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Seguro de Incendio 
                                                <anthem:Label ID="lblDireccion" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class='form-group'>
                                                        <label for="ddlSeguroIncendio">Póliza</label>
                                                        <anthem:DropDownList ID="ddlSeguroIncendio" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroIncendio_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="txtTasaSeguroIncendio">Tasa Seguro</label>
                                                        <anthem:TextBox ID="txtTasaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class='form-group'>
                                                        <label for="txtMontoAseguradoIncendio">Monto Asegurado</label>
                                                        <anthem:TextBox ID="txtMontoAseguradoIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" Enabled="false" AutoCallBack="True"></anthem:TextBox>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="txtPrimaSeguroIncendio">Prima Mensual Seguro</label>
                                                        <anthem:TextBox ID="txtPrimaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" Enabled="false" AutoCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 text-center">
                                                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarProp" class="btn-sm btn-primary" OnClick="btnGuardarProp_Click" TextDuringCallBack="Guardando..." runat="server"><span class="glyphicon glyphicon-floppy-disk"></span>Actualizar Propiedad</anthem:LinkButton>
                                                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarProp" class="btn-sm btn-warning" OnClick="btnCancelarProp_Click" TextDuringCallBack="Cancelando..." runat="server"><span class="glyphicon glyphicon-trash"></span>Cancelar Actualización</anthem:LinkButton>
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
        <%-- <div class="tab-pane fade" id="Propiedades">
            <uc1:DatosPropiedad runat="server" ID="DatosPropiedad" />
        </div>--%>
        <div class="tab-pane fade active in small" id="Instructivo">



            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos del Crédito</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlObjetivo" class="control-label">Objetivo</label>
                                        <anthem:DropDownList ID="ddlObjetivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlObjetivo_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlDestino" class="control-label">Destino</label>
                                        <anthem:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlFinalidad" class="control-label">Finalidad</label>
                                        <anthem:DropDownList ID="ddlFinalidad" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlFinalidad_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlUtilidad" class="control-label">Utilidad</label>
                                        <anthem:DropDownList ID="ddlUtilidad" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info" id="DatosActividadEconominaDeudor">
                        <div class="panel-heading">
                            <h3 class="panel-title">Actividades Económinas (SII) del Deudor Principal (<anthem:Label ID="lblNombreDeudor" runat="server"></anthem:Label>)</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">1° Actividad Económica (SII)</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='form-group'>
                                                        <label for="ddlCategoria1">Categoría</label>
                                                        <anthem:DropDownList ID="ddlCategoria1" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlCategoria1_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="ddlSubCategoria1">Sub Categoría</label>
                                                        <anthem:DropDownList ID="ddlSubCategoria1" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSubCategoria1_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="ddlActividad1">Actividad</label>
                                                        <anthem:DropDownList ID="ddlActividad1" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">2° Actividad Económica (SII)</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='form-group'>
                                                        <label for="ddlCategoria2">Categoría</label>
                                                        <anthem:DropDownList ID="ddlCategoria2" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlCategoria2_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="ddlSubCategoria2">Sub Categoría</label>
                                                        <anthem:DropDownList ID="ddlSubCategoria2" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSubCategoria2_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="ddlActividad2">Actividad</label>
                                                        <anthem:DropDownList ID="ddlActividad2" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">3° Actividad Económica (SII)</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='form-group'>
                                                        <label for="ddlCategoria3">Categoría</label>
                                                        <anthem:DropDownList ID="ddlCategoria3" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlCategoria3_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group'>
                                                        <label for="ddlSubCategoria3">Sub Categoría</label>
                                                        <anthem:DropDownList ID="ddlSubCategoria3" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSubCategoria3_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                    <div class='form-group radio'>
                                                        <label for="ddlActividad3">Actividad</label>
                                                        <anthem:DropDownList ID="ddlActividad3" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
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

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos de Escrituración</h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Abogado</label>
                                        <anthem:DropDownList ID="ddlAbogado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Evento de Desembolso</label>
                                        <anthem:DropDownList ID="ddlEventoDesembolso" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Notaría</label>
                                        <anthem:DropDownList ID="ddlNotaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Mes de Escrituración</label>
                                        <anthem:DropDownList ID="ddlMesEscrituracion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlAbogado">Art. 150</label>
                                        <asp:RadioButtonList ID="rblArt150" runat="server" AutoUpdateAfterCallBack="true" RepeatDirection="Vertical">
                                            <asp:ListItem Value="true">SI</asp:ListItem>
                                            <asp:ListItem Value="false">NO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class='form-group'>
                                        <label for="ddlBeneficiarioTributario">Beneficiario Tributario</label>
                                        <anthem:DropDownList ID="ddlBeneficiarioTributario" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Flujo de Alzamiento de Hipoteca</h3>
                            <anthem:HiddenField ID="hdfSeleccionUnicaHipoteca" AutoUpdateAfterCallBack="true" runat="server" />
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-check">
                                        <anthem:CheckBox ID="chkIndCartaResguardo" CssClass="form-check-input" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnCheckedChanged="chkIndCartaResguardo_CheckedChanged" />
                                        <label class="form-check-label" for="chkIndCartaResguardo">Carta de Resguardo</label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-check">
                                        <anthem:CheckBox ID="chkIndInstruccionNotarial" CssClass="form-check-input" runat="server" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnCheckedChanged="chkIndInstruccionNotarial_CheckedChanged" />
                                        <label class="form-check-label" for="chkIndInstruccionNotarial">Instrucción Notarial</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
        <div class="tab-pane fade" id="DestinoFondos">
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class='form-group'>
                                <label for="ddlTipoDestinoFondo">Distribución de Pagos</label>
                                <anthem:DropDownList ID="ddlTipoDestinoFondo" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoDestinoFondo_SelectedIndexChanged"></anthem:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class='form-group'>
                                <label for="ddlBeneficiario">Beneficiario</label>
                                <anthem:DropDownList ID="ddlBeneficiario" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class='form-group'>
                                <label for="txtMontoDestinoFondo">Monto</label>
                                <anthem:TextBox ID="txtMontoDestinoFondo" runat="server" class="form-control" AutoUpdateAfterCallBack="True" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGrabarDestinoFondo" class="btn-sm btn-primary" OnClick="btnGrabarDestinoFondo_Click" TextDuringCallBack="Guardando..." runat="server"><span class="glyphicon glyphicon-floppy-disk"></span>Grabar Destino</anthem:LinkButton>
                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDestinoFondo" class="btn-sm btn-warning" OnClick="btnCancelarDestinoFondo_Click" TextDuringCallBack="Cancelando..." runat="server"><span class="glyphicon glyphicon-trash"></span>Cancelar Ingreso</anthem:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Destinos Ingresados</h3>
                        </div>
                        <div class="panel-body">
                            <anthem:GridView runat="server" ID="gvDestinoFondos" AutoGenerateColumns="false" Width="100%"
                                AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionTipoDestinoFondo" HeaderText="Distribución de Pago" />
                                    <asp:BoundField DataField="NombreBeneficiario" HeaderText="Beneficiario" />
                                    <asp:BoundField DataField="MontoUF" HeaderText="Monto" />
                                    <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnModificarDestinoFondo" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                ToolTip="Modificar" OnClick="btnModificarDestinoFondo_Click" TextDuringCallBack="Buscando Registro..." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnEliminarDestinoFondo" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                ToolTip="Eliminar" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarDestinoFondo_Click" />
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

                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtTotalDestinar">Total a Destinar</label>
                        <anthem:TextBox ID="txtTotalDestinar" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtPorDestinar">Por Destinar</label>
                        <anthem:TextBox ID="txtPorDestinar" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtDestinado">Monto Destinado</label>
                        <anthem:TextBox ID="txtDestinado" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade" id="ResumenAprobacion">
            <div class="row">
                <div class="col-lg-12">
                    <uc1:ResumenResolucion runat="server" ID="ResumenResolucion" />
                </div>
            </div>
        </div>

        <div class="tab-pane fade" id="divDatosSubsidio">
            <div class="row">
                <div class="col-lg-3"></div>
                <div class="col-lg-6">


                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos del Subsidio</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <label for="txtAhorroPrevio">Ahorro Previo (UF)</label>
                                    <anthem:TextBox ID="txtAhorroPrevioSubsidio" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,4,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <label for="txtSerieSubsidio">Serie N°</label>
                                    <anthem:TextBox ID="txtSerieSubsidio" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <label for="txtNumeroCertificadoSubsidio">N° de Certificado</label>
                                    <anthem:TextBox ID="txtNumeroCertificadoSubsidio" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label for="ddlMesCertificadoSubsidio">Mes del Certificado</label>
                                    <anthem:DropDownList ID="ddlMesCertificadoSubsidio" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    <label for="ddlAñoCertificadoSubsidio">Año del Certificado</label>
                                    <anthem:DropDownList ID="ddlAñoCertificadoSubsidio" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    <label for="txtNumeroLibretaSubsidio">N° Libreta de Ahorro</label>
                                    <anthem:TextBox ID="txtNumeroLibretaSubsidio" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3"></div>


            </div>
        </div>


    </div>
    <div class="modal fade" id="ModalDocumentos" tabindex="-1" role="dialog" aria-labelledby="ModalDocumentosLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content  modal-content-client">
                <div class="modal-header">
                    <h3 class="modal-title" id="ModalDocumentosLabel">Documentos</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <anthem:LinkButton ID="btnImprimirOrden" runat="server" CssClass="btn-sm btn-success" OnClick="btnImprimirOrden_Click" EnableCallBack="False"><span class="glyphicon glyphicon-cloud-download"></span> Orden de Escrituración</anthem:LinkButton>
                        </div>
                        <div class="col-md-6">
                            <anthem:LinkButton ID="btnImprimirHojaResumen" runat="server" CssClass="btn-sm btn-success" OnClick="btnImprimirHojaResumen_Click" EnableCallBack="False"><span class="glyphicon glyphicon-cloud-download"></span> Hoja Resumen</anthem:LinkButton>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <h5>Debe Generar Todos los Documentos</h5>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
