<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="BandejaDesembolso.aspx.cs" Inherits="WorkFlow.Aplicaciones.BandejaDesembolso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Bandeja de Desembolso
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Buscar Solicitudes
            </h3>
        </div>
        <div class="panel-body">
            <div class="row fivecolumns">
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtNumeroSolicitud" class="text-left">Solicitud</label>
                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtNumeroSolicitud" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtRutCliente" class="text-left">Rut Cliente</label>
                        <anthem:TextBox ID="txtRutCliente" runat="server" class="form-control" onchange="validaRut(this);"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtNombreCliente" class="text-left">Nombre</label>
                        <anthem:TextBox ID="txtNombreCliente" runat="server" class="form-control"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtApePatCliente" class="text-left">Apellido Paterno</label>
                        <anthem:TextBox ID="txtApePatCliente" runat="server" class="form-control"></anthem:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class='form-group'>
                        <label for="txtApeMatCliente" class="text-left">Apellido Paterno</label>
                        <anthem:TextBox ID="txtApeMatCliente" runat="server" class="form-control"></anthem:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="ddlInmobiliaria">Inmobiliaria</label>
                    <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-md-6">
                    <label for="ddlProyecto">Proyecto</label>
                    <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                </div>

            </div>
            <hr />
            <div class="row">
                <div class="col-md-12 text-center">
                    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscar" CssClass="btn-sm btn-primary" runat="server" Text="Buscar"
                        OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." EnableCallBack="False" />
                    <anthem:LinkButton ID="btnIniciarFlujoPrepago" CssClass="btn-sm btn-primary btn-success" runat="server" OnClick="btnIniciarFlujoPrepago_Click" TextDuringCallBack="Procesando..."
                        OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Iniciar El Flujo de Desembolso de las Solicitudes Seleccionadas?',this);"><span class="glyphicon glyphicon-forward"></span> Iniciar Flujo de Desembolso</anthem:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 text-left">

            <a href="#"><span class="badge">
                <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span></a>
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
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIniciarFlujo" runat="server" Text=""></asp:CheckBox>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSeleccionarTodos" Text="Seleccionar Todos" runat="server" OnCheckedChanged="chkSeleccionarTodos_CheckedChanged"
                                AutoPostBack="true"></asp:CheckBox>
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Solicitud_Id" HeaderText="Número de Solicitud" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Cliente" />
                    <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" />
                    <asp:BoundField DataField="DescripcionEvento" HeaderText="Evento Desembolso" />
                    <asp:BoundField DataField="FechaTermino" HeaderText="Fecha Término" />
                    <asp:BoundField DataField="DiasTranscurridos" HeaderText="Dias Transcurridos" />
                </Columns>
            </anthem:GridView>
        </div>

    </div>
</asp:Content>

