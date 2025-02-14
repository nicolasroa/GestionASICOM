<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaEstadoAvance.aspx.cs" Inherits="WorkFlow.Aplicaciones.ConsultaEstadoAvance" %>
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
    </script>
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Consulta Estado de Avance
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading panel-pading">
                <a id="lnkBuscar" href="#" class="btn-sm btn-buscarSol" onclick="MostrarFormularioBuscarSolicitudes();">
                    <span class="glyphicon glyphicon-search"></span>Buscar Solicitudes</a>
        </div>
        <div class="panel-body" id="DivDatosDocumentoSolicitud" hidden="hidden">
            <div class="row fivecolumns">
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtNumeroSolicitud" class="text-left">Número de Solicitud</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNumeroSolicitud" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtRutCliente" class="text-left">Rut Cliente</label>
                        <anthem:TextBox ID="txtRutCliente" runat="server" class="form-control" onchange="validaRut(this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtNombreCliente" class="text-left">Nombre</label>
                        <anthem:TextBox ID="txtNombreCliente" runat="server" class="form-control" ></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtApePatCliente" class="text-left">Apellido Paterno</label>
                        <anthem:TextBox ID="txtApePatCliente" runat="server" class="form-control" ></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtApeMatCliente" class="text-left">Apellido Paterno</label>
                        <anthem:TextBox ID="txtApeMatCliente" runat="server" class="form-control" ></anthem:TextBox>
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
                    <asp:TemplateField HeaderText="Abrir Solicitud" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <anthem:ImageButton ID="btnAbrirAvento" runat="server" ImageUrl="~/Img/Grid/AbrirEvento_grid.png"
                                PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                ToolTip="Abrir" OnClick="btnAbrirAvento_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Número de Solicitud" />

                    <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" />
                    <asp:BoundField DataField="DescripcionTipoFinanciamiento" HeaderText="Tipo Financiamiento" />
                    <asp:BoundField DataField="DescripcionDestino" HeaderText="Destino" />
                    <asp:BoundField DataField="Rut" HeaderText="Rut" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Paterno" HeaderText="Apellido Paterno" />
                    <asp:BoundField DataField="Materno" HeaderText="Apellido Materno" />
                </Columns>
            </anthem:GridView>
        </div>

    </div>
</asp:Content>

