<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="WorkFlow.Mantenedores.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function InicioCliente() {
            $("#<%= txtFechaNacimiento.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                constrainInput: true //La entrada debe cumplir con el formato
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnValidarRutCliente" runat="server" CssClass="btn-sm btn-success form-control2" Text="Validar"
                    OnClick="btnValidarRutCliente_Click" TextDuringCallBack="Buscando Cliente..." />
            </div>
        </div>
    </div>

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
    <div class="row">
        <div class="col-md-3">
            <div class='form-group'>
                <anthem:Label ID="lblFechaNacimiento" for="txtFechaNacimiento" AutoUpdateAfterCallBack="true" class="labelForm" Text="Fecha de Nacimiento" runat="server"></anthem:Label>
                <anthem:TextBox ID="txtFechaNacimiento" runat="server" class="form-control" onblur="esFechaValida(this);" AutoUpdateAfterCallBack="True"></anthem:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class='form-group'>
                <label for="txtEmail" class="text-left">Mail</label>
                <anthem:TextBox ID="txtEmail" runat="server" class="form-control input-sm" placeholder="name@example.com" onBlur="validarEmail(this)" AutoUpdateAfterCallBack="True" EnableCallBack="true"></anthem:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class='form-group'>
                <label for="txtCelular" class="text-left">Celular</label>
                <div class="row">

                    <div class="col-md-1">
                        <label for="txtCelular" class="text-left">+56</label>
                    </div>
                    <div class="col-md-8">
                        <anthem:TextBox ID="txtCelular" runat="server" class="form-control input-sm" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True" EnableCallBack="true"></anthem:TextBox>
                    </div>
                </div>

            </div>
        </div>
        <div class="col-md-3">
            <div class='form-group'>
                <label for="txtFono" class="text-left">Fijo</label>
                <anthem:TextBox ID="txtFono" runat="server" class="form-control input-sm" onKeyPress="return SoloNumeros(event,this.value,0,this);" AutoUpdateAfterCallBack="True" EnableCallBack="true"></anthem:TextBox>
            </div>
        </div>
    </div>
    <div class="modal-footer center">
        <anthem:Button ID="btnGrabarCliente" runat="server" CssClass="btn-sm btn-success" Text="Guardar" OnClick="btnGrabarCliente_Click" />
        <anthem:Button ID="btnCerrarCliente" runat="server" class="btn-sm btn-danger" data-dismiss="modal" Text="Cancelar" OnClick="btnCerrarCliente_Click" />
    </div>
</asp:Content>
