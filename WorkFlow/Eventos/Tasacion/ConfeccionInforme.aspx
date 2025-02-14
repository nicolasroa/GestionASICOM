<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ConfeccionInforme.aspx.cs" Inherits="WorkFlow.Eventos.Tasacion.ConfeccionInforme" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function InicioSolicitudTasaciones() {
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

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Observaciones" data-toggle="tab" aria-expanded="false">Observaciones</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class="active"><a href="#DatosTasacion" data-toggle="tab" aria-expanded="true">Informe de la Tasación</a></li>
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
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Observaciones">
            <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade active in" id="DatosTasacion">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Solicitante del Crédito</h3>
                        </div>
                        <div class="panel-body">
                            <anthem:GridView runat="server" ID="gvParticipantes" AutoGenerateColumns="false" Width="100%"
                                PageSize="5" AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                                    <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                                    <asp:BoundField DataField="RutCompleto" HeaderText="Rut" />
                                    <asp:BoundField DataField="DescripcionSexo" HeaderText="Sexo" NullDisplayText="No Ingresado" />
                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Propiedades</h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <anthem:GridView runat="server" ID="gvPropiedadesTasacion" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowPaging="True" AllowSorting="True"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                                            <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                                            <asp:BoundField DataField="FechaSolicitudTasacion" HeaderText="Fecha de Solicitud de Tasación" NullDisplayText="No Aplica" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="FechaCoordinacionTasacion" HeaderText="Fecha Visita Tasación" NullDisplayText="No Aplica" />
                                            <asp:BoundField DataField="FechaTasacion" HeaderText="Fecha de Tasación" NullDisplayText="Pendiente" DataFormatString="{0:d}" />
                                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnSeleccionarPropTasacion" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                        ToolTip="Modificar" OnClick="btnSeleccionarPropTasacion_Click" TextDuringCallBack="Buscando Registro..." />
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
            <div class="row small ">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos Tasación 
                                                    <anthem:Label ID="lblDireccionTasacion" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Datos Generales
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="AddlFabTasadores" class="control-label">Fábrica Tasadores</label>
                                                        <anthem:DropDownList ID="AddlFabTasadores" Enabled="false" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="AddlTasador" class="control-label">Tasador</label>
                                                        <anthem:DropDownList ID="AddlTasador" Enabled="false" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtFechaTasacion">Fecha Tasación</label>
                                                        <anthem:TextBox ID="txtFechaTasacion" runat="server" onblur="esFechaValida(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="ddlComunaSii">Comuna - SII</label>
                                                        <anthem:DropDownList ID="ddlComunaSii" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="ddlInmobiliaria">Inmobiliaria</label>
                                                        <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="ddlProyecto">Proyecto</label>
                                                        <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">

                                                    <div class='form-group'>
                                                        <label for="chIndDfl2" class="control-label"></label>
                                                        <anthem:CheckBox ID="chIndDfl2" AutoUpdateAfterCallBack="true" class="control-label" runat="server" Text="DFL-2" />
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>


                                </div>
                                <div class="col-lg-6">


                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Datos Generales
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="ddlTipoInmueble" class="control-label">Tipo Inmueble</label>
                                                        <anthem:DropDownList ID="ddlTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="ddlAntiguedad" class="control-label">Antigüedad</label>
                                                        <anthem:DropDownList ID="ddlAntiguedadProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="ddlTipoConstruccion" class="control-label">Tipo de Construcción</label>
                                                        <anthem:DropDownList ID="ddlTipoConstruccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">

                                                    <div class='form-group'>
                                                        <label for="ddlDestinoProp" class="control-label">Destino</label>
                                                        <anthem:DropDownList ID="ddlDestinoProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Datos Recepción Final
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="txtFechaRecepcionFinal">Fecha Recepción Final</label>
                                                        <anthem:TextBox ID="txtFechaRecepcionFinal" runat="server" onblur="esFechaValida(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="txtPermisoEdificacion">Permiso de Edificación</label>
                                                        <anthem:TextBox ID="txtPermisoEdificacion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="txtAñoConstruccion">Año de Construcción</label>
                                                        <anthem:TextBox ID="txtAñoConstruccion" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" MaxLength="4" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="chkIndUsoGoce" class="control-label"></label>
                                                        <anthem:CheckBox ID="chkIndUsoGoce" runat="server" Text="Uso y Goce" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnCheckedChanged="chkIndUsoGoce_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="txtRolManzana">Rol - Manzana</label>
                                                        <anthem:TextBox ID="txtRolManzana" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class='form-group'>
                                                        <label for="txtRolSitio">Rol - Sitio</label>
                                                        <anthem:TextBox ID="txtRolSitio" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>



                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Detalle de la Propiedad
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtValorComercial">Valor Comercial</label>
                                                        <anthem:TextBox ID="txtValorComercial" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMontoTasacion">Monto Tasación</label>
                                                        <anthem:TextBox ID="txtMontoTasacion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMontoAseguradoIncendio">Monto Asegurado</label>
                                                        <anthem:TextBox ID="txtMontoAseguradoIncendio" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMontoLiquidacion">Monto de Liquidación</label>
                                                        <anthem:TextBox ID="txtMontoLiquidacion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMetrosTerreno">Metros Terreno</label>
                                                        <anthem:TextBox ID="txtMetrosTerreno" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMetrosConstruccion">Metros Construcción</label>
                                                        <anthem:TextBox ID="txtMetrosConstruccion" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMetrosLogia">Metros Logia</label>
                                                        <anthem:TextBox ID="txtMetrosLogia" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMetrosTerraza">Metros Terraza</label>
                                                        <anthem:TextBox ID="txtMetrosTerraza" runat="server" onKeyPress="return SoloNumeros(event,this.value,4,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row text-center">
                                <div class="col-md-12">
                                    <anthem:LinkButton ID="btnGrabarTasacion" class="btn btn-primary" runat="server" EnabledDuringCallBack="false" TextDuringCallBack="Grabando Datos..." OnClick="btnGrabarTasacion_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Tasación</anthem:LinkButton>

                                    <anthem:LinkButton ID="btnCancelarTasacion" class="btn btn-warning" runat="server" OnClick="btnCancelarTasacion_Click"><span class="glyphicon glyphicon-trash"></span> Cancelar Ingreso</anthem:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
