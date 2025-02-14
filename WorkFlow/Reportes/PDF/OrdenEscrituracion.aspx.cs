using WorkFlow.Entidades;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PDF
{
    public partial class OrdenEscrituracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarImagen();
            CargarParticipantes();
            CargarSolicitud();
            CargarPropiedades();
        }

        private void CargarImagen()
        {
            imgLogoCertificado.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");
        }

        private void CargarParticipantes()
        {
            string ContenidoHTML = "";
            List<ParticipanteInfo> lstInfo = new List<ParticipanteInfo>();
            SolicitudInfo oSolicitud = new SolicitudInfo();
            ClientesInfo oCliente = new ClientesInfo();
            NegClientes oNegCliente = new NegClientes();
            Resultado<ClientesInfo> oResultado = new Resultado<ClientesInfo>();

            oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            lstInfo = NegParticipante.lstParticipantes.Where(s => s.Solicitud_Id == oSolicitud.Id).ToList();
            string tblParticipante = "";
            string divEncabezado = "";
            foreach (ParticipanteInfo OInfo in lstInfo)
            {
                oNegCliente = new NegClientes();
                oResultado = new Resultado<ClientesInfo>();
                oCliente = new ClientesInfo();
                oCliente.Rut = OInfo.Rut;
                oResultado = oNegCliente.Buscar(oCliente);
                if (oResultado.ResultadoGeneral)
                {
                    oCliente = oResultado.Lista[0];
                }




                divEncabezado = string.Format("<div class='SubTituloDatos'> {0} </div>", OInfo.DescripcionTipoParticipante);

                tblParticipante = string.Format("<table class='ContenedorTabla'><tr><td style='width:15%'>Nombre</td><td style='width:2%'>:</td><td style='width:40%'>{0}</td><td style='width:15%'>Rut</td><td style='width:2%'>:</td><td style='width:26%'>{1}</td></tr>", OInfo.NombreCliente, OInfo.RutCompleto);
                tblParticipante = tblParticipante + string.Format("<tr><td>Profesión</td><td>:</td><td>{0}</td><td>Telefono</td><td>:</td><td>{1}</td></tr>", oCliente.DescripcionProfesion, oCliente.TelefonoFijo);
                tblParticipante = tblParticipante + string.Format("<tr><td>Correo Electrónico</td><td>:</td><td>{0}</td><td>% Deuda</td><td>:</td><td>{1}</td></tr>", oCliente.Mail, OInfo.PorcentajeDeuda);
                tblParticipante = tblParticipante + string.Format("<tr><td>Estado Civil</td><td>:</td><td>{0}</td><td>% Desgravamen</td><td>:</td><td>{1}</td></tr>", oCliente.DescripcionEstadoCivil, OInfo.PorcentajeDesgravamen);
                tblParticipante = tblParticipante + string.Format("<tr><td>Regimen Matrimonial</td><td>:</td><td>{0}</td><td>% Dominio</td><td>:</td><td>{1}</td></tr>", oCliente.DescripcionRegimenMatrimonial, OInfo.PorcentajeDominio);
                tblParticipante = tblParticipante + string.Format("<tr><td>Dirección Particular</td><td>:</td><td>{0}</td><td>Comuna</td><td>:</td><td>{1}</td></tr>", oCliente.DireccionCompleta, oCliente.DescripcionComuna);
                tblParticipante = tblParticipante + string.Format("<tr><td>Ciudad</td><td>:</td><td>{0}</td><td>Región</td><td>:</td><td>{1}</td></tr>", oCliente.DescripcionProvincia, oCliente.DescripcionRegion);
                tblParticipante = tblParticipante + "</table>";

                ContenidoHTML = ContenidoHTML + divEncabezado + tblParticipante;
            }

            lstParticipantes.InnerHtml = ContenidoHTML;

        }

        private void CargarSolicitud()
        {
            NegSolicitudes nSolicitud = new NegSolicitudes();
            Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
            SolicitudInfo oSolicitud = new SolicitudInfo();

            oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            oSolicitud.FechaParidad = DateTime.Parse("01/" + NegSolicitudes.objSolicitudInfo.MesEscritura.ToString() + "/" + NegSolicitudes.objSolicitudInfo.AñoEscritura.ToString());


            rSolicitud = nSolicitud.Buscar(oSolicitud);
            if (rSolicitud.ResultadoGeneral)
            {
                if (rSolicitud.Lista.Count != 0)
                {
                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                    oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                    lblNumeroSolicitud.Text = oSolicitud.Id.ToString();
                    lblNumeroOperacion.Text = oSolicitud.NumeroOperacion;
                    lblMesEscritura.Text = dtinfo.GetMonthName((int)oSolicitud.MesEscritura).ToUpper() + " - " + oSolicitud.AñoEscritura.ToString();
                    lblDescripcionProducto.Text = oSolicitud.DescripcionProducto.ToUpper();
                    lblAbogado.Text = oSolicitud.NombreAbogado;
                    lblSucursal.Text = oSolicitud.NombreSucursal;
                    lblFechaParidad.Text = string.Format("{0:d}", oSolicitud.FechaParidad);
                    lblValorUF.Text = string.Format("{0:C2}", oSolicitud.ValorUF);


                    lblCompraVentaUF.Text = string.Format("{0:N4}", oSolicitud.MontoPropiedad);
                    lblCompraVentaPesos.Text = string.Format("{0:C}", oSolicitud.MontoPropiedadPesos);
                    lblMontoContadoUF.Text = string.Format("{0:N4}", oSolicitud.MontoContado);
                    lblMontoContadoPesos.Text = string.Format("{0:C}", oSolicitud.MontoContadoPesos);
                    lblMutuoHipotecarioUF.Text = string.Format("{0:N4}", oSolicitud.MontoCredito);
                    lblMutuoHipotecarioPesos.Text = string.Format("{0:C}", oSolicitud.MontoCreditoPesos);
                    lblSubsidio.Text = oSolicitud.DescripcionSubsidio == null ? "SIN SUBSIDIO" : oSolicitud.DescripcionSubsidio;
                    lblMontoSubsidioUF.Text = string.Format("{0:N4}", oSolicitud.MontoSubsidio);
                    lblMontoSubsidioPesos.Text = string.Format("{0:C}", oSolicitud.MontoSubsidioPesos);
                    lblMontoBonoIntegracionUF.Text = string.Format("{0:N4}", oSolicitud.MontoBonoIntegracion);
                    lblMontoBonoIntegracionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoIntegracionPesos);
                    lblMontoBonoCaptacionUF.Text = string.Format("{0:N4}", oSolicitud.MontoBonoCaptacion);
                    lblMontoBonoCaptacionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoCaptacionPesos);
                    lblPlazoGracia.Text = oSolicitud.Dividendos.ToString();

                    lblPlazoGracia.Text = (oSolicitud.Dividendos + oSolicitud.Gracia).ToString();
                    lblPlazo.Text = oSolicitud.Dividendos.ToString();
                    lblFechaPrimerDividendo.Text = oSolicitud.FechaPrimerDividendo == null ? "" : oSolicitud.FechaPrimerDividendo.Value.ToShortDateString();
                    lblCantidadDividendosMenos1.Text = (oSolicitud.Dividendos + -1).ToString();
                    lblValorDividendo.Text = string.Format("{0:N4}", oSolicitud.ValorDividendoUF);
                    lblValorUltimoDividendo.Text = string.Format("{0:N4}", oSolicitud.ValorUltimoDividendoUF);

                    lblMesExcepcion.Text = "";
                    lblAños.Text = oSolicitud.Plazo.ToString();
                    lblPrimeroDias.Text = oSolicitud.DiaPago.ToString();

                    lblTasaAnual.Text = string.Format("{0:N4}", oSolicitud.TasaFinal);
                    lblValorDividendoUF.Text = string.Format("{0:N4}", oSolicitud.ValorDividendoUF);
                    lblSpread.Text = string.Format("{0:N4}", oSolicitud.Spread);
                    lblCostoFondo.Text = string.Format("{0:N4}", oSolicitud.TasaBase);

                    lblObjetivo.Text = oSolicitud.DescripcionObjetivo;
                    lblProducto.Text = oSolicitud.DescripcionProducto;
                    lblCAE.Text = string.Format("{0:N2}", oSolicitud.CAE);
                    lblCostoTotal.Text = string.Format("{0:N4}", oSolicitud.CTC);
                    lblMesesGracia.Text = oSolicitud.Gracia == 0 ? "Sin Meses de Gracia" : oSolicitud.Gracia.ToString();


                    lblNotariaProtocolizacion.Text = oSolicitud.NotariaProtocolizacion == null ? "No Registrada" : oSolicitud.NotariaProtocolizacion;
                    lblRepertorioProtocolizacion.Text = oSolicitud.RepertorioProtocolizacion == null ? "No Registrado" : oSolicitud.RepertorioProtocolizacion;
                    lblFechaProtocolizacion.Text = oSolicitud.FechaProtocolizacion == null ? "No Registrada" : oSolicitud.FechaProtocolizacion.Value.ToShortDateString();
                    lblNumeroProtocolizacion.Text = oSolicitud.NumeroProtocolizacion == null ? "No Registrado" : oSolicitud.NumeroProtocolizacion;
                    lblSerie.Text = oSolicitud.Serie;


                }
            }
        }

        private void CargarPropiedades()
        {
            string ContenidoHTML = "";
            List<TasacionInfo> lstInfo = new List<TasacionInfo>();
            SolicitudInfo oSolicitud = new SolicitudInfo();
            NegPropiedades oNegPropiedad = new NegPropiedades();
            PropiedadInfo oPropiedad = new PropiedadInfo();
            Resultado<PropiedadInfo> oResultado = new Resultado<PropiedadInfo>();

            oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            lstInfo = NegPropiedades.lstTasaciones.Where(s => s.Solicitud_Id == oSolicitud.Id).ToList();

            foreach (TasacionInfo oInfo in lstInfo)
            {
                oNegPropiedad = new NegPropiedades();
                oPropiedad = new PropiedadInfo();
                oResultado = new Resultado<PropiedadInfo>();
                oPropiedad.Id = oInfo.Propiedad_Id;
                oResultado = oNegPropiedad.BuscarPropiedad(oPropiedad);
                if (oResultado.ResultadoGeneral)
                {
                    oPropiedad = oResultado.Lista[0];
                }

                string tblPropiedad = "";
                string divEncabezado = "";

                divEncabezado = string.Format("<div class='SubTituloDatos'> {0} ROL {1} - {2} UBICADO EN {3} {4}, {5}, {6}</div>", oPropiedad.DescripcionTipoInmueble, oPropiedad.RolManzana, oPropiedad.RolSitio, oPropiedad.DescripcionVia, oInfo.DireccionCompleta, oPropiedad.DescripcionProvincia, oPropiedad.DescripcionRegion);

                tblPropiedad = string.Format("<table class='ContenedorTabla'>");
                //tblPropiedad = tblPropiedad + string.Format("<tr><td>Tipo Garantia</td><td>:</td><td>{0}</td><td>Limite</td><td>:</td><td>{1}</td></tr>", "", "");
                //tblPropiedad = tblPropiedad + string.Format("<tr><td>Participación</td><td>:</td><td>{0}</td><td><td>:</td><td></td></tr>", "", "");
                tblPropiedad = tblPropiedad + string.Format("<tr><td style='width:25%'>Fecha Tasación</td><td style='width:2%'>:</td><td>{0:d}</td><td style='width:24%'>Tasada Por</td><td style='width:2%'>:</td><td>{1}</td></tr>", oInfo.FechaTasacion, oInfo.NombreEmpresaTasacion);
                tblPropiedad = tblPropiedad + string.Format("<tr><td>Valor Comercial</td><td>:</td><td>{0}</td><td>Monto Liquidación</td><td>:</td><td>{1}</td></tr>", oInfo.MontoTasacion.ToString("#,##0.0000"), oInfo.MontoLiquidacion.ToString("#,##0.0000"));
                tblPropiedad = tblPropiedad + string.Format("<tr><td>Monto Seguro Incendio</td><td>:</td><td>{0}</td><td></td><td></td><td></td></tr>", oInfo.MontoAsegurado.ToString("#,##0.0000"));
                tblPropiedad = tblPropiedad + string.Format("<tr><td>M2 Terrero</td><td>:</td><td>{0}</td><td>M2 Construido</td><td>:</td><td>{1}</td></tr>", oInfo.MetrosTerreno, oInfo.MetrosConstruidos);
                tblPropiedad = tblPropiedad + string.Format("<tr><td>Tipo</td><td>:</td><td>{0}</td><td>Uso</td><td>:</td><td>{1}</td></tr>", oPropiedad.DescripcionTipoInmueble, oPropiedad.DescripcionDestino);
                tblPropiedad = tblPropiedad + string.Format("<tr><td>Año Construción</td><td>:</td><td>{0}</td><td>Tipo Construcción</td><td>:</td><td>{1}</td></tr>", oPropiedad.AñoConstruccion, oPropiedad.DescripcionTipoConstruccion);
                tblPropiedad = tblPropiedad + string.Format("<tr><td>Institución Alzante de Hipoteca</td><td>:</td><td>{0}</td><td></td><td></td><td></td></tr>", oInfo.NombreInstitucionAlzamientoHipoteca);
                tblPropiedad = tblPropiedad + "</table>";

                ContenidoHTML = ContenidoHTML + divEncabezado + tblPropiedad;
            }

            lstPropiedades.InnerHtml = ContenidoHTML;
            lstPropiedadesCompraventa.InnerHtml = ContenidoHTML;

            //Cargar los seguros
            NegSeguros nSeguros = new NegSeguros();
            Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
            SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();

            oSeguros.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);

            if (rSeguros.ResultadoGeneral)
            {
                agvSeguros.DataSource = rSeguros.Lista;
                agvSeguros.DataBind();
            }



        }
    }
}