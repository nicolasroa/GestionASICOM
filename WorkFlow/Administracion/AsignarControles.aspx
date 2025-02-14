<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PoPup.Master" AutoEventWireup="true"
    CodeBehind="AsignarControles.aspx.cs" Inherits="WebApp.Administracion.AsignarControles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorMasterPopup" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%;">
                    <tr>
                        <td class="style1">
                            Rol:
                        </td>
                        <td>
                            <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlRol" runat="server" AutoCallBack="True"
                                OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
                            </anthem:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                &nbsp; Configuración
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <anthem:GridView ID="gvConfiguracionControles" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-condensed" AutoUpdateAfterCallBack="true" PageSize="18" UpdateAfterCallBack="True"
                    Width="100%" TotalRecordString="" UseCoolPager="False" Height="16px" ShowHeader="True">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="IdInterno" HeaderText="Identificación" />
                        <asp:TemplateField HeaderText="Visible">
                            <ItemTemplate>
                                <anthem:CheckBox ID="chkVisible" runat="server" Checked='<%#Convert.ToBoolean(Eval("Visible")) %>'>
                                </anthem:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <anthem:CheckBox ID="chkActivo" runat="server" Checked='<%#Convert.ToBoolean(Eval("Activo")) %>'>
                                </anthem:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnProcesar" runat="server" CssClass="btn btn-primary" OnClick="btnProcesar_Click"
                    Text="Procesar Asignación" TextDuringCallBack="Procesando..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
                &nbsp;
                <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCerrar" runat="server" CssClass="btn btn-primary" OnClick="btnCerrar_Click"
                    Text="Cerrar" />
            </td>
        </tr>
    </table>
</asp:Content>
