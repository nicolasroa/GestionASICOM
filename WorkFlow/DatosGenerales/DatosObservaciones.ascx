<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosObservaciones.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosObservaciones" %>
<div class="row">
    <div class="col-md-12 small">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <anthem:Label ID="lblTituloObservacion" AutoUpdateAfterCallBack="true" runat="server" ToolTip="Si es Sujeto a revisión, se debe confirmar.">
                    </anthem:Label>
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2">
                        <label for="ddlTipoObservacion">Tipo de Observación</label>
                    </div>
                    <div class="col-lg-3">
                        <anthem:DropDownList ID="ddlTipoObservacion" class="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                    </div>
                    <div class="col-lg-7">
                        <anthem:TextBox ID="txtObservacion" runat="server" AutoUpdateAfterCallBack="True" TextMode="MultiLine" Width="100%"></anthem:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <anthem:Button ID="btnAgregaComentario" runat="server" Text="Agregar Comentario" AutoUpdateAfterCallBack="True" OnClick="btnAgregaComentario_Click" CssClass="btn btn-sm btn-success" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <anthem:GridView runat="server" ID="gdvComentarios" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True" AllowPaging="true" CssClass="table table-condensed" 
                            AutoUpdateAfterCallBack="true" OnPageIndexChanging="gdvComentarios_PageIndexChanging" OnRowDataBound="gdvComentarios_RowDataBound">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:DropDownList ID="ddlEstadoObservacion" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" runat="server" OnSelectedIndexChanged="ddlEstadoObservacion_SelectedIndexChanged"></anthem:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsableSubsano" HeaderText="Responsable Subsano" NullDisplayText="No Aplica" />
                                <asp:BoundField DataField="ResponsableIngreso" HeaderText="Responsable Ingreso" />
                                <asp:BoundField DataField="DescripcionTipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" />
                                <asp:BoundField DataField="FechaSubsano" HeaderText="Fecha Subsano" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Comentario" ItemStyle-Font-Bold="true" />
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                            ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." OnClick="btnModificar_Click" AutoUpdateAfterCallBack="True" />
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
<anthem:HiddenField ID="hfIdObservacion" AutoUpdateAfterCallBack="true" runat="server" />
