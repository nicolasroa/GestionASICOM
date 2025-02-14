<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosReparoRiesgo.ascx.cs" Inherits="WorkFlow.DatosGenerales.DatosReparoRiesgo" %>

<div class="row">
    <div class="col-md-12">
        <h6>
            <span class="titulo">
               <%-- <img src="../Img/icons/favicon_xsmall.png" />--%>Datos Resumen y Resolución
            </span>
            <span class="TextoDerecha">Valor UF al <%= DateTime.Now.ToLongDateString() %>: $
                <anthem:Label ID="lblValorUF" AutoUpdateAfterCallBack="true" runat="server"></anthem:Label>
                <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtValorUF" runat="server" CssClass="oculto" Width="1px"></anthem:TextBox>
            </span>
            <br>
        </h6>
    </div>
</div>


<div class="row form-horizontal well">
    <div class="col-md-12 small">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Sujeto A</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class='form-group'>
                        <div class="col-lg-12">
                            <anthem:Button ID=btnAgregaComentario runat="server" Text="Agregar Comentario" AutoUpdateAfterCallBack=True OnClick="btnAgregaComentario_Click" />
                        </div>
                         <div class="col-lg-12">
                             <anthem:TextBox ID=txtObservacion runat="server" AutoUpdateAfterCallBack=True></anthem:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <anthem:GridView runat="server" ID="gdvComentarios" AutoGenerateColumns="false" Width="100%"
                            PageSize="5" AllowSorting="True"
                            AutoUpdateAfterCallBack="true" CssClass="table table-condensed">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Comentario" />
                            </Columns>
                        </anthem:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
