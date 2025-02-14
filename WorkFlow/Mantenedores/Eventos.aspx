<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="WorkFlow.Mantenedores.Eventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-lg-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Maestro de Eventos
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
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlMalla" class="text-left">Malla</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlMalla" TabIndex="4" runat="server" AutoCallBack="True" OnSelectedIndexChanged="ddlMalla_SelectedIndexChanged">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">

                    <div class='form-group'>
                        <label for="ddlEtapa" class="text-left">Etapa</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEtapa" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>

                </div>

                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlRolAsignado" class="text-left">Rol Asignado</label>
                        <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlRolAsignado" TabIndex="4" runat="server">
                        </anthem:DropDownList>
                    </div>

                </div>
            </div>


            <div class="row">
                <div class="col-lg-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                </div>
            </div>

            <div class="row">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="ImageButton2" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Registro"
                    OnClick="btnNuevoRegistro_Click" />
                <div class="col-lg-12">
                    <span class="badge bg-info">
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span>




                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="DescripcionMalla" HeaderText="Malla" />
                            <asp:BoundField DataField="DescripcionEtapa" HeaderText="Etapa" />
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
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormDescripcion" class="text-left">Descripción</label>
                        <anthem:TextBox CssClass="form-control" ID="txtFormDescripcion" runat="server" TabIndex="7"
                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                    </div>

                </div>

                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormMalla" class="text-left">Malla</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormMalla" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormMalla_SelectedIndexChanged">
                        </anthem:DropDownList>
                    </div>

                </div>

                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormEtapa" class="text-left">Etapa</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEtapa" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormEstado" class="text-left">Estado</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEstado" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="chkFormDesembolso" class="text-left">Genera Desembolso</label>
                        <anthem:CheckBox ID="chkFormDesembolso" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" Text="" />
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="chkFormEndoso" class="text-left">Genera Endoso</label>
                        <anthem:CheckBox ID="chkFormEndoso" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="ddlFormRolAsignado" class="text-left">Rol Asignado</label>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormRolAsignado" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormDuracionEstandar" class="text-left">Duración Estándar</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormDuracionEstandar" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="chkFormEventoInicial" class="text-left">Evento Inicial</label>
                        <anthem:CheckBox ID="chkFormEventoInicial" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="txtFormDescripcionPlantilla" class="text-left">Plantilla</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormDescripcionPlantilla" runat="server" TabIndex="1"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-lg-4">

                    <div class='form-group'>
                        <label for="chkFormIndFlujoEspecial" class="text-left">Flujo Especial</label>
                        <anthem:CheckBox ID="chkFormIndFlujoEspecial" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" Text="" AutoCallBack="True" OnCheckedChanged="chkFormIndFlujoEspecial_CheckedChanged" />
                    </div>

                </div>
                <div class="col-lg-4">
                    <div class='form-group'>
                        <label for="chkFormEventoFinal" class="text-left">Evento Final</label>
                        <anthem:CheckBox ID="chkFormEventoFinal" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4">

                    <div class='form-group'>
                        <label for="txtFormProcedimientoDeTermino" class="text-left">Proceso a Ejecutar</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFormProcedimientoDeTermino" runat="server" TabIndex="999"></anthem:TextBox>
                    </div>

                </div>
                <div class="col-lg-4">
                </div>
            </div>


            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Configuración de Formularios</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="chkIndModificaDatosCredito" class="text-left">Modifica Datos del Crédito</label>
                                    <anthem:CheckBox ID="chkFormIndModificaDatosCredito" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="chkIndModificaDatosParticipantes" class="text-left">Modifica Datos de los Participantes</label>
                                    <anthem:CheckBox ID="chkFormIndModificaDatosParticipantes" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="chkIndModificaDatosPropiedades" class="text-left">Modifica Datos de las Propiedades</label>
                                    <anthem:CheckBox ID="chkFormIndModificaDatosPropiedades" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class='form-group'>
                                    <label for="chkIndModificaDatosSeguros" class="text-left">Modifica Datos de los Seguros</label>
                                    <anthem:CheckBox ID="chkFormIndModificaDatosSeguros" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-primary center">
                        <div class="panel-heading">
                            <h3 class="panel-title">Asignación de Roles</h3>
                        </div>
                        <div class="panel-body center">
                            <div class="row">
                                <div class="col-lg-8 text-left">
                                    <anthem:DropDownList CssClass="form-control" ID="ddlFormRoles" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>

                                </div>
                                <div class="col-lg-4 text-left">
                                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnAsignarRol" runat="server" CssClass="btn-success btn-sm" Text="Asignar"
                                        TextDuringCallBack="Asignando Rol..." OnClick="btnAsignarRol_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 center">
                                    <anthem:GridView runat="server" ID="gvRolesEvento" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionRol" HeaderText="Descripción" />
                                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                        ToolTip="Eliminar Asignación" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminar_Click" />
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
                <div class="col-lg-6">
                    <div class="panel panel-primary center">
                        <div class="panel-heading">
                            <h3 class="panel-title">Configuración del Flujo</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6 text-left">
                                    <div class='form-group'>
                                        <label for="ddlFormAccion" class="text-left">Acción</label>
                                        <anthem:DropDownList CssClass="form-control" ID="ddlFormAccion" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                        </anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <div class='form-group'>
                                        <label for="ddlFormEventoDestino" class="text-left">Evento Destino</label>
                                        <anthem:DropDownList CssClass="form-control" ID="ddlFormEventoDestino" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                        </anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 text-left">
                                    <div class='form-group'>
                                        <label for="ddlEstadoSolicitud" class="text-left">EstadoSolicitud</label>
                                        <anthem:DropDownList CssClass="form-control" ID="ddlEstadoSolicitud" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                        </anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <div class='form-group'>
                                        <label for="ddlSubEstadoSolicitud" class="text-left">SubEstado Solicitud</label>
                                        <anthem:DropDownList CssClass="form-control" ID="ddlSubEstadoSolicitud" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                        </anthem:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnAsignarFlujo" runat="server" CssClass="btn-success btn-sm" Text="Asignar"
                                        TextDuringCallBack="Asignando Flujo..." OnClick="btnAsignarFlujo_Click" />
                                </div>


                            </div>
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <anthem:GridView runat="server" ID="gvAsignacionFlujo" AutoGenerateColumns="false" Width="100%"
                                        PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                                        AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="DescripcionAccion" HeaderText="Acción" />
                                            <asp:BoundField DataField="DescripcionEventoDestino" HeaderText="Destino" />
                                            <asp:BoundField DataField="DescripcionEstadoSolicitud" HeaderText="Estado Solicitud" />
                                            <asp:BoundField DataField="DescripcionSubEstadoSolicitud" HeaderText="SubEstado Solicitud" />
                                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnEliminarFlujo" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                        ToolTip="Eliminar Asignación" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarFlujo_Click" />
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
            <div class="row">
                <div class="col-lg-12 text-center">
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
