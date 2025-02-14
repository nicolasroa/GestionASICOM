<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="BandejaEntrada.aspx.cs" Inherits="WorkFlow.Aplicaciones.BandejaEntrada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function MostrarFormularioBuscarSolicitudes() {
            try {

                var esVisible = $("#DivDatosDocumentoSolicitud").is(":visible");
                if (esVisible === false) {
                    $('#DivDatosDocumentoSolicitud').show(500);
                    $('#lnkBuscar').html("<span class='glyphicon glyphicon-circle-arrow-left'></span> Ocultar");
                }
                else {
                    $('#DivDatosDocumentoSolicitud').hide(500);
                    $('#lnkBuscar').html("<span class='glyphicon glyphicon-search'></span> Buscar Solicitudes");
                }
            } catch (e) {
                alert(e.Message);
            }

        }

        function DetalleActivacion(MesEscritura, AñoEscritura, Activada) {
            try {

                hdfMesEscritura = document.getElementById('<%=hdfMesEscritura.ClientID%>');
                hdfAñoEscritura = document.getElementById('<%=hdfAñoEscritura.ClientID%>');
                hdfActivada = document.getElementById('<%=hdfActivada.ClientID%>');
                hdfMesEscritura.value = MesEscritura;
                hdfAñoEscritura.value = AñoEscritura;
                hdfActivada.value = Activada;
                $('#' + '<%=btnBuscarDetalleActivacion.ClientID%>').trigger("click");


            } catch (e) {
                alert(e.message);
            }
        }


        function DetalleEtapa(Etapa_Id) {
            try {

                IdEtapa = document.getElementById('<%=hdfEtapa_Id.ClientID%>');
                IdEtapa.value = Etapa_Id;
                $('#' + '<%=btnBuscarEtapa.ClientID%>').trigger("click");

            } catch (e) {
                alert(e.message);
            }
        }
        function DetalleEvento(Evento_Id) {
            try {

                IdEvento = document.getElementById('<%=hdfEvento_Id.ClientID%>');
                IdEvento.value = Evento_Id;
                $('#' + '<%=btnBuscarEvento.ClientID%>').trigger("click");

            } catch (e) {
                alert(e.message);
            }
        }
        function DetalleEstadoEtapa(Etapa_Id, Estado_Id) {
            try {

                IdEtapa = document.getElementById('<%=hdfEtapa_Id.ClientID%>');
                IdEtapa.value = Etapa_Id;
                IdEstado = document.getElementById('<%=hdfEstadoVigencia_Id.ClientID%>');
                IdEstado.value = Estado_Id;
                $('#' + '<%=btnBuscarEstadoEtapa.ClientID%>').trigger("click");

            } catch (e) {
                alert(e.message);
            }
        }
        function DetalleEstadoEvento(Evento_Id, Estado_Id) {
            try {

                IdEvento = document.getElementById('<%=hdfEvento_Id.ClientID%>');
                IdEvento.value = Evento_Id;
                IdEstado = document.getElementById('<%=hdfEstadoVigencia_Id.ClientID%>');
                IdEstado.value = Estado_Id;
                $('#' + '<%=btnBuscarEstadoEvento.ClientID%>').trigger("click");

            } catch (e) {
                alert(e.message);
            }
        }

    </script>

    <div class="row ">
        <div class="col-md-6">
            <anthem:LinkButton ID="btnVerActivacionesRechazadas" CssClass="btn-block btn-sm btn-warning" runat="server" Visible="false" AutoUpdateAfterCallBack="true" EnableCallBack="False" OnClick="btnVerActivacionesRechazadas_Click"><span class="glyphicon glyphicon-warning-sign"></span>Activaciones Rechazadas</anthem:LinkButton>
        </div>
        <div class="col-md-6">
            <anthem:LinkButton ID="btnVerOperacionesDesactivadas" CssClass="btn-block btn-sm btn-danger" runat="server" Visible="false" AutoUpdateAfterCallBack="true" EnableCallBack="False" OnClick="btnVerOperacionesDesactivadas_Click"><span class="glyphicon glyphicon-warning-sign"></span>Operaciones Desactivadas</anthem:LinkButton>
        </div>
    </div>


    <div class="row spacenavmax">
        <div class="col-md-12">
            <ul class="nav nav-tabs2">
                <li class="active"><a href="#Bandeja" data-toggle="tab" aria-expanded="true">Bandeja de Entrada</a></li>
                <li class=""><a href="#BandejaSeguimiento" data-toggle="tab" aria-expanded="false">Bandeja de Seguimiento</a></li>
                <li class=""><a href="#Activaciones" data-toggle="tab" aria-expanded="false">Activaciones</a></li>


            </ul>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade active in" id="Bandeja">
            <div class="row">
                <div class="col-md-6">
                    <h3 class="titulo">Bandeja de Entrada
                        <br>
                    </h3>

                </div>
               
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading panel-pading">
                    <a id="lnkBuscar" href="#" class="btn-sm btn-buscarSol" onclick="MostrarFormularioBuscarSolicitudes();">
                        <span class="glyphicon glyphicon-search"></span>Buscar Solicitudes</a>
                </div>
                <div class="panel-body" id="DivDatosDocumentoSolicitud" hidden="hidden">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class='form-group'>
                                <label for="txtNumeroSolicitud" class="text-left">Número de Solicitud</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNumeroSolicitud" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                            </div>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class='form-group'>
                                <label for="txtRutCliente" class="text-left">Rut Cliente</label>
                                <anthem:TextBox ID="txtRutCliente" runat="server" class="form-control" onchange="validaRut(this);"></anthem:TextBox>
                            </div>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class='form-group'>
                                <label for="txtOperacion" class="text-left">Número de Operación</label>
                                <anthem:TextBox ID="txtOperacion" runat="server" class="form-control"></anthem:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                                OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." EnableCallBack="False" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 text-left">

                    <a href="#"><span class="badge">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span></a>


                </div>
                <div class="col-md-6 text-right">
                    <anthem:LinkButton ID="lnkBuscarEtapas" EnableCallBack="false" class="btn-sm btn-default" AutoUpdateAfterCallBack="true" runat="server" OnClick="lnkBuscarEtapas_Click" Visible="False"><span class="glyphicon glyphicon-circle-arrow-left"></span> Volver a las Etapas</anthem:LinkButton>
                    <anthem:LinkButton ID="lnkBuscarEventos" EnableCallBack="false" class="btn-sm btn-default" AutoUpdateAfterCallBack="true" runat="server" OnClick="lnkBuscarEventos_Click" Visible="False"><span class="glyphicon glyphicon-circle-arrow-left"></span> Volver a los Eventos</anthem:LinkButton>

                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEtapas">
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>

            <div class="row" style="margin-top: 5px;">
                <div class="col-md-12 small">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:TemplateField HeaderText="Abrir Evento" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnAbrirAvento" runat="server" ImageUrl="~/Img/Grid/AbrirEvento_grid.png"
                                        PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                        ToolTip="Abrir" OnClick="btnAbrirAvento_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Solicitud_Id" HeaderText="Número de Solicitud" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DescripcionEvento" HeaderText="Evento" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="DescripcionMalla" HeaderText="Malla" />
                            <asp:BoundField DataField="NombreSolicitantePrincipal" HeaderText="Solicitante" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Inicio" />
                            <asp:BoundField DataField="FechaEsperada" HeaderText="Fecha Esperada" />
                            <asp:BoundField DataField="UsuarioResponsable" HeaderText="Responsable" />

                        </Columns>
                    </anthem:GridView>
                </div>

            </div>
            <anthem:HiddenField ID="hdfEtapa_Id" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:HiddenField ID="hdfEvento_Id" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:HiddenField ID="hdfEstadoVigencia_Id" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarEtapa" CssClass="oculto" runat="server" Text=""
                OnClick="btnBuscarEtapa_Click" EnableCallBack="False" />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarEvento" CssClass="oculto" runat="server" Text=""
                OnClick="btnBuscarEvento_Click" EnableCallBack="False" />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarEstadoEtapa" CssClass="oculto" runat="server" Text=""
                OnClick="btnBuscarEstadoEtapa_Click" EnableCallBack="False" />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarEstadoEvento" CssClass="oculto" runat="server" Text=""
                OnClick="btnBuscarEstadoEvento_Click" EnableCallBack="False" />
        </div>
        <div class="tab-pane fade" id="BandejaSeguimiento">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="titulo">Bandeja de Seguimiento de Solicitudes
                        <br>
                    </h3>

                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-md-12 small">
                    <anthem:GridView runat="server" ID="gvBandejaSeguimiento" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBandejaSeguimiento_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ver Solicitud" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnAbrirSolicitud" runat="server" ImageUrl="~/Img/Grid/AbrirEvento_grid.png"
                                        PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                        ToolTip="Ver Solicitud" OnClick="btnAbrirSolicitud_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Número de Solicitud" />
                            <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" />
                            <asp:BoundField DataField="EventoEnCurso" HeaderText="Evento en Curso" />
                            <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" NullDisplayText="Piloto" />

                        </Columns>
                    </anthem:GridView>
                </div>

            </div>

        </div>
        <div class="tab-pane fade" id="Activaciones">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="titulo">Resumen de Activaciones
                        <br>
                    </h3>

                </div>
            </div>
            <div class="row">

                <div class="row small" runat="server" id="divActivaciones">
                </div>
            </div>

            <anthem:HiddenField ID="hdfMesEscritura" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:HiddenField ID="hdfAñoEscritura" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:HiddenField ID="hdfActivada" runat="server" AutoUpdateAfterCallBack="true" />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarDetalleActivacion" CssClass="oculto" runat="server" Text=""
                OnClick="btnBuscarDetalleActivacion_Click" EnableCallBack="False" />
        </div>



    </div>


</asp:Content>


