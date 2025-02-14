<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PoPup.Master" AutoEventWireup="true" CodeBehind="ControlesMenu.aspx.cs" Inherits="WebApp.Administracion.ControlesMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 333px;
        }
        .style2
        {
            width: 104px;
        }
        .style3
        {
            width: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorMasterPopup" runat="server">
 <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Controles
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                <anthem:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/icons/document_new.png" />
                <anthem:LinkButton ID="LinkButton1" runat="server" OnClick="btnNuevoRegistro_Click">Nuevo Registro</anthem:LinkButton>
            </td>
        </tr>
    </table>
    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" class="tdFormulario01" href="#">Búsqueda</a>
        </h2>
        <div>
            <table style="width: 100%;" class="tdFormulario01">
                <tr>
                    <td class="tdTitulo02">
                        Resultado de la Búsqueda:
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdGrid" align="center">
                        <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                            AutoUpdateAfterCallBack="true" PageSize="10" AllowPaging="True" AllowSorting="True"
                            OnPageIndexChanging="gvBusqueda_PageIndexChanging"  CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="IdInterno" HeaderText="Id Interno" />
                                <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscado Registro..." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </anthem:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <h2 id="hFormulario" style="display: none;">
            <a id="lnkFormulario" class="tdFormulario01" href="#">Formulario</a></h2>
        <div>
            <table class="tdFormulario01" style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td class="style2">
                                    Id Interno
                                </td>
                                <td class="style1">
                                    <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormIdInterno" runat="server"
                                        Width="225px"></anthem:TextBox>
                                </td>
                                <td class="style3">
                                    Estado:</td>
                                <td>
                                    <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" runat="server" AutoCallBack="true"
                                        AutoUpdateAfterCallBack="True">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Menú Padre</td>
                                <td>
                                    <anthem:Label ID="lblFormMenuPadre" runat="server" 
                                        AutoUpdateAfterCallBack="True" CssClass="tdInfoData" UpdateAfterCallBack="True"></anthem:Label>
                                </td>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar"
                            TextDuringCallBack="Guardando..." OnClick="btnGuardar_Click" />
                        &nbsp;<anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary"
                            OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
