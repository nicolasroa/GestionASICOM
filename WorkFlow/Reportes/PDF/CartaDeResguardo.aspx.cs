using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PDF
{
    public partial class CartaDeResguardo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDetalleCliente();
            }
        }
        private void CargaDetalleCliente()
        {

            var lstTasaciones = NegCartaResguardo.oReporteCartaResguardo.lstTasacion;
            try
            {
                string inmuebles = "";
                int cont = 0;

                foreach (TasacionInfo row in lstTasaciones)
                {
                    cont = cont + 1;
                    if (1 == lstTasaciones.Count())
                    {
                        inmuebles = inmuebles + row.DireccionCompleta;
                    }
                    else
                    {
                        if (cont < lstTasaciones.Count())
                        {
                            if (lstTasaciones.Count() - cont == 1)
                            { inmuebles = row.DireccionCompleta + " y "; }
                            else
                            { inmuebles = row.DireccionCompleta + ", "; }
                        }
                        else
                        {
                            inmuebles = inmuebles + row.DireccionCompleta;
                        }
                    }

                }
                if (cont == 1)
                {
                    inmuebles = " el inmueble ubicado en " + inmuebles;
                }
                else
                {
                    inmuebles = " los inmuebles ubicados en " + inmuebles;
                }

                lblInmuebles.Text = inmuebles;
                lblInstitucionFinancieraResguardo1.Text = lstTasaciones[0].NombreInstitucionAlzamientoHipoteca;
                lblInstitucionFinancieraResguardo2.Text = lstTasaciones[0].NombreInstitucionAlzamientoHipoteca;

                lblMontoEquivalente1.Text = string.Format("{0:0.0,0}",NegCartaResguardo.oReporteCartaResguardo.oSolicitud.MontoCredito);
                lblMontoEquivalente2.Text = string.Format("{0:0.0,0}", NegCartaResguardo.oReporteCartaResguardo.oSolicitud.MontoCredito);
                lblNombreCliente.Text = NegCartaResguardo.oReporteCartaResguardo.oCliente.NombreCompleto.ToString() + ", Rut " + NegCartaResguardo.oReporteCartaResguardo.oCliente.RutCompleto.ToString();
                lblPlazo.Text = NegCartaResguardo.oReporteCartaResguardo.oSolicitud.Plazo.ToString();
                var ObjParticipante = NegCartaResguardo.oReporteCartaResguardo.lstParticipante.FirstOrDefault(s => s.Solicitud_Id == NegCartaResguardo.oReporteCartaResguardo.oSolicitud.Id && s.TipoParticipacion_Id == 4);
                if (ObjParticipante != null)
                {
                    lblVendedor.Text = ObjParticipante.NombreCliente + ", Rut " + formatearRut(ObjParticipante.RutCompleto.ToString());
                }
                else
                {
                    lblVendedor.Text = " el vendedor ";
                }

                lblFecha.Text = DateTime.Now.ToLongDateString();
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


        public string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

    }
}