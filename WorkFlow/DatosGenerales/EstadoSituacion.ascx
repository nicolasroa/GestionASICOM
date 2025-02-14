<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EstadoSituacion.ascx.cs" Inherits="WorkFlow.DatosGenerales.EstadoSituacion" %>

<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title">Participantes</h3>
    </div>
    <div class="panel-body">
        <div class='form-group'>
            <anthem:GridView runat="server" ID="AgdvParticipante" AutoGenerateColumns="false" Width="100%"
                PageSize="5" AllowSorting="True"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                    <asp:BoundField DataField="PorcentajeDominio" HeaderText="% Prop" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="PorcentajeDeuda" HeaderText="% Deuda" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="PorcentajeDesgravamen" HeaderText="% Desgravamen" DataFormatString="{0:F2}" />
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <anthem:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                ToolTip="Seleccionar" TextDuringCallBack="Buscando Registro..." OnClick="btnSeleccionar_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </anthem:GridView>
        </div>
    </div>
</div>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title">
            <anthem:Label ID="lblIngresoDeuda" AutoUpdateAfterCallBack="true" runat="server" Text="Ingreso Deuda"></anthem:Label>&nbsp;</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-6 small">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Activos</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="ddlTipoActivo" class="control-label">Tipo Activo</label>
                                    <anthem:DropDownList ID="ddlTipoActivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="ddlMonedaActivo" class="control-label">Moneda</label>
                                    <anthem:DropDownList ID="ddlMonedaActivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlMonedaActivo_SelectedIndexChanged"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="txtMontoTotalActivo" class="control-label">Monto</label>
                                    <anthem:TextBox ID="txtMontoTotalActivo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class='form-group'>
                                    <label for="txtObservacionActivo" class="control-label">Observación</label>
                                    <anthem:TextBox ID="txtObservacionActivo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row text-center">
                            <div class="col-lg-12">
                                <anthem:Button ID="AbtnAgregaActivo" runat="server" Text="Grabar Activo" AutoUpdateAfterCallBack="true" CssClass="btn btn-sm btn-success" OnClick="AbtnAgregaActivo_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <anthem:GridView runat="server" ID="gvActivos" AutoGenerateColumns="False" Width="100%"
                                    PageSize="5" AllowSorting="True" CssClass="table table-condensed"
                                    AutoUpdateAfterCallBack="True" OnRowDataBound="gvActivos_RowDataBound" UpdateAfterCallBack="True">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:Label ID="lblDescripcionActivo" runat="server" Text='<%# Eval("DescripcionActivo")%>'>&nbsp;</anthem:Label><anthem:Image ID="ImgObservacionesActivos" ImageUrl="~/Img/Grid/Info_grid.png" ToolTip='<%# Eval("Observacion")%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DescripcionMoneda" HeaderText="Moneda" />

                                        <asp:TemplateField HeaderText="Monto">
                                            <ItemTemplate>
                                                <anthem:Label ID="lblMtoTotal" runat="server" Text='<%# Bind("MontoTotal") %>'></anthem:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnModificarActivo" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                    ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." OnClick="btnModificarActivo_Click" />

                                                <anthem:ImageButton ID="btnEliminarActivo" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                    ToolTip="Eliminar Activo" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarActivo_Click" />
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

            <div class="col-md-6 small">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Pasivos</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="txtTotapPasivosCMF" class="control-label">Total Pasivos CMF</label>
                                    <anthem:TextBox ID="txtTotalPasivosCMF" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="ddlFechaValorizacionCMF" class="control-label">Fecha de Valorización</label>
                                    <anthem:DropDownList ID="ddlFechaValorizacionCMF" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                </div>
                            </div>
                             <div class="col-lg-3">
                                <div class='form-group'>
                                    <anthem:LinkButton ID="btnGrabarPasivoCMF" CssClass="btn btn-xs btn-success" runat="server" OnClick="btnGrabarPasivoCMF_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Pasivo CMF</anthem:LinkButton>
                                </div>
                            </div>
                            
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="ddlTipoPasivo" class="control-label">Tipo Pasivo</label>
                                    <anthem:DropDownList ID="ddlTipoPasivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="ddlMonedaPasivo" class="control-label">Moneda</label>
                                    <anthem:DropDownList ID="ddlMonedaPasivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlMonedaPasivo_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="txtMontoTotalPasivo" class="control-label">Saldo</label>
                                    <anthem:TextBox ID="txtMontoTotalPasivo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="txtCuotaMensual" class="control-label">Cuota Mensual</label>
                                    <anthem:TextBox ID="txtCuotaMensual" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="ddlInstitucionPasivo" class="control-label">Institución</label>
                                    <anthem:DropDownList ID="ddlInstitucionPasivo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class='form-group'>
                                    <label for="txtObservacionPasivo" class="control-label">Observación</label>
                                    <anthem:TextBox ID="txtObservacionPasivo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row text-center">
                            <div class="col-md-12">
                                <anthem:Button ID="AbtnAgregaPasivo" runat="server" Text="Grabar Pasivo" AutoUpdateAfterCallBack="True" class="btn btn-sm btn-success center" OnClick="AbtnAgregaPasivo_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <anthem:GridView runat="server" ID="gvPasivos" AutoGenerateColumns="false" Width="100%"
                                    PageSize="5" AllowSorting="True" CssClass="table table-condensed"
                                    AutoUpdateAfterCallBack="true" OnRowDataBound="gvPasivos_RowDataBound">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pasivo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:Label ID="lblDescripcionActivo" runat="server" Text='<%# Eval("DescripcionPasivo")%>'>&nbsp;</anthem:Label><anthem:Image ID="ImgObservacionesActivos" ImageUrl="~/Img/Grid/Info_grid.png" ToolTip='<%# Eval("Observacion")%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DescripcionInstitucion" HeaderText="Institución" NullDisplayText="Sin Institución" />
                                        <asp:BoundField DataField="DescripcionMoneda" HeaderText="Moneda" />
                                        <asp:TemplateField HeaderText="Saldo Deuda">
                                            <ItemTemplate>
                                                <anthem:Label ID="lblMtoTotalP" runat="server" Text='<%# Bind("MontoTotal") %>'></anthem:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cuota Mensual">
                                            <ItemTemplate>
                                                <anthem:Label ID="lblCuotaMensual" runat="server" Text='<%# Bind("CuotaMensual") %>'></anthem:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnModificarPasivo" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                    ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." OnClick="btnModificarPasivo_Click" />
                                                &nbsp;
                                                         <anthem:ImageButton ID="btnEliminarPasivo" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                             ToolTip="Eliminar Pasivo" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarPasivo_Click" />

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </anthem:GridView>
                            </div>
                        </div>
                        <anthem:HiddenField ID="hdnIdParticipante" runat="server" AutoUpdateAfterCallBack="true" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 small">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Renta</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="ddlTipoRenta" class="control-label">Tipo Renta</label>
                                            <anthem:DropDownList ID="ddlTipoRenta" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></anthem:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="txtMes1" class="control-label">Renta Promedio</label>
                                            <anthem:TextBox ID="txtMes1" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="100"></anthem:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="row text-center">
                                    <div class="col-md-12">
                                        <anthem:Button ID="AbtnAgregaRenta" runat="server" Text="Grabar Renta" AutoUpdateAfterCallBack="true" CssClass="btn btn-sm btn-success" OnClick="AbtnAgregaRenta_Click" />
                                    </div>
                                </div>



                            </div>
                            <div class="col-md-9">
                                <anthem:GridView runat="server" ID="gvRentas" AutoGenerateColumns="false" Width="100%"
                                    PageSize="5" AllowSorting="True"
                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:BoundField DataField="DescripcionTipoRenta" HeaderText="Tipo Renta" />
                                        <asp:BoundField DataField="Mes1" HeaderText="Renta Promedio" DataFormatString="{0:C}" />

                                        <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnModificarRenta" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                    ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." OnClick="btnModificarRenta_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnEliminarRenta" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                    ToolTip="Eliminar Activo" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarRenta_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </anthem:GridView>
                            </div>
                        </div>







                        <%-- <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="txtMes2" class="control-label">Mes 2</label>
                                            <anthem:TextBox ID="txtMes2" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="102"></anthem:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="txtMes3" class="control-label">Mes 3</label>
                                            <anthem:TextBox ID="txtMes3" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="103"></anthem:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="txtMes4" class="control-label">Mes 4</label>
                                            <anthem:TextBox ID="txtMes4" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="104"></anthem:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="txtMes5" class="control-label">Mes 5</label>
                                            <anthem:TextBox ID="txtMes5" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="105"></anthem:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="txtMes6" class="control-label">Mes 6</label>
                                            <anthem:TextBox ID="txtMes6" runat="server" MaxLength="9" class="form-control" AutoUpdateAfterCallBack="True" TabIndex="106"></anthem:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
