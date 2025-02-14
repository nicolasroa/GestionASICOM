<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="NuevaTasacionPiloto.aspx.cs" Inherits="WorkFlow.Aplicaciones.FlujosParalelos.NuevaTasacionPiloto" %>

<%@ Register Src="~/DatosGenerales/DatosInmobiliariaProyecto.ascx" TagPrefix="uc1" TagName="DatosInmobiliariaProyecto" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../../Img/icons/favicon_xsmall.png" />--%>Solicitud de Tasación Piloto
                </span>
                <br>
            </h4>
           <%-- <div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>

    <uc1:DatosInmobiliariaProyecto runat="server" id="DatosInmobiliariaProyecto" />

    <div class="row">

        <div class="col-md-12 text-center">
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                EnabledDuringCallBack="False" ID="btnProcesarTasacion" runat="server" CssClass="btn-sm btn-primary"
                OnClick="btnProcesarTasacion_Click" Text="Procesar Inicio del Flujo" TextDuringCallBack="Procesando..."
                OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Crear una Solicitud de Tasación para el Proyecto Seleccionado?',this);" />
        </div>
    </div>

</asp:Content>
