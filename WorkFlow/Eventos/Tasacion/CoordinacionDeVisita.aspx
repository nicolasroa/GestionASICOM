<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="CoordinacionDeVisita.aspx.cs" Inherits="WorkFlow.Eventos.Tasacion.CoordinacionDeVisita" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function InicioSolicitudTasaciones() {
            try {
                $("#<%= txtFechaSolicitudTasacion.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0,
                    constrainInput: true //La entrada debe cumplir con el formato
                });
                $("#<%= txtFechaCoordinacionTasacion.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
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
                    <div class="panel panel-success">
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
                                            <asp:BoundField DataField="FechaSolicitudTasacion" HeaderText="Fecha de Solicitud de Tasación" NullDisplayText="Pendiente" DataFormatString="{0:d}" />
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
                <div class="col-md-6">
                    <div class="panel panel-success">
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
                                    </div>
                                    <div class='form-group'>
                                        <label for="AddlTasador" class="control-label">Tasador</label>
                                        <anthem:DropDownList ID="AddlTasador" runat="server" Enabled ="false" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                    </div>

                                </div>
                                <div class="col-lg-4">

                                    <div class='form-group'>
                                        <label for="txtFechaSolicitudTasacion">Fecha Solicitud Tasación</label>
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
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Coordinación de Visita</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtFechaCoordinacionTasacion">Fecha de la Visita</label>
                                        <anthem:TextBox ID="txtFechaCoordinacionTasacion" runat="server" onblur="esFechaValidaFutura(this);" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtHoraCoordinacionTasacion">Hora de la Visita</label>
                                        <anthem:DropDownList ID="ddlHoraVisitaTasacion" runat="server" AutoUpdateAfterCallBack="true" CssClass="form-control">
                                            <asp:ListItem Value="">-- Hora Visita --</asp:ListItem>
                                            <asp:ListItem Value="8:00">8:00</asp:ListItem>
                                            <asp:ListItem Value="8:30">8:30</asp:ListItem>
                                            <asp:ListItem Value="9:00">9:00</asp:ListItem>
                                            <asp:ListItem Value="9:30">9:30</asp:ListItem>
                                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                                            <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                            <asp:ListItem Value="13:30">13:30</asp:ListItem>
                                            <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                            <asp:ListItem Value="14:30">14:30</asp:ListItem>
                                            <asp:ListItem Value="15:00">15:00</asp:ListItem>
                                            <asp:ListItem Value="15:30">15:30</asp:ListItem>
                                            <asp:ListItem Value="16:00">16:00</asp:ListItem>
                                            <asp:ListItem Value="16:30">16:30</asp:ListItem>
                                            <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                            <asp:ListItem Value="17:30">17:30</asp:ListItem>
                                            <asp:ListItem Value="10:00">18:00</asp:ListItem>
                                        </anthem:DropDownList>
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
