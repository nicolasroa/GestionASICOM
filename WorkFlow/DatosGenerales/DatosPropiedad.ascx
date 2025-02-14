<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosPropiedad.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosPropiedad" %>
<script>
    $(function () {
        $("#accordionProp").accordion();
    });
    function MostrarBusquedaProp() {
        getElement('lnkBusquedaProp').click();
        getElement('hFormularioProp').style.display = "none";
        getElement('hBusquedaProp').style.display = "block";
    }
    function MostrarFormularioProp() {

        getElement('lnkFormularioProp').click();
        getElement('hFormularioProp').style.display = "block";
        getElement('hBusquedaProp').style.display = "none";
    }

</script>
<div id="accordionProp">
    <h2 id="hBusquedaProp">
        <a id="lnkBusquedaProp" href="#">Propiedades de la Solicitud</a>
    </h2>
    <div>
        <div class="row">
            <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="ImageButton2" CssClass="btn-success btn-sm" runat="server" Text="Nueva Propiedad"
                OnClick="btnNuevoRegistro_Click" />
            <div class="col-lg-12">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <anthem:GridView runat="server" ID="gvPropiedades" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                    PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvPropiedades_PageIndexChanging"
                    AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                        <asp:BoundField DataField="DescripcionAntiguedad" HeaderText="Antigüedad" />
                        <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" NullDisplayText="No Ingresada" />
                        <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <anthem:ImageButton ID="btnModificarProp" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                    ToolTip="Modificar" OnClick="btnModificarProp_Click" TextDuringCallBack="Buscando Registro..." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <anthem:ImageButton ID="btnEliminarPropiedad" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                    ToolTip="Eliminar Participante" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarPropiedad_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </anthem:GridView>
            </div>

        </div>

    </div>
    <h2 id="hFormularioProp" style="display: none;">
        <a id="lnkFormularioProp" href="#">Formulario</a></h2>
    <div style="height: 90%;">

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Información Principal</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-6">
                                <anthem:RadioButtonList ID="rblPropiedadPrincipal" runat="server" AutoCallBack="True" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="rblPropiedadPrincipal_CheckedChanged">
                                    <Items>
                                        <asp:ListItem Value="true">Propiedad Principal</asp:ListItem>
                                        <asp:ListItem Value="false">Propiedad Secundaria</asp:ListItem>
                                    </Items>
                                </anthem:RadioButtonList>
                            </div>
                            <div class="col-lg-6">
                                <div class='form-group'>
                                    <label for="ddlTasacionPadre" class="control-label">Propiedad Principal:</label>
                                    <anthem:DropDownList ID="ddlTasacionPadre" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlTasacionPadre_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="ddlTipoInmueble" class="control-label">Tipo Inmueble</label>
                                    <anthem:DropDownList ID="ddlTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True" OnSelectedIndexChanged="ddlTipoInmueble_SelectedIndexChanged" AutoCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="ddlAntiguedad" class="control-label">Antigüedad</label>
                                    <anthem:DropDownList ID="ddlAntiguedadProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class='form-group'>
                                    <label for="ddlDestinoProp" class="control-label">Destino</label>
                                    <anthem:DropDownList ID="ddlDestinoProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Seguro de Incendio</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-6">
                                            <div class='form-group'>
                                                <label for="ddlSeguroIncendio">Póliza</label>
                                                <anthem:DropDownList ID="ddlSeguroIncendio" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroIncendio_SelectedIndexChanged"></anthem:DropDownList>
                                            </div>
                                            <div class='form-group'>
                                                <label for="txtTasaSeguroIncendio">Tasa Seguro</label>
                                                <anthem:TextBox ID="txtTasaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class='form-group'>
                                                <label for="txtMontoAseguradoIncendio">Monto Asegurado</label>
                                                <anthem:TextBox ID="txtMontoAseguradoIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                            </div>
                                            <div class='form-group'>
                                                <label for="txtPrimaSeguroCesantia">Prima Mensual Seguro</label>
                                                <anthem:TextBox ID="txtPrimaSeguroIncendio" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>



            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Información Secundaria</h3>
            </div>
            <div class="panel-body">
                <div class="row">


                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="ddlRegionProp" class="control-label">Región</label>
                            <anthem:DropDownList ID="ddlRegionProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlRegionProp_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="ddlProvinciaProp" class="control-label">Provincia</label>
                            <anthem:DropDownList ID="ddlProvinciaProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlProvinciaProp_SelectedIndexChanged"></anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="ddlComunaProp" class="control-label">Comuna</label>
                            <anthem:DropDownList ID="ddlComunaProp" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="chIndDfl2" class="control-label"></label>
                            <anthem:CheckBox ID="chIndDfl2" AutoUpdateAfterCallBack="true" class="control-label" runat="server" Text="DFL-2" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-lg-2">
                        <div class='form-group'>
                            <label for="ddlVia" class="control-label">Via</label>
                            <anthem:DropDownList ID="ddlVia" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class='form-group'>
                            <label for="txtDireccionProp" class="control-label">Dirección/Calle</label>
                            <anthem:TextBox ID="txtDireccionProp" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class='form-group'>
                            <label for="txtNumeroProp" class="control-label">Número</label>
                            <anthem:TextBox ID="txtNumeroProp" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-1">
                        <div class='form-group'>
                            <label for="txtDeptoOficinaProp" class="control-label">Depto/Oficina</label>
                            <anthem:TextBox ID="txtDeptoOficinaProp" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class='form-group'>
                            <label for="txtUbicacionProp" class="control-label">Ubicación/Manzana - Lote</label>
                            <anthem:TextBox ID="txtUbicacionProp" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row" style="visibility: hidden;">
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="ddlTipoConstruccion" class="control-label">Tipo de Construcción</label>
                            <anthem:DropDownList ID="ddlTipoConstruccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="chkIndUsoGoce" class="control-label"></label>
                            <anthem:CheckBox ID="chkIndUsoGoce" runat="server" Text="Uso y Goce" CssClass="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnCheckedChanged="chkIndUsoGoce_CheckedChanged" />
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="txtRolManzana" class="control-label">Rol - Manzana</label>
                            <anthem:TextBox ID="txtRolManzana" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class='form-group'>
                            <label for="txtRolSitio" class="control-label">Rol - Sitio</label>
                            <anthem:TextBox ID="txtRolSitio" CssClass="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12 text-center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarProp" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
                            OnClick="btnGuardarProp_Click" TextDuringCallBack="Guardando..." />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarProp" runat="server" Text="Cancelar" CssClass="btn-success btn-sm"
                            OnClick="btnCancelarProp_Click" TextDuringCallBack="Cancelando..." AutoUpdateAfterCallBack="True" />

                    </div>
                </div>
            </div>
        </div>



        <anthem:HiddenField ID="hfIdTasacion" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
        <anthem:HiddenField ID="hfIdPropiedad" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
    </div>
</div>
