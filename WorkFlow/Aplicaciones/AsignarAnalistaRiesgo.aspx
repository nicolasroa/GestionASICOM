<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AsignarAnalistaRiesgo.aspx.cs" Inherits="WorkFlow.AsignarAnalistaRiesgo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CargarInicioReasignacion() {


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Reasignación de Usuarios
                </span>
                <br>
            </h4>
            <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar Solicitudes a Para Asignar"
                OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Procesar la Asignación?',this);" EnabledDuringCallBack="False" ID="btnProcesarReasignacion" CssClass="btn-sm btn-primary" runat="server" Text="Procesar Reasignación"
                OnClick="btnProcesarReasignacion_Click" TextDuringCallBack="Procesando..." />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Solicitudes
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <anthem:LinkButton ID="btnSeleccionarTodas" runat="server" class="btn btn-xs btn-info" OnClick="btnSeleccionarTodas_Click">Seleccionar Todas</anthem:LinkButton>
                            <anthem:LinkButton ID="btnSeleccionarMitad" runat="server" class="btn btn-xs btn-info" OnClick="btnSeleccionarMitad_Click">Seleccionar La Mitad</anthem:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                            <anthem:GridView runat="server" ID="gvSolicitudesParaAsignar" AutoGenerateColumns="false" Width="100%"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Asignar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAsignar" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Analista" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:DropDownList ID="ddlAnalistaRiesgo" runat="server" CssClass="text-left form-control col-lg-12" AutoUpdateAfterCallBack="true" Width="100%" AutoCallBack="True"></anthem:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Solicitud_Id" HeaderText="Solicitud" />
                                    <asp:BoundField DataField="NombreSolicitantePrincipal" HeaderText="Solicitante" />
                                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Inicio" />
                                    <asp:BoundField DataField="FechaEsperada" HeaderText="Fecha Esperada" />

                                </Columns>
                            </anthem:GridView>


                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                </div>
            </div>



        </div>



    </div>



</asp:Content>
