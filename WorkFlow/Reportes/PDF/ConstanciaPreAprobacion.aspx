<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="ConstanciaPreAprobacion.aspx.cs" Inherits="WorkFlow.Reportes.PDF.ConstanciaPreAprobacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="ContenedorPDF">
        <div class="ContenedorPDFDatos" style="text-align: left;">
           
        </div>
        <br />
        <div class="ContenedorPDFDatos" style="text-align: right;">
            <anthem:Label ID="lblFechaCabecera" runat="server"></anthem:Label>
        </div>
        <br />
        <div class="ContenedorPDFTitulo" style="text-align: center;">
            CONSTANCIA DE
            <anthem:Label ID="lblTitulo" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
            &nbsp;
        </div>
        <br />
        <div class="ContenedorPDFDatos" style="font-weight: bold;">
            Señor(a)
        </div>
        <div class="auto-style2" style="font-weight: bold;">
        </div>
        <br />
        <div class="ContenedorPDFDatos" style="text-align: justify;">
            Nuevo Capital Hipotecario S.A., en adelante NCH, deja constancia que al Sr (a) 
                        <anthem:Label ID="lblCliente" runat="server"></anthem:Label>
            , cédula de identidad N °
                        <anthem:Label ID="lblRut" runat="server" Font-Underline="False"></anthem:Label>
            &nbsp;se le ha pre aprobado un Mutuo Hipotecario Endosable por
                        <anthem:Label ID="lblMontoCredito" runat="server" Font-Underline="False"></anthem:Label>
            &nbsp;pagadero en un plazo de 
                        <anthem:Label ID="lblPlazoSolicitado" runat="server" Font-Underline="False"></anthem:Label>
            &nbsp;para la compra de una propiedad 
                        <anthem:Label ID="lblDireccion" runat="server" Font-Underline="False"></anthem:Label>
            &nbsp;a un precio total de
                        <anthem:Label ID="lblValorPropiedad" runat="server"></anthem:Label>
            .&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <br />
        <div class="ContenedorPDFDatos">
            Esta pre - aprobación se encuentra sujeta a las siguientes condiciones suspensivas:
        </div>
        <div class="ContenedorPDFDatos">
            <br />




            <table style="width: 100%; border-collapse: collapse">
                <tr>

                    <td class="tdColumnaEnumeracion">a)</td>

                    <td class="ContenedorPDFCeldaSinBorde" style="width: 65%">Que la tasación comercial del inmueble efectuada por tasador elegido por el cliente entre las opciones presentadas por NCH sea al menos de
                        <anthem:Label ID="lblValorPropiedad2" runat="server"></anthem:Label>
            &nbsp;y que el destino del bien sea el aceptado por NCH.</td>
                </tr>
                <tr>

                    <td class="tdColumnaEnumeracion">b)</td>

                    <td class="ContenedorPDFCeldaSinBorde">Que el estudio de títulos se encuentre conforme a derecho a juicio del abogado que haya designado NCH.</td>
                </tr>
                <tr>

                    <td class="tdColumnaEnumeracion">c)</td>

                    <td class="ContenedorPDFCeldaSinBorde">Que no existan cambios en las condiciones económicas y financieras, como en los antecedentes entregados a NCH, tenidos todos en consideración para la Pre- Aprobación del señalado crédito.</td>
                </tr>
                <tr>

                    <td class="tdColumnaEnumeracion">d)</td>

                    <td class="ContenedorPDFCeldaSinBorde">Que la declaración personal de salud, se encuentre aprobada por la compañía de seguro respectiva y que se cumplan las demás condiciones requeridas para el otorgamiento de los seguros,</td>
                </tr>
                <tr id="tdCodeudor" runat="server">

                    <td class="tdColumnaEnumeracion">e)</td>

                    <td class="ContenedorPDFCeldaSinBorde">Que Don(ña) 
                        <anthem:Label ID="lblCodeudor" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                        . Cédula de identidad N °
                        <anthem:Label ID="lblRutCodeudor" runat="server"></anthem:Label>
                        &nbsp;se constituya como fiador y codeudor solidario del crédito que se cursará.</td>
                </tr>

            </table>
        </div>
        <br />
        <div class="ContenedorPDFDatos">
            En caso de no cumplirse cualquiera de las condiciones señaladas, NCH, podrá retractarse de aprobar y cursar la operación financiera sin ulterior responsabilidad.
             <br />
            <br />
            La tasa de emisión del crédito será fijada por NCH y aceptada por el cliente al momento de suscribirse la escritura pública que da cuenta del mutuo hipotecario, quedando en consecuencia condicionada la oferta a la aceptación de la señalada tasa.
             <br />
            <br />
            La pre - aprobación que da cuenta el presente instrumento tiene una vigencia de
            <anthem:Label ID="lblDiasValidez" runat="server"></anthem:Label>
            días contados desde esta fecha.
             <br />
            <br />
            Se extiende la presente constancia a solicitud del interesado, sin ulterior responsabilidad para Nuevo Capital Hipotecario S.A.
             <br />
            <br />
            Esta constancia no constituye una cotización, para efectos legales.
             <br />
            <br />
            <br />

            Sin otro particular, le saluda atentamente
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="ContenedorPDFDatos" style="text-align: right;">
            pp. Nuevo Capital Hipotecario S.A.
        </div>




    </div>
</asp:Content>