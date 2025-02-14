<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosParticipante.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosParticipante" %>

<script>
    function DesplegarDatosRelacionados(Visible) {
        if (Visible == "true") {
            $('#divRelacionados').show(0);
        } else {
            $('#divRelacionados').hide(0);
        }
    }

    function DesplegarDatosLaborales(visible) {
        if (visible == "true") {
            $('#TabDatosLaborales').show();
        }
        else {
            $('#TabDatosLaborales').hide();
        }
    }
    function DesplegarDatosPersonales(visible) {
        if (visible == "true") {
            $('#DivDatosPersonales').show();
        }
        else {
            $('#DivDatosPersonales').hide();
        }
    }
    function InicioParticipante() {
        try {



            $("#<%= txtFechaNacimiento.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                constrainInput: true //La entrada debe cumplir con el formato
            });
            $("#<%= txtFechaInicioContrato.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 3,
                constrainInput: true, //La entrada debe cumplir con el formato
                onClose: function (selectedDate) {
                    if ($("#<%= txtFechaInicioContrato.ClientID %>").value != "") {
                        $("#<%= txtFechaTerminoContrato.ClientID %>").datepicker("option", "minDate", selectedDate);
                    }
                }
            });
            $("#<%= txtFechaTerminoContrato.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                maxDate: 0,
                changeYear: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    if ($("#<%= txtFechaTerminoContrato.ClientID %>").value != "")
                        $("#<%= txtFechaInicioContrato.ClientID %>").datepicker("option", "maxDate", selectedDate);
                }
            });

            $("#<%= txtFechaPersoneria.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                constrainInput: true //La entrada debe cumplir con el formato
            });

            $('#<%=ddlProfesion.ClientID%>').selectpicker();
            $('#<%=ddlProfesionRelacionado.ClientID%>').selectpicker();




        } catch (e) {
            alert(e.message);
        }
    }
    function ActivaDrop() {
        $('#<%=ddlProfesion.ClientID%>').selectpicker();
        $('#<%=ddlProfesionRelacionado.ClientID%>').selectpicker();
    }


    $(function () {
        $("#accordionPart").accordion();
    });
    function MostrarBusquedaPart() {
        getElement('lnkBusquedaPart').click();
        getElement('hFormularioPart').style.display = "none";
        getElement('hBusquedaPart').style.display = "block";
    }
    function MostrarFormularioPart() {

        getElement('lnkFormularioPart').click();
        getElement('hFormularioPart').style.display = "block";
        getElement('hBusquedaPart').style.display = "none";
    }
</script>

<div id="accordionPart">
    <h3 id="hBusquedaPart" class="panel-title">
        <a id="lnkBusquedaPart" href="#">Participantes de la Solicitud</a>
    </h3>
    <div>
        <div class="row">
            <div class="col-lg-12">
                <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="ImageButton2" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Participante"
                    OnClick="btnNuevoRegistro_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                    PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                    AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="DescripcionTipoParticipante" HeaderText="Participación" />
                        <asp:BoundField DataField="RutCompleto" HeaderText="Rut" />
                        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Completo" />
                        <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                    ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <anthem:ImageButton ID="btnEliminarParticipante" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                    ToolTip="Eliminar Participante" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarParticipante_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </anthem:GridView>
            </div>
        </div>

    </div>
    <h2 id="hFormularioPart" style="display: none;">
        <a id="lnkFormularioPart" href="#">Formulario</a></h2>
    <div>
        <div class="row">
            <div class="col-lg-6">
                <ul class="nav small nav-tabs">
                    <li class="active"><a href="#DatosPersonales" data-toggle="tab" aria-expanded="true">Datos Personales</a></li>
                    <li class=""><a href="#DatosGenerales" data-toggle="tab" aria-expanded="false">Datos Generales</a></li>
                    <li class=""><a href="#DatosLaborales" id="TabDatosLaborales" data-toggle="tab" aria-expanded="false">Datos Laborales</a></li>
                </ul>
            </div>
            <div class="col-lg-6 text-center">
                <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="btn-sm btn-primary" Text="Confirmar Ingreso de Participante"
                    OnClick="btnGuardar_Click" TextDuringCallBack="Guardando..." />
                &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar Ingreso de Participante" CssClass="btn-sm btn-primary"
                    OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." AutoUpdateAfterCallBack="true" />
            </div>
        </div>

        <div class="row">
            <div id="tabPersona" class="tab-content">
                <div class="tab-pane fade active in" id="DatosPersonales">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Datos Personales</h3>
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class='form-group'>
                                            <label for="txtRutCliente" class="control-label">Rut Participante</label>
                                            <anthem:TextBox ID="txtRutCliente" runat="server" onchange="validaRut(this);" MaxLength="13" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-1">
                                        <div class='form-group'>
                                            <label for="btnValidarRutCliente" class="control-label">&nbsp;</label>
                                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnValidarRutCliente" runat="server" CssClass="btn-sm btn-success" Text="Validar"
                                                OnClick="btnValidarRutCliente_Click" TextDuringCallBack="Buscando Cliente..." />
                                        </div>
                                    </div>
                                </div>
                                <div id="DivDatosPersonales" hidden="hidden">

                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Información Obligatoria</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="ddlTipoPersona">Tipo Persona</label>
                                                        <anthem:DropDownList ID="ddlTipoPersona" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtNombre">
                                                            <anthem:Label ID="lblNombreRazonSocial" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></label>
                                                        <anthem:TextBox ID="txtNombre" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtPaterno">
                                                            <anthem:Label ID="lblPaternoGiro" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label></label>
                                                        <anthem:TextBox ID="txtPaterno" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class='form-group'>
                                                        <label for="txtMaterno">Apellido Materno</label>
                                                        <anthem:TextBox ID="txtMaterno" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Información Opcional</h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlEstadoCivil">Estado Civil</label>
                                                        <anthem:DropDownList ID="ddlEstadoCivil" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlRegimenMatrimonial">Régimen Matrimonial</label>
                                                        <anthem:DropDownList ID="ddlRegimenMatrimonial" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlRegimenMatrimonial_SelectedIndexChanged"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlNacionalidad">Nacionalidad</label>
                                                        <anthem:DropDownList ID="ddlNacionalidad" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlSexo">Sexo</label>
                                                        <anthem:DropDownList ID="ddlSexo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlNivelEducacional">Nivel Educacional</label>
                                                        <anthem:DropDownList ID="ddlNivelEducacional" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlProfesion">Profesión</label>
                                                        <anthem:DropDownList ID="ddlProfesion" runat="server" class="form-control selectpicker" data-live-search="true" data-size="5" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="ddlResidencia">Residencia</label>
                                                        <anthem:DropDownList ID="ddlResidencia" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <anthem:Label ID="lblFechaNacimiento" for="txtFechaNacimiento" AutoUpdateAfterCallBack="true" class="labelForm" Text="Fecha de Nacimiento" runat="server"></anthem:Label>
                                                        <anthem:TextBox ID="txtFechaNacimiento" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="txtNumeroHijos">N° de Hijos</label>
                                                        <anthem:TextBox ID="txtNumeroHijos" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class='form-group'>
                                                        <label for="txtCargasFamiliares">Cargas Familiares</label>
                                                        <anthem:TextBox ID="txtCargasFamiliares" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="row" id="divRelacionados">
                                        <div class="col-lg-12">
                                            <div class="panel panel-success">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Relacionados</h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-lg-6">
                                                            <div class="row">
                                                                <div class="col-lg-4">
                                                                    <div class='form-group'>
                                                                        <label for="ddlTipoRelacion">Tipo</label>
                                                                        <anthem:DropDownList ID="ddlTipoRelacion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <div class='form-group'>
                                                                        <label for="txtRutClienteRelacionado" class="control-label">Rut Relacionado</label>
                                                                        <anthem:TextBox ID="txtRutClienteRelacionado" runat="server" onchange="validaRut(this);" MaxLength="13" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <div class='form-group'>
                                                                        <label for="btnValidarRutRelacionado" class="control-label">&nbsp;</label>
                                                                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnValidarRutRelacionado" runat="server" CssClass="btn-sm btn-success" Text="Validar"
                                                                            OnClick="btnValidarRutRelacionado_Click" TextDuringCallBack="Buscando Cliente..." />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">

                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="txtNombreRelacionado">Nombre</label>
                                                                        <anthem:TextBox ID="txtNombreRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="txtPaternoRelacionado">Apellido Paterno</label>
                                                                        <anthem:TextBox ID="txtPaternoRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="txtMaternoRelacionado">Apellido Materno</label>
                                                                        <anthem:TextBox ID="txtMaternoRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="ddlEstadoCivilRelacionado">Estado Civil</label>
                                                                        <anthem:DropDownList ID="ddlEstadoCivilRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="ddlRegimenMatrimonialRelacionado">Régimen Matrimonial</label>
                                                                        <anthem:DropDownList ID="ddlRegimenMatrimonialRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="ddlNacionalidadRelacionado">Nacionalidad</label>
                                                                        <anthem:DropDownList ID="ddlNacionalidadRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="ddlResidenciaRelacionado">Residencia</label>
                                                                        <anthem:DropDownList ID="ddlResidenciaRelacionado" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class='form-group'>
                                                                        <label for="ddlProfesionRelacionado">Profesión</label>
                                                                        <anthem:DropDownList ID="ddlProfesionRelacionado" runat="server" class="form-control selectpicker" data-live-search="true" data-size="5" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class='form-group'>
                                                                        <label for="ddlNotariaPersoneria">Notaría Personeria</label>
                                                                        <anthem:DropDownList ID="ddlNotariaPersoneria" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <div class='form-group'>
                                                                        <label for="txtFechaPersoneria">Fecha Personería</label>
                                                                        <anthem:TextBox ID="txtFechaPersoneria" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row text-center">
                                                                <div class="col-md-12">
                                                                    <anthem:LinkButton ID="btnGrabarRelacionado" class="btn-sm btn-success" AutoUpdateAfterCallBack="true" runat="server" OnClick="btnGrabarRelacionado_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Relacionado</anthem:LinkButton>
                                                                    <anthem:LinkButton ID="btnCancelarRelacionado" class="btn-sm btn-warning" AutoUpdateAfterCallBack="true" runat="server" OnClick="btnCancelarRelacionado_Click"><span class="glyphicon glyphicon-trash"></span> Cancelar</anthem:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-6">
                                                            <anthem:GridView runat="server" ID="gvRelacionados" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                                                                PageSize="10" AllowPaging="True" AllowSorting="True"
                                                                AutoUpdateAfterCallBack="true">
                                                                <RowStyle CssClass="GridItem" />
                                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="DescripcionTipoRelacion" HeaderText="Relación" />
                                                                    <asp:BoundField DataField="NombreClienteRelacionado" HeaderText="Nombre" />

                                                                    <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnModificarRelacionado" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                                                ToolTip="Modificar" OnClick="btnModificarRelacionado_Click" TextDuringCallBack="Buscando Registro..." />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnEliminarRelacionado" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                                                ToolTip="Eliminar Relacionado" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarRelacionado_Click" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </anthem:GridView>
                                                        </div>



                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Datos de Contacto</h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtMail">Email</label>
                                                                <anthem:TextBox ID="txtMail" runat="server" class="form-control" placeholder="name@example.com" onBlur="validarEmail(this)" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtCelular">Celular</label>
                                                                <anthem:TextBox ID="txtCelular" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtFono">Fono</label>
                                                                <anthem:TextBox ID="txtFono" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-lg-12">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Registro de Direcciones</h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <label for="ddlRegion">Tipo</label>
                                                            <anthem:DropDownList ID="ddlTipoDireccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <label for="ddlRegion">Región</label>
                                                            <anthem:DropDownList ID="ddlRegion" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></anthem:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <label for="ddlProvincia">Ciudad/Provincia</label>
                                                            <anthem:DropDownList ID="ddlProvincia" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></anthem:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <label for="ddlComuna">Comuna</label>
                                                            <anthem:DropDownList ID="ddlComuna" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <div class='form-group'>
                                                                <label for="txtDireccion">Dirección (Calle)</label>
                                                                <anthem:TextBox ID="txtDireccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <div class='form-group'>
                                                                <label for="txtNumeroDireccion">Número</label>
                                                                <anthem:TextBox ID="txtNumeroDireccion" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3">
                                                            <div class='form-group'>
                                                                <label for="txtNroDpto">Dpto</label>
                                                                <anthem:TextBox ID="txtNroDpto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row text-center">
                                                        <div class="col-md-12">
                                                            <anthem:LinkButton ID="btnGrabarDireccion" class="btn-sm btn-success" AutoUpdateAfterCallBack="true" runat="server" OnClick="btnGrabarDireccion_Click"><span class="glyphicon glyphicon-floppy-save"></span> Grabar Dirección</anthem:LinkButton>
                                                            <anthem:LinkButton ID="btnCancelarDireccion" class="btn-sm btn-warning" AutoUpdateAfterCallBack="true" runat="server" OnClick="btnCancelarDireccion_Click"><span class="glyphicon glyphicon-trash"></span> Cancelar Ingreso de Dirección</anthem:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">

                                                            <anthem:GridView runat="server" ID="gvDirecciones" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                                                                AutoUpdateAfterCallBack="true">
                                                                <RowStyle CssClass="GridItem" />
                                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="DescripcionTipoDireccion" HeaderText="Tipo" />
                                                                    <asp:BoundField DataField="DireccionCompleta" HeaderText="Dirección" />
                                                                    <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnModificarDireccion" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                                                ToolTip="Modificar Dirección" OnClick="btnModificarDireccion_Click" TextDuringCallBack="Buscando Registro..." />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnEliminarDireccion" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                                                ToolTip="Eliminar Direccion" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarDireccion_Click" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </anthem:GridView>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 text-center">
                                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarCliente" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
                                                OnClick="btnGuardarCliente_Click" TextDuringCallBack="Guardando..." />
                                            &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarCliente" runat="server" Text="Cancelar" CssClass="btn-success btn-sm"
                                                OnClick="btnCancelarCliente_Click" TextDuringCallBack="Cancelando..." />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="DatosGenerales">
                    <div class="row">

                        <div class="col-lg-4">
                            <div class='form-group'>
                                <label for="ddlTipoParticipacion">Tipo Participación</label>
                                <anthem:DropDownList ID="ddlTipoParticipacion" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoParticipacion_SelectedIndexChanged"></anthem:DropDownList>
                            </div>
                            <div class='form-group'>
                                <label for="txtPorcentajeDeuda">% de Deuda</label>
                                <anthem:TextBox ID="txtPorcentajeDeuda" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                            </div>
                            <div class='form-group'>
                                <label for="txtPorcentajeDominio">% de Dominio</label>
                                <anthem:TextBox ID="txtPorcentajeDominio" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                            </div>
                            <div class='form-group'>
                                <label for="txtPorcentajeDesgravamen">% de Desgravamen</label>
                                <anthem:TextBox ID="txtPorcentajeDesgravamen" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,2,this);" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnTextChanged="txtPorcentajeDesgravamen_TextChanged"></anthem:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguro de Desgravamen</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="ddlSeguroDesgravamen">Póliza</label>
                                        <anthem:DropDownList ID="ddlSeguroDesgravamen" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroDesgravamen_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTasaSeguroDesgravamen">Tasa Seguro</label>
                                        <anthem:TextBox ID="txtTasaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtPrimaSeguroDesgravamen">Prima Mensual Seguro</label>
                                        <anthem:TextBox ID="txtPrimaSeguroDesgravamen" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Seguro de Cesantía</h3>
                                </div>
                                <div class="panel-body">
                                    <div class='form-group'>
                                        <label for="ddlSeguroCesantia">Póliza</label>
                                        <anthem:DropDownList ID="ddlSeguroCesantia" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlSeguroCesantia_SelectedIndexChanged"></anthem:DropDownList>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtTasaSeguroCesantia">Tasa Seguro</label>
                                        <anthem:TextBox ID="txtTasaSeguroCesantia" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                    <div class='form-group'>
                                        <label for="txtPrimaSeguroCesantia">Prima Mensual Seguro</label>
                                        <anthem:TextBox ID="txtPrimaSeguroCesantia" runat="server" class="form-control" disabled="true" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>




                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGuardarDatosGenerales" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
                                OnClick="btnGuardarDatosGenerales_Click" TextDuringCallBack="Guardando..." />
                            &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDatosGenerales" runat="server" Text="Cancelar" CssClass="btn-success btn-sm"
                                OnClick="btnCancelarDatosGenerales_Click" TextDuringCallBack="Cancelando..." />
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="DatosLaborales">
                    <div class="row">

                        <div class="col-lg-12">
                            <anthem:Button PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnNuevoDatoLaboral" CssClass="btn-success btn-sm" runat="server" Text="Nuevo Dato Laboral"
                                OnClick="btnNuevoDatoLaboral_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">

                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Datos Obligatorios</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="ddlTipoActividad">Tipo Actividad</label>
                                                <anthem:DropDownList ID="ddlTipoActividad" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="ddlSituacionLaboral">Situación Laboral</label>
                                                <anthem:DropDownList ID="ddlSituacionLaboral" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Datos Opcionales</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="txtFechaInicioContrato">Fecha Inicio Contrato/Actividad</label>
                                                <anthem:TextBox ID="txtFechaInicioContrato" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="txtFechaTerminoContrato">Fecha Término Contrato/Actividad</label>
                                                <anthem:TextBox ID="txtFechaTerminoContrato" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="txtRutEmpleador">Rut Empleador</label>
                                                <anthem:TextBox ID="txtRutEmpleador" runat="server" onchange="validaRut(this);" MaxLength="13" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="txtEmpleador">Empleador</label>
                                                <anthem:TextBox ID="txtEmpleador" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class='form-group'>
                                                <label for="txtCargo">Cargo</label>
                                                <anthem:TextBox ID="txtCargo" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Dirección Laboral</h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <label for="ddlRegionLab">Región</label>
                                                            <anthem:DropDownList ID="ddlRegionLab" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlRegionLab_SelectedIndexChanged"></anthem:DropDownList>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <label for="ddlProvinciaLab">Ciudad/Provincia</label>
                                                            <anthem:DropDownList ID="ddlProvinciaLab" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlProvinciaLab_SelectedIndexChanged"></anthem:DropDownList>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <label for="ddlComunaLab">Comuna</label>
                                                            <anthem:DropDownList ID="ddlComunaLab" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtDireccionLab">Dirección</label>
                                                                <anthem:TextBox ID="txtDireccionLab" runat="server" class="form-control" AutoUpdateAfterCallBack="True" Width="100%"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtMailLab">Email</label>
                                                                <anthem:TextBox ID="txtMailLab" runat="server" class="form-control" placeholder="name@example.com" onBlur="validarEmail(this)" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class='form-group'>
                                                                <label for="txtFonoContacto">Fono Contacto</label>
                                                                <anthem:TextBox ID="txtFonoContacto" runat="server" class="form-control" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnGrabarDatosLaborales" runat="server" CssClass="btn-success btn-sm" Text="Guardar"
                                OnClick="btnGuardarDatosLaborales_Click" TextDuringCallBack="Guardando..." />
                            &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnCancelarDatosLaborales" runat="server" Text="Cancelar" CssClass="btn-success btn-sm"
                                OnClick="btnCancelarDatosLaborales_Click" TextDuringCallBack="Cancelando..." />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <anthem:GridView runat="server" ID="gvDatosLaborales" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed"
                                PageSize="5" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvDatosLaborales_PageIndexChanging"
                                AutoUpdateAfterCallBack="true">
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                <Columns>
                                    <asp:BoundField DataField="NombreEmpleador" HeaderText="Empleador" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="DescripcionTipoActividad" HeaderText="Tipo de Actividad" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FechaInicioContrato" HeaderText="Inicio" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FechaTerminoContrato" HeaderText="Término" DataFormatString="{0:d}" NullDisplayText="A la Fecha" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnModificarDatoLaboral" runat="server" ImageUrl="~/Img/Grid/Editar_grid.png"
                                                ToolTip="Modificar" OnClick="btnModificarDatoLaboral_Click" TextDuringCallBack="Buscando Registro..." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnEliminarDatoLaboral" runat="server" ImageUrl="~/Img/Grid/Delete_grid.png"
                                                ToolTip="Eliminar Dato Laboral" TextDuringCallBack="Eliminando Registro..." OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea eliminar el registro?',this);" OnClick="btnEliminarDatoLaboral_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfCliente" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfLaborales" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfParticipante" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
</div>

