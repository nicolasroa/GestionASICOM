<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosRegistroAntecedentesSalud.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosRegistroAntecedentesSalud" %>
<script>

    function InicioAntecedentesSalud() {

        try {
            $("#<%= txtFechaEnvioDPS.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                constrainInput: true //La entrada debe cumplir con el formato
            });

        } catch (e) {
            alert(e.message);
        }
    }
</script>


<div class="row small">
    <div class="col-lg-12">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Participantes del Crédito</h3>
        </div>
        <div class="panel-body">
            <anthem:GridView runat="server" ID="gvParticipantesAntecedentes" AutoGenerateColumns="false" Width="100%"
                PageSize="5" AllowSorting="True"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Relación Crédito" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Nombre" />
                    <asp:BoundField DataField="FechaIngresoDPS" HeaderText="Fecha de Envío DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FechaAprobacionDPS" HeaderText="Fecha de Resolución DPS" NullDisplayText="No Ingresada" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="DescripcionEstadoDps" HeaderText="Estado DPS" NullDisplayText="No Procesado" />
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
</div>
<div class="row small">
    <div class="col-lg-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Información del Participante</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2">
                        <label for="txtRut">Rut:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtRut" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>
                    <div class="col-lg-2">
                        <label for="txtParticipacion">Participación:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtParticipacion" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>
                    <div class="col-lg-2">
                        <label for="txtPorcentajeDeuda">% Deuda:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtPorcentajeDeuda" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-2">
                        <label for="txtNombreParticipante">Nombre:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtNombreParticipante" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>

                    <div class="col-lg-2">
                        <label for="txtEdadPlazo">Edad+Plazo:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtEdadPlazo" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>
                    <div class="col-lg-2">
                        <label for="txtPorcentajeDesgravamen">% Desgravamen:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtPorcentajeDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row small">
    <div class="col-lg-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Seguro de Desgravamen</h3>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-lg-2">
                        <label for="ddlSeguroDesgravamen">Póliza:</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:DropDownList ID="ddlSeguroDesgravamen" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroDesgravamen_SelectedIndexChanged"></anthem:DropDownList>
                    </div>

                    <div class="col-lg-2">
                        <label for="txtCompañia">Compañía de Seguros</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtCompañia" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                    </div>

                    <div class="col-lg-2">
                        <label for="txtMontoAsegurado">Monto Asegurado</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtMontoAsegurado" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                    </div>

                </div>


                <div class="row">
                    <div class="col-lg-2">
                        <label for="txtTasaSeguroDesgravamen">Tasa Seguro</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtTasaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                    </div>

                    <div class="col-lg-2">
                        <label for="txtPrimaSeguroDesgravamen">Prima Mensual Seguro</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtPrimaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                    </div>
                    <div class="col-lg-2">
                        <label for="txtMontoAsegurado">Plazo del Crédito</label>
                    </div>
                    <div class="col-lg-2">
                        <anthem:TextBox ID="txtPlazo" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-lg-2">
        <anthem:Label ID="lblFechaEnvioDPS" runat="server" AutoUpdateAfterCallBack="true" Text="Fecha de Envio DPS:"></anthem:Label>
    </div>
    <div class="col-lg-2">
        <anthem:TextBox ID="txtFechaEnvioDPS" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>

    </div>
    <div class="col-lg-1">
        <anthem:Label ID="lblEstadoDps" runat="server" AutoUpdateAfterCallBack="true" Text="Estado:"></anthem:Label></div>
    <div class="col-lg-2">
        <anthem:DropDownList ID="ddlEstadoDps" runat="server" class="form-control" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlEstadoDps_SelectedIndexChanged"></anthem:DropDownList>
    </div>
    <div class="col-lg-2">
        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarRegistroAntecedentes" runat="server" CssClass="btn btn-primary" Text="Guardar Antecedentes"
            OnClick="btnGuardarRegistroAntecedentes_Click" TextDuringCallBack="Guardando..." />
    </div>
    <div class="col-lg-2">
        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarRegistroAntecedentes" runat="server" Text="Cancelar" CssClass="btn btn-primary"
            OnClick="btnCancelarRegistroAntecedentes_Click" TextDuringCallBack="Cancelando..." />
    </div>
</div>
<div class="row small">
    <div class="col-lg-12">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Antecedentes de Salud</h3>
        </div>
        <div class="panel-body">
            <anthem:GridView runat="server" ID="gvAntecedentesSalud" AutoGenerateColumns="false" Width="100%"
                PageSize="5" AllowSorting="True"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <anthem:CheckBox ID="chkAntecedenteSeleccionado" runat="server" OnCheckedChanged="chkAntecedenteSeleccionado_CheckedChanged" EnableCallBack="true" AutoUpdateAfterCallBack="true" AutoCallBack="True" />
                        </ItemTemplate>
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DescripcionAntecedente" HeaderText="Relación Crédito" HeaderStyle-Width="20%">
                        <HeaderStyle Width="20%"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="70%">
                        <ControlStyle Width="100%" />
                        <ItemTemplate>
                            <anthem:TextBox ID="txtObservacionAntecedente" Enabled="false" class="form-control" runat="server" AutoUpdateAfterCallBack="true" desable="true" Width="100%"></anthem:TextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="70%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </anthem:GridView>
        </div>
    </div>
        </div>
</div>


