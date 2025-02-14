<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="AsignacionMenu.aspx.cs" Inherits="WebSite.Administracion.AsignacionMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .collapsed-row {
            display: none;
            padding: 0px;
            margin: 0px;
        }
    </style>
    <script type="text/javascript">

        function Inicio() {


        }

        function ToggleGridPanel(btn, row) {
            var current = $('#' + row).css('display');
            if (current == 'none') {
                $('#' + row).show();
                $(btn).removeClass('glyphicon-plus')
                $(btn).addClass('glyphicon-minus')
            } else {
                $('#' + row).hide();
                $(btn).removeClass('glyphicon-minus')
                $(btn).addClass('glyphicon-plus')
            }
            return false;
        }








    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Asignación de Menús
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class='form-group'>
                <label for="ddlEstado" class="text-left">Rol</label>
                <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlRol" runat="server" CssClass="form-control">
                </anthem:DropDownList>
            </div>
        </div>
        <div class="col-md-4">
            <div class='form-group'>
                <label for="ddlEstado" class="text-left">Sección</label>
                <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlSeccion" runat="server" CssClass="form-control">
                </anthem:DropDownList>
            </div>
        </div>
        <div class="col-md-2 text-center" style="margin-top:5px;">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar Asignación"
                OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
        </div>
        <div class="col-md-2 text-center" style="margin-top:5px;">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                EnabledDuringCallBack="False" ID="btnProcesar" runat="server" CssClass="btn-sm btn-primary"
                OnClick="btnProcesar_Click" Text="Procesar Asignación" TextDuringCallBack="Procesando..."
                OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
        </div>




    </div>
    <hr />
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                    <h3 class="panel-title">Menús Operacionales</h3>
                <div class="panel-body">
                    <anthem:GridView ID="gvOperacionales" runat="server"
                        DataKeyNames="Id"
                        AutoUpdateAfterCallBack="true"
                        Width="100%"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-hoverd"
                        OnRowDataBound="gvOperacionales_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <button class="btn btn-xs btn-primary glyphicon glyphicon-plus" onclick="return ToggleGridPanel(this, 'tr<%# Eval("Id") %>')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                            <asp:TemplateField HeaderText="Acceso">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAcceso" runat="server" Text=""></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# MyNewRow(Eval("Id")) %>
                                    <anthem:GridView ID="gvHijosOperacionales" runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="false"
                                        OnRowDataBound="gvHijoOperacionales_RowDataBound"
                                        AutoUpdateAfterCallBack="true"
                                        CssClass="table table-striped table-hoverd"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Menús Tipo Opercionales" />
                                            <asp:TemplateField HeaderText="Acceso">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                    <anthem:GridView ID="gvHijosMantenedores" runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="false"
                                        AutoUpdateAfterCallBack="true"
                                        OnRowDataBound="gvHijoMantenedores_RowDataBound"
                                        CssClass="table table-striped table-hoverd"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Menús Tipo Mantenendores" />
                                            <asp:TemplateField HeaderText="Acceso">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crear">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCrear" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkModificar" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEliminar" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>
            </div>


        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                    <h3 class="panel-title">Menús de Mantenedores</h3>
                <div class="panel-body">
                    <anthem:GridView ID="gvMantenedores" runat="server"
                        DataKeyNames="Id"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AutoUpdateAfterCallBack="true"
                        CssClass="table table-striped table-hoverd"
                        OnRowDataBound="gvMantenedores_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <button class="btn btn-xs btn-primary glyphicon glyphicon-plus" onclick="return ToggleGridPanel(this, 'tr<%# Eval("Id") %>')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                            <asp:TemplateField HeaderText="Acceso">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAcceso" runat="server" Text=""></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crear">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCrear" runat="server" Text=""></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkModificar" runat="server" Text=""></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEliminar" runat="server" Text=""></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# MyNewRow(Eval("Id")) %>
                                    <anthem:GridView ID="gvHijosOperacionales" runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="false"
                                        AutoUpdateAfterCallBack="true"
                                        CssClass="table table-striped table-hoverd"
                                        OnRowDataBound="gvHijoOperacionales_RowDataBound"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Menús Tipo Opercionales" />
                                            <asp:TemplateField HeaderText="Asignar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                    <anthem:GridView ID="gvHijosMantenedores" runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="false"
                                        OnRowDataBound="gvHijoMantenedores_RowDataBound"
                                        AutoUpdateAfterCallBack="true"
                                        class="table table-striped table-hover table-bordered"
                                        DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Menús Tipo Mantenendores" />
                                            <asp:TemplateField HeaderText="Acceso">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crear">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCrear" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkModificar" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEliminar" runat="server" Text=""></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>
            </div>


        </div>
    </div>
</asp:Content>