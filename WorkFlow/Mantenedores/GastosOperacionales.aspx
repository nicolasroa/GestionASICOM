<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="GastosOperacionales.aspx.cs" Inherits="WorkFlow.Mantenedores.GastosOperacionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                PageSize="20" AllowPaging="True" AllowSorting="True"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:BoundField DataField="DescripcionTipoGasto" HeaderText="Tipo Gasto" />
                    <asp:TemplateField HeaderText="Valor UF">
                        <ItemTemplate>
                            <asp:Label ID="lblValor" runat="server" Text='<%# (Eval("Moneda_Id").ToString() == "999") ? string.Format("{0:C}",Eval("Valor")) : string.Format("{0:F4}",Eval("Valor"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor $">
                        <ItemTemplate>
                            <asp:Label ID="lblValorPesos" runat="server" Text='<%# string.Format("{0:C}",Eval("ValorPesos"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </anthem:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 text-right">
        </div>
        <div class="col-lg-3 text-right">
        </div>
        <div class="col-lg-3 text-right">
        </div>
        <div class="col-lg-3">
            <div class='form-group'>
                <label for="txtTotalGastos">Total de Gastos ($)</label>
                <anthem:TextBox ID="txtTotalGastos" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false"></anthem:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCerrar" CssClass="btn btn-primary" runat="server" Text="Cerrar"
                OnClick="btnCerar_Click" TextDuringCallBack="Cerrando..." />
        </div>
    </div>




</asp:Content>
