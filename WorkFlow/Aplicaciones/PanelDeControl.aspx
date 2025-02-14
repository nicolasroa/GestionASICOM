<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PanelDeControl.aspx.cs" Inherits="WorkFlow.Aplicaciones.PanelDeControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function Inicio() {
            $("#<%= txtFechaInicioDesde.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 3,
                maxDate: 0,
                constrainInput: true, //La entrada debe cumplir con el formato
                onClose: function (selectedDate) {
                    $("#<%= txtFechaInicioHasta.ClientID %>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFechaInicioHasta.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                maxDate: 0,
                changeYear: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    $("#<%= txtFechaInicioDesde.ClientID %>").datepicker("option", "maxDate", selectedDate);
                }
            });
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
        <div class="row spacenavmax">
            <div class="col-md-12">
                <h4>
                    <span class="titulo">
                        <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Panel de Control
                    </span>
                    <br>
                </h4>
               <%-- <div style="width: 100%;">
                    <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
                </div>--%>
            </div>
        </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">
                <span class="glyphicon glyphicon-search"></span>Búsqueda
            </h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-8">
                    <div class="row">

                        <div class="col-md-3 col-sm-6">
                            <div class='form-group'>
                                <label for="txtNumeroSolicitud" class="text-left">Número de Solicitud</label>
                                <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNumeroSolicitud" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                            </div>

                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div class='form-group'>
                                <label for="txtRutCliente" class="text-left">Rut Cliente</label>
                                <anthem:TextBox ID="txtRutCliente" runat="server" class="form-control" onchange="validaRut(this);"></anthem:TextBox>
                            </div>

                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div class='form-group'>
                                <label for="ddlEjecutivo" class="text-left">Ejecutivo</label>
                                <anthem:DropDownList ID="ddlEjecutivo" AutoUpdateAfterCallBack="true" class="form-control" runat="server"></anthem:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div class='form-group'>
                                <label for="ddlEstadoSolicitud" class="text-left">Estado Solicitud</label>
                                <anthem:DropDownList ID="ddlEstadoSolicitud" AutoUpdateAfterCallBack="true" class="form-control" runat="server"></anthem:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-sm-6">
                            <div class='form-group'>
                                <label for="ddlMalla" class="text-left">Malla</label>
                                <anthem:DropDownList ID="ddlMalla" AutoUpdateAfterCallBack="true" class="form-control" runat="server" AutoCallBack="True" OnSelectedIndexChanged="ddlMalla_SelectedIndexChanged"></anthem:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-4 col-sm-6">
                            <div class='form-group'>
                                <label for="ddlEtapa" class="text-left">Etapa</label>
                                <anthem:DropDownList ID="ddlEtapa" AutoCallBack="true" AutoUpdateAfterCallBack="true" class="form-control" runat="server" OnSelectedIndexChanged="ddlEtapa_SelectedIndexChanged"></anthem:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-4 col-sm-6">
                            <div class='form-group'>
                                <label for="ddlEvento" class="text-left">Evento</label>
                                <anthem:DropDownList ID="ddlEvento" AutoUpdateAfterCallBack="true" class="form-control" runat="server"></anthem:DropDownList>
                            </div>

                        </div>



                    </div>

                </div>

                <div class="col-md-4">

                    <div class="row">
                        <div class="col-md-6">
                            <div class='form-group'>
                                <label for="txtFechaInicioDesde" class="text-left">Fecha Desde</label>
                                <anthem:TextBox CssClass="form-control" runat="server" ID="txtFechaInicioDesde" AutoUpdateAfterCallBack="true"
                                    onblur="esFechaValida(this);"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class='form-group'>
                                <label for="txtFechaInicioHasta" class="text-left">Fecha Hasta</label>
                                <anthem:TextBox CssClass="form-control" runat="server" ID="txtFechaInicioHasta" AutoUpdateAfterCallBack="true"
                                    onblur="esFechaValida(this);"></anthem:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-sm-4 text-center" style="margin-top:10px; margin-right:-10px;">
                            <anthem:LinkButton ID="btnBuscar" class="btn btn-sm btn-primary" runat="server" EnableCallBack="false" TextDuringCallBack="Buscando..." PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" OnClick="btnBuscar_Click"><span class="glyphicon glyphicon-list"></span> Cargar Lista</anthem:LinkButton>
                        </div>
                        <div class="col-md-4 col-sm-4 text-center" style="margin-top:10px; margin-right:-5px; margin-left:-5px;">
                            <anthem:LinkButton ID="btnBuscarEtapas" class="btn btn-sm btn-primary" runat="server" EnableCallBack="false" TextDuringCallBack="Buscando..." PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" OnClick="btnBuscarEtapas_Click"><span class="glyphicon glyphicon-th-large"></span> Cargar Etapas</anthem:LinkButton>
                        </div>
                        <div class="col-md-4 col-sm-4 text-center" style="margin-top:10px; margin-right:-5px; margin-left:-5px;">
                            <anthem:LinkButton ID="btnGenerarReporteBandeja" class="btn btn-sm btn-primary" EnableCallBack="false" runat="server" OnClick="btnGenerarReporteBandeja_Click" Visible="False"><span class="glyphicon glyphicon-list"></span> Generar Reporte</anthem:LinkButton>
                        </div>


                    </div>

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

    <div class="row">
        <div class="col-md-12 small">
            <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:TemplateField HeaderText="Ver Detalle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <anthem:ImageButton ID="btnAbrirAvento" runat="server" ImageUrl="~/Img/Grid/AbrirEvento_grid.png"
                                PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                ToolTip="Abrir" OnClick="btnAbrirAvento_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Solicitud_Id" HeaderText="Número de Solicitud" />
                    <asp:BoundField DataField="DescripcionEvento" HeaderText="Evento" ItemStyle-Font-Bold="true" />
                    <asp:BoundField DataField="DescripcionMalla" HeaderText="Malla" />
                    <asp:BoundField DataField="NombreSolicitantePrincipal" HeaderText="Cliente" />
                    <asp:BoundField DataField="UsuarioResponsable" HeaderText="Responsable" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Inicio" />
                    <asp:BoundField DataField="FechaEsperada" HeaderText="Fecha Esperada" />
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



</asp:Content>
