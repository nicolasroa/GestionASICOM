<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosInmobiliariaProyecto.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosInmobiliariaProyecto" %>
<div class="row">
    <div class="col-lg-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Inmobiliaria Proyectos
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ddlInmobiliaria">Inmobiliaria</label>
                        <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlProyecto">Proyecto</label>
                        <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"></anthem:DropDownList>
                    </div>



                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos de la Inmobiliaria
                </h3>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-warning">
                            <div class="panel-heading">
                                <h3 class="panel-title">Datos Principales</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label for="txtFormDescripcion" class="text-left">Descripción</label>
                                        <anthem:TextBox CssClass="form-control" ID="txtFormDescripcion" runat="server" TabIndex="7" Enabled="false"
                                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label for="txtFormRut" class="text-left">Rut</label>
                                        <anthem:TextBox CssClass="form-control" ID="txtFormRut" onchange="validaRut(this);" MaxLength="13" runat="server" TabIndex="12" Enabled="false"
                                            AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-warning">
                            <div class="panel-heading">
                                <h3 class="panel-title">Datos de Contacto</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class='form-group'>
                                            <label for="txtFormContacto" class="text-left">Nombre Contacto</label>
                                            <anthem:TextBox CssClass="form-control" ID="txtFormContacto" runat="server" TabIndex="7"
                                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">x
                                        <div class='form-group'>
                                            <label for="txtFormCargoContacto" class="text-left">Cargo</label>
                                            <anthem:TextBox CssClass="form-control" ID="txtFormCargoContacto" runat="server" TabIndex="7"
                                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class='form-group'>
                                            <label for="txtFormMailContacto" class="text-left">Mail</label>
                                            <anthem:TextBox CssClass="form-control" ID="txtFormMailContacto" runat="server" TabIndex="7" placeholder="name@example.com" onBlur="validarEmail(this)"
                                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class='form-group'>
                                            <label for="txtFormFonoFijoContacto" class="text-left">Teléfono Fijo</label>
                                            <anthem:TextBox CssClass="form-control" ID="txtFormFonoFijoContacto" runat="server" TabIndex="7" onBlur="ValidaFono(this)" placeholder="+56212345678"
                                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class='form-group'>
                                            <label for="txtFormCelularContacto" class="text-left">Celular</label>
                                            <anthem:TextBox CssClass="form-control" ID="txtFormCelularContacto" runat="server" TabIndex="7" onBlur="ValidaFono(this)" placeholder="+56912345678"
                                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
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
    <div class="col-lg-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos del Proyecto
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class='form-group'>
                            <label for="txtFormDescripcionProyecto" class="text-left">Nombre Proyecto</label>
                            <anthem:TextBox CssClass="form-control" ID="txtFormDescripcionProyecto" runat="server" TabIndex="5" Enabled="false"
                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class='form-group'>
                            <label for="ddlFormRegion" class="text-left">Región</label>
                            <anthem:DropDownList CssClass="form-control" ID="ddlFormRegion" TabIndex="7" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormRegion_SelectedIndexChanged" Enabled="false">
                            </anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class='form-group'>
                            <label for="ddlFormProvincia" class="text-left">Provincia</label>
                            <anthem:DropDownList CssClass="form-control" ID="ddlFormProvincia" TabIndex="8" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormProvincia_SelectedIndexChanged" Enabled="false">
                            </anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class='form-group'>
                            <label for="ddlFormComuna" class="text-left">Comuna</label>
                            <anthem:DropDownList CssClass="form-control" ID="ddlFormComuna" TabIndex="9" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="True" OnSelectedIndexChanged="ddlFormComuna_SelectedIndexChanged">
                            </anthem:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class='form-group'>
                            <label for="ddlFormComunaSII" class="text-left">Comuna Sii</label>
                            <anthem:DropDownList CssClass="form-control" ID="ddlFormComunaSII" TabIndex="7" runat="server" AutoUpdateAfterCallBack="true">
                            </anthem:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class='form-group'>
                            <label for="txtFormRolMatriz" class="text-left">Rol Matriz</label>
                            <anthem:TextBox CssClass="form-control" ID="txtFormRolMatriz" runat="server" TabIndex="9"
                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class='form-group'>
                            <label for="ddlFormTipoInmueble" class="control-label">Tipo Inmueble</label>
                            <anthem:DropDownList ID="ddlFormTipoInmueble" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>
                        </div>
                    </div>


                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class='form-group'>
                            <label for="txtFormDireccion" class="text-left">Dirección de Proyecto</label>
                            <anthem:TextBox CssClass="form-control" ID="txtFormDireccion" runat="server" TabIndex="9" Enabled="false"
                                AutoUpdateAfterCallBack="true"></anthem:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
