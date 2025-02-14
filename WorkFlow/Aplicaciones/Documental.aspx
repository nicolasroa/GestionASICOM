<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="Documental.aspx.cs" Inherits="WorkFlow.Aplicaciones.Documental" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function InicioDocumental() {
            try {
               
                $("#<%= txtFechaEmisionDocSolicitud.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0,
                    constrainInput: true //La entrada debe cumplir con el formato
                });
            } catch (e) {
                alert('1' + e.message);
            }
        }
        function InhabilitarSolicitudes() {

            try {
                document.getElementById("tabParticipantes").click();
                ObtenerControl('tabSolicitud').style.display = "none";
            } catch (e) {
                alert('2' + e.Message);
            }


        }
        function ValidarTamaño(obj, rep) {

            try {
                return;
                var input, file, Maximo;
                Maximo = 100000;// document.getElementById('txtMaximo').value;
                if (!window.FileReader) {
                    return;
                }

                if (rep == 1) {

                    input = document.getElementById('<%= fileDocSolicitud.ClientID %>');
                }
                else {
                    input = document.getElementById('<%= fileDocSolicitud.ClientID %>');
                }
                if (!input) {
                    MostrarMensajeAlerta("Debe Seleccionar un Archivo");
                }
                else if (!input.files) {
                    MostrarMensajeAlerta("Debe Seleccionar un Archivo");
                }
                else if (!input.files[0]) {
                    MostrarMensajeAlerta("Debe Seleccionar un Archivo");
                }
                else {
                    file = input.files[0];
                    var tamañoArchivo = file.size / 1000;

                    if (tamañoArchivo > Maximo) {
                        MostrarMensajeAlerta("Archivo excede el tamaño maximo permitido de " + Maximo + " KB");
                    }
                    else {
                        obj.click();
                    }
                }
            } catch (e) {
                alert( "22222"+ e.Message);
            }

        }
        function MostrarFormularioSubirDocSolicitud() {
            try {

                var esVisible = $("#DivDatosDocumentoSolicitud").is(":visible");

                if (esVisible === false) {
                    $('#DivDatosDocumentoSolicitud').show(500);
                }
                else {
                    $('#DivDatosDocumentoSolicitud').hide(500);
                }
            } catch (e) {
                alert(e.Message);
            }

        }

        function MostrarFormularioSubirDocSolicitudlEditar() {
            $('#DivDatosDocumentoSolicitud').show(500);
        }

        function MostrarFormularioPortalRep() {
            if (ObtenerControl('divFormularioEditarRep').style.display == "none") {
                ObtenerControl('divFormularioEditarRep').style.display = "block";
            }
            else {
                getElement('divFormularioEditarRep').style.display = "none";
            }
        }

        function MostrarFormularioPortalEditarRep() {
            ObtenerControl('divFormularioEditarRep').style.display = "block";
        }

    </script>

    <div class="row" style="background-color: #ffffff; width: 100%;">
        <ul class="nav small nav-tabs">
            <li class="active"><a id="tabSolicitud" href="#RespositorioSolicitud" data-toggle="tab" aria-expanded="true">Repositorio de la Solicitud</a></li>
            <%--<li class=""><a id="tabParticipantes" runat="server" href="#RespositorioParticipantes" data-toggle="tab" aria-expanded="false">Repositorio de los Participantes</a></li>--%>
            <li class=""><a href="#Historial" data-toggle="tab" aria-expanded="false">Historial de Movimientos</a></li>
        </ul>
        <div class="tab-content">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos Generales</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblRutSolicitante">Rut Solicitante:</label>
                        </div>
                        <div class="col-md-4">
                            <anthem:Label ID="lblRutSolicitante" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                        </div>
                        <div class="col-md-2">
                            <label for="lblProceso">Proceso:</label>
                        </div>
                        <div class="col-md-4">
                            <anthem:Label ID="lblProceso" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblNombreSolicitante">Nombre Solicitante:</label>
                        </div>
                        <div class="col-md-4">
                            <anthem:Label ID="lblNombreSolicitante" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                        </div>

                        <div class="col-md-2">
                            <label for="lblSolicitud">N° de Solicitud:</label>
                        </div>
                        <div class="col-md-4">
                            <anthem:Label ID="lblSolicitud" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade active in" id="RespositorioSolicitud">
                <div class="col-md-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Resumen de Documentos</h3>
                        </div>
                        <div class="panel-body">
                            <anthem:GridView runat="server" ID="gvResumenDocumentosSolicitud" AutoGenerateColumns="false" Width="100%"
                                AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <anthem:LinkButton runat="server" ID="lnkNombreArchivoBanSol" Text='<%# Eval("GrupoDocumento")%>'
                                                CommandArgument="GrupoDocumentoSolicitud"
                                                AutoUpdateAfterCallBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            (<anthem:Label ID="lblGrupoCantidad" runat="server" Text='<%# Bind("Cantidad") %>'
                                                AutoUpdateAfterCallBack="true" />)
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Simbología</h3>
                        </div>
                        <div class="panel-body small">

                            <div class="row">
                                <div class="col-md-2">
                                    <anthem:Image ID="imgVigente" runat="server" CssClass="simbologia" ImageUrl="~/Img/icons/Vigente.png" />
                                </div>
                                <div class="col-md-10">
                                    <label for="imgVigente">Documento Vigente</label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <anthem:Image ID="imgPorVencer" runat="server" CssClass="simbologia" ImageUrl="~/Img/icons/Advertencia.png" />
                                </div>
                                <div class="col-md-10">
                                    <label for="imgPorVencer">Documento Por Vencer</label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <anthem:Image ID="imgVencido" runat="server" CssClass="simbologia" ImageUrl="~/Img/icons/Vencido.png" />
                                </div>
                                <div class="col-md-10">
                                    <label for="imgVencido">Documento Vencido</label>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-9">
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <anthem:LinkButton ID="lnkFormularioEditar" runat="server" Text="Subir Documento" ForeColor="White"
                                        OnClientClick="MostrarFormularioSubirDocSolicitud();"></anthem:LinkButton></h3>
                            </div>
                            <div class="panel-body" id="DivDatosDocumentoSolicitud">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="ddlGrupoDocSolicitud">Grupo:</label>
                                        <anthem:DropDownList ID="ddlGrupoDocSolicitud" class="form-control" AutoUpdateAfterCallBack="true" runat="server" AutoCallBack="True" OnSelectedIndexChanged="ddlGrupoDocSolicitud_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtFechaEmisionDocSolicitud">Fecha de Emisión:</label>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFechaEmisionDocSolicitud" onblur="esFechaValida(this);" runat="server"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="ddlTipoDocumentoDocSolicitud">Tipo de Documento:</label>
                                        <anthem:DropDownList ID="ddlTipoDocumentoDocSolicitud" class="form-control" AutoUpdateAfterCallBack="true" runat="server" AutoCallBack="True"></anthem:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="fileDocSolicitud">Archivo:</label>
                                        <anthem:FileUpload ID="fileDocSolicitud" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" AllowMultiple="True" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="txtObservacionDocSolicitud">Observación:</label>
                                        <anthem:TextBox ID="txtObservacionDocSolicitud" runat="server" AutoUpdateAfterCallBack="True" TextMode="MultiLine" Style="width: 100%"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <anthem:Button AutoUpdateAfterCallBack="true" ID="btnGuardarDocSolicitud" EnableCallBack="false" runat="server" CssClass="btn btn-primary" Text="Guardar"
                                            OnClick="btnGuardarDocSolicitud_Click" TextDuringCallBack="Guardando..." OnMouseDown="javascript:ValidarTamaño(this,0);" />
                                        &nbsp;<anthem:Button  ID="btnCancelarDocSolicitud" runat="server" Text="Cancelar" CssClass="btn btn-primary" AutoUpdateAfterCallBack="true"
                                            OnClick="btnCancelarDocSolicitud_Click" TextDuringCallBack="Cancelando..." />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Documentos Digitalizados</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="ddlBusqGrupoSolicitud">Grupo:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <anthem:DropDownList ID="ddlBusqGrupoSolicitud" class="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="ddlBusqTipoDocumentoSolicitud">Tipo de Documento:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <anthem:DropDownList ID="ddlBusqTipoDocumentoSolicitud" class="form-control" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center panel-pading">
                                        <anthem:LinkButton ID="btnBuscarDocSolicitud" runat="server" class="btn-sm btn-primary" OnClick="btnBuscarDocSolicitud_Click"><span class="glyphicon glyphicon-search"></span> Buscar</anthem:LinkButton>
                                    </div>
                                </div>
                                <div class="row small">
                                    <div class="col-md-12">
                                    <anthem:GridView ID="gvDocumentosSolicitud" runat="server" AutoGenerateColumns="False"  CssClass="table table-condensed"
                                        AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="99%"
                                        CellPadding="0" UpdateAfterCallBack="True" OnRowDataBound="gvDocumentosSolicitud_RowDataBound" OnPageIndexChanging="gvDocumentosSolicitud_PageIndexChanging">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <anthem:Image ID="imgEstadoAlertaDoc" runat="server" Height="16px" Width="16px" ImageUrl='<%# Eval("EstadoAlertaDoc") %>' />
                                                    <anthem:HiddenField ID="hdnEstadoAlertaDoc" runat="server" Value='<%# Eval("EstadoAlertaDoc") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <anthem:LinkButton runat="server" ID="lnkNombreDocSolicitud" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                        OnClick="lnkNombreDocSolicitud_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false"
                                                        ToolTip='<%# (Eval("NombreArchivo").ToString())%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <anthem:Image ID="imgTipoDocumento" runat="server" Height="16px" Width="16px" />
                                                    <anthem:HiddenField ID="hdnTipoDocumento" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Emisión" DataField="FechaEmision" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField HeaderText="Vencimiento" DataField="FechaVencimiento" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:TemplateField HeaderText="Grupo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrupo" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 25) ? Eval("TipoDocumento").ToString().Substring(0, 24) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <anthem:ImageButton ID="btnDescargarDocSolicitud" alt="Descargar" runat="server" ImageUrl="~/Img/Grid/Descargar_grid.png"
                                                        EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="16px" Width="16px" OnClick="btnDescargarDocSolicitud_Click" />
                                                    <anthem:ImageButton ID="btnEditarDocSolicitud" alt="Editar" runat="server" EnableCallBack="true" AutoUpdateAfterCallBack="true" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                        Height="16px" Width="16px" OnClick="btnEditarDocSolicitud_Click" />
                                                    <anthem:ImageButton ID="btnEliminarDocSolicitud" alt="Eliminar" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                        Height="16px" Width="16px" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Eliminar el Registro?',this);" OnClick="btnEliminarDocSolicitud_Click" />
                                                    <anthem:HiddenField ID="IdSolDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                    <anthem:HiddenField ID="hdnPermisoDescargar" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                    <anthem:HiddenField ID="hdnPermisoVer" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                    <anthem:HiddenField ID="hdnPermisoModificar" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                    <anthem:HiddenField ID="hdnPermisoEliminar" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                    <anthem:HiddenField ID="hdnPermisoSubir" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <%-- <div class="tab-pane fade" id="RespositorioParticipantes">
            </div>--%>
            <div class="tab-pane fade" id="Historial">
                <div>
                    <anthem:GridView ID="gvHistorial" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed"
                        AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="99%"
                        CellPadding="0" UpdateAfterCallBack="True" OnPageIndexChanging="gvHistorial_PageIndexChanging" OnRowDataBound="gvHistorial_RowDataBound">
                        <RowStyle CssClass="GridItem" />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAtlItem" />
                        <Columns>
                            <asp:BoundField HeaderText="Fecha" DataField="FechaHoraEvento" DataFormatString="{0:dd-MM-yyyy}">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Usuario" DataField="Usuario">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Descripción Evento">
                                <ItemTemplate>
                                    <%# (Eval("DescripcionEvento").ToString().Length >= 80) ? Eval("DescripcionEvento").ToString().Substring(0, 80) + "..." : Eval("DescripcionEvento")%>
                                </ItemTemplate>
                                <HeaderStyle Width="420px" />
                                <ItemStyle Width="420px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Archivo">
                                <ItemTemplate>
                                    <%# (Eval("NombreArchivo").ToString().Length >= 20) ? Eval("NombreArchivo").ToString().Substring(0, 19) + "..." : Eval("NombreArchivo")%>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                                <ItemStyle Width="140px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Documento">
                                <ItemTemplate>
                                    <%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                                <ItemStyle Width="140px" />
                            </asp:TemplateField>
                        </Columns>
                    </anthem:GridView>
                </div>
                <br />
                <%-- <div class="BotonExportarExcel" style="width:100%; text-align:center;">
                    <anthem:Button ID="btnExportar" runat="server" Text="Exportar Historial" AutoUpdateAfterCallBack="true"
                        CssClass="myButton" EnableCallBack="false" OnClick="btnExportar_Click" />
                </div>--%>
            </div>
        </div>
    </div>
    <anthem:HiddenField ID="hfIdDocSolicitud" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdDocParticipante" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdArchivoSolicitud" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdArchivoParticipante" runat="server" AutoUpdateAfterCallBack="true" />

    <anthem:HiddenField ID="hdfTamañoMaximo" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
    <input id="txtMaximo" type="text" runat="server" style="visibility: hidden" />
</asp:Content>
