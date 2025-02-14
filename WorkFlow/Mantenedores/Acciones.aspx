<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Acciones.aspx.cs" Inherits="WorkFlow.Mantenedores.Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Acciones del WorkFlow
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
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="ddlSentido" class="text-left">Sentido</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlSentido" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="ddlEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                </div>
            </div>

            <div class="row">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="ImageButton2" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
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
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="DescripcionSentido" HeaderText="Sentido" />
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
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormDescripcion" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="ddlFormSentido" class="text-left">Sentido</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlFormSentido" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="ddlFormEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlFormEstado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>

                </div>
                <div class="col-md-3">
                    <div class='form-group'>
                        <label for="ddlFormEstadoEvento" class="text-left">Estado del Evento</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlFormEstadoEvento" TabIndex="4" runat="server" ToolTip="Estado en que se dejará el Evento al seleccionar esta Acción">
                        </anthem:DropDownList>
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary" Text="Guardar"
                        OnClick="btnGuardar_Click" TextDuringCallBack="Guardando..." />
                    &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-sm btn-primary"
                        OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                </div>
            </div>
            <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />

        </div>
    </div>





</asp:Content>
