<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ConfirmacionPropiedadSeguros.aspx.cs" Inherits="WorkFlow.Eventos.WfOriginacionValidacionAntecedentes.ConfirmacionPropiedadSeguros" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/ResumenResolucion.ascx" TagPrefix="uc1" TagName="ResumenResolucion" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
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


        function DesplegarDatosPropiedad(Visible) {
            try {
                if (Visible === '1') {
                    $('#divDatosPropiedadTasacion').show(500);
                }
                else {
                    $('#divDatosPropiedadTasacion').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }

        function DesplegarDatosEETT(Visible) {
            try {
                if (Visible === '1') {
                    $('#DatosEstudioTitulo').show(500);
                }
                else {
                    $('#DatosEstudioTitulo').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }
        function InicioConfirmacion() {
            try {
                $("#<%= txtFechaTasacion.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0,
                    constrainInput: true //La entrada debe cumplir con el formato
                });
                $("#<%= txtFechaRecepcionFinal.ClientID %>").datepicker({
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

        <div class="col-lg-8 col-md-8 col-sm-8">

            <ul class="nav small nav-tabs">
                <li class="active"><a href="#ResumenCredito" data-toggle="tab" aria-expanded="true">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <%--<li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>--%>
                <%--<li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>--%>
                <%--<li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen y Resolución</a></li>--%>
                <li class=""><a href="#ConfTasacion" data-toggle="tab" aria-expanded="true">Confirmación de Tasaciones</a></li>
                <li class=""><a href="#ConfEETT" data-toggle="tab" aria-expanded="false">Confirmación de Estudio de Títulos</a></li>
                <li class=""><a href="#ConfDPS" data-toggle="tab" aria-expanded="false">Confirmación de DPS</a></li>
                <%--<li class="active"><a href="#DocumentosOriginacionComercial" data-toggle="tab" aria-expanded="true">Revisión Carpeta Comercial</a></li>--%>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-4">

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

        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btndocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> carpeta digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade active in" id="ResumenCredito">
            <uc1:DatosResumen runat="server" ID="DatosResumen" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade" id="Credito">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>
        <%--<div class="tab-pane fade" id="Participantes">
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
        </div>
        <div class="tab-pane fade" id="Propiedades">
            <uc1:DatosPropiedad runat="server" ID="DatosPropiedad" />
        </div>
        <div class="tab-pane fade" id="Resumen">
            <div class="row">
                <div class="col-md-12">
                    <uc1:ResumenResolucion runat="server" ID="ResumenResolucion" />
                </div>
            </div>
        </div>--%>
        <div class="tab-pane fade" id="ConfTasacion">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Garantias de la Solicitud
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <anthem:GridView runat="server" ID="gvPropiedadesTasacion" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowPaging="False" AllowSorting="False"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                            <asp:BoundField DataField="Propiedad_Id" HeaderText="Id Propiedad" Visible="false" />
                                            <asp:BoundField DataField="DireccionCompleta" HeaderText="Propiedad" />
                                            <asp:BoundField DataField="MontoAsegurado" HeaderText="Monto Asegurado" />
                                            <asp:BoundField DataField="NombreEmpresaTasacion" HeaderText="Fábrica Tasación" NullDisplayText="Pendiente" />
                                            <asp:BoundField DataField="NombreTasador" HeaderText="Tasador" NullDisplayText="Pendiente" />
                                            <asp:BoundField DataField="FechaTasacion" HeaderText="Fecha de Tasación" DataFormatString="{0:d}" NullDisplayText="Pendiente" />
                                            <asp:BoundField DataField="DescripcionEstadoTasacion" HeaderText="Estado del Flujo" />
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
                            <div class="row small" id="divDatosPropiedadTasacion">

                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Datos Tasación 
                                                     <anthem:Label ID="lblDireccion" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlSeguroIncendio">Póliza</label>
                                                    <anthem:DropDownList ID="ddlSeguroIncendio" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroIncendio_SelectedIndexChanged"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtTasaSeguroIncendio">Tasa Seguro</label>
                                                    <anthem:TextBox ID="txtTasaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMontoAseguradoIncendio">Monto Asegurado</label>
                                                    <anthem:TextBox ID="txtMontoAseguradoIncendio" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtPrimaSeguroIncendio">Prima Mensual Seguro</label>
                                                    <anthem:TextBox ID="txtPrimaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" Enabled="false" AutoCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtRolManzana">Rol - Manzana</label>
                                                    <anthem:TextBox ID="txtRolManzana" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtRolSitio">Rol - Sitio</label>
                                                    <anthem:TextBox ID="txtRolSitio" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">


                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="chkIndUsoGoce" class="control-label"></label>
                                                    <anthem:CheckBox ID="chkIndUsoGoce" runat="server" Text="Uso y Goce" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnCheckedChanged="chkIndUsoGoce_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtFechaTasacion">Fecha Tasación</label>
                                                    <anthem:TextBox ID="txtFechaTasacion" runat="server" onblur="esFechaValida(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlComunaSii">Comuna - SII</label>
                                                    <anthem:DropDownList ID="ddlComunaSii" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlInmobiliaria">Inmobiliaria</label>
                                                    <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlProyecto">Proyecto</label>
                                                    <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">

                                                <div class='form-group'>
                                                    <label for="chIndDfl2" class="control-label"></label>
                                                    <anthem:CheckBox ID="chIndDfl2" AutoUpdateAfterCallBack="true" class="form-control" runat="server" Text="DFL-2" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtAñoConstruccion">Año de Construcción</label>
                                                    <anthem:TextBox ID="txtAñoConstruccion" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" MaxLength="4" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlTipoInmueble" class="control-label">Tipo Inmueble</label>
                                                    <anthem:DropDownList ID="ddlTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlAntiguedad" class="control-label">Antigüedad</label>
                                                    <anthem:DropDownList ID="ddlAntiguedadProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlTipoConstruccion" class="control-label">Tipo de Construcción</label>
                                                    <anthem:DropDownList ID="ddlTipoConstruccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="ddlDestinoProp" class="control-label">Destino</label>
                                                    <anthem:DropDownList ID="ddlDestinoProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtFechaRecepcionFinal">Fecha Recepción Final</label>
                                                    <anthem:TextBox ID="txtFechaRecepcionFinal" runat="server" onblur="esFechaValida(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtPermisoEdificacion">Permiso de Edificación</label>
                                                    <anthem:TextBox ID="txtPermisoEdificacion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>



                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtValorComercial">Valor Comercial</label>
                                                    <anthem:TextBox ID="txtValorComercial" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMontoTasacion">Monto Tasación</label>
                                                    <anthem:TextBox ID="txtMontoTasacion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMontoLiquidacion">Monto de Liquidación</label>
                                                    <anthem:TextBox ID="txtMontoLiquidacion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMetrosTerreno">Metros Terreno</label>
                                                    <anthem:TextBox ID="txtMetrosTerreno" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">

                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMetrosConstruccion">Metros Construcción</label>
                                                    <anthem:TextBox ID="txtMetrosConstruccion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMetrosLogia">Metros Logia</label>
                                                    <anthem:TextBox ID="txtMetrosLogia" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class='form-group'>
                                                    <label for="txtMetrosTerraza">Metros Terraza</label>
                                                    <anthem:TextBox ID="txtMetrosTerraza" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
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

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Tasaciones Relacionadas
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">

                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#TasacionInd" data-toggle="tab" aria-expanded="true">Individuales/Previas</a></li>
                                        <li class=""><a href="#TasacionPiloto" data-toggle="tab" aria-expanded="false">Pilotos</a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="tab-content">
                                        <div class="tab-pane fade active in" id="TasacionInd" style="padding-top: 10px;">
                                            <anthem:Repeater ID="rep" OnItemDataBound="rpt_tasaciones" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class='form-group'>
                                                                <label>Solicitud de Tasación:</label>
                                                                <anthem:Label ID="lblSolicitudTasacion" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>

                                                                <anthem:GridView runat="server" ID="gvRelacionados" AutoGenerateColumns="false" Width="100%"
                                                                    PageSize="10" AllowPaging="False" AllowSorting="False"
                                                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                                    <RowStyle CssClass="GridItem" />
                                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Solicitud_Id" HeaderText="" Visible="false" />
                                                                        <asp:BoundField DataField="SolicitudEstudioTitulo_Id" HeaderText="" Visible="false" />
                                                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                                        <asp:BoundField DataField="Propiedad_Id" HeaderText="Id Propiedad" Visible="false" />
                                                                        <asp:BoundField DataField="DireccionCompleta" HeaderText="Propiedad" />
                                                                        <asp:BoundField DataField="MontoAsegurado" HeaderText="Monto Asegurado" />
                                                                        <asp:BoundField DataField="NombreEmpresaTasacion" HeaderText="Fábrica Tasación" NullDisplayText="Pendiente" />
                                                                        <asp:BoundField DataField="NombreTasador" HeaderText="Tasador" NullDisplayText="Pendiente" />
                                                                        <asp:BoundField DataField="FechaTasacion" HeaderText="Fecha de Tasación" DataFormatString="{0:d}" NullDisplayText="Pendiente" />
                                                                        <asp:BoundField DataField="DescripcionEstadoTasacion" HeaderText="Estado del Flujo" />
                                                                        <asp:BoundField DataField="EstadoTasacion_Id" HeaderText="" Visible="false" />
                                                                        <asp:TemplateField HeaderText="Importar a Solicitud" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <anthem:Button ID="btnImportarData" runat="server" CssClass="btnsmall btn-success" Text="Importar" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                                    OnClick="btnImportarDataTasacion_Click" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </anthem:GridView>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </anthem:Repeater>
                                        </div>
                                        <div class="tab-pane fade" id="TasacionPiloto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <anthem:GridView runat="server" ID="gvTasacionesPiloto" AutoGenerateColumns="false" Width="100%"
                                                        PageSize="10" AllowPaging="False" AllowSorting="False"
                                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                        <RowStyle CssClass="GridItem" />
                                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="Solicitud" />
                                                            <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" />
                                                            <asp:BoundField DataField="DescripcionProyecto" HeaderText="Proyecto" />

                                                            <asp:TemplateField HeaderText="Ver Tasacion Piloto" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <anthem:Button ID="btnVerTasacionPiloto" runat="server" CssClass="btnsmall btn-success" Text="Ver Informe" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                        OnClick="btnVerTasacionPiloto_Click" />
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
        <div class="tab-pane fade" id="ConfEETT">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Garantías de la Solicitud
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <anthem:GridView runat="server" ID="gvEETT" AutoGenerateColumns="false" Width="100%"
                                    PageSize="10" AllowPaging="False" AllowSorting="False"
                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="Propiedad_Id" HeaderText="Id Propiedad" Visible="false" />
                                        <asp:BoundField DataField="DireccionCompleta" HeaderText="Propiedad" />
                                        <asp:BoundField DataField="DescripcionDestino" HeaderText="Destino" />
                                        <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución Alzamiento" NullDisplayText="Sin Alzamiento de Hipoteca" />
                                        <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnSeleccionarPropEETT" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                    ToolTip="Modificar" OnClick="btnSeleccionarPropEETT_Click" TextDuringCallBack="Buscando Registro..." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </anthem:GridView>
                            </div>
                            <div class="row" id="DatosEstudioTitulo">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Datos Estudio de Título
                                    <anthem:Label ID="lblDireccionEETT" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <anthem:CheckBox ID="chkIndAlzamientoHipoteca" AutoUpdateAfterCallBack="true" Text="Incluye Alzamiento de Hipoteca" runat="server" AutoCallBack="True" OnCheckedChanged="chkIndAlzamientoHipoteca_CheckedChanged" />
                                                </div>
                                                <div class="col-md-3">
                                                    <div class='form-group'>
                                                        <label for="ddlInstitucionCartaResguardo">Institución Alzamiento Hipoteca</label>
                                                        <anthem:DropDownList ID="ddlInstitucionAlzamientoHipoteca" class="form-control" Enabled="false" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                    <anthem:LinkButton ID="btnGrabarAlzamiento" class="btn btn-primary" runat="server" TextDuringCallBack="Procesando" EnabledDuringCallBack="false" OnClick="btnGrabarAlzamiento_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Alzamiento</anthem:LinkButton>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                    <anthem:LinkButton ID="btnCancelarAlzamiento" class="btn btn-primary" runat="server" TextDuringCallBack="Procesando" EnabledDuringCallBack="false" OnClick="btnCancelarAlzamiento_Click"><span class="glyphicon glyphicon-folder-close"></span> Limpiar</anthem:LinkButton>
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
                            <h3 class="panel-title">EETT Realizados
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#EETTInd" data-toggle="tab" aria-expanded="true">Estudio de Titulos Individual</a></li>
                                        <li class=""><a href="#EETTPiloto" data-toggle="tab" aria-expanded="false">Estudio de Titulos Piloto</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="tabEETTInd" class="tab-content">
                                        <div class="tab-pane fade active in" id="EETTInd" style="padding-top: 10px;">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <anthem:Repeater ID="repEETT" OnItemDataBound="rpt_eett" runat="server">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class='form-group'>
                                                                        <label>Solicitud de EETT:</label>
                                                                        <anthem:Label ID="lblSolicitudEETT" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>

                                                                        <anthem:GridView runat="server" ID="gvRelacionadosEETT" AutoGenerateColumns="false" Width="100%"
                                                                            PageSize="10" AllowPaging="False" AllowSorting="False"
                                                                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                                            <RowStyle CssClass="GridItem" />
                                                                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                            <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Solicitud_Id" HeaderText="" Visible="false" />
                                                                                <asp:BoundField DataField="SolicitudTasacion_Id" HeaderText="" Visible="false" />
                                                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                                                <asp:BoundField DataField="Propiedad_Id" HeaderText="Id Propiedad" Visible="false" />
                                                                                <asp:BoundField DataField="DireccionCompleta" HeaderText="Propiedad" />
                                                                                <asp:BoundField DataField="DescripcionDestino" HeaderText="Destino" />
                                                                                <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución Alzamiento" NullDisplayText="Sin Alzamiento de Hipoteca" />
                                                                                <asp:BoundField DataField="DescripcionEstadoEETT" HeaderText="Estado del Flujo" />
                                                                                <asp:BoundField DataField="EstadoEstudioTitulo_Id" HeaderText="" Visible="false" />
                                                                                <asp:TemplateField HeaderText="Importar a Solicitud" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <anthem:Button ID="btnImportarDataEETT" runat="server" CssClass="btnsmall btn-success" Text="Importar" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                                            OnClick="btnImportarDataEETT_Click" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </anthem:GridView>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </anthem:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="EETTPiloto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <anthem:GridView runat="server" ID="gvEETTPiloto" AutoGenerateColumns="false" Width="100%"
                                                        PageSize="10" AllowPaging="False" AllowSorting="False"
                                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                        <RowStyle CssClass="GridItem" />
                                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="Solicitud" />
                                                            <asp:BoundField DataField="NombreInstitucionAlzamientoHipoteca" HeaderText="Institución" NullDisplayText="Sin Alzamiento" />
                                                            <asp:BoundField DataField="DescripcionEstadoEETT" HeaderText="Estado" />
                                                            <asp:TemplateField HeaderText="Ver EETT Piloto" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <anthem:Button ID="btnVerEETTPiloto" runat="server" CssClass="btnsmall btn-success" Text="Ver Informe" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                        OnClick="btnVerEETTPiloto_Click" />
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
        <div class="tab-pane fade" id="ConfDPS">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Participantes de la Solicitud
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <anthem:GridView runat="server" ID="gvParticipantesDPS" AutoGenerateColumns="false" Width="100%"
                                    PageSize="5" AllowSorting="True"
                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                                        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                                        <asp:BoundField DataField="FechaIngresoDPS" HeaderText="Fecha de Envío DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="FechaAprobacionDPS" HeaderText="Fecha de Resolución DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="DescripcionEstadoDps" HeaderText="Estado DPS" NullDisplayText="No Procesado" />
                                        <asp:BoundField DataField="DescripcionSeguroDesgravamen" HeaderText="Seg. Desgravamen" NullDisplayText="Sin Seguro" />
                                    </Columns>
                                </anthem:GridView>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">DPS Realizadas
                                            </h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <anthem:Repeater ID="repDPS" OnItemDataBound="rpt_dps" runat="server">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class='form-group'>
                                                                        <label>Solicitud de DPS:</label>
                                                                        <anthem:Label ID="lblSolicitudDPS" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>

                                                                        <anthem:GridView runat="server" ID="gvRelacionadosDPS" AutoGenerateColumns="false" Width="100%"
                                                                            PageSize="10" AllowPaging="False" AllowSorting="False"
                                                                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                                                            <RowStyle CssClass="GridItem" />
                                                                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                            <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                                                                                <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                                                                                <asp:BoundField DataField="FechaIngresoDPS" HeaderText="Fecha de Envío DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                                                                                <asp:BoundField DataField="FechaAprobacionDPS" HeaderText="Fecha de Resolución DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                                                                                <asp:BoundField DataField="DescripcionEstadoDps" HeaderText="Estado DPS" NullDisplayText="No Procesado" />
                                                                                <asp:BoundField DataField="DescripcionSeguroDesgravamen" HeaderText="Seg. Desgravamen" NullDisplayText="Sin Seguro" />
                                                                                <asp:TemplateField HeaderText="Importar a Solicitud" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <anthem:Button ID="btnImportarDataDPS" runat="server" CssClass="btnsmall btn-success" Text="Importar" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                                                                            OnClick="btnImportarDataDPS_Click" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </anthem:GridView>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </anthem:Repeater>
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
