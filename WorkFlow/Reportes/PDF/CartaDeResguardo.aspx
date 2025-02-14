<%@ Page Title="" Language="C#" MasterPageFile="~/Master/PDF.Master" AutoEventWireup="true" CodeBehind="CartaDeResguardo.aspx.cs" Inherits="WorkFlow.Reportes.PDF.CartaDeResguardo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function formateaRut(rut) {

            var actual = rut.replace(/^0+/, "");
            if (actual != '' && actual.length > 1) {
                var sinPuntos = actual.replace(/\./g, "");
                var actualLimpio = sinPuntos.replace(/-/g, "");
                var inicio = actualLimpio.substring(0, actualLimpio.length - 1);
                var rutPuntos = "";
                var i = 0;
                var j = 1;
                for (i = inicio.length - 1; i >= 0; i--) {
                    var letra = inicio.charAt(i);
                    rutPuntos = letra + rutPuntos;
                    if (j % 3 == 0 && j <= inicio.length - 1) {
                        rutPuntos = "." + rutPuntos;
                    }
                    j++;
                }
                var dv = actualLimpio.substring(actualLimpio.length - 1);
                rutPuntos = rutPuntos + "-" + dv;
            }
            return rutPuntos;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="position: relative; width: 100%; height: 100%; margin: 0 auto;">
        <div class="ParrafoLegal" style="width: 80%; margin-right: auto; margin-left: auto; margin-top: 100px;">
            <p style="text-align: right;">
                <%--<%=DateTime.Now.ToLongDateString()%>--%>

                <anthem:Label AutoUpdateAfterCallBack="True" ID="lblFecha" runat="server" UpdateAfterCallBack="True"></anthem:Label>
            </p>
            <br />
            <p style="text-align: center;"><strong>CARTA DE RESGUARDO</strong></p>

            <p>Señores</p>
            <p>
                <strong>
                    <anthem:Label ID="lblInstitucionFinancieraResguardo1" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label></strong>
            </p>
            <p>Presente</p>

            <p>De nuestra consideración:</p>

            <p>
                Por la presente, tenemos el agrado de informar a ustedes que
               <strong>
                   <anthem:Label ID="lblVendedor" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label></strong>
                ha conferido a
 <strong>NUEVO CAPITAL S.A.</strong> un mandato mercantil irrevocable en los términos del artículo 241 del Código de Comercio, con el objeto de que  
                <strong>NUEVO CAPITAL S.A.</strong>  pague a vuestra entidad por cuenta del mandante todas las deudas directa e indirectas que mantiene en esa Institución, 
                hasta por un monto máximo equivalente a
              <strong>
                  <anthem:Label ID="lblMontoEquivalente1" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                  Unidades de Fomento.</strong>
            </p>

            <p>
                Dichas obligaciones se encuentran actualmente garantizadas con hipoteca constituida a vuestro favor sobre
            <anthem:Label ID="lblInmuebles" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>.
            </p>

            <p>
                Este pago lo efectuaremos con cargo a Mutuo Hipotecario que se encuentra aprobado y otorgado en Nuevo Capital S.A.
                a nombre de 
                <anthem:Label ID="lblNombreCliente" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>, por un monto total de UF
            <anthem:Label ID="lblMontoEquivalente2" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                y a un plazo de
                <anthem:Label ID="lblPlazo" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
                años, 
                mutuo que deberá ser garantizado con hipoteca sobre el y/o los inmuebles individualizados precedentemente.
            </p>

            <p>
                Este compromiso se cumplirá dentro del plazo de 10 días hábiles bancarios, 
                contados desde la efectiva inscripción de la propiedad a nombre del comprador, hipoteca y prohibición en favor de
                NUEVO CAPITAL S.A. en el Conservador de Bienes Raíces respectivo y por el monto que se obtenga de la operación de crédito,
                hasta enterar el pago que por el presente instrumento nos comprometemos a cancelar.
            </p>

            <p>
                Con el objeto de continuar la tramitación de la mencionada garantía y poder dar así cumnplimiento al compromiso asumido, 
                les agradeceremos disponer el alzamiento de la hipoteca constituida en favor de vuestra Entidad sobre la referida propiedad, 
                así como de cualquier otro gravamen o prohibición que la afecten, constituidas en favor de
                <anthem:Label ID="lblInstitucionFinancieraResguardo2" runat="server" AutoUpdateAfterCallBack="True"></anthem:Label>
            </p>


            <p>Saluda atentamente a ustedes,</p>

            <p>
                Gerente<br />
                Nuevo Capital<br />
                p.p Nuevo Capital
            </p>
        </div>
    </div>
</asp:Content>
