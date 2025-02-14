<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosResumen.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosResumen" %>
<%@ Register Src="~/DatosGenerales/DatosObservaciones.ascx" TagPrefix="uc1" TagName="DatosObservaciones" %>


<div class="row">
    <div class="col-xl-3 col-lg-3 col-md-6 col-sm-6 col-xs-6 small">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos de la Solicitud</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <label for="lblNroSolicitud">Nro Solicitud</label>
                    </div>
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <anthem:Label ID="lblNroSolicitud" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <label for="lblSucursal">Sucursal</label>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                        <anthem:Label ID="lblSucursal" runat="server" AutoUpdateAfterCallBack="True" Text=""></anthem:Label>
                    </div>
                    </div>
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <label for="lblEstado">Estado</label>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                        <anthem:Label ID="lblEstado" runat="server" AutoUpdateAfterCallBack="True" Text=""></anthem:Label>
                    </div>
                </div>
                    <div class="row">
                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                <label for="lblSubEstado">Sub-Estado</label>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                        <anthem:Label ID="lblSubEstado" runat="server" AutoUpdateAfterCallBack="True" Text=""></anthem:Label>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                <label for="lblAbogado">Abogado</label>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                        <anthem:Label ID="lblAbogado" runat="server" AutoUpdateAfterCallBack="True" Text=""></anthem:Label>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                                <label for="lblNotaria">Notaría</label>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                        <anthem:Label ID="lblNotaria" runat="server" AutoUpdateAfterCallBack="True" Text=""></anthem:Label>
                </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xl-3 col-lg-3 col-md-6 col-sm-6 col-xs-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Asignación</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12">
                        <anthem:GridView runat="server" ID="gvAsignacion" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="DescripcionRol" HeaderText="Rol" />
                                <asp:BoundField DataField="NombreResponsable" HeaderText="Responsable" />
                            </Columns>
                        </anthem:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xl-3 col-lg-3 col-md-6 col-sm-6 col-xs-6 small">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Datos del Solicitante</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <label for="lblRut">Rut</label>
                        </div>
                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <anthem:Label ID="lblRut" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                        </div>
                    </div>
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <label for="lblNombre">Nombre Cliente</label>
                         </div>
                        <div class="col-xl-6 col-lg-6 col-md-6 col-xs-6">
                            <anthem:Label ID="lblNombre" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                        </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xl-3 col-lg-3 col-md-6 col-sm-6 col-xs-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Inmobiliaria/Proyecto</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class='form-group'>
                            <label for="ddlInmobiliaria">Inmobiliaria</label>
                            <anthem:DropDownList ID="ddlInmobiliaria" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True" OnSelectedIndexChanged="ddlInmobiliaria_SelectedIndexChanged"></anthem:DropDownList>

                        </div>
                        <div class='form-group'>
                            <label for="ddlProyecto">Proyecto</label>
                            <anthem:DropDownList ID="ddlProyecto" runat="server" class="form-control" AutoUpdateAfterCallBack="True"></anthem:DropDownList>

                        </div>
                        <div class='form-group'>
                            <label for="ddlCooperativa">Instituciones</label>
                            <anthem:DropDownList ID="ddlCooperativa" runat="server" class="form-control" AutoUpdateAfterCallBack="True" AutoCallBack="True"></anthem:DropDownList>

                        </div>
                        <div class='form-group'>

                            <anthem:LinkButton ID="btnActualizarDatos" runat="server" class="btn btn-xs btn-success" OnClick="btnActualizarDatos_Click"><span class="glyphicon glyphicon-floppy-save"></span> Actualizar Información</anthem:LinkButton>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <uc1:DatosObservaciones runat="server" ID="DatosObservaciones" />
    </div>
</div>
