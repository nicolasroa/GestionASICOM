<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="NuevoEstudioDeTitulos.aspx.cs" Inherits="WorkFlow.Aplicaciones.FlujosParalelos.NuevoEstudioDeTitulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


</script>
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Nuevo Estudio de Títulos Individual
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class ="col-lg-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Buscar Solicitud para Iniciar Flujo de Estudio de Títulos
                </h3>
            </div>
            <div class="panel-body" id="DivDatosDocumentoSolicitud">
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
                            <anthem:TextBox ID="txtNombreCliente" runat="server" class="form-control"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class='form-group'>
                            <label for="txtApePatCliente" class="text-left">Apellido Paterno</label>
                            <anthem:TextBox ID="txtApePatCliente" runat="server" class="form-control"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class='form-group'>
                            <label for="txtApeMatCliente" class="text-left">Apellido Paterno</label>
                            <anthem:TextBox ID="txtApeMatCliente" runat="server" class="form-control"></anthem:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                            OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />

                    </div>
                </div>
            </div>
        </div>

</div>
    </div>
    <div class="row">
        <div class="col-md-7 small">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Solicitudes Aptas para Iniciar Flujo
                    </h3>
                </div>
                <div class="panel-body">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:TemplateField HeaderText="Iniciar Flujo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnAbrirAvento" runat="server" ImageUrl="~/Img/Grid/AbrirEvento_grid.png"
                                        PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..."
                                        ToolTip="Abrir" OnClick="btnAbrirAvento_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Nº de Solicitud" />

                            <asp:BoundField DataField="EventoEnCurso" HeaderText="Evento en Curso" />
                            <asp:BoundField DataField="NombreCliente" HeaderText="Solicitante Principal" />
                        </Columns>
                    </anthem:GridView>
                </div>
            </div>
        </div>
        <div class="col-md-5 small">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Propiedades de la Solicitud 
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <anthem:GridView runat="server" ID="gvPropiedades" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                                PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvPropiedades_PageIndexChanging"
                                AutoUpdateAfterCallBack="true">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                                    <asp:BoundField DataField="DescripcionAntiguedad" HeaderText="Antigüedad" />
                                    <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" NullDisplayText="No Ingresada" />
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIniciarFlujo" runat="server" Text=""></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:GridView>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                                EnabledDuringCallBack="False" ID="btnProcesarEETT" runat="server" CssClass="btn-sm btn-primary"
                                OnClick="BtnProcesarEETT_Click" Text="Procesar Inicio del Flujo" TextDuringCallBack="Procesando..."
                                OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Crear un Estudio de Títulos para las Propiedades Seleccionadas?',this);" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
