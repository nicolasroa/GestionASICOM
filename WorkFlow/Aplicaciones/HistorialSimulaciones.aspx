<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="HistorialSimulaciones.aspx.cs" Inherits="WorkFlow.Aplicaciones.HistorialSimulaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 small">
            <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                PageSize="20" AllowPaging="True" AllowSorting="True"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:BoundField DataField="FechaSimulacion" HeaderText="Fecha" />
                    <asp:BoundField DataField="NombreEjecutivo" HeaderText="Ejecutivo" />
                    <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" />
                    <asp:BoundField DataField="MontoCredito" HeaderText="Monto Crédito" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="ValorPropiedad" HeaderText="Monto Propiedad" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="PorcentajeFinanciamiento" HeaderText="% Financiamiento" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                    <asp:BoundField DataField="TasaAnual" HeaderText="Tasa" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="DividendoUF" HeaderText="Dividendo Sin Seguros UF" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="DividendoTotal" HeaderText="Dividendo Final" />
                    <asp:BoundField DataField="CAE" HeaderText="CAE" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F4}" />
                    <asp:TemplateField HeaderText="Solicitud">
                        <ItemTemplate>
                            <asp:Label ID="lblSolicitud" runat="server" Text='<%# (Eval("Solicitud_Id").ToString() == "-1") ? "Sin Solicitud" : Eval("Solicitud_Id").ToString()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <anthem:LinkButton ID="btnSeleccionar" runat="server" CssClass="btn-sm btn-success" OnClick="btnSeleccionar_Click" OnMouseDown="javascript:Espera(this, 'Recuperando Información');" PostCallBackFunction="$.unblockUI();"><span class="glyphicon glyphicon-ok"></span> </anthem:LinkButton>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                </Columns>
            </anthem:GridView>
        </div>
    </div>
</asp:Content>
