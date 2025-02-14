<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WorkFlow.Login" ValidateRequest="true" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type  X-Content-Type-Options: nosniff" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Last-Modified" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title><%: Page.Title %></title>

    <link href="~/Content/Blitzer/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/customized-clients.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/tipped.css" rel="stylesheet" />

</head>
<body>
    <form id="frmLogin" method="post" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>

                <asp:ScriptReference Path="Scripts/jquery-3.3.1.js" />
                <asp:ScriptReference Path="Scripts/jquery-ui.js" />
                <asp:ScriptReference Path="Scripts/bootstrap.js" />
                <asp:ScriptReference Path="Scripts/jquery.dataTables.min.js" />
                <asp:ScriptReference Path="Scripts/dataTables.bootstrap.min.js" />
                <asp:ScriptReference Path="Scripts/dataTables.responsive.min.js" />
                <asp:ScriptReference Path="Scripts/responsive.bootstrap.min.js" />
                <asp:ScriptReference Path="Scripts/jquery.dataTables.min.js" />
                <asp:ScriptReference Path="Scripts/Controles.js" />
                <asp:ScriptReference Path="~/Scripts/excanvas.js" />

                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <p>
                    &nbsp;
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-5 col-xl-5 col-md-5 col-sm-5 col-xs-12">
                <div class="SubTituloLogin">GESTOR CORPORATIVO</div>
                <div class="TituloLogin">
                    <anthem:Image ID="imgLogoCliente" runat="server" ImageUrl="~/Img/LogoCliente.png" Width="60%" />
                       </div>
                <div class="SubTitulo2Login" >
                    <anthem:Label ID="lblAmbiente" AutoUpdateAfterCallBack="true" visible="false" runat="server"></anthem:Label></div>

            </div>
            <div class="col-lg-7 col-xl-7 col-md-7 col-sm-7 col-xs-12">
                <div class="CirculoLogin">
                    <div class="Texto1Login">INICIAR SESIÓN</div>
                    <div class="row text-center">
                        <div>
                        <anthem:TextBox class="form-control-login" ID="txtUsuario" runat="server" placeholder="Usuario" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                        </div>
                        <div style="margin-top:5px;">
                        <anthem:TextBox class="form-control-login" ID="txtClave" runat="server" placeholder="Contraseña" TextMode="Password" AutoCompleteType="Disabled"></anthem:TextBox>
                            </div>
                    </div>
                    <div class="row text-center" style="margin-top:20px;">
                        <anthem:Button ID="btnValidar" class="btn-Login" Text="Ingresar" runat="server" OnClick="btnValidar_Click" />
                    </div>
                    <div class="Texto2Login text-center"><a class="underlineHover" style="color:#fff" href="#">Olvidé mi Contraseña</a></div>
                    
                </div>
            </div>
            </div>
        <div class="modal fade" id="MensajesModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div id="TipoMensaje">
                    <h4 id="Titulo"></h4>
                    <p id="Texto">
                    </p>
                </div>
            </div>
        </div>
        <div style="display: none">
            <div id="divModalPopup1" title="VentanaModal">
            </div>
            <div id="divModalPopup2" title="VentanaModal">
            </div>
            <div id="divModalPopup3" title="VentanaModal">
            </div>
            <div id="divMensajes">
                <div>
                    <label id="lbltextoMensaje">
                    </label>
                </div>
            </div>
            <div id="divConfirmacion">
                <label id="lbltextoConfirmacion">
                </label>
            </div>
            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                EnabledDuringCallBack="False" ID="btnCerrarPopup" runat="server" Text="btnCerrar"
                CssClass="CssCerrarPopup" />
            <anthem:HiddenField ID="hdnIsPostBack" runat="server" AutoUpdateAfterCallBack="true" />
        </div>

    </form>
    
</body>


</html>
