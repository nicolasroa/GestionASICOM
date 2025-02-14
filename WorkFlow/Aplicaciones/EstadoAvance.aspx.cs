using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Aplicaciones
{
    public partial class EstadoAvance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarControlEventos();
            }
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            NegBandejaEntrada.oBandejaEntrada = new BandejaEntradaInfo();
            NegBandejaEntrada.oBandejaEntrada.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        protected void gdvControlEventos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gdvControlEventos.PageIndex = e.NewPageIndex;
            CargarControlEventos();
        }

        private void CargarControlEventos()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                List<BandejaEntradaInfo> lstBandeja = new List<BandejaEntradaInfo>();

                //Asignación de Variables de Búsqueda
                oFiltro.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEstadoAvance(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<BandejaEntradaInfo>(ref gdvControlEventos, oResultado.Lista, new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Eventos");
                }
            }
        }


    }
}