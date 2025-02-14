<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="CertificadoAprobacion.aspx.cs" Inherits="WorkFlow.Reportes.PDF.CertificadoAprobacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="position: relative; width: 100%; height: 100%; margin: 0 auto;" >
        <div class="ParrafoLegal" style="width: 80%; margin-right: auto; margin-left: auto; margin-top:100px;">
            <p style="text-align: right;"><%=DateTime.Now.ToLongDateString()%></p>
            <br />
            <p style="text-align: center;"><strong>CERTIFICADO DE APROBACIÓN DE CRÉDITO HIPOTECARIO</strong></p>

            <p>Santiago,</p>

            <p>Señor(a)(ita)</p>

            <p>
                <anthem:Label ID=lblNombreCliente runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
            </p>

            <p>
                Por la presente le informamos que su solicitud de crédito para
            <anthem:Label ID=lblDestinoCredito runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                con garantía hipotecaria sobre la propiedad ubicada en
            <anthem:Label ID=lblDireccionPropiedad runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>,  ha sido aprobada por U.F.
            <anthem:Label ID=lblMontoCredito runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                a un plazo de
            <anthem:Label ID=lblPlazoCredito runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                años,
            y a una tasa de interés anual de
            <anthem:Label ID=lblTasaInteresAnual runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                %.
            </p>

            <p>Esta aprobación está sujeta a las siguientes condiciones:</p>

            <div class="ParrafoLegalSangria">
                <p>1) Que el monto del crédito solicitado no sea superior al 80% de la tasación practicada por profesionales designados por Nuevo Capital.</p>

                <p>2) Que los títulos de la propiedad sean aprobados y estimados conforme a derecho por profesionales designados por Nuevo Capital.</p>

                <p>3) Que la respectiva compañía aseguradora apruebe el seguro de desagravamen del deudor, y de los codeudores, en su caso.</p>

                <p>4) Mantener las mismas condiciones de renta, patrimonio y deudas acreditadas con ocasión de la presente aprobación</p>

                <p>
                    5) Que usted, y sus codeudores, en su caso, no tengan protestos o morosidades vigentes o deudas vencidas en las bases de datos personales de acceso público,
            tales como DICOM, Cámara de Comercio de Santiago y Sistema Financiero, desde esta fecha y hasta la materialización del contrato de compraventa.
                </p>
            </div>
            <p>
                Se deja constancia que la tasa de interés expresada en esta comunicación es la vigente a esta fecha. La tasa de interés definitiva será la que rija al momento de la firma de la escritura correspondiente.
            </p>

            <p>La presente aprobación de crédito tendrá una vigencia de 60 días a contar de esta fecha.</p>

            <p>Se extiende la presente constancia a petición del interesado.</p>

            <p>Sin otro particular, le saluda atentamente a usted.</p>

            <p>
                Nuevo Capital<br />
                p.p Nuevo Capital
            </p>
        </div>
    </div>
</asp:Content>
