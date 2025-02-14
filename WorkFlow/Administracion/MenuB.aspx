<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="MenuB.aspx.cs" Inherits="WebSite.Administracion.MenuB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                   <%-- <img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Menús
                </span>
                <br>
            </h4>
           <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" class="tdFormulario01" href="#">Búsqueda</a>
        </h2>
        <div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class='form-group'>
                        <label for="ddlMenuPadre" class="text-left">Menú Padre</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlMenuPadre" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                </div>
            </div>

            <div class="row">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="Button2" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
                    OnClick="btnNuevoRegistro_Click" />
                <div class="col-md-12">
                    <span class="badge bg-info">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        AutoUpdateAfterCallBack="true" PageSize="10" AllowPaging="True" AllowSorting="True"
                        OnPageIndexChanging="gvBusqueda_PageIndexChanging" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                            <asp:BoundField DataField="DescripcionMenuPadre" HeaderText="Menú Padre Padre" />
                            <asp:BoundField DataField="DescripcionVisible" HeaderText="Estado" />
                            <asp:BoundField DataField="DescripcionMantenedor" HeaderText="Tipo" />
                            <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                        ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscado Registro..." />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Configurar Controles" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:LinkButton ID="btnVerControles" runat="server" class="btn-sm btn-primary" OnClick="btnVerControles_Click"><span class="glyphicon glyphicon-adjust"></span> Ver Controles</anthem:LinkButton>
                                    &nbsp;&nbsp;
                                    <anthem:LinkButton ID="btnAsignarControles" runat="server" class="btn-sm btn-primary" OnClick="btnAsignarControles_Click"><span class="glyphicon glyphicon-adjust"></span> Asignar Controles</anthem:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>
            </div>
        </div>
        <h2 id="hFormulario" style="display: none;">
            <a id="lnkFormulario" class="tdFormulario01" href="#">Formulario</a></h2>
        <div>

            <div class="row">
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormDescripcion" runat="server"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="ddlFormNivel" class="text-left">Nivel</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormNivel" runat="server" AutoCallBack="true" OnSelectedIndexChanged="ddlFormNivel_SelectedIndexChanged"
                            AutoUpdateAfterCallBack="True" Width="50px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="ddlMenuPadre" class="text-left">Menú Padre</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormMenuPadre" runat="server" Enabled="False" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class='form-group'>
                        <label for="txtFormOrden" class="text-left">Orden</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormOrden" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Width="50px"></anthem:TextBox>

                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="chkFormVisible" class="text-left"></label>
                        <anthem:CheckBox AutoUpdateAfterCallBack="true" CssClass="form-control" ID="chkFormVisible" runat="server"
                            Text="Visible" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left"></label>
                        <anthem:CheckBox ID="chkAdministracion" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True"
                            Text="Administración" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="txtFormUrl" class="text-left">Url</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormUrl" runat="server" Width="500px"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="rblFormTipo" class="text-left">Tipo</label>
                        <anthem:RadioButtonList ID="rblFormTipo" CssClass="form-control" runat="server" RepeatDirection="Horizontal"
                            AutoUpdateAfterCallBack="true" RepeatLayout="Flow">
                            <asp:ListItem Value="true">Mantenedor</asp:ListItem>
                            <asp:ListItem Value="false">Operacional</asp:ListItem>
                        </anthem:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                        EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary"
                        Text="Guardar" TextDuringCallBack="Guardando..." OnClick="btnGuardar_Click" />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                        EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn-sm btn-primary" OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
        </div>
    </div>
</asp:Content>
