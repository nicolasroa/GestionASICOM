<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosDocumentosEstudioTitulo.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosDocumentosEstudioTitulo" %>
<script>
    function DesplegarDatosEstudioTitulo(Visible) {
        if (Visible == "1") {
            $('#DatosEstudioTitulo').show(0);
        } else {
            $('#DatosEstudioTitulo').hide(0);
        }
    }


</script>


<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">Propiedades 
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <anthem:GridView runat="server" ID="gvPropiedadesEETT" AutoGenerateColumns="false" Width="100%"
                    PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvPropiedadesEETT_PageIndexChanging"
                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="DescripcionTipoInmueble" HeaderText="Tipo" />
                        <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                        <asp:BoundField DataField="NombreInstitucionALzamientoHipoteca" HeaderText="Institución" NullDisplayText="Sin Alzamiento" />
                        <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <anthem:ImageButton ID="btnSeleccionarPropEETT" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                    ToolTip="Modificar" OnClick="btnSeleccionarPropEETT_Click" TextDuringCallBack="Buscando Registro..." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </anthem:GridView>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="row" id="DatosEstudioTitulo">
                    <div class="col-md-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Datos Estudio de Título
                                    <anthem:Label ID="lblDireccionEETT" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label></h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <anthem:CheckBox ID="chkIndAlzamientoHipoteca" AutoUpdateAfterCallBack="true" Text="Incluye Alzamiento de Hipoteca" runat="server" AutoCallBack="True" OnCheckedChanged="chkIndAlzamientoHipoteca_CheckedChanged" />
                                    </div>
                                    <div class="col-md-3">
                                        <div class='form-group'>
                                            <label for="ddlInstitucionCartaResguardo">Institución Alzamiento Hipoteca</label>
                                            <anthem:DropDownList ID="ddlInstitucionAlzamientoHipoteca" class="form-control" Enabled="false" AutoUpdateAfterCallBack="true" runat="server"></anthem:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <anthem:LinkButton ID="btnGrabarAlzamiento" class="btn btn-primary" runat="server" TextDuringCallBack="Procesando" EnabledDuringCallBack="false" OnClick="btnGrabarAlzamiento_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Alzamiento</anthem:LinkButton>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="col-md-12">
                        <anthem:LinkButton ID="btnGrabarEstudioTitulo" class="btn btn-primary" runat="server" TextDuringCallBack="Procesando" EnabledDuringCallBack="false" OnClick="btnGrabarEstudioTitulo_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Documentos</anthem:LinkButton>

                        <anthem:LinkButton ID="btnCancelarEstudioTitulo" class="btn btn-primary" runat="server" OnClick="btnCancelarEstudioTitulo_Click"><span class="glyphicon glyphicon-trash"></span> Cancelar Ingreso Documentos</anthem:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Registro de Documentación 
                                </h3>
                            </div>
                            <div class="panel-body">
                                <anthem:GridView runat="server" ID="gvDocumentosEETT" AutoGenerateColumns="false" Width="100%"
                                    PageSize="5" AllowSorting="True"
                                    AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Validación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <anthem:RadioButtonList ID="rblEstadoDocumento" runat="server" AutoUpdateAfterCallBack="true" RepeatDirection="Horizontal"></anthem:RadioButtonList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DescripcionDocumento" HeaderText="Documento"></asp:BoundField>
                                    </Columns>
                                </anthem:GridView>
                            </div>
                        </div>
                    </div>

                </div>


            </div>
        </div>
    </div>
</div>
