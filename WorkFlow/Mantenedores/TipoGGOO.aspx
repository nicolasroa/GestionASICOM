<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="TipoGGOO.aspx.cs" Inherits="WorkFlow.Mantenedores.TipoGGOO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
                <div class="col-md-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="5" AllowSorting="True"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="Valor" HeaderText="Valor" />
                            <asp:BoundField DataField="Gasto" HeaderText="Gasto" />
                        </Columns>
                    </anthem:GridView>
                </div>

            </div>
     <div class="row">
        <div class="col-md-3">
            <anthem:Button ID="btnAceptar" runat="server" class="btn btn-sm btn-success center" Text="Aceptar" />
        </div>
    </div>
</asp:Content>
