using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web;

namespace WorkFlow.Reportes.PDF
{
    public partial class SimulacionCreditoHipotecario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaReporte();
        }

        private void CargaReporte()
        {
            CargaDetalleCliente();
            CargaDetalleSimulacion();
            CargaResultadoSimulacion();
            CargaGastosOperacionales();
            CargaEjecutivo();
            CargaParrafos();
          
        }

        private void CargaDetalleCliente()
        {

            var objCliente = NegSimulacionHipotecaria.oReporteSimulacion.oCliente;
            try
            {
                lblNombreCompleto.Text = objCliente.NombreCompleto;
                lblFono.Text = objCliente.TelefonoFijo;
                lblRutCompleto.Text = objCliente.RutCompleto;
                lblCelular.Text = objCliente.TelefonoMovil;
                lblMail.Text = objCliente.Mail;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    lblNombreCompleto.Text = "Error al Cargar Detalle Cliente";
                }
            }
        }

        private void CargaDetalleSimulacion()
        {
            var objDetalleSimulacion = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion;
            try
            {
                lblFecha.Text = DateTime.Now.ToLongDateString();
                lblValorUF.Text = objDetalleSimulacion.StrValorUF;
                lblTipoInmueble.Text = objDetalleSimulacion.DescripcionTipoInmueble;
                lblCooperativa.Text = objDetalleSimulacion.DescripcionCooperativa;
                lblInmobiliaria.Text = objDetalleSimulacion.DescripcionInmobiliaria;
                lblProyecto.Text = objDetalleSimulacion.DescripcionProyecto;
                lblComuna.Text = objDetalleSimulacion.DescripcionComuna;
                lblTipoFinanciamiento.Text = objDetalleSimulacion.DescripcionTipoFinanciamiento;
                lblProducto.Text = objDetalleSimulacion.DescripcionProducto;
                lblObjetivo.Text = objDetalleSimulacion.DescripcionObjetivo;
                lblDestino.Text = objDetalleSimulacion.DescripcionDestino;
                lblNombreSubsidio.Text = objDetalleSimulacion.DescripcionSubsidio;
                lblPrecioVenta.Text = "UF " + string.Format("{0:N4}", objDetalleSimulacion.ValorPropiedad);
                lblMontoSubsidio.Text = "UF " + String.Format("{0:N4}", objDetalleSimulacion.MontoSubsidio);
                lblMontoBonoIntegracion.Text = "UF " + String.Format("{0:N4}", objDetalleSimulacion.MontoBonoIntegracion);
                lblMontoBonoCaptacion.Text = "UF " + String.Format("{0:N4}", objDetalleSimulacion.MontoBonoCaptacion);
                lblMontoContado.Text = "UF " + String.Format("{0:N4}", objDetalleSimulacion.MontoContado);
                lblMontoCredito.Text = "UF " + String.Format("{0:N4}", objDetalleSimulacion.MontoCredito);
                lblPorcFinanciamiento.Text = "% " + objDetalleSimulacion.StrPorcentajeFinanciamiento;

                lblPrecioVentaPesos.Text = objDetalleSimulacion.StrPrecioVentaPesos;
                lblMontoSubsidioPesos.Text = objDetalleSimulacion.StrMontoSubsidioPesos;
                lblMontoBonoIntegracionPesos.Text = objDetalleSimulacion.StrMontoBonoIntegracionPesos;
                lblMontoBonoCaptacionPesos.Text = objDetalleSimulacion.StrMontoBonoCaptacionPesos;
                lblMontoContadoPesos.Text = objDetalleSimulacion.StrMontoContadoPesos;
                lblMontoCreditoPesos.Text = objDetalleSimulacion.StrMontoCreditoPesos;
                
                lblPlazo.Text = objDetalleSimulacion.Plazo.ToString();
                lblTasa.Text = "% " + String.Format("{0:N4}", NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion.FirstOrDefault(s => s.Plazo == objDetalleSimulacion.Plazo).TasaAnual);
                lblGracia.Text = objDetalleSimulacion.DescripcionGracia;
                lblDeudores.Text = objDetalleSimulacion.CantidadDeudores.ToString();
                lblSegDesgravamen.Text = objDetalleSimulacion.DescripcionSegDesgravamen;
                lblSegIncendio.Text = objDetalleSimulacion.DescripcionSegIncendio;
                lblSegCesantia.Text = objDetalleSimulacion.DescripcionSegCesantia;
                
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Detalle Simulación");
                }
            }
        }

        private void CargaResultadoSimulacion()
        {
            var lstResultadoSimulacion = NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion;
            try
            {
                if (NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Subsidio_Id != -1)
                {
                    DivDividendosMinvu.Visible = true;
                    Controles.CargarGrid(ref gvDetalleDivMinvu, lstResultadoSimulacion, new string[] { "Plazo" });
                    Controles.CargarGrid(ref gvAhorroMinvu, lstResultadoSimulacion, new string[] { "Plazo" });
                }
                Controles.CargarGrid<SimulacionHipotecariaInfo>(ref gvDetalleDiv, lstResultadoSimulacion, new string[] { "Plazo" });

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Simulacion Hipotecaria");
                }
            }
        }

        private void CargaGastosOperacionales()
        {
            var lstGastosOperacionales = NegSimulacionHipotecaria.oReporteSimulacion.lstGastoOperacional;
            try
            {
                Controles.CargarGrid<GastoOperacionalInfo>(ref gvBusqueda, lstGastosOperacionales, new string[] { "Id" });
                lblTotalGGOO.Text = "Total Gastos Operacionales $" + string.Format("{0:0,0}", lstGastosOperacionales.Sum(item => item.ValorPesos));
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Gastos Operacionales");
                }
            }
        }


        private void CargaParrafos()
        {
            var lstParrafo = NegSimulacionHipotecaria.oReporteSimulacion.lstParrafos;
            try
            {
                LblParrafo1.Text = lstParrafo[0].Parrafo;
                LblParrafo2.Text = lstParrafo[1].Parrafo;
                LblParrafo3.Text = lstParrafo[2].Parrafo;
                LblParrafo4.Text = lstParrafo[3].Parrafo;
                LblParrafo5.Text = lstParrafo[4].Parrafo;
                LblParrafo6.Text = lstParrafo[5].Parrafo;
                LblParrafo7.Text = lstParrafo[6].Parrafo;
                LblParrafo8.Text = lstParrafo[7].Parrafo;
                LblParrafo9.Text = lstParrafo[8].Parrafo;
                LblParrafo10.Text = lstParrafo[9].Parrafo;
                LblParrafo11.Text = lstParrafo[10].Parrafo;
                LblParrafo12.Text = lstParrafo[11].Parrafo;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Texto Reportes");
                }
            }
        }

        private void CargaEjecutivo()
        {
            var objEjecutivo = NegSimulacionHipotecaria.oReporteSimulacion.oEjecutivo;
            try
            {
                lblEjecutivoNombre.Text = objEjecutivo.Nombre;
                lblEjecutivoMail.Text = objEjecutivo.Email;
                lblEjecutivoFono.Text = objEjecutivo.Telefono == null ? "" : objEjecutivo.Telefono;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNombreCompleto.Text = Ex.Message;
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Ejecutivo");
                }
            }
        }
    }
}