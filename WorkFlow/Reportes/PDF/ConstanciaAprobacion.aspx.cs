using WorkFlow.Entidades;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PDF
{
    public partial class ConstanciaAprobacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CL");
            lblFechaCabecera.Text = string.Format("Santiago, {0} de {1} de {2}", DateTime.Now.Day, DateTime.Now.ToString("MMMM", culture), DateTime.Now.Year);
            CargarImagen();
            CargarParticipantes();
            CargarDatos();
            if (Session["Aprobacion"] != null)
                lblTitulo.Text = "APROBACIÓN";
            else
                lblTitulo.Text = "PRE - APROBACIÓN";
        }

        private void CargarImagen()
        {


        }

        private void CargarParticipantes()
        {
            int IdSolicitud = -1;
            ParticipanteInfo oInfo = new ParticipanteInfo();
            ClientesInfo oCliente = new ClientesInfo();
            NegClientes oNegCliente = new NegClientes();
            Resultado<ClientesInfo> oResultado = new Resultado<ClientesInfo>();

            IdSolicitud = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            oInfo = NegParticipante.lstParticipantes.Where(s => s.Solicitud_Id == IdSolicitud && s.TipoParticipacion_Id == 1).ToList()[0];


            lblCliente.Text = oInfo.NombreCliente;
            lblRut.Text = oInfo.RutCompleto;


            //Codeudor
            tdCodeudor.Visible = false;
            if (NegParticipante.lstParticipantes.Where(s => s.Solicitud_Id == IdSolicitud && s.TipoParticipacion_Id == 2).ToList().Count > 0)
            {
                oInfo = NegParticipante.lstParticipantes.Where(s => s.Solicitud_Id == IdSolicitud && s.TipoParticipacion_Id == 2).ToList()[0];
                lblCodeudor.Text = oInfo.NombreCliente;
                lblRutCodeudor.Text = oInfo.RutCompleto;
                tdCodeudor.Visible = true;
            }

        }

        private void CargarDatos()
        {
            SolicitudInfo oSolicitud = new SolicitudInfo();

            Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
            NegSolicitudes nSolicitud = new NegSolicitudes();

            oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

            rSolicitud = nSolicitud.Buscar(oSolicitud);
            if (rSolicitud.ResultadoGeneral)
            {
                NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                NegSolicitudes.objSolicitudInfo = rSolicitud.Lista.FirstOrDefault();
            }


            lblValorPropiedad.Text = String.Format("UF {0:N4}", NegSolicitudes.objSolicitudInfo.MontoPropiedad);
            lblValorPropiedad2.Text = String.Format("UF {0:N4}", NegSolicitudes.objSolicitudInfo.MontoPropiedad);
            lblMontoCredito.Text = String.Format("UF {0:N4}", NegSolicitudes.objSolicitudInfo.MontoCredito);
            lblPlazoSolicitado.Text = string.Format("{0} años", NegSolicitudes.objSolicitudInfo.Plazo);

            if (NegPropiedades.lstTasaciones != null)
            {

                NegPropiedades nPropiedades = new NegPropiedades();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();

                PropiedadInfo oPropiedad = new PropiedadInfo();


                oPropiedad.Id = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).Propiedad_Id;
                rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                if (!rPropiedad.ResultadoGeneral)
                {

                    return;
                }

                oPropiedad = rPropiedad.Lista.FirstOrDefault();


                if (NegSolicitudes.objSolicitudInfo.DescripcionProyecto != "" && NegSolicitudes.objSolicitudInfo.DescripcionProyecto != null)
                    lblDireccion.Text = " en el Proyecto " + NegSolicitudes.objSolicitudInfo.DescripcionProyecto + " de la inmobiliaria " + NegSolicitudes.objSolicitudInfo.DescripcionInmobiliaria + " ubicado en " + oPropiedad.DescripcionComuna + ", dirección " + oPropiedad.DireccionCompleta;
                else
                    lblDireccion.Text = " ubicada en " + oPropiedad.DescripcionComuna + ", dirección " + oPropiedad.DireccionCompleta;


            }
            lblDiasValidez.Text = NegSimulacionHipotecaria.oReporteSimulacion.VigenciaDoc;







        }
    }
}