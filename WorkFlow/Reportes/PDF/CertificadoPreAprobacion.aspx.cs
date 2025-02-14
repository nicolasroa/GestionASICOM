using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;

namespace WorkFlow.Reportes.PDF
{
    public partial class CertificadoPreAprobacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaReporte();
        }


        private void CargaReporte()
        {
            CargaDetalleCliente();
            CargaDetalleSimulacion();
        }

        private void CargaDetalleCliente()
        {
            var objCliente = NegSimulacionHipotecaria.oReporteSimulacion.oCliente;
            try
            {
                string dpto = String.IsNullOrEmpty(objCliente.Departamento) ? "" : " Dpto." + objCliente.Departamento.ToString();
                lblNombreCliente.Text = objCliente.NombreCompleto.ToUpper();
                lblDireccionPropiedad.Text = objCliente.Direccion.ToString() + " #" + objCliente.Numero.ToString() + dpto;
                lblComunaPropiedad.Text = objCliente.DescripcionComuna + ", " + objCliente.DescripcionProvincia;


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