<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosFlujoSolicitud.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosFlujoSolicitud" %>

<script>
    document.addEventListener('DOMContentLoaded', function () {
        var data = document.getElementsByName('flujos');
        for (var x = 0; x < data.length; x += 1) {
            const ele1 = data[x];
            ele1.style.cursor = 'grab';
            let pos1 = { top: 0, left: 0, x: 0, y: 0 };

            const mouseDownHandler1 = function (e) {
                ele1.style.cursor = 'grabbing';
                ele1.style.userSelect = 'none';

                pos1 = {
                    left: ele1.scrollLeft,
                    top: ele1.scrollTop,
                    // Get the current mouse position
                    x: e.clientX,
                    y: e.clientY,
                };

                document.addEventListener('mousemove', mouseMoveHandler1);
                document.addEventListener('mouseup', mouseUpHandler1);
            };
            const mouseMoveHandler1 = function (e) {
                // How far the mouse has been moved
                const dx1 = e.clientX - pos1.x;
                const dy1 = e.clientY - pos1.y;

                // Scroll the element
                ele1.scrollTop = pos1.top - dy1;
                ele1.scrollLeft = pos1.left - dx1;
            };
            const mouseUpHandler1 = function () {
                ele1.style.cursor = 'grab';
                ele1.style.removeProperty('user-select');

                document.removeEventListener('mousemove', mouseMoveHandler1);
                document.removeEventListener('mouseup', mouseUpHandler1);
            };
            ele1.addEventListener('mousedown', mouseDownHandler1);
        }
    });
</script>

<div class="panel panel-primary" >
    <div class="panel-heading">
        <h3 class="panel-title">Flujo Solicitud 
        </h3>
    </div>
    <div class="panel-body"  style="-webkit-touch-callout: none; -webkit-user-select: none; -khtml-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none;">
        
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEventos" style="margin-right: 15px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        
       <%-- <div id="Container2" runat="server" class="containerFlows">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEventos2" style="margin-right: 15px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Container3" runat="server" class="containerFlows">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEventos3" style="margin-right: 15px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Container4" runat="server" class="containerFlows">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEventos4" style="margin-right: 15px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Container5" runat="server" class="containerFlows">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="row small" runat="server" id="DivEventos5" style="margin-right: 15px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
--%>

</div>
</div>
