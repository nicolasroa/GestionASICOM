<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CentralHipotecariaWF.Error" %>

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
    <title>Error</title>

    <link href="~/Content/Blitzer/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/customized-clients.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/tipped.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true">
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
        <div class="jumbotron">
            <h1>Error</h1>
            <p>Ha ocurrido un Error inesperado en el sistema.</p>
            <p><a class="btn btn-primary btn-lg" href="Login.aspx">Volver a Login</a></p>
        </div>
    </form>
</body>
</html>
