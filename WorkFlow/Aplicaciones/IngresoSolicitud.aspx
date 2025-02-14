<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="IngresoSolicitud.aspx.cs" Inherits="WorkFlow.Aplicaciones.IngresoSolicitud" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h5>
                <span class="titulo">
                   <%-- <img src="../Img/icons/favicon_xsmall.png" />--%>Ingreso de Solicitudes
                </span>
                <br>
            </h5>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>
    </div>
    <ul class="nav small nav-tabs">
        <li class="active"><a href="#Solicitud" data-toggle="tab" aria-expanded="true">Solicitud</a></li>
        <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
        <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>

    </ul>
    <div id="tabEvento" class="tab-content">
        <div class="tab-pane fade active in" id="Solicitud">
            <uc1:DatosSolicitud runat="server" ID="DatosSolicitud" />
        </div>
        <div class="tab-pane fade" id="Participantes">
            <uc1:DatosParticipante runat="server" ID="DatosParticipante" />
            <%--<p>DATOS DE LOS PARTICIPANTES</p>--%>
        </div>
        <div class="tab-pane fade" id="Propiedades">
            <p>DATOS DE LAS PROPIEDADES</p>
        </div>

    </div>



</asp:Content>
