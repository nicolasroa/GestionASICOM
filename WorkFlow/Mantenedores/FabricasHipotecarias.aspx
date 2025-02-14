<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="FabricasHipotecarias.aspx.cs" Inherits="WorkFlow.Mantenedores.FabricasHipotecarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-lg-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Fábricas Hipotecarias
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>

    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" href="#">Búsqueda</a>
        </h2>
        <div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                </div>
            </div>

            <div class="row">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="ImageButton2" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
                    OnClick="btnNuevoRegistro_Click" />
                <div class="col-lg-12">
                    <span class="badge bg-info">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                            <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                        ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>

            </div>

        </div>
        <h2 id="hFormulario" style="display: none;">
            <a id="lnkFormulario" href="#">Formulario</a></h2>
        <div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormDescripcion" runat="server" TabIndex="7"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormNombreResponsable" class="text-left">Responsable</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormNombreResponsable" runat="server" TabIndex="7"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormContactoResponsable" class="text-left">Datos de Contacto</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormContactoResponsable" runat="server" TabIndex="7"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>
                </div>
            </div>
          
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-primary center">
                        <div class="panel-heading">
                            <h3 class="panel-title">Asignación de Tipos de Fábrica</h3>
                        </div>
                        <div class="panel-body center">
                            <div class="row">
                                <div class="col-lg-8 text-left">
                                    <anthem:DropDownList CssClass="form-control" ID="ddlFormTipoFabrica" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>

                                </div>
                                <div class="col-lg-4 text-left">
                                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnAsignarTipoFabrica" runat="server" CssClass="btn-success btn-sm" Text="Asignar"
                                        TextDuringCallBack="Asignando Tipo..." OnClick="btnAsignarTipoFabrica_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 center">
                                    <anthem:GridView runat="server" ID="gvTipoFabrica" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionTipoFabrica" HeaderText="Descripción" />
                                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                        ToolTip="Eliminar Asignación" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminar_Click" />
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
                <div class="col-lg-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary" Text="Guardar"
                        OnClick="btnGuardar_Click" TextDuringCallBack="Guardando..." />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-sm btn-primary"
                        OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />

        </div>
    </div>

</asp:Content>
