<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="SolicitudTasacion.aspx.cs" Inherits="WorkFlow.Reportes.PDF.SolicitudTasacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 207px;
        }
        .auto-style2 {
            width: 265px;
        }
        .auto-style3 {
            width: 173px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br />
    <div class="ContenedorPDF">
        
        <div class="ContenedorPDFTitulo" style="text-align: center;">
            SOLICITUD Y ELECCIÓN TASADOR</div>
        <br />
        <div class="ContenedorPDFDatos" style="text-align: justify;">
            En
            <anthem:Label ID="lblFechaSolicitudTasacion" runat="server" AutoUpdateAfterCallBack="True" Font-Underline="True" UpdateAfterCallBack="True"></anthem:Label>
            .<br />
            Yo <anthem:Label ID="lblNombreSolicitante" runat="server" AutoUpdateAfterCallBack="True" Font-Underline="True" UpdateAfterCallBack="True"></anthem:Label>
            , Rut 
            <anthem:Label ID="lblRutSolicitante" runat="server" AutoUpdateAfterCallBack="True" Font-Underline="True" UpdateAfterCallBack="True"></anthem:Label>
            , declaro lo siguiente:<br />
            <br />
            A través del presente documento informo que 
            <anthem:Label ID="lblDatosMutuaria" runat="server" Font-Bold="True">Nuevo Capital Administradora de Mutuos Hipotecarios S.A. Rut: 77.005.401-K;</anthem:Label>
            en virtud de los establecido en el artículo Nº 3, Inciso Segundo Letra d) de la Ley de Protección al Consumidor Nº 19.496; me ha permitido elegir al tasador del bien ofrecido en garantía; presentando distintas alternativas para efectuar la tasación del bien raíz.</div>
        <br />
        <div class="ContenedorPDFDatos">
            DATOS DE LA PROPIEDAD A TASAR:
        </div>
        <div class="ContenedorPDFDatos">
           <div id="divPropiedades" runat="server" ></div>
            CONTACTO DE LA PROPIEDAD A VISITAR:<br />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1" style="background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;">Nombre</td>
                    <td colspan="3" style="border: thin solid #000000">
                        <anthem:Label ID="lblNombreContacto" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" style="background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;">E-Mail</td>
                    <td class="auto-style2" style="border: thin solid #000000">
                        <anthem:Label ID="lblMailContacto" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                    </td>
                    <td class="auto-style3" style="background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;">Teléfono</td>
                    <td style="border: thin solid #000000">
                        <anthem:Label ID="lblTelefonoContacto" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                    </td>
                </tr>
            </table>
            <br />
            TASADOR A ASIGNAR (Marcar con una X)<br />
            <table style="width:100%;">
                <tr>
                    <td style="text-align: center; font-size: 16px; border: thin solid #000000">
                        <div style="align-content:center; width:100%;">
                        <anthem:CheckBoxList ID="chkEmpresasTasacion" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="871px" Font-Bold="False" Font-Size="X-Large">
                            
                        </anthem:CheckBoxList>
                            </div>
                    </td>
                </tr>
            </table>
            <br />
            COSTO DE LA TASACIÓN:<br />
            El costo por la tasación es de
            <anthem:Label ID="lblValorTasacion" runat="server" AutoUpdateAfterCallBack="True" Font-Bold="True" UpdateAfterCallBack="True"></anthem:Label>
            , este costo podrá aumentar para propiedades ubicadas fuera del radio urbano o en comunas de difícul acceso, y valor comercial mayor a
            <anthem:Label ID="lblValorMaximoPropiedad" runat="server" AutoUpdateAfterCallBack="True" Font-Bold="True" UpdateAfterCallBack="True">UF 10.000</anthem:Label>
            . El cliente será informado oportunamente del mayor costo previo a la tasación.<br />




        </div>
        
        <br />
        <br />
        <br />
        
        <br />
        <br />

        <div class="ContenedorPDFDatos" style="text-align: right;">
            Firma: ___________________________
        </div>




    </div>
</asp:Content>
