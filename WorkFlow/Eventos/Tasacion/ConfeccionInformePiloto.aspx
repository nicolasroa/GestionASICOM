<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Eventos.Master" AutoEventWireup="true" CodeBehind="ConfeccionInformePiloto.aspx.cs" Inherits="WorkFlow.Eventos.Tasacion.ConfeccionInformePiloto" %>

<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>
<%@ Register Src="~/DatosGenerales/DatosFlujoSolicitud.ascx" TagPrefix="uc1" TagName="DatosFlujoSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function InicioSolicitudTasaciones() {
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
                alert(e.message);
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
                alert("22222" + e.Message);
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row navbar-fixed-top" style="background-color: #ffffff">

        <div class="col-lg-8 col-md-8 col-sm-12">

            <ul class="nav small nav-tabs">
                <li class=""><a href="#Observaciones" data-toggle="tab" aria-expanded="false">Observaciones</a></li>
                <li class=""><a id="TabFlujo" href="#Flujo" runat="server" data-toggle="tab" aria-expanded="false">Resumen Flujo</a></li>
                <li class="active"><a href="#DatosTasacion" data-toggle="tab" aria-expanded="true">Informe de la Tasación</a></li>
            </ul>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-12">

            <div class='form-group'>
                <div class="col-lg-7 col-md-7 col-sm-12">
                    <anthem:DropDownList ID="ddlAccionEvento" runat="server" CssClass="form-control" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="ddlAccionEvento_SelectedIndexChanged"></anthem:DropDownList>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12">
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
        <div class="tab-pane fade" id="Observaciones">
            <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
        </div>
        <div class="tab-pane fade" id="Flujo">
            <uc1:DatosFlujoSolicitud runat="server" ID="DatosFlujoSolicitud" />
        </div>
        <div class="tab-pane fade active in" id="DatosTasacion">
            <div class="row">

                <div class="col-lg-3">
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Informe de Tasación</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="fileDocSolicitud">Archivo:</label>
                                    <anthem:FileUpload ID="fileDocSolicitud" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" AllowMultiple="True" />
                                </div>
                                 <div class="col-md-4">
                                        <label for="txtFechaEmisionDocSolicitud">Fecha de Emisión:</label>
                                        <anthem:TextBox CssClass="form-control" AutoUpdateAfterCallBack="true" ID="txtFechaEmisionDocSolicitud" onblur="esFechaValida(this);" runat="server"></anthem:TextBox>
                                    </div>
                                <div class="col-md-4">
                                    <anthem:Button AutoUpdateAfterCallBack="true" ID="btnGuardarDocSolicitud" EnableCallBack="false" runat="server" CssClass="btn btn-primary" Text="Guardar"
                                        OnClick="btnGuardarDocSolicitud_Click" TextDuringCallBack="Guardando..." OnMouseDown="javascript:ValidarTamaño(this,0);" />
                                </div>
                                <anthem:HiddenField ID="hfIdArchivoSolicitud" runat="server" AutoUpdateAfterCallBack="true" />
                            </div>
                            <hr />
                            <div class="row">
                                <anthem:GridView ID="gvDocumentosSolicitud" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed"
                                    AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="99%"
                                    CellPadding="0" UpdateAfterCallBack="True" OnRowDataBound="gvDocumentosSolicitud_RowDataBound">
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
                <div class="col-lg-3">
                </div>


            </div>

        </div>
    </div>
</asp:Content>
