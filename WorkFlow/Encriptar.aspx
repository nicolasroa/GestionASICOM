<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encriptar.aspx.cs" Inherits="WorkFlow.Encriptar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>



        </div>
        <anthem:Label ID="lblUsuario" runat="server"></anthem:Label>
        <anthem:TextBox ID="txtUsuario" AutoUpdateAfterCallBack ="true" runat="server"></anthem:TextBox>
        <anthem:Label ID="lblClave" AutoUpdateAfterCallBack ="true"  runat="server"></anthem:Label>
        <anthem:TextBox ID="txtClave" runat="server"></anthem:TextBox>
        <anthem:Button ID="btnEncriptat" runat="server" OnClick="btnEncriptat_Click" Text="Desencriptar" />
        <anthem:Label ID="lblClaveDesencriptada" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
    </form>
</body>
</html>
