<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="AsignacionTasador.aspx.cs" Inherits="WorkFlow.Eventos.Tasacion.AsignacionTasador" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Observaciones" data-toggle="tab" aria-expanded="false">Bitácora</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class="active"><a href="#CoordinacionTasacion" data-toggle="tab" aria-expanded="true">Coordinación de Tasación</a></li>
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
        <div class="tab-pane fade active in" id="CoordinacionTasacion">
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
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Propiedades a Tasar</h3>
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
                                            <asp:BoundField DataField="NombreEmpresaTasacion" HeaderText="Empresa" />
                                            <asp:BoundField DataField="NombreTasadorTasacionPrevia" HeaderText="Tasador Previo" />
                                            <asp:BoundField DataField="NombreTasador" HeaderText="Tasador Asignado" />
                                            <asp:BoundField DataField="FechaCoordinacionTasacion" HeaderText="Fecha Visita Tasación" NullDisplayText="Pendiente" />
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
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Contacto Tasación
                                                    <anthem:Label ID="lblDireccionTasacion" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class='form-group'>
                                        <label for="AddlFabTasadores" class="control-label">Fábrica Tasadores</label>
                                        <anthem:DropDownList ID="AddlFabTasadores" Enabled="false" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                        <div class='form-group'>
                                            <label for="AddlTasador" class="control-label">Tasador</label>
                                            <anthem:DropDownList ID="AddlTasador" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True"></anthem:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-4">

                                    <div class='form-group'>
                                        <anthem:Label ID="lblFechaSolicitudTasacion" runat="server" AutoUpdateAfterCallBack="true">Fecha</anthem:Label>
                                        
                                        <anthem:TextBox ID="txtFechaSolicitudTasacion" Enabled="false" runat="server" onblur="esFechaValida(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtNombreContactoTasacion">Nombre Contacto</label>
                                        <anthem:TextBox ID="txtNombreContactoTasacion" Enabled="false" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>

                                </div>
                                <div class="col-lg-4">

                                    <div class='form-group'>
                                        <label for="txtEmailContactoTasacion">Email Contacto</label>
                                        <anthem:TextBox ID="txtEmailContactoTasacion" Enabled="false" runat="server" placeholder="name@example.com" onBlur="validarEmail(this)" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTelefonoContactoTasacion">Teléfono Contacto</label>
                                        <anthem:TextBox ID="txtTelefonoContactoTasacion" Enabled="false" runat="server" onKeyPress="return SoloNumeros(event,this.value,0,this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row text-center">
                                <div class="col-md-12">
                                    <anthem:LinkButton ID="btnGrabarTasacion" class="btn btn-primary" runat="server" OnClick="btnGrabarTasacion_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Datos</anthem:LinkButton>
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
