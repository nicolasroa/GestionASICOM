<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Inmobiliaria.aspx.cs" Inherits="WorkFlow.Mantenedores.Inmobiliaria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Inmobiliaria
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>


    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" href="#">Búsqueda</a>
        </h2>
        <div>
            <div class="row">

                <div class="col-md-6">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class='form-group'>
                        <label for="ddlEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>

                </div>

            </div>



            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-primary btn-sm" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                    
                    <anthem:LinkButton ID="btnGenerarReporte" CssClass="btn-primary btn-sm" EnableCallBack="false" runat="server" OnClick="btnGenerarReporte_Click"><span class="glyphicon glyphicon-export"></span> Generar Reporte Excel</anthem:LinkButton>
                </div>
            </div>

            <div class="row">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnNuevoRegistro" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
                    OnClick="btnNuevoRegistro_Click" />
                <div class="col-md-12">
                    <span class="badge bg-info">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="RutInmobiliaria" HeaderText="Rut" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                            <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                        ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>

            </div>

        </div>
        <h2 id="hFormulario" style="display: none;">
            <a id="lnkFormulario" href="#">Formulario</a></h2>
        <div>
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos Principales</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-4">
                            <div class='form-group'>
                                <label for="txtFormDescripcion" class="text-left">Descripción</label>
                                <anthem:TextBox CssClass="form-control" ID="txtFormDescripcion" runat="server" TabIndex="7"
                                    AutoUpdateAfterCallBack="true"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class='form-group'>
                                <label for="txtFormRut" class="text-left">Rut</label>
                                <anthem:TextBox CssClass="form-control" ID="txtFormRut" onchange="validaRut(this);" MaxLength="13" runat="server" TabIndex="12"
                                    AutoUpdateAfterCallBack="true"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class='form-group'>
                                <label for="ddlFormEstado" class="text-left">Estado</label>
                                <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                </anthem:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de Contacto</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class='form-group'>
                                    <label for="txtFormDescripcion" class="text-left">Nombre Contacto</label>
                                    <anthem:TextBox CssClass="form-control" ID="txtFormContacto" runat="server" TabIndex="7"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class='form-group'>
                                    <label for="txtFormCargoContacto" class="text-left">Cargo</label>
                                    <anthem:TextBox CssClass="form-control" ID="txtFormCargoContacto" runat="server" TabIndex="7"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class='form-group'>
                                    <label for="txtFormMailContacto" class="text-left">Mail</label>
                                    <anthem:TextBox CssClass="form-control" ID="txtFormMailContacto" runat="server" TabIndex="7" placeholder="name@example.com" onBlur="validarEmail(this)"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class='form-group'>
                                    <label for="txtFormFonoFijoContacto" class="text-left">Teléfono Fijo</label>
                                    <anthem:TextBox CssClass="form-control" ID="txtFormFonoFijoContacto" runat="server" TabIndex="7" onBlur="ValidaFono(this)" placeholder="+56212345678"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class='form-group'>
                                    <label for="txtFormCelularContacto" class="text-left">Celular</label>
                                    <anthem:TextBox CssClass="form-control" ID="txtFormCelularContacto" runat="server" TabIndex="7"  onBlur="ValidaFono(this)" placeholder="+56912345678"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Configuración en WorkFlow</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6">
                            <div class='form-group'>
                                <anthem:CheckBox ID="chIndDesembolso" AutoUpdateAfterCallBack="true" runat="server" Text="  Indicador de Desembolso" AutoCallBack="True" OnCheckedChanged="chIndDesembolso_CheckedChanged" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class='form-group'>
                                <label for="ddlFormEventoDesembolso" class="text-left">Evento de Desembolso</label>
                                <anthem:DropDownList CssClass="form-control" ID="ddlFormEventoDesembolso" TabIndex="13" runat="server" AutoUpdateAfterCallBack="true">
                                </anthem:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

            </div>


            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-primary btn-sm" Text="Guardar"
                        OnClick="btnGuardar_Click" TextDuringCallBack="Buscando..." />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-primary btn-sm"
                        OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />

        </div>
    </div>


</asp:Content>
