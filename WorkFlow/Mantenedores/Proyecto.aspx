<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="WorkFlow.Mantenedores.Proyecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Proyectos
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

                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlInmobiliaria" class="text-left">Inmobiliaria</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlInmobiliaria" TabIndex="1" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server" TabIndex="2"></anthem:TextBox>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="3" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>
            </div>



            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-primary btn-sm" runat="server" Text="Buscar"
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
                            <asp:BoundField DataField="DescripcionInmobiliaria" HeaderText="Inmobiliaria" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Proyecto" />
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

                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormInmobiliaria" class="text-left">Inmobiliaria</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormInmobiliaria" TabIndex="4" runat="server" AutoUpdateAfterCallBack="True" AutoCallBack="True">
                        </anthem:DropDownList>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left">Nombre Proyecto</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormDescripcion" runat="server" TabIndex="5"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" TabIndex="6" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormRegion" class="text-left">Región</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormRegion" TabIndex="7" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormRegion_SelectedIndexChanged">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormProvincia" class="text-left">Provincia</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormProvincia" TabIndex="8" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormProvincia_SelectedIndexChanged">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormComuna" class="text-left">Comuna</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormComuna" TabIndex="9" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormComuna_SelectedIndexChanged">
                        </anthem:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="ddlFormComunaSII" class="text-left">Comuna Sii</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormComunaSII" TabIndex="7" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class='form-group'>
                        <label for="txtFormRolMatriz" class="text-left">Rol Matriz</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormRolMatriz" runat="server" TabIndex="9"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormTipoInmueble" class="control-label">Tipo Inmueble</label>
                        <anthem:DropDownList ID="ddlFormTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                    </div>
                </div>


            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class='form-group'>
                        <label for="txtFormDireccion" class="text-left">Dirección de Proyecto</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormDireccion" runat="server" TabIndex="9"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
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
