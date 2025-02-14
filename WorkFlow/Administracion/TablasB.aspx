<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="TablasB.aspx.cs" Inherits="WebSite.Administracion.TablasB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/jscript">

        function Inicio() {
            //$('#<%=gvBusqueda.ClientID %>').Scrollable({
            //  ScrollHeight: 400,
            //Width: 0
            //});
        }
    </script>
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestros Generales
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
            <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="Button1" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
                    OnClick="btnNuevoRegistro_Click" />
        </div>    
    </div>
    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" href="#">Búsqueda</a>
        </h2>
        <div>
            <div class="row">
                <div class="col-lg-3">
                    <div class='form-group'>
                        <label for="txtNombre" class="text-left">Nombre</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNombre" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class='form-group'>
                        <label for="txtNombre" class="text-left">Código</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtCodigo" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class='form-group'>
                        <label for="txtNombre" class="text-left">Nombre Tabla Padre</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNombreTablaPadre" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class='form-group'>
                        <label for="txtNombre" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" runat="server">
                                    </anthem:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-lg-12 text-center">
                    <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click"
                            TextDuringCallBack="Buscando..." />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 busqueda">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="NombreTablaPadre" HeaderText="Tabla Padre" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" FooterStyle-Width="20%">
                                    <FooterStyle Width="20%"></FooterStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ver Hijos" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnVerHijos" runat="server" ImageUrl="~/Img/Grid/Info_grid.png"
                                            ToolTip="Ver Hijos" OnClick="btnVerHijos_Click" />
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
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormNombre" class="text-left">Nombre</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormNombre" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                 <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormNombrePadre" class="text-left">Nombre Tabla Padre</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormNombrePadre" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormCodigo" class="text-left">Código</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormCodigo" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormCodigoInterno" class="text-left">Código Interno</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormCodigoInterno" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                 <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormConcepto" class="text-left">Concepto</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormConcepto" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 text-center">
                   <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary"
                            Text="Guardar" OnClick="btnGuardar_Click" TextDuringCallBack="Guardando..." />
                        &nbsp;<anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-sm btn-primary"
                            OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                        <anthem:HiddenField ID="hfTablaPadreId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>