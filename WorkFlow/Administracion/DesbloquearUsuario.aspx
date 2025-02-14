<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="DesbloquearUsuario.aspx.cs" Inherits="WebSite.Administracion.DesbloquearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row spacenavmax">
        <div class="col-md-12">
            <h4>
                <span class="titulo">
                    <%--<img src="../Img/icons/favicon_xsmall.png" />--%>Desbloqueo de Usuarios
                </span>
                <br>
            </h4>
            <%--<div style="width: 100%;">
                <img style="width: 100%; height: 10px" src="../Img/LineaAsicom.png" />
            </div>--%>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Usuarios Bloqueados</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6 col-md-6">
                            <anthem:DropDownList CssClass="form-control" AutoUpdateAfterCallBack="true" ID="ddlUsuarioBloqueado" runat="server">
                </anthem:DropDownList>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False" ID="btnDesbloquea" runat="server" CssClass="btn-sm btn-primary" TextDuringCallBack="Desbloqueando Usuario..."
                    Text="Desbloquear Usuario" OnClick="btnDesbloquea_Click" />
                        </div>
                        
                       
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
