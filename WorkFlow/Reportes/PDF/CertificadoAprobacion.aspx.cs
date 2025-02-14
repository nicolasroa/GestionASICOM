using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkFlow.Reportes.PDF
{
    public partial class CertificadoAprobacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaReporte();
        }

        private void CargaReporte()
        {
            CargaDetalleCliente();
            CargaDetalleSimulacion();
            CargarPropiedades();
        }

        private void CargarPropiedades()
        {
            List<TasacionInfo> lstInfo = new List<TasacionInfo>();
            SolicitudInfo oSolicitud = new SolicitudInfo();
            NegPropiedades oNegPropiedad = new NegPropiedades();
            PropiedadInfo oPropiedad = new PropiedadInfo();
            Resultado<PropiedadInfo> oResultado = new Resultado<PropiedadInfo>();

            oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            lblDireccionPropiedad.Text = NegPropiedades.lstTasaciones.Where(s => s.Solicitud_Id == oSolicitud.Id && s.IndPropiedadPrincipal == true).ToList().FirstOrDefault().DireccionCompleta;


        }


        private void CargaDetalleCliente()
        {
            var objCliente = NegSimulacionHipotecaria.oReporteSimulacion.oCliente;
            try
            {
                string dpto = String.IsNullOrEmpty(objCliente.Departamento) ? "" : " Dpto." + objCliente.Departamento.ToString();
                lblNombreCliente.Text = objCliente.NombreCompleto.ToUpper();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCliente.Text = Ex.Message;
                }
                else
                {
                    lblNombreCliente.Text = "Error al Cargar Detalle Cliente";
                }
            }
        }


        private void CargaDetalleSimulacion()
        {
            var objSolicitud = NegSimulacionHipotecaria.oReporteSimulacion.oSolicitud;
            try
            {

                lblDestinoCredito.Text = objSolicitud.DescripcionDestino;
                lblMontoCredito.Text = String.Format("{0:N4}", objSolicitud.MontoCredito);

                lblPlazoCredito.Text = objSolicitud.Plazo.ToString();
                lblTasaInteresAnual.Text = String.Format("{0:N4}", objSolicitud.TasaFinal);

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblTasaInteresAnual.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Detalle Certificado");
                }
            }
        }


    }
}