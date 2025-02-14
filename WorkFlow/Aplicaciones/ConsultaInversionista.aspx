<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaInversionista.aspx.cs" Inherits="WorkFlow.Aplicaciones.ConsultaInversionista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        function DetalleEtapa(Etapa_Id, IndVenta) {
            try {
                IdEtapa = document.getElementById('<%=hdfEtapa_Id.ClientID%>');
                IdEtapa.value = Etapa_Id;


                Venta = document.getElementById('<%=hdfIndVenta.ClientID%>');
                Venta.value = IndVenta;

                $('#' + '<%=btnBuscarEtapa.ClientID%>').trigger("click");

            } catch (e) {
                alert(e.message);
            }
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Consulta Inversionista
                </span>
                <br>
            </h4>
           <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <div class="row">
          <div class="col-md-3">
                    <div class='form-group'>
                        <anthem:DropDownList CssClass="form-control" ID="ddlFormInversionista" TabIndex="11" runat="server" Visible ="false" AutoUpdateAfterCallBack="true">
                        </anthem:DropDownList>
                    </div>
                </div>
        <div class="col-md-3 text-center">
            <anthem:LinkButton ID="btnBuscar" class="btn-sm btn-primary" runat="server" EnableCallBack="false" TextDuringCallBack="Buscando..." PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" OnClick="btnBuscar_Click"><span class="glyphicon glyphicon-list"></span> Cargar Detalle Completo</anthem:LinkButton>
        </div>
        <div class="col-md-3 text-center">
            <anthem:LinkButton ID="btnBuscarEtapas" class="btn-sm btn-primary" runat="server" EnableCallBack="false" TextDuringCallBack="Buscando..." PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" OnClick="btnBuscarEtapas_Click"><span class="glyphicon glyphicon-th-large"></span> Cargar Etapas</anthem:LinkButton>
        </div>
        <div class="col-md-3 text-center">
            <anthem:LinkButton ID="btnGenerarReporteBandeja" class="btn-sm btn-primary" EnableCallBack="false" runat="server" OnClick="btnGenerarReporteBandeja_Click" Visible="False"><span class="glyphicon glyphicon-export"></span> Generar Reporte Excel</anthem:LinkButton>
        </div>


    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span class="glyphicon glyphicon-list"></span>Resumen de Operaciones
                    </h3>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row small" runat="server" id="DivEtapas">
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
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
        <div class="col-md-12 small">




            <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                AutoUpdateAfterCallBack="true" CssClass="table table-condensed" OnRowDataBound="gvBusqueda_RowDataBound">
                <RowStyle CssClass="GridItem" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridAtlItem" />
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <anthem:Image ID="imgEstadoVenta" runat="server" Height="16px" Width="16px" />
                             <anthem:HiddenField ID="hdfVendido" runat="server" Value='<%# Eval("EstadoVenta") %>' />
                                                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NumeroSolicitud" HeaderText="Número de Solicitud" ItemStyle-Font-Bold="true" />
                    <asp:BoundField DataField="NumeroOperacion" HeaderText="Número de Operación" />
                    <asp:BoundField DataField="RutCliente" HeaderText="Rut Cliente" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                     <asp:BoundField DataField="MontoVenta" HeaderText="Monto Venta" DataFormatString="{0:F4}" />
                    <asp:BoundField DataField="MontoCredito" HeaderText="Crédito"  DataFormatString="{0:F4}"/>
                    <asp:BoundField DataField="PorcentajeFinanciamiento" HeaderText="% de Financiamiento"  DataFormatString="{0:F2}"/>
                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                    <asp:BoundField DataField="TasaCredito" HeaderText="Tasa del Crédito" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="FechaInicioEtapa" HeaderText="Fecha de Inicio"  DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FechaTerminoEtapa" HeaderText="Fecha de Término"  DataFormatString="{0:d}" />
                    <asp:BoundField DataField="CaratulaCBR" HeaderText="Nro. de Igreso CBR (Carátula)" NullDisplayText=""/>
                    <asp:BoundField DataField="FechaIngresoCBR" HeaderText="Fecha de Ingreso a CBR" NullDisplayText=""  DataFormatString="{0:d}" />
                    <asp:BoundField DataField="DescripcionCBR" HeaderText="CBR" NullDisplayText=""/>
                    <asp:BoundField DataField="DiasEnEtapa" HeaderText="Días en la Etapa" />
                </Columns>
            </anthem:GridView>
        </div>

    </div>

    <anthem:HiddenField ID="hdfEtapa_Id" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hdfIndVenta" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnBuscarEtapa" CssClass="oculto" runat="server" Text=""
        OnClick="btnBuscarEtapa_Click" EnableCallBack="False" />



</asp:Content>
