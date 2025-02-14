<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="InscripcionDeHipotecas.aspx.cs" Inherits="WorkFlow.Eventos.InscripcionDeHipotecas" %>

<%@ Register Src="~/DatosGenerales/DatosSolicitud.ascx" TagPrefix="uc1" TagName="DatosSolicitud" %>
<%@ Register Src="~/DatosGenerales/DatosPropiedad.ascx" TagPrefix="uc1" TagName="DatosPropiedad" %>
<%@ Register Src="~/DatosGenerales/DatosParticipante.ascx" TagPrefix="uc1" TagName="DatosParticipante" %>
<%@ Register Src="~/DatosGenerales/DatosResumen.ascx" TagPrefix="uc1" TagName="DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>

<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function InicioDatepicker() {

            try {
                //$('.txtFechaPicker').datepicker({
                $("#<%= txtFechaInscripcionHipoteca.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato
                    maxDate: 0
                });
                $("#<%= txtFechaIngreso.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato
                    maxDate: 0
                });
                $("#<%= txtFechaReingreso.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato
                    maxDate: 0
                });
                $("#<%= txtFechaReparo.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato 
                    maxDate: 0
                });
                $("#<%= txtFechaInformePrevio.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true, //La entrada debe cumplir con el formato 
                    maxDate: 0
                });
            } catch (e) {
                alert(e.message);
            }
        }
    </script>

    <div class="row navbar-fixed-top" style="background-color: #ffffff">

       <div class="col-lg-8 col-md-8 col-sm-12">
            <ul class="nav small nav-tabs">
                <li class=""><a href="#Resumen" data-toggle="tab" aria-expanded="false">Resumen</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class=""><a href="#Credito" data-toggle="tab" aria-expanded="false">Crédito</a></li>
                <li class=""><a href="#Participantes" data-toggle="tab" aria-expanded="false">Participantes</a></li>
                <li class=""><a href="#Propiedades" data-toggle="tab" aria-expanded="false">Propiedades en Garantía</a></li>
                <li class="active"><a href="#Inscripcion" data-toggle="tab" aria-expanded="true">Inscripción de Hipotecas</a></li>
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
            <anthem:LinkButton ID="btnDocumental" runat="server" AutoUpdateAfterCallBack="true" OnClick="btnDocumental_Click" CssClass="btn-sm btn-success"><span class="glyphicon glyphicon-folder-open"></span> Carpeta Digital</anthem:LinkButton>
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
        <div class="tab-pane fade active in" id="Inscripcion">
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos de Inscripción de Hipoteca</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">

                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtFechaIngreso">Fecha Ingreso</label>
                                        <anthem:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtFechaIngreso">Fecha Inscripción</label>
                                        <anthem:TextBox ID="txtFechaInscripcionHipoteca" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtFechaIngreso">Fojas</label>
                                        <anthem:TextBox ID="txtFojas" CssClass="form-control" AutoUpdateAfterCallBack="True" runat="server"></anthem:TextBox>
                                    </div>

                                    <div class='form-group'>
                                        <label for="txtFechaIngreso">Número</label>
                                        <anthem:TextBox ID="txtNroInscripcion" CssClass="form-control" AutoUpdateAfterCallBack="True" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Datos de Re-Ingreso</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtFechaReingreso">Fecha Reingreso</label>
                                        <anthem:TextBox ID="txtFechaReingreso" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtFechaInformePrevio">Fecha Informe Previo</label>
                                        <anthem:TextBox ID="txtFechaInformePrevio" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class='form-group'>
                                        <label for="txtFechaReparo">Fecha Reparo</label>
                                        <anthem:TextBox ID="txtFechaReparo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" onblur="esFechaValida(this);"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>

            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                     <anthem:LinkButton ID="AbtnGuarda" class="btn-sm btn-primary" runat="server" TextDuringCallBack="Procesando" EnabledDuringCallBack="false" OnClick="AbtnGuarda_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Datos</anthem:LinkButton>

                        <anthem:LinkButton ID="AbtnCancela" class="btn-sm btn-warning" runat="server" OnClick="AbtnCancela_Click"><span class="glyphicon glyphicon-trash"></span> Cancelar Ingreso</anthem:LinkButton>

                   
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
