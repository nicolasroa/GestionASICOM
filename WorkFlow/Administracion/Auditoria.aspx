<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Auditoria.aspx.cs" Inherits="WebSite.Administracion.Auditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/Validaciones/Validacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        function Inicio() {
            $("#<%= txtFechaInicio.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 3,
                maxDate: 0,
                constrainInput: true, //La entrada debe cumplir con el formato
                onClose: function (selectedDate) {
                    $("#<%= txtFechaTermino.ClientID %>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFechaTermino.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                maxDate: 0,
                changeYear: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    $("#<%= txtFechaInicio.ClientID %>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

    </script>
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Auditoría
                </span>
                <br>
            </h4>
           <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Parámetros de Búsqueda</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <label>Seleccionar Tablas a Consultar</label>

                            <asp:CheckBoxList ID="chklstTablasCriticas" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="ddlUsuarios">Usuario</label>
                            <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlUsuarios" runat="server">
                            </anthem:DropDownList>
                        </div>
                        <div class="col-md-3">

                            <label for="ddlMovimiento">Movimiento</label>
                            <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlMovimiento" runat="server">
                                <asp:ListItem> -- Todos --</asp:ListItem>
                                <asp:ListItem Value="I">Creación de Registros</asp:ListItem>
                                <asp:ListItem Value="U">Modificación de Registros</asp:ListItem>
                                <asp:ListItem Value="D">Eliminación de Registros</asp:ListItem>
                            </anthem:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="txtFechaInicio">Fecha Inicio</label>
                            <anthem:TextBox CssClass="form-control" runat="server" ID="txtFechaInicio" AutoUpdateAfterCallBack="true"
                                onblur="esFechaValida(this);"></anthem:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="txtFechaTermino">Fecha Término</label>
                            <anthem:TextBox CssClass="form-control" runat="server" ID="txtFechaTermino" AutoUpdateAfterCallBack="true"
                                onblur="esFechaValida(this);"></anthem:TextBox>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <anthem:Button OnMouseDown="javascript:Espera(this, 'Buscando Registros');" EnabledDuringCallBack="True"
                                PostCallBackFunction="setTimeout($.unblockUI, 0);" ID="btnBuscar" CssClass="btn-sm btn-primary"
                                runat="server" Text="Buscar" OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <a href="#"><span class="badge">
                                <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></span></a>
                        
                            <input type="button" value="Exportar" data-dropdown="#Exportar" class="btn btn-info" style="float:right;" />
                            <div id="Exportar" class="dropdown dropdown-tip">
                                <ul class="dropdown-menu">
                                    <li>
                                        <anthem:ImageButton ID="Image2" runat="server" ImageUrl="~/Img/icons/xls.png" EnableCallBack="False"
                                            title="Exportar a Excel" OnClick="lnkExpotarExcel_Click" />
                                    </li>
                                    <li class="dropdown-divider"></li>
                                    <li>
                                        <anthem:ImageButton ID="Image1" runat="server" ImageUrl="~/Img/icons/pdf.png" EnableCallBack="False"
                                            title="Exportar a Pdf" OnClick="lnkExpotarPdf_Click" />
                                    </li>
                                </ul>

                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top:5px;">
                    <div class="col-lg-12">
                        <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo de Operación" />
                                <asp:BoundField DataField="NombreTabla" HeaderText="Tabla Afectada" />
                                <asp:BoundField DataField="NombreCampo" HeaderText="Campo Afectado" />
                                <asp:BoundField DataField="DatoAnterior" HeaderText="Dato Anterior" />
                                <asp:BoundField DataField="DatoNuevo" HeaderText="Dato Nuevo" />
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Responsable" />
                                <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha de la Operación"
                                    DataFormatString="{0:g}" />
                            </Columns>
                        </anthem:GridView>
                    </div>
                </div>
                </div>
                
            </div>
        </div>
    </div>



</asp:Content>
