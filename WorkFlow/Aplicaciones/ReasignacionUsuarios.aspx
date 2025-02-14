<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ReasignacionUsuarios.aspx.cs" Inherits="WorkFlow.Aplicaciones.ReasignacionUsuarios" %>

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
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Usuario a Reasignar
                    </h3>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-2">
                            <label for="ddlSucursal">Sucursal</label>
                            <anthem:DropDownList ID="ddlSucursal" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <label for="ddlFabrica">Fábrica</label>
                            <anthem:DropDownList ID="ddlFabrica" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <anthem:LinkButton ID="btnBuscarUsuario" runat="server" class="btn btn-xs btn-primary" OnClick="btnBuscarUsuario_Click"><span class="glyphicon glyphicon-search"></span> Buscar Usuario</anthem:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <label for="ddlUsuario">Usuario a Reasignar</label>
                            <anthem:DropDownList ID="ddlUsuario" runat="server" AutoCallBack="True" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged"></anthem:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <label for="ddlProyecto">Rol</label>
                            <anthem:DropDownList ID="ddlRol" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ddlNuevoUsuario">Nuevo Usuario Asignado</label>
                            <anthem:DropDownList ID="ddlNuevoUsuario" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>

                        </div>

                    </div>
                </div>
            </div>
        </div>


    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar Solicitudes a Reasignar"
                OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Procesar la Reasignación?',this);" EnabledDuringCallBack="False" ID="btnProcesarReasignacion" CssClass="btn-sm btn-primary" runat="server" Text="Procesar Reasignación"
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
                            <anthem:GridView runat="server" ID="gvSolicitudesAsignadas" AutoGenerateColumns="false" Width="100%"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Reasignar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkReasignar" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Solicitud_Id" HeaderText="Solicitud" />
                                    <asp:BoundField DataField="NombreCliente" HeaderText="Solicitante" />
                                    <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" />
                                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="EventoEnCurso" HeaderText="Evento En Curso" />

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
