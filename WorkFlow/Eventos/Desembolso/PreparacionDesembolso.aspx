<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="PreparacionDesembolso.aspx.cs" Inherits="WorkFlow.Eventos.Desembolso.PreparacionDesembolso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
    <%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
    <%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
    <%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
    <%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
    <%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#DestinoFondos" data-toggle="tab" aria-expanded="true">Destino de Fondos</a></li>
            </ul>
        </div>
       <div class="col-lg-4 col-md-4 col-sm-12">

            <div class='form-group'>
                <div class="col-lg-7">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5">
                    <anthem:LinkButton ID="btnAccion" Visible="false" runat="server" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);" AutoUpdateAfterCallBack="true" OnClick="btnAccion_Click" EnabledDuringCallBack="false" TextDuringCallBack="Procesando" PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"></anthem:LinkButton>
                </div>
            </div>
        </div>

           </div>
    <div class="row spacenav">
        <div class="col-lg-12 text-right">
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
        </div>
    </div>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade" id="Resumen">
            <uc1:DatosResumen runat="server" ID="DatosResumen" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade" id="Credito">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>
        <div class="tab-pane fade" id="Participantes">
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
        </div>
        <div class="tab-pane fade" id="Propiedades">
            <uc1:DatosPropiedad runat="server" ID="DatosPropiedad" />
        </div>
        <div class="tab-pane fade active in" id="DestinoFondos">
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class='form-group'>
                                <label for="ddlTipoDestinoFondo">Distribución de Pagos</label>
                                <anthem:DropDownList ID="ddlTipoDestinoFondo" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoDestinoFondo_SelectedIndexChanged"></anthem:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class='form-group'>
                                <label for="ddlBeneficiario">Beneficiario</label>
                                <anthem:DropDownList ID="ddlBeneficiario" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class='form-group'>
                                <label for="txtMontoDestinoFondo">Monto</label>
                                <anthem:TextBox ID="txtMontoDestinoFondo" runat="server" class="form-control" AutoUpdateAfterCallBack="True" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGrabarDestinoFondo" class="btn btn-sm btn-primary" OnClick="btnGrabarDestinoFondo_Click" TextDuringCallBack="Guardando..." runat="server"><span class="glyphicon glyphicon-floppy-disk"></span>Grabar Destino</anthem:LinkButton>
                    <anthem:LinkButton PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDestinoFondo" class="btn btn-sm btn-warning" OnClick="btnCancelarDestinoFondo_Click" TextDuringCallBack="Cancelando..." runat="server"><span class="glyphicon glyphicon-trash"></span>Cancelar Ingreso</anthem:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Destinos Ingresados</h3>
                        </div>
                        <div class="panel-body">
                            <anthem:GridView runat="server" ID="gvDestinoFondos" AutoGenerateColumns="false" Width="100%"
                                AllowSorting="True"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="DescripcionTipoDestinoFondo" HeaderText="Distribución de Pago" />
                                    <asp:BoundField DataField="NombreBeneficiario" HeaderText="Beneficiario" />
                                    <asp:BoundField DataField="MontoUF" HeaderText="Monto" DataFormatString="{0:F4}" />
                                    <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnModificarDestinoFondo" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                ToolTip="Modificar" OnClick="btnModificarDestinoFondo_Click" TextDuringCallBack="Buscando Registro..." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnEliminarDestinoFondo" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                ToolTip="Eliminar" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarDestinoFondo_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                </div>
                <div class="col-lg-2">
                    <div class='form-group'>
                        <label for="txtTotalDestinar">Total a Destinar</label>
                        <anthem:TextBox ID="txtTotalDestinar" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class='form-group'>
                        <label for="txtPorDestinar">Por Destinar</label>
                        <anthem:TextBox ID="txtPorDestinar" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class='form-group'>
                        <label for="txtDestinado">Monto Destinado</label>
                        <anthem:TextBox ID="txtDestinado" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Enabled="false" onKeyPress="return SoloNumeros(event,this.value,4,this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-lg-3">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

